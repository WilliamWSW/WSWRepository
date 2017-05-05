/**
* 命名空间: ISC_Web.Service
*
* 功 能： 排序显示功能
* 类 名： SortDisplay
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
using ISC_Web.DAL;
using ISC_Web.Models;
using ISC_Web.Repository;
using System.Collections.Generic;
using System.Linq;


namespace ISC_Web.Service
{
    public class SortDisplay
    {

        /// <summary>
        /// 根据传入参数,按相应排序规则和关键字进行查询.
        /// </summary>
        /// <param name="order">排序规则</param>
        /// <param name="search">查询关键字</param>
        /// <returns></returns>
        public static List<bas_Department> SortBybas_Department_1(string order, string search)
        {
             using (UnitOfWork uw = new UnitOfWork())
            {
                bool v_search = false;
                bool v_order = false;

                List<bas_Department> result = new List<bas_Department>();

                if (string.IsNullOrEmpty(search))
                {
                    v_search = true;
                }
                if (string.IsNullOrEmpty(order))
                {
                    v_order = true;
                }
                //都为空值
                if (v_order && v_search)
                {
                    result = uw.bas_Department.SelectByWhere(order:wsw=>wsw.OrderBy(ii=>ii.deptName));
                }
                else if (!v_order && v_search)
                {
                    result = uw.bas_Department.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.deptName));
                }
                else if (!v_order && !v_search)
                {
                    result = uw.bas_Department.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.deptName), filters: ii => ii.deptName.Contains(search));
                }
                else if (v_order && !v_search)
                {
                    result = uw.bas_Department.SelectByWhere(order: oo => oo.OrderBy(ii => ii.deptName), filters: ii => ii.deptName.Contains(search));
                }

                return result;
            }
        }

        /// <summary>
        /// 根据传入参数,按相应排序规则和关键字进行查询.
        /// </summary>
        /// <param name="order">排序规则</param>
        /// <param name="search">查询关键字</param>
        /// <returns></returns>
        public static List<bas_Department> SortBybas_Department_2(string order, string search)
        {
           
            using (UnitOfWork uw = new UnitOfWork())
            {
                bool v_search = false;
               

                List<bas_Department> result = new List<bas_Department>();

                if (string.IsNullOrEmpty(search))
                {
                    v_search = true;
                }
                           
                if (v_search&&order.Equals("ASC"))
                {
                    result = uw.bas_Department.SelectByWhere(order: wsw => wsw.OrderBy(ii => ii.deptName));
                }
                else if ( v_search&& order.Equals("DESC"))
                {
                    result = uw.bas_Department.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.deptName));
                }
                else if (!v_search&& order.Equals("DESC"))
                {
                    result = uw.bas_Department.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.deptName), filters: ii => ii.deptName.Contains(search));
                }
                else if (!v_search&& order.Equals("ASC"))
                {
                    result = uw.bas_Department.SelectByWhere(order: oo => oo.OrderBy(ii => ii.deptName), filters: ii => ii.deptName.Contains(search));
                }

                return result;
            }
        }


        /// <summary>
        /// 根据传入参数,按相应排序规则和关键字进行查询.
        /// </summary>
        /// <param name="order">排序规则</param>
        /// <param name="search">查询关键字</param>
        /// <returns></returns>
        public static List<sys_LocatorSet> SortBysys_LocatorSet_1(string order, string search)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                bool v_search = false;
                bool v_order = false;

                List<sys_LocatorSet> result = new List<sys_LocatorSet>();

                if (string.IsNullOrEmpty(search))
                {
                    v_search = true;
                }
                if (string.IsNullOrEmpty(order))
                {
                    v_order = true;
                }
                //都为空值
                if (v_order && v_search)
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: wsw => wsw.OrderBy(ii => ii.terminalID).ThenBy(ii=>ii.locatorID));
                }
                else if (!v_order && v_search)
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.terminalID).ThenByDescending(ii => ii.locatorID));
                }
                else if (!v_order && !v_search)
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.terminalID).ThenByDescending(ii => ii.locatorID), filters: ii => ii.terminalID.Contains(search));
                }
                else if (v_order && !v_search)
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: oo => oo.OrderBy(ii => ii.terminalID).ThenBy(ii => ii.locatorID), filters: ii => ii.terminalID.Contains(search));
                }

                return result;
            }
        }

        /// <summary>
        /// 根据传入参数,按相应排序规则和关键字进行查询.
        /// </summary>
        /// <param name="order">排序规则</param>
        /// <param name="search">查询关键字</param>
        /// <returns></returns>
        public static List<sys_LocatorSet> SortBysys_LocatorSet_2(string order, string search)
        {

            using (UnitOfWork uw = new UnitOfWork())
            {
                bool v_search = false;


                List<sys_LocatorSet> result = new List<sys_LocatorSet>();

                if (string.IsNullOrEmpty(search))
                {
                    v_search = true;
                }

                if (v_search && order.Equals("ASC"))
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: wsw => wsw.OrderBy(ii => ii.terminalID).ThenBy(ii => ii.locatorID));
                }
                else if (v_search && order.Equals("DESC"))
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.terminalID).ThenBy(ii => ii.locatorID));
                }
                else if (!v_search && order.Equals("DESC"))
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.terminalID).ThenBy(ii => ii.locatorID), filters: ii => ii.terminalID.Contains(search));
                }
                else if (!v_search && order.Equals("ASC"))
                {
                    result = uw.sys_LocatorSet.SelectByWhere(order: oo => oo.OrderBy(ii => ii.terminalID).ThenBy(ii => ii.locatorID), filters: ii => ii.terminalID.Contains(search));
                }

                return result;
            }
        }



        /// <summary>
        /// 根据传入参数,按相应排序规则和关键字进行查询.
        /// </summary>
        /// <param name="order">排序规则</param>
        /// <param name="search">查询关键字</param>
        /// <returns></returns>
        public static List<bas_Goods> SortBybas_Goods_1(string order, string search)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                bool v_search = false;
                bool v_order = false;

                List<bas_Goods> result = new List<bas_Goods>();

                if (string.IsNullOrEmpty(search))
                {
                    v_search = true;
                }
                if (string.IsNullOrEmpty(order))
                {
                    v_order = true;
                }
                //都为空值
                if (v_order && v_search)
                {
                    result = uw.bas_Goods.SelectByWhere(order: wsw => wsw.OrderBy(ii => ii.goodsID));
                }
                else if (!v_order && v_search)
                {
                    result = uw.bas_Goods.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.goodsID));
                }
                else if (!v_order && !v_search)
                {
                    result = uw.bas_Goods.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.goodsID), filters: ii => ii.goodsID.Contains(search));
                }
                else if (v_order && !v_search)
                {
                    result = uw.bas_Goods.SelectByWhere(order: oo => oo.OrderBy(ii => ii.goodsID), filters: ii => ii.goodsID.Contains(search));
                }

                return result;
            }
        }

        /// <summary>
        /// 根据传入参数,按相应排序规则和关键字进行查询.
        /// </summary>
        /// <param name="order">排序规则</param>
        /// <param name="search">查询关键字</param>
        /// <returns></returns>
        public static List<bas_Goods> SortBybas_Goods_2(string order, string search)
        {

            using (UnitOfWork uw = new UnitOfWork())
            {
                bool v_search = false;


                List<bas_Goods> result = new List<bas_Goods>();

                if (string.IsNullOrEmpty(search))
                {
                    v_search = true;
                }

                if (v_search && order.Equals("ASC"))
                {
                    result = uw.bas_Goods.SelectByWhere(order: wsw => wsw.OrderBy(ii => ii.goodsID));
                }
                else if (v_search && order.Equals("DESC"))
                {
                    result = uw.bas_Goods.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.goodsID));
                }
                else if (!v_search && order.Equals("DESC"))
                {
                    result = uw.bas_Goods.SelectByWhere(order: oo => oo.OrderByDescending(ii => ii.goodsID), filters: ii => ii.goodsID.Contains(search));
                }
                else if (!v_search && order.Equals("ASC"))
                {
                    result = uw.bas_Goods.SelectByWhere(order: oo => oo.OrderBy(ii => ii.goodsID), filters: ii => ii.goodsID.Contains(search));
                }

                return result;
            }
        }

    }
}