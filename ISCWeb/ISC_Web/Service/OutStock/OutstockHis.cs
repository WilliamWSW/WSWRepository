/**
* 命名空间: ISC_Web.Service.OutStock
*
* 功 能： N/A
* 类 名： OutStockHis
*
* Ver   变更日期            负责人   变更内容
* ───────────────────────────────────
* V1.0  2017/4/13 18:46:09    王思伟   初版
*
* Copyright (c) 2017 CQP Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　重庆医药(集团)股份有限公司-信息中心系统研发部　　　　　　　　　　 │
*└──────────────────────────────────┘
*/

using ISC_Web.DAL;
using ISC_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISC_Web.Service.OutStock
{
    public class OutstockHis
    {
        public static List<ViewModels.VM_OutStockHis> GetOutStockDtHis(string terminalid, string goodscode, string goodsname, DateTime starttime, DateTime endtime)
        {
            List<biz_OutStockDt> Insd = new List<biz_OutStockDt>();
            List<bas_Goods> list_goods = new List<bas_Goods>();

            string id = "";
            string goods = "";
            string vgoodsname = "";
            bool GoodsNull = false;
            bool TerminalNull = false;
            //bool GoodsNmaeNull = false;
            //判断终端ID是否为空
            if (!string.IsNullOrEmpty(terminalid))
            {
                id = terminalid;
            }
            else
            {
                TerminalNull = true;
            }
            //判断商品编号是否为空
            if (!string.IsNullOrEmpty(goodscode))
            {
                goods = goodscode;
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
                //GoodsNmaeNull = true;
            }

            ISCContext db = new ISCContext();
            var result = (from aa in db.biz_OutStockDt
                          join bb in db.bas_Goods on aa.goodsID equals bb.goodsID
                          into temp
                          from t in temp.DefaultIfEmpty()
                          where aa.terminalID == (TerminalNull ? aa.terminalID : id)
                          && aa.updateTime >= starttime && aa.updateTime <= endtime
                          && aa.goodsID == (GoodsNull ? aa.goodsID : goods)
                          && (string.IsNullOrEmpty(vgoodsname) || t.goodsName.Contains(vgoodsname))
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

                              goodsName = t.goodsName
                          }

                           );
            
            //判断条件准备完成,进入查询.
            //为了查询效率,在where使用正则表达式不能超过2个.
            //using (UnitOfWork uw = new UnitOfWork())
            //{
            //    if (GoodsNull)
            //    {
            //        Insd = uw.biz_OutStockDt.SelectByWhere(order: o => o.OrderByDescending(i => i.updateTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
            //           & wsw.updateTime >= starttime & wsw.updateTime <= endtime).Take(1000).ToList();
            //        list_goods = uw.bas_Goods.SelectByWhere(filters: wsw => (string.IsNullOrEmpty(goodsname)||wsw.goodsName.Contains(goodsname)));

            //    }
            //    else
            //    {
            //        Insd = uw.biz_OutStockDt.SelectByWhere(order: o => o.OrderByDescending(i => i.updateTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
            //          & wsw.updateTime >= starttime & wsw.updateTime <= endtime & wsw.goodsID.Contains(goods)).Take(1000).ToList();
            //        list_goods = uw.bas_Goods.SelectByWhere(filters: wsw => (string.IsNullOrEmpty(goodsname) || wsw.goodsName.Contains(goodsname)));

            //    }
            //    var result = from a in Insd join b in uw.bas_Goods.SelectByWhere() on a.goodsID equals b.goodsID
            //                 into temp
            //                 from t in temp.DefaultIfEmpty()
            //                 where (string.IsNullOrEmpty(goodsname)||t.goodsName.Contains(goodsname))
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
            //                     goodsName = t.goodsName
            //                 };
            List<ViewModels.VM_OutStockHis> list_ins = new List<ViewModels.VM_OutStockHis>();
            foreach (var item in result)
            {
                ViewModels.VM_OutStockHis ins = new ViewModels.VM_OutStockHis();
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
                ins.goodsName = item.goodsName;
                list_ins.Add(ins);
            }
            return list_ins;
        }

        // }
    }
}