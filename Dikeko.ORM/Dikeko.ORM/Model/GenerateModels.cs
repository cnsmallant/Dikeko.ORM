using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dikeko.ORM
{
    /// <summary>
    /// 数据库表
    /// </summary>
    public class DbTable
    {

        /// <summary>
        /// ID
        /// </summary>
        public int object_id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 数据库表字段
    /// </summary>
    public class DbField
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表说明
        /// </summary>
        public string TableDescription { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int QueueNumber { get; set; }
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 列说明
        /// </summary>
        public string ColumnDescription { get; set; }
        /// <summary>
        /// 是否标识列
        /// </summary>
        public bool IsIdentityColumn { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        public string ColumnType { get; set; }
        /// <summary>
        /// 字节数
        /// </summary>
        public int NumberBytes { get; set; }
        /// <summary>
        /// 列长度
        /// </summary>
        public int ColumnLength { get; set; }
        /// <summary>
        /// 小数位
        /// </summary>
        public int DecimalPlaces { get; set; }
        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool IsAllowEmpty { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string Defaults { get; set; }


    }

    /// <summary>
    /// 生成实体类
    /// </summary>
    public class GenerateModels
    {

     // public  string connstring =ConfigurationManager.ConnectionStrings["connstr"].ConnectionString.ToString();


        /// <summary>
        /// 读取数据库表
        /// </summary>
        /// <returns></returns>
        public IList<DbTable> GetDbTables(string connstring)
        {
            string sql = "SELECT object_id, name from sys.tables";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                IList<DbTable> dbTables = new List<DbTable>();
                while (dr.Read())
                {
                    DbTable dbTale = new DbTable();
                    dbTale.object_id = Convert.ToInt32(dr["object_id"].ToString());
                    dbTale.name = dr["name"].ToString();
                    dbTables.Add(dbTale);
                }
                return dbTables;
            }
        }

        /// <summary>
        /// 读取表字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public IList<DbField> GetDbFields(string tableName,string connstring)
        {
            string sql = @"SELECT  TableName= d.name,
       TableDescription = isnull(f.value, d.name),
       QueueNumber = a.colorder,
       ColumnName = a.name,
       ColumnDescription = isnull(g.[value], a.name),
       IsIdentityColumn = case when COLUMNPROPERTY(a.id, a.name,'IsIdentity')= 1 then 1 else 0 end,
       IsPrimaryKey = case when exists(SELECT 1 FROM sysobjects where xtype = 'PK' and parent_obj = a.id and name in (SELECT name FROM sysindexes WHERE indid in(SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid))) then 1 else 0 end,
       ColumnType = b.name,
       NumberBytes = a.length,
       ColumnLength = COLUMNPROPERTY(a.id, a.name, 'PRECISION'),
       DecimalPlaces = isnull(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0),
       IsAllowEmpty = case when a.isnullable = 1 then 1 else 0 end,
       Defaults = isnull(e.text, '')
FROM syscolumns a
LEFT JOIN systypes b on a.xusertype = b.xusertype
INNER JOIN sysobjects d on a.id = d.id  and d.xtype = 'U' and d.name <> 'dtproperties'
LEFT JOIN  syscomments e on a.cdefault = e.id
LEFT JOIN  sys.extended_properties g on a.id = G.major_id and a.colid = g.minor_id
LEFT JOIN  sys.extended_properties f on d.id = f.major_id and f.minor_id = 0
WHERE d.name = @tableName ORDER BY  QueueNumber ASC";
            SqlParameter parameter = new SqlParameter("@tableName", tableName);
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(parameter);
                SqlDataReader dr = cmd.ExecuteReader();
                IList<DbField> dbFields = new List<DbField>();
                while (dr.Read())
                {
                    DbField dbField = new DbField();
                    dbField.TableName = dr["TableName"].ToString();// 表名称
                    dbField.TableDescription = dr["TableDescription"].ToString();//表说明
                    dbField.QueueNumber = Convert.ToInt32(dr["QueueNumber"].ToString());//排序号
                    dbField.ColumnName = dr["ColumnName"].ToString();//列名称
                    dbField.ColumnDescription = dr["ColumnDescription"].ToString();//列说明
                    dbField.IsIdentityColumn = Convert.ToBoolean(Convert.ToInt32(dr["IsIdentityColumn"].ToString()));//标识列
                    dbField.IsPrimaryKey = Convert.ToBoolean(Convert.ToInt32(dr["IsPrimaryKey"].ToString()));//主键列
                    dbField.ColumnType = dr["ColumnType"].ToString();//列类型
                    dbField.NumberBytes = Convert.ToInt32(dr["NumberBytes"].ToString());//字节数
                    dbField.ColumnLength = Convert.ToInt32(dr["ColumnLength"].ToString());//列长度
                    dbField.DecimalPlaces = Convert.ToInt32(dr["DecimalPlaces"].ToString());//小数位
                    dbField.IsAllowEmpty = Convert.ToBoolean(Convert.ToInt32(dr["IsAllowEmpty"].ToString())); ;//允许空
                    dbField.Defaults = dr["TableName"].ToString();// 默认值
                    dbFields.Add(dbField);
                }
                return dbFields;
            }
        }

        /// <summary>
        /// 数据库字段类型转程序变量类型
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public string DbToNet(string dbFieldType)
        {
            string netFieldType = string.Empty;
            switch (dbFieldType)
            {
                case "int":
                    netFieldType = "int";
                    break;
                case "text":
                    netFieldType = "string";
                    break;
                case "bigint":
                    netFieldType = "long";
                    break;
                case "binary":
                    netFieldType = "byte[]";
                    break;
                case "bit":
                    netFieldType = "bool";
                    break;
                case "char":
                    netFieldType = "string";
                    break;
                case "datetime":
                    netFieldType = "DateTime";
                    break;
                case "decimal":
                    netFieldType = "decimal";
                    break;
                case "float":
                    netFieldType = "double";
                    break;
                case "image":
                    netFieldType = "byte[]";
                    break;
                case "money":
                    netFieldType = "decimal";
                    break;
                case "nchar":
                    netFieldType = "string";
                    break;
                case "ntext":
                    netFieldType = "string";
                    break;
                case "numeric":
                    netFieldType = "decimal";
                    break;
                case "nvarchar":
                    netFieldType = "string";
                    break;
                case "real":
                    netFieldType = "Single";
                    break;
                case "smalldatetime":
                    netFieldType = "DateTime";
                    break;
                case "smallint":
                    netFieldType = "short";
                    break;
                case "smallmoney":
                    netFieldType = "decimal";
                    break;
                case "timestamp":
                    netFieldType = "DateTime";
                    break;
                case "tinyint":
                    netFieldType = "byte";
                    break;
                case "uniqueidentifier":
                    netFieldType = "Guid";
                    break;
                case "varbinary":
                    netFieldType = "byte[]";
                    break;
                case "varchar":
                    netFieldType = "string";
                    break;
                case "Variant":
                    netFieldType = "Object";
                    break;
                default:
                    netFieldType = "string";
                    break;
            }
            return netFieldType;
        }


        public string GenerateEntityClass(string connstring)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\n");
            sb.Append("using System.Collections.Generic;\n");
            sb.Append("using System.Text;\n");
            sb.Append("using System.Data;\n");
            sb.Append($"namespace {MethodBase.GetCurrentMethod().DeclaringType.Namespace}.Model \n ");
            sb.Append("{\n");
            var tables = GetDbTables(connstring);//获取数据库表
            foreach (var item in tables)
            {
                sb.Append($"[TableName(\"{item.name}\")]\n");
                sb.Append($"public class {item.name}\n");
                sb.Append("{\n");
                var fields = GetDbFields(item.name, connstring);
                foreach (var fieldsitem in fields)
                {
                    sb.Append("/// <summary>\n");
                    sb.Append($"///{fieldsitem.ColumnDescription}\n ");
                    sb.Append("/// </summary>\n");
                    sb.Append($"[Column(\"{fieldsitem.ColumnName}\")]\n");
                    if (fieldsitem.IsPrimaryKey)
                    {
                        if (fieldsitem.IsIdentityColumn)
                        {
                            sb.Append($"[PrimaryKey(\"{fieldsitem.ColumnName}\",true)]\n");
                        }
                        else
                        {
                            sb.Append($"[PrimaryKey(\"{fieldsitem.ColumnName}\",false)]\n");
                        }
                    }
                    string netFieldType = DbToNet(fieldsitem.ColumnType);
                    sb.Append($"public  {netFieldType}  {fieldsitem.ColumnName}" + "{ get; set; }\n");
                }
                sb.Append("}\n");
            }
            sb.Append("}\n");


            return sb.ToString();

        }
    }
}
