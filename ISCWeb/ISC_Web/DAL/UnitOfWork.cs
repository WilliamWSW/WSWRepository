/**
* 命名空间: ISC_Web.DAL
*
* 功 能： 工作单元类,封装Repository对象,实现EF事务提交
* 类 名： UnitOfWork
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
using ISC_Web.Repository;
using System;
using System.Web.Mvc;

namespace ISC_Web.DAL
{
    public class UnitOfWork : IDisposable
    {
        //
        // Repository<bas_Department>: 公共Repository方法。所有实体都可以使用的方法，比如CRUD。
        // ***Repository:    私有Repository方法,比如"bas_Department","bas_Department"实体类型特有
        //                   的一些方法可以放在/Repository/***Repository.cs里面。
        //
        //          私有（***Repository）继承公共Repository所有方法，因此
        //          在使用私有（***Repository）类型的时候可以调用公共Repository所有方法，
        //          接口继承方法重用的好处充分体现出来。
        // 命名规则：
        // 1. 变量命名：变量有下划线，公共Repository变量名放在后面，比如"Repository_bas_Department".
        //    私有Repositoy变量名放在前面，比如"bas_Department_Repository".
        // 2. 方法命名：公共类型的规则是采用原始数据库表名作为方法名称，便于使用。 私有类型的规则是***Repository.
        //

        #region 变量定义

        private ISCContext isc = new ISCContext();
        private bas_DepartmentRepository bas_Department_Repository;
        private Repository<bas_Department> Repository_bas_Department;
        private Repository<bas_Goods> Repository_bas_Goods;
        private Repository<bas_Locator> Repository_bas_Locator;
        private Repository<bas_LocatorGoods> Repository_bas_LocatorGoods;
        private Repository<bas_User> Repository_bas_User;
        private Repository<bas_UserFinger> Repository_bas_UserFinger;
        private Repository<bas_UserLocator> Repository_bas_UserLocator;
        private Repository<bas_UserRoots> Repository_bas_UserRoots;
        private Repository<biz_GoodsAdjust> Repository_biz_GoodsAdjust;
        private Repository<biz_GoodsLog> Repository_biz_GoodsLog;
        private Repository<biz_InStock> Repository_biz_InStock;
        private Repository<biz_InStockDt> Repository_biz_InStockDt;
        private Repository<biz_Inventory> Repository_biz_Inventory;
        private Repository<biz_InventoryDt> Repository_biz_InventoryDt;
        private Repository<biz_LossReport> Repository_biz_LossReport;
        private Repository<biz_OutStock> Repository_biz_OutStock;
        private Repository<biz_OutStockAddtion> Repository_biz_OutStockAddtion;
        private Repository<biz_OutStockDt> Repository_biz_OutStockDt;
        private Repository<biz_RFIDLog> Repository_biz_RFIDLog;
        private Repository<biz_RFID_IF_History> Repository_biz_RFID_IF_History;
        private Repository<biz_Stock> Repository_biz_Stock;
        private Repository<interface_backSPD_RF> Repository_interface_backSPD_RF;
        private Repository<interface_InStock_RF> Repository_interface_InStock_RF;
        private Repository<interface_OutStock> Repository_interface_OutStock;
        private Repository<interface_OutStock_RF> Repository_interface_OutStock_RF;
        private Repository<sys_Camera> Repository_sys_Camera;
        private Repository<sys_Hospital> Repository_sys_Hospital;
        private Repository<sys_LocatorSet> Repository_sys_LocatorSet;
        private Repository<sys_Log> Repository_sys_Log;
        private Repository<sys_Module> Repository_sys_Module;
        private Repository<sys_Parameter> Repository_sys_Parameter;
        private Repository<sys_Role> Repository_sys_Role;
        private Repository<sys_RoleModule> Repository_sys_RoleModule;
        private Repository<sys_Terminal> Repository_sys_Terminal;
        private Repository<sys_TypeDic> Repository_sys_TypeDic;
        private Repository<sys_OrderNo> Repository_sys_OrderNo;
        private Repository<sys_UserRole> Repository_sys_UserRole;
        #endregion 变量定义

        #region Storge_new 所有表初始化为Repository对象

        public Repository<sys_OrderNo> sys_OrderNo
        {
            get
            {
                if (this.Repository_sys_OrderNo == null)
                {
                    this.Repository_sys_OrderNo = new Repository<sys_OrderNo>(isc);
                }
                return Repository_sys_OrderNo;
            }
        }

        public Repository<sys_TypeDic> sys_TypeDic
        {
            get
            {
                if (this.Repository_sys_TypeDic == null)
                {
                    this.Repository_sys_TypeDic = new Repository<sys_TypeDic>(isc);
                }
                return Repository_sys_TypeDic;
            }
        }

        public Repository<sys_Terminal> sys_Terminal
        {
            get
            {
                if (this.Repository_sys_Terminal == null)
                {
                    this.Repository_sys_Terminal = new Repository<sys_Terminal>(isc);
                }
                return Repository_sys_Terminal;
            }
        }

        public Repository<sys_RoleModule> sys_RoleModule
        {
            get
            {
                if (this.Repository_sys_RoleModule == null)
                {
                    this.Repository_sys_RoleModule = new Repository<sys_RoleModule>(isc);
                }
                return Repository_sys_RoleModule;
            }
        }

        public Repository<sys_Role> sys_Role
        {
            get
            {
                if (this.Repository_sys_Role == null)
                {
                    this.Repository_sys_Role = new Repository<sys_Role>(isc);
                }
                return Repository_sys_Role;
            }
        }

        public Repository<sys_Parameter> sys_Parameter
        {
            get
            {
                if (this.Repository_sys_Parameter == null)
                {
                    this.Repository_sys_Parameter = new Repository<sys_Parameter>(isc);
                }
                return Repository_sys_Parameter;
            }
        }

        public Repository<sys_Module> sys_Module
        {
            get
            {
                if (this.Repository_sys_Module == null)
                {
                    this.Repository_sys_Module = new Repository<sys_Module>(isc);
                }
                return Repository_sys_Module;
            }
        }

        public Repository<sys_Log> sys_Log
        {
            get
            {
                if (this.Repository_sys_Log == null)
                {
                    this.Repository_sys_Log = new Repository<sys_Log>(isc);
                }
                return Repository_sys_Log;
            }
        }

        public Repository<sys_LocatorSet> sys_LocatorSet
        {
            get
            {
                if (this.Repository_sys_LocatorSet == null)
                {
                    this.Repository_sys_LocatorSet = new Repository<sys_LocatorSet>(isc);
                }
                return Repository_sys_LocatorSet;
            }
        }

        public Repository<sys_Hospital> sys_Hospital
        {
            get
            {
                if (this.Repository_sys_Hospital == null)
                {
                    this.Repository_sys_Hospital = new Repository<sys_Hospital>(isc);
                }
                return Repository_sys_Hospital;
            }
        }

        public Repository<sys_Camera> sys_Camera
        {
            get
            {
                if (this.Repository_sys_Camera == null)
                {
                    this.Repository_sys_Camera = new Repository<sys_Camera>(isc);
                }
                return Repository_sys_Camera;
            }
        }

        public Repository<interface_OutStock_RF> interface_OutStock_RF
        {
            get
            {
                if (this.Repository_interface_OutStock_RF == null)
                {
                    this.Repository_interface_OutStock_RF = new Repository<interface_OutStock_RF>(isc);
                }
                return Repository_interface_OutStock_RF;
            }
        }

        public Repository<interface_OutStock> interface_OutStock
        {
            get
            {
                if (this.Repository_interface_OutStock == null)
                {
                    this.Repository_interface_OutStock = new Repository<interface_OutStock>(isc);
                }
                return Repository_interface_OutStock;
            }
        }

        public Repository<interface_InStock_RF> interface_InStock_RF
        {
            get
            {
                if (this.Repository_interface_InStock_RF == null)
                {
                    this.Repository_interface_InStock_RF = new Repository<interface_InStock_RF>(isc);
                }
                return Repository_interface_InStock_RF;
            }
        }

        public Repository<interface_backSPD_RF> interface_backSPD_RF
        {
            get
            {
                if (this.Repository_interface_backSPD_RF == null)
                {
                    this.Repository_interface_backSPD_RF = new Repository<interface_backSPD_RF>(isc);
                }
                return Repository_interface_backSPD_RF;
            }
        }

        public Repository<biz_Stock> biz_Stock
        {
            get
            {
                if (this.Repository_biz_Stock == null)
                {
                    this.Repository_biz_Stock = new Repository<biz_Stock>(isc);
                }
                return Repository_biz_Stock;
            }
        }

        public Repository<biz_RFID_IF_History> biz_RFID_IF_History
        {
            get
            {
                if (this.Repository_biz_RFID_IF_History == null)
                {
                    this.Repository_biz_RFID_IF_History = new Repository<biz_RFID_IF_History>(isc);
                }
                return Repository_biz_RFID_IF_History;
            }
        }

        public Repository<biz_RFIDLog> biz_RFIDLog
        {
            get
            {
                if (this.Repository_biz_RFIDLog == null)
                {
                    this.Repository_biz_RFIDLog = new Repository<biz_RFIDLog>(isc);
                }
                return Repository_biz_RFIDLog;
            }
        }

        public Repository<biz_OutStockDt> biz_OutStockDt
        {
            get
            {
                if (this.Repository_biz_OutStockDt == null)
                {
                    this.Repository_biz_OutStockDt = new Repository<biz_OutStockDt>(isc);
                }
                return Repository_biz_OutStockDt;
            }
        }

        public Repository<biz_OutStockAddtion> biz_OutStockAddtion
        {
            get
            {
                if (this.Repository_biz_OutStockAddtion == null)
                {
                    this.Repository_biz_OutStockAddtion = new Repository<biz_OutStockAddtion>(isc);
                }
                return Repository_biz_OutStockAddtion;
            }
        }

        public Repository<biz_OutStock> biz_OutStock
        {
            get
            {
                if (this.Repository_biz_OutStock == null)
                {
                    this.Repository_biz_OutStock = new Repository<biz_OutStock>(isc);
                }
                return Repository_biz_OutStock;
            }
        }

        public Repository<biz_LossReport> biz_LossReport
        {
            get
            {
                if (this.Repository_biz_LossReport == null)
                {
                    this.Repository_biz_LossReport = new Repository<biz_LossReport>(isc);
                }
                return Repository_biz_LossReport;
            }
        }

        public Repository<biz_InventoryDt> biz_InventoryDt
        {
            get
            {
                if (this.Repository_biz_Inventory == null)
                {
                    this.Repository_biz_InventoryDt = new Repository<biz_InventoryDt>(isc);
                }
                return Repository_biz_InventoryDt;
            }
        }

        public Repository<biz_Inventory> biz_Inventory
        {
            get
            {
                if (this.Repository_biz_Inventory == null)
                {
                    this.Repository_biz_Inventory = new Repository<biz_Inventory>(isc);
                }
                return Repository_biz_Inventory;
            }
        }

        public Repository<biz_InStockDt> biz_InStockDt
        {
            get
            {
                if (this.Repository_biz_InStockDt == null)
                {
                    this.Repository_biz_InStockDt = new Repository<biz_InStockDt>(isc);
                }
                return Repository_biz_InStockDt;
            }
        }

        public Repository<biz_InStock> biz_InStock
        {
            get
            {
                if (this.Repository_biz_InStock == null)
                {
                    this.Repository_biz_InStock = new Repository<biz_InStock>(isc);
                }
                return Repository_biz_InStock;
            }
        }

        public Repository<biz_GoodsLog> biz_GoodsLog
        {
            get
            {
                if (this.Repository_biz_GoodsLog == null)
                {
                    this.Repository_biz_GoodsLog = new Repository<biz_GoodsLog>(isc);
                }
                return Repository_biz_GoodsLog;
            }
        }

        public Repository<biz_GoodsAdjust> biz_GoodsAdjust
        {
            get
            {
                if (this.Repository_biz_GoodsAdjust == null)
                {
                    this.Repository_biz_GoodsAdjust = new Repository<biz_GoodsAdjust>(isc);
                }
                return Repository_biz_GoodsAdjust;
            }
        }

        public Repository<bas_UserRoots> bas_UserRoots
        {
            get
            {
                if (this.Repository_bas_UserRoots == null)
                {
                    this.Repository_bas_UserRoots = new Repository<bas_UserRoots>(isc);
                }
                return Repository_bas_UserRoots;
            }
        }

        public Repository<bas_UserLocator> bas_UserLocator
        {
            get
            {
                if (this.Repository_bas_UserLocator == null)
                {
                    this.Repository_bas_UserLocator = new Repository<bas_UserLocator>(isc);
                }
                return Repository_bas_UserLocator;
            }
        }

        public Repository<bas_UserFinger> bas_UserFinger
        {
            get
            {
                if (this.Repository_bas_UserFinger == null)
                {
                    this.Repository_bas_UserFinger = new Repository<bas_UserFinger>(isc);
                }
                return Repository_bas_UserFinger;
            }
        }

        public Repository<bas_User> bas_User
        {
            get
            {
                if (this.Repository_bas_User == null)
                {
                    this.Repository_bas_User = new Repository<bas_User>(isc);
                }
                return Repository_bas_User;
            }
        }

        public Repository<bas_LocatorGoods> bas_LocatorGoods
        {
            get
            {
                if (this.Repository_bas_LocatorGoods == null)
                {
                    this.Repository_bas_LocatorGoods = new Repository<bas_LocatorGoods>(isc);
                }
                return Repository_bas_LocatorGoods;
            }
        }

        public Repository<bas_Locator> bas_Locator
        {
            get
            {
                if (this.Repository_bas_Locator == null)
                {
                    this.Repository_bas_Locator = new Repository<bas_Locator>(isc);
                }
                return Repository_bas_Locator;
            }
        }

        public Repository<bas_Goods> bas_Goods
        {
            get
            {
                if (this.Repository_bas_Goods == null)
                {
                    this.Repository_bas_Goods = new Repository<bas_Goods>(isc);
                }
                return Repository_bas_Goods;
            }
        }

        public Repository<bas_Department> bas_Department
        {
            get
            {
                if (this.Repository_bas_Department == null)
                {
                    this.Repository_bas_Department = new Repository<bas_Department>(isc);
                }
                return Repository_bas_Department;
            }
        }

        public Repository<sys_UserRole> sys_UserRole
        {
            get
            {
                if (this.Repository_sys_UserRole == null)
                {
                    this.Repository_sys_UserRole = new Repository<sys_UserRole>(isc);
                }
                return Repository_sys_UserRole;
            }
        }

        #endregion Storge_new 所有表初始化为Repository对象

        #region 实体对象私有方法集合

        public bas_DepartmentRepository bas_DepartmentRepository
        {
            get
            {
                if (this.bas_Department_Repository == null)
                {
                    this.bas_Department_Repository = new bas_DepartmentRepository(isc);
                }
                return bas_Department_Repository;
            }
        }

        #endregion 实体对象私有方法集合

        #region Commit 提交变更集到数据库
       
        /// <summary>
        /// 提交变更集到数据库。
        /// </summary>
        public void Commit()
        {
            
                isc.SaveChanges();
            
            
        }

        #endregion Commit 提交变更集到数据库

        #region IDisposable Support

        #region Dispose

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    isc.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~UnitOfWork()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        /// <summary>
        /// 释放托管和非托管资源。
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }

        #endregion Dispose

        #endregion IDisposable Support
    }
}