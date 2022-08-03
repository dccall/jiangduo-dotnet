using Furion;
using Furion.RemoteRequest;
using System.Net.Http;
using System.Threading.Tasks;

namespace JiangDuo.Core.Services.GaoDe
{
	public interface IGaoDeHttp : IHttpDispatchProxy
	{
		/// <summary>
		/// 根据关键词搜索地点
		/// </summary>
		/// <param name="keywords">关键词</param>
		/// <param name="location">位置</param>
		/// <param name="cityCode">城市编号</param>
		/// <returns></returns>
		[Get("v3/assistant/inputtips?keywords={keywords}&location={Location}&city={CityCode}"), Client("GaoDe")]
		Task<string> SearchLocation(string keywords, string location, string cityCode = "");
		/// <summary>
		/// 获取附近地点
		/// </summary>
		/// <param name="location">坐标中心点经纬度 12.000,23.000</param>
		/// <param name="types">类型编码 090100为医院</param>
		/// <param name="radius">当前坐标半径</param>
		/// <returns></returns>
		[Get("/v5/place/around?location={location}&types={types}&radius={radius}"), Client("GaoDe")]
		Task<string> GetNearbyLocations(string location, string types = "090100", string radius = "1000");
		/// <summary>
		/// 获取当前地点信息
		/// </summary>
		/// <param name="location">坐标中心点经纬度 0.000,0.000</param>
		/// <returns></returns>
		[Get("/v3/geocode/regeo?location={location}"), Client("GaoDe")]
		Task<string> GetLocationInfo(string location);
		/// <summary>
		/// 根据起始地的坐标和目的地坐标获取两地距离
		/// </summary>
		/// <param name="origins">目的地经纬度</param>
		/// <param name="destination">起始地经纬度</param>
		/// <param name="type">类型</param>
		/// <returns></returns>
		[Get("/v3/distance?origins={origins}&destination={destination}&type={type}"), Client("GaoDe")]
		Task<string> GetDistance(string origins, string destination, string type);
		/// <summary>
		/// 全局拦截，类中每一个方法都会触发
		/// </summary>
		/// <param name="req"></param>
		[Interceptor(InterceptorTypes.Request)]
		static void OnRequesting1(HttpRequestMessage req)
		{
			//追加参数
			req.AppendQueries(new { key = App.Configuration["Gaode:AppKey"], output = "JSON" });
		}
	}
}