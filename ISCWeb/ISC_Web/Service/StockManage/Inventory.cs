using ISC_Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISC_Web.Service.StockManage
{
    public class Inventory
    {
        /// <summary>
        /// 根据参数获取所有库存信息
        /// </summary>
        /// <param name="terminalid">终端号</param>
        /// <param name="goodscode">商品编码</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public static List<ViewModels.VM_InventoryDt> Inverntory_Search(string terminalid, string goodscode, DateTime starttime, DateTime endtime)
        {
            string id = "";
            string goods = "";

            bool GoodsNull = false;
            bool TerminalNull = false;
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
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Models.biz_InventoryDt> resultStock = new List<Models.biz_InventoryDt>();
                List<Models.bas_Goods> resultGoods = new List<Models.bas_Goods>();
                if (GoodsNull)
                {
                    resultStock = uw.biz_InventoryDt.SelectByWhere(order: o => o.OrderByDescending(i => i.updateTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
                                 & wsw.updateTime >= starttime & wsw.updateTime <= endtime&wsw.status=="2").Take(1000).ToList();
                    resultGoods = uw.bas_Goods.SelectByWhere();
                }
                else
                {
                    resultStock = uw.biz_InventoryDt.SelectByWhere(order: o => o.OrderByDescending(i => i.updateTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
                                 & wsw.updateTime >= starttime & wsw.updateTime <= endtime & wsw.status == "2").Take(1000).ToList();
                    resultGoods = uw.bas_Goods.SelectByWhere(filters: a => a.goodsID.Contains(goodscode) || a.goodsName.Contains(goodscode) || a.spellCode.Contains(goodscode));
                }
                var doubleQuery = from a in resultStock.AsEnumerable()
                                  from b in resultGoods.AsEnumerable()
                                  where a.goodsID == b.goodsID
                                  select new
                                  {
                                      terminalID = a.terminalID,
                                      orderID = a.orderID,
                                      goodsID = a.goodsID,
                                      goodsName = b.goodsName,
                                      batchNo = a.batchNo,
                                      expiryDate = a.expiryDate,
                                      productDate = a.productDate,
                                      orderNum = a.orderNum,
                                      actualNum = a.actualNum,
                                      locatorID = a.locatorID,
                                      RFIDCode = a.RFIDCode,
                                      barCode = a.barcode,
                                      updateMan = a.updateMan,
                                      updateTime = a.updateTime
                                  };


                //ViewModel类,存放结果.
                List<ViewModels.VM_InventoryDt> ResultList = new List<ViewModels.VM_InventoryDt>();
                foreach (var item in doubleQuery)
                {
                    ViewModels.VM_InventoryDt TempObject = new ViewModels.VM_InventoryDt();
                    TempObject.terminalID = item.terminalID;
                    TempObject.orderID = item.orderID;
                    TempObject.goodsID = item.goodsID;
                    TempObject.goodsName = item.goodsName;
                    TempObject.batchNo = item.batchNo;
                    TempObject.expiryDate = item.expiryDate;
                    TempObject.productDate = item.productDate;
                    TempObject.locatorID = item.locatorID;
                    TempObject.RFIDCode = item.RFIDCode;
                    TempObject.barcode = item.barCode;
                    TempObject.orderNum = item.orderNum;
                    TempObject.actualNum = item.actualNum;
                    TempObject.updateMan = item.updateMan;
                    TempObject.updateTime = item.updateTime;
                    ResultList.Add(TempObject);
                }
                return ResultList;
            }
        }
    }
}