using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.Logging;
using JiangDuo.Application.AppService.OfficialService.Dto;
using JiangDuo.Application.AppService.OfficialService.Dtos;
using JiangDuo.Application.AppService.OfficialsstructService.Dto;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using JiangDuo.Application.AppService.VillageService.Dto;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.OfficialService.Services
{
    public class OfficialService : IOfficialService, ITransient
    {
        private readonly ILogger<OfficialService> _logger;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<Officialsstruct> _officialsstructRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        private readonly IRepository<Village> _villageRepository;
        private readonly IRepository<SysUploadFile> _uploadRepository;

        public OfficialService(ILogger<OfficialService> logger,
            IRepository<SysUploadFile> uploadRepository,
            IRepository<Official> officialRepository,
            IRepository<Officialsstruct> officialsstructRepository,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<Village> villageRepository


            )
        {
            _logger = logger;
            _officialRepository = officialRepository;
            _selectAreaRepository = selectAreaRepository;
            _villageRepository = villageRepository;
            _officialsstructRepository = officialsstructRepository;
            _uploadRepository = uploadRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoOfficial> GetList(DtoOfficialQuery model)
        {
            var query = _officialRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));
            //不传或者传-1查询全部
            query = query.Where(!(model.SelectAreaId == null || model.SelectAreaId == -1), x => x.SelectAreaId == model.SelectAreaId);
            query = query.Where(model.OfficialRole != null, x => x.OfficialRole == model.OfficialRole);
            query = query.Where(!(model.CategoryId == null || model.CategoryId == "-1"), x => x.CategoryId == model.CategoryId);

            //将数据映射到DtoOfficial中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoOfficial>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoOfficial> GetById(long id)
        {
            var entity = await _officialRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoOfficial>();

            //前端级联传参
            if (dto.SelectAreaId != null && dto.VillageId != null)
            {
                dto.AreaVillage.Add(dto.SelectAreaId.Value);
                dto.AreaVillage.Add(dto.VillageId.Value);
            }
            if (dto != null && !string.IsNullOrEmpty(dto.Avatar))
            {
                var idList = dto.Avatar.Split(',').ToList();
                dto.AvatarList = _uploadRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoOfficialForm model)
        {
            var entity = model.Adapt<Official>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            //前端级联传参
            if (model.AreaVillage.Any() && model.AreaVillage.Count() == 2)
            {
                entity.SelectAreaId = model.AreaVillage[0];
                entity.VillageId = model.AreaVillage[1];
            }
            if (model.AvatarList != null && model.AvatarList.Any())
            {
                entity.Avatar = String.Join(",", model.AvatarList.Select(x => x.FileId));
            }

            _officialRepository.Insert(entity);
            return await _officialRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoOfficialForm model)
        {
            //先根据id查询实体
            var entity = _officialRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);

            //前端级联传参
            if (model.AreaVillage.Any() && model.AreaVillage.Count() == 2)
            {
                entity.SelectAreaId = model.AreaVillage[0];
                entity.VillageId = model.AreaVillage[1];
            }
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            if (model.AvatarList != null && model.AvatarList.Any())
            {
                entity.Avatar = String.Join(",", model.AvatarList.Select(x => x.FileId));
            }
            _officialRepository.Update(entity);
            return await _officialRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _officialRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _officialRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _officialRepository.Context.BatchUpdate<Official>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }


        /// <summary>
        /// 模板excel
        /// </summary>
        /// <returns></returns>
        public MemoryStream ExportTemplateExcel()
        {
            var dtoList = new List<DtoOfficialExport>();

            var officialsstructList = _officialsstructRepository.AsQueryable().Where(x => !x.IsDeleted).ProjectToType<DtoOfficialsstructExport>().ToList();
            var areaList = _selectAreaRepository.AsQueryable().Where(x => !x.IsDeleted).ProjectToType<DtoSelectAreaExport>().ToList();
            var villageList = _villageRepository.AsQueryable().Where(x => !x.IsDeleted).ProjectToType<DtoVillageExport>().ToList();

            var mapper = new Npoi.Mapper.Mapper();
            mapper.Put(dtoList, "人大名单", true);
            mapper.Put(officialsstructList, "人大结构(职务)", true);
            mapper.Put(areaList, "选区", true);
            mapper.Put(villageList, "村", true);
            MemoryStream ms = new MemoryStream();
            mapper.Save(ms);
            return ms;
        }


        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        public MemoryStream ExportExcel( DtoOfficialQuery model)
        {
            var pageList = GetList(model);
            var dtoList= pageList.Items.Adapt<List<DtoOfficialExport>>();
            var officialsstructList = _officialsstructRepository.AsQueryable().Where(x => !x.IsDeleted).ProjectToType<DtoOfficialsstructExport>().ToList();
            var areaList = _selectAreaRepository.AsQueryable().Where(x => !x.IsDeleted).ProjectToType<DtoSelectAreaExport>().ToList();
            var villageList = _villageRepository.AsQueryable().Where(x => !x.IsDeleted).ProjectToType<DtoVillageExport>().ToList();

            var mapper = new Npoi.Mapper.Mapper();
            mapper.Put(dtoList, "人大名单", true);
            mapper.Put(officialsstructList, "人大结构", true);
            mapper.Put(areaList, "选区", true);
            mapper.Put(villageList, "村", true);
            MemoryStream ms = new MemoryStream();
            mapper.Save(ms);
            return ms;
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ImportExcel(IFormFile file)
        {
            if (file == null)
            {
                throw Oops.Oh("缺少上传文件");
            }
            try
            {
                var list = ExcelHelp.ParseExcelToList<DtoOfficialImport>(file.OpenReadStream(), "人大名单");
                var entityList = list.Adapt<List<Official>>();
                var createdId= JwtHelper.GetAccountId();
                var villageList = _villageRepository.AsQueryable().Where(x => !x.IsDeleted).ProjectToType<DtoVillage>().ToList();
                var officialList=_officialRepository.AsQueryable().Where(x => !x.IsDeleted).ToList();

                var structIdList= _officialsstructRepository.AsQueryable().Where(x => !x.IsDeleted).Select(x => x.Id).ToList();
                var IdNumbers = officialList.Select(x=>x.Idnumber).ToList();
                var PhoneNumber = officialList.Select(x=>x.PhoneNumber).ToList();
                var index = 1;

                entityList.ForEach(official =>
                {
                    index++;
                    official.CreatedTime = DateTime.Now;
                    official.Creator = createdId;

                    official.ValidationNullOrEmpty(index);
                    if (IdNumbers.Contains(official.Idnumber))
                    {
                        throw Oops.Oh($"第{index}行身份证号已存在");
                    }
                    if (PhoneNumber.Contains(official.PhoneNumber))
                    {
                        throw Oops.Oh($"第{index}行手机号码已存在");
                    }
                    if (structIdList.Contains(official.Post))
                    {
                        throw Oops.Oh($"第{index}行职务Id不存在");
                    }
                    var exList = villageList.Where(v => v.SelectAreaId == official.SelectAreaId && v.Id == official.VillageId).ToList();
                    if(exList.Count()==0)
                    {
                        throw Oops.Oh($"第{index}行选区Id和村Id不匹配");
                    }

                });

                await _officialRepository.Context.BulkInsertAsync(entityList);
                return true;
            }
            catch (Exception e)
            {
                Log.Error("人大导入失败"+e.Message);
                throw Oops.Oh("人大导入失败" + e.Message);
            }
       
        }

    }

   
}