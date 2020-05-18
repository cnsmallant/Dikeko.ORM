
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Dikeko.ORM.Model
{
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
}

