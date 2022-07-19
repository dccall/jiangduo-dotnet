using JiangDuo.Application.System.Dept.Dtos;
using JiangDuo.Application.System.Dept.Services;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Dept
{
    public class DeptAppService: IDynamicApiController
    {
        /// <summary>
        /// 部门
        /// </summary>
        private readonly IDeptService _deptService;
        public DeptAppService(IDeptService deptService)
        {
            _deptService = deptService;
        }
        /// <summary>
        /// 获取部门列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DeptDto> Get([FromQuery] DeptRequest model)
        {
            return _deptService.GetList(model);
        }
       
        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeptDto> Get(long id)
        {
            return await _deptService.GetById(id);
        }
        /// <summary>
        /// 部门新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DeptDto model)
        {
            return await _deptService.Insert(model);
        }
        /// <summary>
        /// 部门修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DeptDto model)
        {
            return await _deptService.Update(model);
        }
        /// <summary>
        /// 根据id删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _deptService.FakeDelete(id);
        }
        /// <summary>
        /// 批量删除部门
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _deptService.FakeDelete(idList);
        }
    }
}
