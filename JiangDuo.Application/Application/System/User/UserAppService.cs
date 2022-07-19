using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JiangDuo.Application.Filters;
using JiangDuo.Application.User.Dtos;
using JiangDuo.Application.User.Services;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yitter.IdGenerator;

namespace JiangDuo.Application.User;

/// <summary>
/// 用户服务
/// </summary>
public class UserAppService : IDynamicApiController
{
	private readonly IUserService _userService;
	
	public UserAppService(IUserService userService)
	{
		_userService = userService;
	}

	/// <summary>
	/// 查询列表（分页）
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[AuthorizeCode("user_list")]
	public async Task<PagedList<DtoUser>> Get([FromQuery] DtoUserRequert model)
	{
		return await _userService.GetList(model);
	}
	/// <summary>
	/// 根据id获取详情
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<DtoUser> Get(long id)
	{
		return await _userService.GetById(id);
	}
	/// <summary>
	/// 新增用户
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public async Task<int> Insert(DtoUser model)
	{
		return await _userService.Insert(model);
	}
	/// <summary>
	/// 修改用户
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	public async Task<int> Update(DtoUser model)
	{
		return await _userService.Update(model);
	}
	/// <summary>
	/// 根据id删除用户
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<int> Delete(long id)
	{
		return await _userService.FakeDelete(id);
	}
	/// <summary>
	/// 批量删除用户
	/// </summary>
	/// <param name="idList"></param>
	/// <returns></returns>
	public async Task<int> Delete([FromBody] List<long> idList)
	{
		return await _userService.FakeDelete(idList);
	}

	
}