using JiangDuo.Application.AppletAppService.OfficialApplet.Dtos;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletService.OfficialApplet.Services
{
    public interface IOfficialAppletService
    {

        /// <summary>
        /// 获取我的工单
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetMyWorkOrderList(DtoMyWorkOrderQuery model);


        /// <summary>
        /// 申请服务(工单)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> ApplyForServices(DtoWorkOrderForm model);


        /// <summary>
        /// 人大登录（微信code登录）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> LoginWeiXin(DtoOfficialLogin model);

        /// <summary>
        /// 人大登录(手机号登录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> LoginByPhone(DtoOfficialLogin model);



        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Task<bool> GetVerifyCode(string phone);
    }
}
