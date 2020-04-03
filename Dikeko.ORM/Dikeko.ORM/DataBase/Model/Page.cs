using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dikeko.ORM.DataBase.Model
{
    #region 分页实体
    /// <summary>
    /// 分页实体
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class Page<T> : IPage<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 查询集合总个数
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// 每页项数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 查询集合
        /// </summary>
        public IList<T> Items { get; set; }
    }

    /// <summary>
    /// 分页实体接口
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public interface IPage<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; set; }
        /// <summary>
        /// 查询集合总个数
        /// </summary>
        int TotalItems { get; set; }
        /// <summary>
        /// 每页项数
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 查询集合
        /// </summary>
        IList<T> Items { get; set; }
    }
    #endregion

}
