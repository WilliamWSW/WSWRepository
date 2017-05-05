/**
* 命名空间: ISC_Web.Controllers
*
* 功 能： 入库管理模块
* 类 名： InStockController
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISC_Web.Repository;
using ISC_Web.DAL;
using ISC_Web.Models;
using ISC_Web.Service;
using PagedList;
namespace ISC_Web.Controllers
{
    [CustomAuthorize]
    [HandleError(View = "CustomError")]
    /// <summary>
    /// 入库管理模块
    /// </summary>
    public class InStockController : Controller
    {

      
        // GET: InStock
        public ActionResult InstockHis(string id,int? page,string txtirsterminalid_input,string txtirsGoodsCode,string txtirsGoodsName,string txtirsdatetimepicker1,string txtirsdatetimepicker2)
        {
            //id是客户端屏幕分辨率
            int PageNum = SystemSetting.GetScreen(id);
            if (PageNum > 11)
            {
                ViewBag.TableHeight = "500";
            }
            else
            {
                ViewBag.TableHeight = "330";
            }
            List<ViewModels.VM_InStockHis> result = new List<ViewModels.VM_InStockHis>();
            DateTime start;
            DateTime end;
            string vterminalid = "";
            //判断查询开始时间是否为空
            if (!string.IsNullOrEmpty(txtirsdatetimepicker1))
            {

                start = Convert.ToDateTime(txtirsdatetimepicker1);
            }
            else
            {
                //参数空,默认为昨天
                start = Convert.ToDateTime((DateTime.Now.AddDays(-1)).ToShortDateString());
            }
            //判断结束时间是否为空
            if (!string.IsNullOrEmpty(txtirsdatetimepicker2))
            {

                end = Convert.ToDateTime(txtirsdatetimepicker2);
            }
            else
            {
                //参数空,默认明天
                end = Convert.ToDateTime((DateTime.Now.AddDays(1)).ToShortDateString());
            }
            //判断终端ID是否为空,为空就默认为终端1
            if (!string.IsNullOrEmpty(txtirsterminalid_input))
            {
                vterminalid = txtirsterminalid_input;
            }
            else
            {
                vterminalid = "1";
            }
            //调用Serveric层的GetInStockDtHis方法,返回查询结果.
            result = Service.InStock.InstockHis.GetInStockDtHis(vterminalid, txtirsGoodsCode,txtirsGoodsName,start,end);
            //向View传值,为Pagelist分页传值.
            ViewBag.TerminalID = vterminalid;
            ViewBag.GoodsID    = txtirsGoodsCode;
            ViewBag.StartTime  = start.ToString();
            ViewBag.EndTime    = end.ToString();
            ViewBag.GoodsName = txtirsGoodsName;
            
            //页面显示行数设置
            //如果使用Bootstrap默认设置,行数超过页面会出现页面滚动条.为了不影响用户体验可以获取用户屏幕分辨率来设置页面行数值,使表格不超过屏幕.
            //如果使用JSTable或CSSTable,行数超过会出现表格滚动条,不会出现页面滚动条.因此不用写其它方法来设置页面行数.
            //以上两种方法各有好处,推荐使用Bootstrap,响应式布局支持各种大小屏幕,并且不会影响用户体验,不用写任何CSS或JS代码.JStable和CSStable在小屏幕上全部数据重叠在一起,无法识别.
            //本界面使用的是CSStable,桌面端的效果很好,但在小屏幕上表格数据全部重叠在一起,无法使用.通过缩小IE窗口大小便可知道显示效果.
            int pageSize = PageNum;
            int pageNumber = (page ?? 1);
            return View(result.ToPagedList(pageNumber, pageSize));

           
        }

      

        public ActionResult test()
        {
            return View();
        }
    }
}