using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Services.GaoDe.Dtos;

public class DtoAddressInfo
{
	/// <summary>
	/// 名称
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// 地区
	/// </summary>
	public string District { get; set; }
	///// <summary>
	///// 坐标
	///// </summary>
	//public string Location { get; set; }
	/// <summary>
	/// 经度
	/// </summary>
	public double Longitude { get; set; }
	/// <summary>
	/// 纬度
	/// </summary>
	public double Latitude { get; set; }
	/// <summary>
	/// 城市编码
	/// </summary>
	public string Adcode { get; set; }
	/// <summary>
	/// 地址
	/// </summary>
	public string Address { get; set; }
	/// <summary>
	/// 类型编码
	/// </summary>
	public string Typecode { get; set; }
	/// <summary>
	/// 城市名
	/// </summary>
	public string City { get; set; }
}
