/**
* 命名空间: ISC_Web.DAL
*
* 功 能： EF Context
* 类 名： ISCContext
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/2 1:03:12    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：重庆医药(集团)股份有限公司-信息中心系统研发部    　　　 │
*└──────────────────────────────────┘
*/
using ISC_Web.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ISC_Web.DAL
{
    public class ISCContext : DbContext
    {
        public ISCContext() : base("Storage_new")
        {
        }


        public virtual DbSet<bas_Department> bas_Department { get; set; }
        public virtual DbSet<bas_Goods> bas_Goods { get; set; }
        public virtual DbSet<bas_Locator> bas_Locator { get; set; }
        public virtual DbSet<bas_LocatorGoods> bas_LocatorGoods { get; set; }
        //public virtual DbSet<bas_LocatorGoods_Test> bas_LocatorGoods_Test { get; set; }
        public virtual DbSet<bas_User> bas_User { get; set; }
        public virtual DbSet<bas_UserFinger> bas_UserFinger { get; set; }
        public virtual DbSet<bas_UserLocator> bas_UserLocator { get; set; }
        public virtual DbSet<biz_GoodsAdjust> biz_GoodsAdjust { get; set; }
        public virtual DbSet<biz_GoodsLog> biz_GoodsLog { get; set; }
        public virtual DbSet<biz_InStock> biz_InStock { get; set; }
        public virtual DbSet<biz_InStockDt> biz_InStockDt { get; set; }
       // public virtual DbSet<biz_InStockDt_Test> biz_InStockDt_Test { get; set; }
        public virtual DbSet<biz_Inventory> biz_Inventory { get; set; }
        public virtual DbSet<biz_InventoryDt> biz_InventoryDt { get; set; }
        public virtual DbSet<biz_LossReport> biz_LossReport { get; set; }
        public virtual DbSet<biz_OutStock> biz_OutStock { get; set; }
        public virtual DbSet<biz_OutStockAddtion> biz_OutStockAddtion { get; set; }
        public virtual DbSet<biz_OutStockDt> biz_OutStockDt { get; set; }
       // public virtual DbSet<biz_OutStockDt_Test> biz_OutStockDt_Test { get; set; }
        public virtual DbSet<biz_RFIDLog> biz_RFIDLog { get; set; }
        public virtual DbSet<biz_Stock> biz_Stock { get; set; }
        public virtual DbSet<interface_InStock_RF> interface_InStock_RF { get; set; }
        public virtual DbSet<interface_OutStock> interface_OutStock { get; set; }
        public virtual DbSet<interface_OutStock_RF> interface_OutStock_RF { get; set; }
        public virtual DbSet<sys_Camera> sys_Camera { get; set; }
        public virtual DbSet<sys_Hospital> sys_Hospital { get; set; }
        public virtual DbSet<sys_LocatorSet> sys_LocatorSet { get; set; }
        public virtual DbSet<sys_Log> sys_Log { get; set; }
        public virtual DbSet<sys_Module> sys_Module { get; set; }
        public virtual DbSet<sys_Parameter> sys_Parameter { get; set; }
        public virtual DbSet<sys_Role> sys_Role { get; set; }
        public virtual DbSet<sys_RoleModule> sys_RoleModule { get; set; }
        public virtual DbSet<sys_Terminal> sys_Terminal { get; set; }
        public virtual DbSet<sys_TypeDic> sys_TypeDic { get; set; }
        public virtual DbSet<bas_UserRoots> bas_UserRoots { get; set; }
        public virtual DbSet<biz_RFID_IF_History> biz_RFID_IF_History { get; set; }
        public virtual DbSet<interface_backSPD_RF> interface_backSPD_RF { get; set; }
        public virtual DbSet<sys_UserRole> sys_UserRole { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}