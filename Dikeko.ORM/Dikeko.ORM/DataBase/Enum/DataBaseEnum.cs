using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dikeko.ORM.DataBase.Enum
{
    public class DataBaseEnum
    {
        /// <summary>
        /// 添加或者修改
        /// </summary>
        public enum OperationMethodEnum
        {
            /// <summary>
            /// 添加
            /// </summary>
            Add,
            /// <summary>
            /// 修改
            /// </summary>
            Update,
            /// <summary>
            /// 读取
            /// </summary>
            Get
        }

        /// <summary>
        /// sql语句类型
        /// </summary>
        public enum SqlQueryTypeEnum
        {
            /// <summary>
            /// 默认第一条
            /// </summary>
            FirstOrDefault,
            /// <summary>
            /// 默认一条数据
            /// </summary>
            SingleOrDefault,
            /// <summary>
            /// 默认获取
            /// </summary>
            FetchOrDefault,
            /// <summary>
            /// 默认查询总数
            /// </summary>
            CountOrDefault
        }
    }
}
