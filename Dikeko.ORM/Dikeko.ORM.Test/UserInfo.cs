using Dikeko.ORM.DataBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dikeko.ORM.Test
{
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

}
