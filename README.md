

[TOC]

# 简介

## 什么是Dikeko.ORM？

Dikeko.ORM是一个简单的.NET轻量级的ORM，目前仅支持SqlServer数据库。

## 安装

.NET版：https://www.nuget.org/packages/Dikeko.ORM

```
PM>Install-Package Dikeko.ORM
```

.NET CORE版：https://www.nuget.org/packages/Dikeko.ORM.Core

```
PM>Install-Package Dikeko.ORM.Core
```

## 源码

 .NET 版：https://github.com/cnsmallant/Dikeko.ORM

.NET CORE版：https://github.com/cnsmallant/Dikeko.ORM.Core

## 注意

Dikeko.ORM仅支持SqlServer数据库，.NET CORE版需要手动添加实体类，或者下载源码获取T4模版。

如果遇到不明白的或者发现BUG请加入QQ群：**Java .Net Go PHP UI群：574879752** 直接@群主

实体类示例：

```c#
    [TableName("UserInfo")]
    public class UserInfo
    {
        /// <summary>
        ///主键
        /// </summary>
        [Column("UserId")]
        [PrimaryKey("UserId", false)]
        public string UserId { get; set; }
        /// <summary>
        ///UserCode
        /// </summary>
        [Column("UserCode")]
        public string UserCode { get; set; }
        /// <summary>
        ///昵称
        /// </summary>
        [Column("UserNickName")]
        public string UserNickName { get; set; }
        /// <summary>
        ///手机号
        /// </summary>
        [Column("UserPhone")]
        public string UserPhone { get; set; }
        /// <summary>
        ///密码
        /// </summary>
        [Column("UserPassWord")]
        public string UserPassWord { get; set; }
        /// <summary>
        ///微信OPENID
        /// </summary>
        [Column("WxOpenId")]
        public string WxOpenId { get; set; }
        /// <summary>
        ///QQOPENID
        /// </summary>
        [Column("QqOpenId")]
        public string QqOpenId { get; set; }
        /// <summary>
        ///支付宝OPENID
        /// </summary>
        [Column("AlipayOpenId")]
        public string AlipayOpenId { get; set; }
        /// <summary>
        ///新浪OPENID
        /// </summary>
        [Column("SianOpenId")]
        public string SianOpenId { get; set; }
        /// <summary>
        ///状态
        /// </summary>
        [Column("Status")]
        public int Status { get; set; }
        /// <summary>
        ///创建人
        /// </summary>
        [Column("CreateUserId")]
        public string CreateUserId { get; set; }
        /// <summary>
        ///创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///更新人
        /// </summary>
        [Column("UpdateUserId")]
        public string UpdateUserId { get; set; }
        /// <summary>
        ///更新时间
        /// </summary>
        [Column("UpdateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        [Column("Sort")]
        public int Sort { get; set; }
    }
```

# 方法

```c#
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
        /// <param name="args">参数</param>
        /// <returns></returns>
        Page<T> PageOrDefault<T>(int CurrentPage, int PageSize, string sql, params object[] args);


        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        dynamic Transaction(string sql, params object[] args);
      

        #endregion

    }

```

# 使用示例

```c#
public class UserInfo
{

    /// <summary>
    /// 当前选择
    /// </summary>
    public static UserInfo Current
    {
        get
        {
            return new UserInfo();
        }
    }

    /// <summary>
    /// 实例化数据库操作方法
    /// </summary>
    DikekoDataBase db = new DikekoDataBase("DefaultConnection");
    /// <summary>
    /// 添加用户
    /// </summary>
    /// <returns></returns>
    public int UserAdd()
    {
        try
        {
            var guid = Guid.NewGuid().ToString("N");
            Model.UserInfo user = new Model.UserInfo
            {
                UserId = guid,
                UserCode = "Dikeko1234566",
                UserNickName = "Dikeko111",
                UserPhone = "13212345678",
                UserPassWord = "123456",
                WxOpenId = string.Empty,
                QqOpenId = string.Empty,
                AlipayOpenId = string.Empty,
                SianOpenId = string.Empty,
                Status = 0,
                CreateUserId = guid,
                CreateTime = DateTime.Now,
                UpdateUserId = guid,
                UpdateTime = DateTime.Now,
                Sort = 0
            };

            var result = db.Insert(user);
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// sql语句添加用户
    /// </summary>
    /// <returns></returns>
    public int UserAddForSql()
    {
        try
        {
            string sql = @"INSERT INTO UserInfo
       (
        UserId
       ,UserCode
       ,UserNickName
    )
 VALUES
        (
        @UserId
       ,@UserCode
       ,@UserNickName
      )";
            var guid = Guid.NewGuid().ToString("N");
            Model.UserInfo user = new Model.UserInfo
            {
                UserId = guid,
                UserCode = "Dikeko123466",
                UserNickName = "Dikeko66",
            };
            object[] args =
            {
                new SqlParameter("@UserId",user.UserId),
                new SqlParameter("@UserCode",user.UserCode),
                new SqlParameter("@UserNickName",user.UserNickName),
            };
            var result = db.Insert(sql, args);
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    /// <summary>
    /// 用户修改
    /// </summary>
    /// <returns></returns>
    public int UserUpdate()
    {
        Model.UserInfo user = new Model.UserInfo
        {
            UserId = "ce4bf2d6874f4be0adcf5d7fef12eabf",
            UserCode = "Dikeko123456",
            UserNickName = "Dikeko001",
            UserPhone = "13212345678",
            UserPassWord = "123456",
            WxOpenId = string.Empty,
            QqOpenId = string.Empty,
            AlipayOpenId = string.Empty,
            SianOpenId = string.Empty,
            Status = 0,
            CreateUserId = "ce4bf2d6874f4be0adcf5d7fef12eabf",
            CreateTime = DateTime.Now,
            UpdateUserId = "ce4bf2d6874f4be0adcf5d7fef12eabf",
            UpdateTime = DateTime.Now,
            Sort = 0
        };
        var result = db.Update(user);
        return result;
    }
    /// <summary>
    /// 读取用户
    /// </summary>
    /// <returns></returns>
    public dynamic GetUser()
    {
        var list = db.FetchOrDefault<Model.UserInfo>();
        return JsonConvert.SerializeObject(list);//转换成JSON返回
    }

    /// <summary>
    /// 读取用户
    /// </summary>
    /// <returns></returns>
    public dynamic GetUserById()
    {
        Model.UserInfo user = new Model.UserInfo
        {
            UserId = "ce4bf2d6874f4be0adcf5d7fef12eabf",
        };
        var info = db.SingleOrDefault(user);
        return JsonConvert.SerializeObject(info);//转换成JSON返回
    }
    /// <summary>
    /// 分页读取
    /// </summary>
    /// <returns></returns>
    public dynamic GetUserPage()
    {
        string sql = "SELECT * FROM UserInfo";
        var list = db.PageOrDefault<Model.UserInfo>(1, 10, sql);
        return JsonConvert.SerializeObject(list);//转换成JSON返回
    }
}                                      
```
