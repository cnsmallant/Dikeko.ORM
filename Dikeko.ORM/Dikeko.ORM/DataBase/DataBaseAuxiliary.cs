using Dikeko.ORM.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dikeko.ORM.DataBase.Enum.DataBaseEnum;

namespace Dikeko.ORM.DataBase
{
    /// <summary>
    /// 辅助方法
    /// </summary>
    public class DataBaseAuxiliary
    {
        string connectionString = string.Empty;
        /// <summary>
        /// 辅助方法
        /// </summary>
        /// <param name="connectionString"></param>
        public DataBaseAuxiliary(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// 辅助方法
        /// </summary>
        public DataBaseAuxiliary()
        {
        }

        /// <summary>
        /// 当前选择
        /// </summary>
        public static DataBaseAuxiliary Current
        {
            get
            {
                return new DataBaseAuxiliary();
            }
        }

        /// <summary>
        /// 读取表名
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">参数</param>
        /// <returns></returns>
        public string GetTableName<T>(T t)
        {
            Type type = t.GetType();
            var tableName = string.Empty;
            if (type.IsDefined(typeof(TableNameAttribute), true))//判断该方法是否被PrintLogAttribute标记
            {
                TableNameAttribute attribute = type.GetCustomAttribute(typeof(TableNameAttribute)) as TableNameAttribute;//实例化PrintLogAttribute
                tableName = attribute.Name;
            }
            else
            {
                tableName = type.Name;
            }
            return tableName;
        }

        /// <summary>
        /// 读取主键
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">参数</param>
        /// <returns></returns>
        public PrimaryKey GetPrimaryKey<T>(T t)
        {
            Type type = t.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PrimaryKey key = new PrimaryKey();
            foreach (var item in properties)
            {
                if (item.IsDefined(typeof(PrimaryKeyAttribute), true))//判断该方法是否被PrintLogAttribute标记
                {
                    PrimaryKeyAttribute attribute = item.GetCustomAttribute(typeof(PrimaryKeyAttribute)) as PrimaryKeyAttribute;//实例化PrintLogAttribute
                    if (!string.IsNullOrEmpty(attribute.Name))
                    {
                        key.Name = attribute.Name;
                        key.AutoIncrement = attribute.AutoIncrement;
                        break;
                    }
                }
            }
            return key;
        }

        /// <summary>
        /// 获取列
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="t">参数</param>
        /// <param name="operation">操作类型</param>
        /// <returns></returns>
        public List<string> GetColumns<T>(T t, OperationMethodEnum operation)
        {
            var key = GetPrimaryKey(t);
            Type type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            List<string> column = new List<string>();
            foreach (var item in properties)
            {
                if (item.IsDefined(typeof(ColumnAttribute), true))//判断该方法是否被PrintLogAttribute标记
                {
                    ColumnAttribute attribute = item.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;//实例化PrintLogAttribute
                    if (operation == OperationMethodEnum.Add)
                    {
                        if (key.Name == attribute.Name)
                        {
                            if (!key.AutoIncrement)
                            {
                                column.Add(attribute.Name);
                            }
                        }
                        else
                        {
                            column.Add(attribute.Name);
                        }
                    }
                    else
                    {
                        column.Add(attribute.Name);
                    }
                }
                else
                {
                    column.Add(item.Name);
                }
            }

            return column;
        }


        ///// <summary>
        ///// 获取属性名称
        ///// </summary>
        ///// <typeparam name="T">实体类</typeparam>
        ///// <param name="t">实体类参数</param>
        ///// <returns></returns>
        //private static List<string> GetPropertiesName<T>(T t)
        //{
        //    PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        //    List<string> name = new List<string>();
        //    foreach (PropertyInfo item in properties)
        //    {
        //        name.Add(item.Name);
        //    }
        //    return name;
        //}


        /// <summary>
        ///  获取和配置属性参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param operation="operation"></param>
        /// <returns></returns>
        public List<string> GetPropertiesNameSerialization<T>(T t, OperationMethodEnum operation)
        {
            var col = GetColumns(t, OperationMethodEnum.Get);
            var key = GetPrimaryKey(t);
            List<string> name = new List<string>();
            foreach (var item in col)
            {
                if (operation == OperationMethodEnum.Add)
                {
                    if (key.Name == item)
                    {
                        if (!key.AutoIncrement)
                        {
                            name.Add("@" + item);
                        }
                    }
                    else
                    {
                        name.Add("@" + item);
                    }
                }
                else
                {
                    if (key.Name != item)
                    {
                        name.Add($"{item}=@{item}");
                    }
                }
            }
            return name;
        }


        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="t">实体类参数</param>
        /// <param name="operation">操作类型</param>
        /// <returns></returns>
        public List<object> GetPropertiesValue<T>(T t, OperationMethodEnum operation)
        {
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var key = GetPrimaryKey(t);
            List<object> value = new List<object>();
            foreach (PropertyInfo item in properties)
            {
                if (operation == OperationMethodEnum.Add)
                {
                    if (key.Name == item.Name)
                    {
                        if (!key.AutoIncrement)
                        {
                            value.Add(new SqlParameter("@" + item.Name, item.GetValue(t, null)));
                        }
                    }
                    else
                    {
                        value.Add(new SqlParameter("@" + item.Name, item.GetValue(t, null)));
                    }
                }
                else
                {
                    value.Add(new SqlParameter("@" + item.Name, item.GetValue(t, null)));
                }
            }
            return value;
        }

        /// <summary>
        /// 获取主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public PrimaryKey GetPrimaryKeyValue<T>(T t)
        {
            Type type = t.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PrimaryKey key = new PrimaryKey();
            foreach (var item in properties)
            {
                if (item.IsDefined(typeof(PrimaryKeyAttribute), true))//判断该方法是否被PrintLogAttribute标记
                {
                    PrimaryKeyAttribute attribute = item.GetCustomAttribute(typeof(PrimaryKeyAttribute)) as PrimaryKeyAttribute;//实例化PrintLogAttribute
                    if (!string.IsNullOrEmpty(attribute.Name))
                    {
                        key.Name = attribute.Name;
                        key.AutoIncrement = attribute.AutoIncrement;
                        key.Value = item.GetValue(t, null);
                        break;
                    }
                }
            }
            return key;
        }


        /// <summary>
        /// 执行成功返回一条受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params object[] args)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(args);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public int ExecuteTransaction(string sql, params object[] args)
        {
            var result = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(args);
                    result = cmd.ExecuteNonQuery();
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }
        /// <summary>
        /// 返回查询后的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, params object[] args)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(args);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 读取多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="t">实体参数名</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public IEnumerable<T> DataReaderMultiple<T>(T t, string sql, params object[] args)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(args);
                SqlDataReader dr = cmd.ExecuteReader();
                IList<T> list = new List<T>();
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    var col = GetColumns(t, OperationMethodEnum.Get);
                    foreach (var item in col)
                    {
                        if (!IsNullOrDBNull(dr[item]))
                        {
                            PropertyInfo pi = t.GetType().GetProperty(item, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            pi.SetValue(model, HackType(dr[item], pi.PropertyType), null);
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }


        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="t">实体参数名</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public T DataReaderSingle<T>(T t, string sql, params object[] args)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(args);
                SqlDataReader dr = cmd.ExecuteReader();
                T model = Activator.CreateInstance<T>();
                if (dr.Read())
                {
                    var col = GetColumns(t, OperationMethodEnum.Get);
                    foreach (var item in col)
                    {
                        if (!IsNullOrDBNull(dr[item]))
                        {
                            PropertyInfo pi = t.GetType().GetProperty(item, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            pi.SetValue(model, HackType(dr[item], pi.PropertyType), null);
                        }
                    }
                }
                return model;
            }
        }


        /// <summary>
        /// 是否NULL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsNullOrDBNull(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 空类型进行判断转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        private object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);

        }

        /// <summary>
        /// DataSetToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public IList<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
            {
                return null;
            }

            DataTable dt = dataSet.Tables[tableIndex];

            IList<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }

        #region SQL查询语句
        /// <summary>
        /// SQL查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="sqlQueryType">语句类别</param>
        /// <returns></returns>
        public string SelectSQL<T>(T t, SqlQueryTypeEnum sqlQueryType)
        {
            var table = GetTableName(t);
            var col = string.Join(",", GetColumns(t, OperationMethodEnum.Get).ToArray());
            var primaryKey = GetPrimaryKeyValue(t);
            var sql = string.Empty;
            switch (sqlQueryType)
            {
                case SqlQueryTypeEnum.FirstOrDefault:
                    sql = $"SELECT TOP 1 {col}  FROM {table}";
                    break;
                case SqlQueryTypeEnum.SingleOrDefault:
                    sql = $"SELECT  {col}  FROM {table} WHERE {primaryKey.Name}='{primaryKey.Value}'";
                    break;
                case SqlQueryTypeEnum.FetchOrDefault:
                    sql = $"SELECT  {col}  FROM {table}";
                    break;
                case SqlQueryTypeEnum.CountOrDefault:
                    sql = $"SELECT  COUNT(0)  FROM {table}";
                    break;
                default:
                    sql = string.Empty;
                    break;
            }
            return sql;
        }
        #endregion

    }
}
