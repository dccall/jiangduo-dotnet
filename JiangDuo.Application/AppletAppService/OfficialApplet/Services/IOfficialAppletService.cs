using JiangDuo.Application.AppletAppService.OfficialApplet.Dtos;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
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

        /// <summary>
        /// 我的服务列表(一老一小)
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoService> GetMyServices(DtoServiceQuery model);
        /// <summary>
        /// 服务详情(一老一小)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<DtoService> GetServicesDetail(long id);
        /// <summary>
        /// 创建服务(一老一小）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> AddServices(DtoServiceForm model);

        /// <summary>
        /// 删除服务(一老一小)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> DeleteServices(long id);



        /// <summary>
        /// 获取我的预约（有事好商量）
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoReserve> GetMyReserves(DtoReserveQuery model);
        /// <summary>
        /// 获取预约详情(有事好商量)
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public  Task<DtoReserve> GetReserveDetail(long id);
        /// <summary>
        /// 添加预约(有事好商量)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> AddReserve(DtoReserveForm model);
        /// <summary>
        /// 删除预约（有事好商量）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  Task<string> DeleteReserve(long id);




        /// <summary>
        /// 获取我的工单
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetMyWorkOrderList(DtoMyWorkOrderQuery model);

        /// <summary>
        /// 根据id查询工单详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public  Task<DtoWorkOrder> GetWorkOrderDetail(long id);
        /// <summary>
        /// 工单完成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> WorkOrderCompleted(DtoWorkOrderCompletedHandel model);
    }
}
