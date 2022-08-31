using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 操作日志记录
    /// </summary>
    [Table("sys_operLog")]
    public partial class SysOperLog : IEntity
    {
        /// <summary>
        /// 日志主键
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 模块标题
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [MaxLength(100)]
        public string Method { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [MaxLength(10)]
        public string RequestMethod { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        [MaxLength(50)]
        public string OperName { get; set; }

        /// <summary>
        /// 请求URL
        /// </summary>
        [MaxLength(300)]
        public string OperUrl { get; set; }

        /// <summary>
        /// 主机地址
        /// </summary>
        [MaxLength(128)]
        public string OperIp { get; set; }

        /// <summary>
        /// 操作来源
        /// </summary>
        [MaxLength(300)]
        public string OperSource { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string OperParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string JsonResult { get; set; }

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public OperLogStatus Status { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime { get; set; }
    }
}