using Dikeko.ORM.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dikeko.ORM.DataBase.Enum.DataBaseEnum;

namespace Dikeko.ORM.DataBase
{
    public interface IDikekoDataBase
    {
        #region 数据库操作方法
        /// <summary>
        /// 执行插入
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实体参数</param>
        /// <returns></returns>
        dynamic Insert<T>(T t);


        /// <summary>
        /// 执行插入
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        dynamic Insert(string sql, params object[] args);


        /// <summary>
        /// 批量添加（最多一次性1000条数据）
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="t">实体参数</param>
        /// <param name="valuesArray">插入值组合，例如('a1','11','你好1'),('a2','12','你好2'),('a3','13','你好3'),('a4','14','你好4'),('a5','15','你好5')</param>
        /// <returns></returns>
        dynamic BulkInsert<T>(T t, string valuesArray);



        /// <summary>
        /// 批量插入（最多一次性1000条数据）
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">字段</param>
        /// <param name="valuesArray">插入值组合，例如('a1','11','你好1'),('a2','12','你好2'),('a3','13','你好3'),('a4','14','你好4'),('a5','15','你好5')</param>
        /// <returns></returns>
        dynamic BulkInsert(string table, string field, string valuesArray);


        /// <summary>
        /// 执行修改
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实体参数</param>
        /// <returns></returns>
        dynamic Update<T>(T t);

        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        dynamic Update(string sql, params object[] args);


        /// <summary>
        /// 执行删除
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实体参数</param>
        /// <returns></returns>
        dynamic Delete<T>(T t);

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        dynamic Delete(string sql, params object[] args);


        /// <summary>
        /// 默认第一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FirstOrDefault<T>();


        /// <summary>
        /// 默认第一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        T FirstOrDefault<T>(string sql, params object[] args);


        /// <summary>
        /// 默认一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T SingleOrDefault<T>(T t);


        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        T SingleOrDefault<T>(string sql, params object[] args);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        object CountOrDefault<T>();


        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object CountOrDefault(string sql, params object[] args);


        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object CountOrDefault<T>(string sql, params object[] args);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> FetchOrDefault<T>();


        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        List<T> FetchOrDefault<T>(string sql, params object[] args);


        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlVersion">sql版本</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        Page<T> PageOrDefault<T>(int CurrentPage, int PageSize, string sql, SqlVersion sqlVersion, params object[] args);


        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        dynamic Transaction(string sql, params object[] args);
      

        #endregion

    }
}
