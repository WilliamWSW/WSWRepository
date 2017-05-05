using ISC_Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISC_Web.Service.StockManage
{
    public class GoodsLogQuery
    {
        /// <summary>
        /// 根据参数获取所有库存信息
        /// </summary>
        /// <param name="terminalid">终端号</param>
        /// <param name="goodscode">商品编码</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public static List<ViewModels.VM_GoodsLog> GoodsLog_Search(string terminalid, string operationtype, string goodscode, DateTime starttime, DateTime endtime)
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
                List<Models.biz_GoodsLog> resultGoodsLog = new List<Models.biz_GoodsLog>();
                List<Models.bas_Goods> resultGoods = new List<Models.bas_Goods>();
                if (GoodsNull)
                {
                    if (operationtype == null || operationtype == "all" || operationtype == string.Empty)
                    {
                        resultGoodsLog = uw.biz_GoodsLog.SelectByWhere(order: o => o.OrderBy(i => i.startTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
                                 & wsw.startTime >= starttime & wsw.startTime <= endtime).Take(1000).ToList();
                        resultGoods = uw.bas_Goods.SelectByWhere();
                    }
                    else
                    {
                        resultGoodsLog = uw.biz_GoodsLog.SelectByWhere(order: o => o.OrderBy(i => i.startTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
                                     & wsw.startTime >= starttime & wsw.startTime <= endtime&wsw.operationtype.Contains(operationtype)).Take(1000).ToList();
                        resultGoods = uw.bas_Goods.SelectByWhere();
                    }
                }
                else
                {
                    if (operationtype == null || operationtype == "all" || operationtype == string.Empty)
                    {
                        resultGoodsLog = uw.biz_GoodsLog.SelectByWhere(order: o => o.OrderBy(i => i.startTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
                                 & wsw.startTime >= starttime & wsw.startTime <= endtime).Take(1000).ToList();
                        resultGoods = uw.bas_Goods.SelectByWhere(filters: a => a.goodsID.Contains(goodscode) || a.goodsName.Contains(goodscode) || a.spellCode.Contains(goodscode));
                    }
                    else
                    {
                        resultGoodsLog = uw.biz_GoodsLog.SelectByWhere(order: o => o.OrderBy(i => i.startTime), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id)
                                 & wsw.startTime >= starttime & wsw.startTime <= endtime & wsw.operationtype.Contains(operationtype)).Take(1000).ToList();
                        resultGoods = uw.bas_Goods.SelectByWhere(filters: a => a.goodsID.Contains(goodscode) || a.goodsName.Contains(goodscode) || a.spellCode.Contains(goodscode));
                    }
                }
                
                var doubleQuery = from a in resultGoodsLog.AsEnumerable()
                                  from b in resultGoods.AsEnumerable()
                                  where a.goodsID == b.goodsID
                                  select new
                                  {
                                      terminalID = a.terminalID,
                                      operationtype = a.operationtype,
                                      goodsID = a.goodsID,
                                      goodsName = b.goodsName,
                                      batchNo = a.batchNo,
                                      expiryDate = a.expiryDate,
                                      productDate = a.productDate,
                                      actualNum = a.actualNum,
                                      locatorID = a.locatorID,
                                      RFID = a.RFID,
                                      barCode = a.barCode,
                                      operator1=a.operator1,
                                      startTime = a.startTime,
                                      endTime = a.endTime
                                  };


                //ViewModel类,存放结果.
                List<ViewModels.VM_GoodsLog> ResultList = new List<ViewModels.VM_GoodsLog>();
                foreach (var item in doubleQuery)
                {
                    ViewModels.VM_GoodsLog TempObject = new ViewModels.VM_GoodsLog();
                    TempObject.terminalID = item.terminalID;
                    TempObject.operationtype = item.operationtype;
                    TempObject.goodsID = item.goodsID;
                    TempObject.goodsName = item.goodsName;
                    TempObject.batchNo = item.batchNo;
                    TempObject.expiryDate = item.expiryDate;
                    TempObject.productDate = item.productDate;
                    TempObject.locatorID = item.locatorID;
                    TempObject.RFID = item.RFID;
                    TempObject.barCode = item.barCode;
                    TempObject.actualNum = item.actualNum;
                    TempObject.operator1 = item.operator1;
                    TempObject.startTime = item.startTime;
                    TempObject.endTime = item.endTime;
                    ResultList.Add(TempObject);
                }
                return ResultList;
            }
        }

    }
}