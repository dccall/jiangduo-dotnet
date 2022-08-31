using Furion.DependencyInjection;
using JiangDuo.Core.Services.GaoDe.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiangDuo.Core.Services.GaoDe
{
    public class GaoDeService : ITransient
    {
        private readonly IGaoDeHttp _gaoDeHttp;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gaoDeHttp">高德请求</param>
        public GaoDeService(IGaoDeHttp gaoDeHttp)
        {
            _gaoDeHttp = gaoDeHttp;
        }

        /// <summary>
        /// 根据关键词查询地点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<DtoAddressInfo>> SearchLocation(SearchLocationRequestModel model)
        {
            var res = await _gaoDeHttp.SearchLocation(model.Keywords, model.Location, model.CityCode);
            var jsonModel = JsonConvert.DeserializeObject<SearchLocationResultModel>(res);
            if (jsonModel == null)
            {
                return new List<DtoAddressInfo>();
            }

            var filterList = jsonModel.Tips.Where(x => x.Id is string && x.Location is string).ToList();
            var list = filterList.Select(delegate (TipsModel tips)
            {
                var lngLat = Convert.ToString(tips.Location);
                var split = lngLat.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var lng = Convert.ToDouble(split[0]);
                var lat = Convert.ToDouble(split[1]);

                return new DtoAddressInfo()
                {
                    Name = tips.Name == null ? "" : tips.Name.ToString(),
                    District = tips.District == null ? "" : tips.District.ToString(),
                    Adcode = tips.Adcode == null ? "" : tips.Adcode.ToString(),
                    Longitude = lng,
                    Latitude = lat,
                    Typecode = tips.Typecode == null ? "" : tips.Typecode.ToString(),
                    Address = tips.Address == null ? "" : tips.Address.ToString(),
                };
            }).Where(x => x.Name != "[]" && x.Address != "[]" && x.District != "[]").ToList();

            return list;
        }
    }
}