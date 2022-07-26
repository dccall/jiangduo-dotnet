using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JiangDuo.Core.Enums;
using Furion.DatabaseAccessor;

namespace JiangDuo.Core.Base
{
	/// <summary>
	/// 实体类基础基础
	/// </summary>
	[NotMapped]
	public class BaseEntity :IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Key]
		public  long Id { get; set; }
		/// <summary>
		/// 创建者
		/// </summary>
		public long Creator { get; set; }
		/// 创建时间
		/// </summary>
		public DateTime CreatedTime { get; set; }=new DateTime();
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime? UpdatedTime { get; set; }
		/// <summary>
		/// 更新者
		/// </summary>
		public long? Updater { get; set; }
		/// <summary>
		/// <summary>
		/// 是否删除
		/// </summary>
		public bool IsDeleted { get; set; }
    }
}