/**
* 命名空间: ISC_Web.Service.InStock
*
* 功 能： N/A
* 类 名： InstockHis
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/12 9:29:36    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　重庆医药(集团)股份有限公司-信息中心系统研发部　　　　　　　　　　 │
*└──────────────────────────────────┘
*/

using ISC_Web.DAL;
using ISC_Web.Models;
using ISC_Web.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISC_Web.Service.InStock
{
    public class InstockHis
    {

        public static List<ViewModels.VM_InStockHis> GetInStockDtHis(string terminalid,string goodscode,string goodsname,DateTime starttime, DateTime endtime)
        {

             List<biz_InStockDt> Insd = new List<biz_InStockDt>();
            List<bas_Goods> list_goods = new List<bas_Goods>();
          
            string vid = "";
            string vgoods = "";
            string vgoodsname = "";
           
            bool GoodsNull = false;
            bool TerminalNull = false;
            //bool GoodsNmaeNull = false;
            //判断终端ID是否为空
            if (!string.IsNullOrEmpty(terminalid))
            {
              
                vid = terminalid;
            }
            else
            {
                TerminalNull = true;
            }
            //判断商品编号是否为空
            if (!string.IsNullOrEmpty(goodscode))
            {

                vgoods = goodscode;
            }
            else
            {
                GoodsNull = true;
            }
            //判断商品名称是否为空
            if (!string.IsNullOrEmpty(goodsname))
            {
                vgoodsname = goodsname;
            }
            else
            {
                vgoodsname = null;
            }

            ISCContext db = new ISCContext();
            var result = (from aa in db.biz_InStockDt join bb in db.bas_Goods
                           on aa.goodsID equals bb.goodsID
                           into temp
                           from t in temp.DefaultIfEmpty()
                           where aa.terminalID == (TerminalNull ? aa.terminalID : vid)
                           && aa.updateTime >= starttime && aa.updateTime <= endtime
                           && aa.goodsID == (GoodsNull ? aa.goodsID : vgoods)
                           && (string.IsNullOrEmpty(vgoodsname)||t.goodsName.Contains(vgoodsname))
                           select new
                           {
                               terminalID = aa.terminalID,
                               orderID = aa.orderID,
                               detailID = aa.detailID,
                               goodsID = aa.goodsID,
                               batchNo = aa.batchNo,
                               expiryDate = aa.expiryDate,
                               productDate = aa.productDate,
                               orderNum = aa.orderNum,
                               actualNum = aa.actualNum,
                               RFIDCode = aa.RFIDCode,
                               barcode = aa.barcode,
                               locatorID = aa.locatorID,
                               status = aa.status,
                               updateMan = aa.updateMan,
                               updateTime = aa.updateTime,
                               outOrderID = aa.outOrderID,
                               goodsName = t.goodsName
                           }
                           
                           );


            

            //判断条件准备完成,进入查询.
            //为了查询效率,在where使用正则表达式不能超过2个.
            //using (UnitOfWork uw=new UnitOfWork())
            //{

            //    if (GoodsNull)
            //    {
            //        Insd = uw.biz_InStockDt.SelectByWhere(order:o=>o.OrderByDescending(i=>i.updateTime),filters: wsw => wsw.terminalID == (TerminalNull?wsw.terminalID:vid)
            //        & wsw.updateTime >= starttime & wsw.updateTime <= endtime).Take(1000).ToList();
            //        list_goods = uw.bas_Goods.SelectByWhere(filters:wsw=>wsw.goodsName.Contains(GoodsNmaeNull?wsw.goodsName:vgoodsname));
            //    }
            //    else
            //    {
            //        Insd = uw.biz_InStockDt.SelectByWhere(order: o => o.OrderByDescending(i => i.updateTime), filters: wsw => wsw.terminalID == (TerminalNull?wsw.terminalID:vid)
            //           & wsw.updateTime >= starttime & wsw.updateTime <= endtime&wsw.goodsID.Contains(vgoods)).Take(1000).ToList();
            //        list_goods = uw.bas_Goods.SelectByWhere(filters: wsw => wsw.goodsName.Contains(GoodsNmaeNull ? wsw.goodsName : vgoodsname));

            //    }

            //    var result = from a in Insd join b in list_goods
            //                 on a.goodsID equals b.goodsID
            //                 into temp
            //                 from t in temp.DefaultIfEmpty()
            //                 select new
            //                 {
            //                     terminalID = a.terminalID,
            //                     orderID = a.orderID,
            //                     detailID = a.detailID,
            //                     goodsID = a.goodsID,
            //                     batchNo = a.batchNo,
            //                     expiryDate = a.expiryDate,
            //                     productDate = a.productDate,
            //                     orderNum = a.orderNum,
            //                     actualNum = a.actualNum,
            //                     RFIDCode = a.RFIDCode,
            //                     barcode = a.barcode,
            //                     locatorID = a.locatorID,
            //                     status = a.status,
            //                     updateMan = a.updateMan,
            //                     updateTime = a.updateTime,
            //                     outOrderID = a.outOrderID,
            //                     goodsName =t.goodsName
            //                 };
                List<ViewModels.VM_InStockHis> list_ins = new List<ViewModels.VM_InStockHis>();
                foreach (var item in result)
                {
                    ViewModels.VM_InStockHis ins = new ViewModels.VM_InStockHis();
                    ins.terminalID = item.terminalID;
                    ins.orderID = item.orderID;
                    ins.detailID = item.detailID;
                    ins.goodsID = item.goodsID;
                    ins.batchNo = item.batchNo;
                    ins.expiryDate = item.expiryDate;
                    ins.productDate = item.productDate;
                    ins.orderNum = item.orderNum;
                    ins.actualNum = item.actualNum;
                    ins.RFIDCode = item.RFIDCode;
                    ins.barcode = item.barcode;
                    ins.locatorID = item.locatorID;
                    ins.status = item.status;
                    ins.updateMan = item.updateMan;
                    ins.updateTime = item.updateTime;
                    ins.outOrderID = item.outOrderID;
                    ins.goodsName = item.goodsName;
                    list_ins.Add(ins);
                }
                return list_ins;
            //}
            
        }
            
            
        
            


    }
}