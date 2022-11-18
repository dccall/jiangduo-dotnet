using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.LinqBuilder;
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
using System.Linq.Expressions;
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
        public  PagedList<DtoOfficial> GetList(DtoOfficialQuery model)
        {
            var query = _officialRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));
            query = query.Where(!string.IsNullOrEmpty(model.Type), x => x.Type.Contains(model.Type));

            //不传或者传-1查询全部
            //query = query.Where(!(model.SelectAreaId == null || model.SelectAreaId == -1), x => x.SelectAreaId == model.SelectAreaId);

            if (!(model.SelectAreaId == null || model.SelectAreaId == -1))
            {
                //获取所有子节点选区id
                var areaIdList = (from x in _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted && x.Id == model.SelectAreaId.Value)
                                  join x2 in _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted )
                                  on x.Id equals x2.ParentId into result1
                                  from c in result1.DefaultIfEmpty()
                                  where c != null
                                  select c.Id).ToList();

                areaIdList.Add(model.SelectAreaId.Value);
                ////动态lambda
                //Expression<Func<Official, bool>> where = c => true;
                //foreach (var areaId in areaIdList)
                //{
                //    where = where.Or(x => x.SelectAreaId.Contains(areaId.ToString()));
                //}
                //query = query.Where(where);
                query = query.Where(x => areaIdList.Contains(x.SelectAreaId.Value));
            }

            query = query.Where(model.OfficialRole != null, x => x.OfficialRole == model.OfficialRole);
            query = query.Where(!(model.CategoryId == null || model.CategoryId == "-1"), x => x.CategoryId == model.CategoryId);

            //将数据映射到DtoOfficial中
            var dto= query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoOfficial>().ToPagedList(model.PageIndex, model.PageSize);

            //区选区，展示父级区选区名称
            if (model.Type == "1")
            {
                var ids = dto.Items.Select(x => x.SelectAreaId).ToList();
                var parentList= (from x in _selectAreaRepository.AsQueryable(false).Where(x => ids.Contains(x.Id))
                join x2 in _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted)
                on x.ParentId equals x2.Id into result1
                from c in result1.DefaultIfEmpty()
                select new { Id=x.Id,ParentId=x.ParentId, ParentName= c==null? x.SelectAreaName: c.SelectAreaName }).ToList();

                foreach (var item in dto.Items)
                {
                    var temp= parentList.Where(x => x.Id == item.SelectAreaId).FirstOrDefault();
                    if (temp != null)
                    {
                        item.ParentAreaName = temp.ParentName;
                    }
                }
            }
            return dto;
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

            var mapper = new Npoi.Mapper.Mapper();
            mapper.Put(dtoList, "人大名单", true);
            mapper.Put(areaList, "选区", true);
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

            var mapper = new Npoi.Mapper.Mapper();
            mapper.Put(dtoList, "人大名单", true);
            mapper.Put(areaList, "选区", true);
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
                var officialList=_officialRepository.AsQueryable().Where(x => !x.IsDeleted).ToList();

                var structIdList= _officialsstructRepository.AsQueryable().Where(x => !x.IsDeleted).Select(x => x.Id).ToList();
                var IdNumbers = officialList.Select(x=>x.Idnumber).ToList();
                var PhoneNumber = officialList.Select(x=>x.PhoneNumber).ToList();

                var index = 1;//从1开始
                var excelPhoneNumber = new List<string>();
                //验证
                foreach (var official in entityList)
                {
                    index++;
                    official.CreatedTime = DateTime.Now;
                    official.Creator = createdId;

                    official.ValidationNullOrEmpty(index);
                    //if (IdNumbers.Contains(official.Idnumber))
                    //{
                    //    throw Oops.Oh($"第{index}行身份证号已存在");
                    //}
                    if (PhoneNumber.Contains(official.PhoneNumber))
                    {
                        throw Oops.Oh($"第{index}行手机号码已存在");
                    }
                    //验证excel里手机号是否有重复
                    if (excelPhoneNumber.Contains(official.Idnumber))
                    {
                        throw Oops.Oh($"第{index}行手机号码出现重复");
                    }
                    else
                    {
                        excelPhoneNumber.Add(official.PhoneNumber);
                    }
                }
                await _officialRepository.Context.BulkInsertAsync(entityList);
                return true;
            }
            catch (Exception e)
            {
                Log.Error("导入失败"+e.Message);
                throw Oops.Oh("导入失败" + e.Message);
            }
       
        }

    }

   
}