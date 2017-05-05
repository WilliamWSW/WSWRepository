using ISC_Web.DAL;
using ISC_Web.Models;
using ISC_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISC_Web.Service.BaseInfo
{
    public class BaseInfoManagement
    {
        public static List<VM_GoodsList> GetGoodsListByLoginName(string name)
        {
            //DAL.ISCContext db = new ISCContext();


            using (UnitOfWork uw = new UnitOfWork())
            {
                // var result = uw.bas_User.SelectByWhere(filters: wsw => wsw.loginName.Contains((string.IsNullOrEmpty(name) ? wsw.loginName : name)));
                List<bas_Goods> result=new List<bas_Goods>();

                if (!string.IsNullOrEmpty(name))
                { result = uw.bas_Goods.SelectByWhere(order: o => o.OrderBy(i => i.goodsID), filters: a => a.goodsName.Contains(name)); }
                else { result = uw.bas_Goods.SelectByWhere(order: o => o.OrderBy(i => i.goodsID)); }

                    //ViewModel类,存放结果.
                    List<VM_GoodsList> ResultList = new List<VM_GoodsList>();
                foreach (var item in result)
                {
                    VM_GoodsList TempObject = new VM_GoodsList();
                    TempObject.goodsID = item.goodsID;
                    TempObject.extGoodsID = item.extGoodsID;
                    TempObject.goodsName = item.goodsName;
                    TempObject.tradeName = item.tradeName;
                    TempObject.factory = item.factory;
                    TempObject.spec = item.spec;
                    TempObject.unit = item.unit;
                    TempObject.spellCode = item.spellCode;
                    TempObject.goodsType = item.goodsType;
                    TempObject.extGoodsType = item.extGoodsType;
                    TempObject.goodsCode = item.goodsCode;
                    TempObject.supervisionCode = item.supervisionCode;
                    TempObject.valid = item.valid;
                    TempObject.updateMan = item.updateMan;
                    TempObject.updateTime = item.updateTime;
                    ResultList.Add(TempObject);
                }
                return ResultList;
            }

        }

        public static List<VM_LocatorList> GetLocatorListByLoginName(string name)
        {
            DAL.ISCContext db = new ISCContext();


            using (UnitOfWork uw = new UnitOfWork())
            {
                // var result = uw.bas_User.SelectByWhere(filters: wsw => wsw.loginName.Contains((string.IsNullOrEmpty(name) ? wsw.loginName : name)));
                List<bas_Locator> result = new List<bas_Locator>();

                if (!string.IsNullOrEmpty(name))
                { result = uw.bas_Locator.SelectByWhere(order: o => o.OrderBy(i => i.terminalID).ThenBy(i => i.locatorID), filters: a => a.terminalID.Contains(name)); }
                else { result = uw.bas_Locator.SelectByWhere(order: o => o.OrderBy(i => i.terminalID)); }

                //ViewModel类,存放结果.
                List<VM_LocatorList> ResultList = new List<VM_LocatorList>();
                foreach (var item in result)
                {
                    VM_LocatorList TempObject = new VM_LocatorList();

                    TempObject.terminalID = item.terminalID;
                    TempObject.locatorID = item.locatorID;
                    TempObject.locatorName = item.locatorName;
                    TempObject.lockType = item.lockType;
                    TempObject.isMulti = item.isMulti;
                    TempObject.isMixBatch = item.isMixBatch;
                    TempObject.locatorType = item.locatorType;
                    TempObject.cubage = item.cubage;
                    TempObject.valid = item.valid;
                    TempObject.updateMan = item.updateMan;
                    TempObject.updateTime = item.updateTime;

                    ResultList.Add(TempObject);
                }
                return ResultList;
            }

        }

        public static List<VM_TerminalList> GeTerminalListByLoginName()
        {
            DAL.ISCContext db = new ISCContext();


            using (UnitOfWork uw = new UnitOfWork())
            {
                List<sys_Terminal> result_Terminal = new List<sys_Terminal>();
                List<bas_Department> result_Department = new List<bas_Department>();

                result_Terminal = uw.sys_Terminal.SelectByWhere();
                result_Department = uw.bas_Department.SelectByWhere();

                var doubleQuery = from a in result_Terminal.AsEnumerable()
                                  from b in result_Department.AsEnumerable()
                                  where a.departmentID == b.deptID
                                  select new
                                  {
                                      terminalID = a.terminalID,
                                      terminalName = a.terminalName,
                                      terminalMAC = a.terminalMAC,
                                      terminalIP = a.terminalIP,
                                      terminalType = a.terminalType,
                                      openMode = a.openMode,
                                      departmentID = b.deptName,
                                      updateMan = a.updateMan,
                                      updateTime = a.updateTime
    };


                //ViewModel类,存放结果.
                List<VM_TerminalList> ResultList = new List<VM_TerminalList>();
                foreach (var item in doubleQuery)
                {
                    VM_TerminalList TempObject = new VM_TerminalList();

                    TempObject.terminalID = item.terminalID;
                    TempObject.terminalName = item.terminalName;
                    TempObject.terminalMAC = item.terminalMAC;
                    TempObject.terminalIP = item.terminalIP;
                    TempObject.terminalType = item.terminalType;
                    TempObject.openMode = item.openMode;
                    TempObject.departmentID = item.departmentID;
                    TempObject.updateMan = item.updateMan;
                    TempObject.updateTime = item.updateTime;

                    ResultList.Add(TempObject);
                }
                return ResultList;
            }

        }
        public static int GetMaxGoodsID()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var result = uw.bas_Goods.SelectByWhere().Max(wsw => wsw.goodsID);
                return Convert.ToInt32(result);
            }

        }

        public static List<VM_UserFinger> GetUserFingerListByUserName(string username)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<VM_UserFinger> List_UF = new List<VM_UserFinger>();
               
                var vu = uw.bas_User.SelectByWhere(filters: wsw => wsw.userName.Contains(string.IsNullOrEmpty(username) ? wsw.userName : username)).ToList();
                var vuf = uw.bas_UserFinger.SelectByWhere();
                var vUF = from us in vu
                          from uf in vuf
                          where us.userID == uf.userID
                          orderby uf.updateTime descending
                          select new
                          {
                              ID = uf.ID,
                              userName = us.userName,
                              userID = uf.userID,
                              fingerID = uf.fingerID,
                              finger = uf.finger,
                              updateMan = uf.updateMan,
                              updateTime = uf.updateTime
                          };
                foreach (var item in vUF)
                {
                    VM_UserFinger vmuf = new VM_UserFinger();
                    vmuf.ID = item.ID;
                    vmuf.userName = item.userName;
                    vmuf.userID = item.userID;
                    vmuf.fingerID = item.fingerID;
                    vmuf.finger = item.finger;
                    vmuf.updateMan = item.updateMan;
                    vmuf.updateTime = item.updateTime;
                    List_UF.Add(vmuf);
                }
               
               // List_UF.AddRange(vUF);
                return List_UF;
            }
           
        }

        public static List<ViewModels.VM_LocatorGoods> GetLocatorGoodsByLoginName(string terminalID,string goodsID,string locatorID)
        {
            DAL.ISCContext db = new ISCContext();
            string id = "";
            string goods = "";
            string locatorid = "";

            bool GoodsNull = false;
            bool TerminalNull = false;
            bool LocatorNull = false;
            //判断终端ID是否为空
            if (!string.IsNullOrEmpty(terminalID))
            {

                id = terminalID;
            }
            else
            {
                TerminalNull = true;
            }
            //判断商品编号是否为空
            if (!string.IsNullOrEmpty(goodsID))
            {

                goods = goodsID;
            }
            else
            {
                GoodsNull = true;
            }
            //判断货位ID是否为空
            if (!string.IsNullOrEmpty(locatorID))
            {

                locatorid = locatorID;
            }
            else
            {
                LocatorNull = true;
            }
            using (UnitOfWork uw = new UnitOfWork())
            {
                List<Models.bas_LocatorGoods> result = new List<Models.bas_LocatorGoods>();
                List<Models.bas_Goods> resultGoods = new List<Models.bas_Goods>();
                if (GoodsNull)
                {
                    result = uw.bas_LocatorGoods.SelectByWhere(order: o => o.OrderBy(i => i.terminalID), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id) & (wsw.locatorID == (LocatorNull ? wsw.locatorID : locatorid)|| wsw.extLocatorID == (LocatorNull ? wsw.locatorID : locatorid))
                                 ).Take(1000).ToList();
                    resultGoods = uw.bas_Goods.SelectByWhere();
                }
                else
                {
                    result = uw.bas_LocatorGoods.SelectByWhere(order: o => o.OrderBy(i => i.terminalID), filters: wsw => wsw.terminalID == (TerminalNull ? wsw.terminalID : id) & (wsw.locatorID == (LocatorNull ? wsw.locatorID : locatorid) || wsw.extLocatorID == (LocatorNull ? wsw.locatorID : locatorid))
                                 ).Take(1000).ToList();
                    resultGoods = uw.bas_Goods.SelectByWhere(filters: a => a.goodsID.Contains(goodsID) || a.goodsName.Contains(goodsID) || a.spellCode.Contains(goodsID));
                }

                var doubleQuery = from a in result.AsEnumerable()
                                  from b in resultGoods.AsEnumerable()
                                  where a.goodsID == b.goodsID
                                  select new
                                  {
                                      terminalID = a.terminalID,
                                      goodsID = a.goodsID,
                                      goodsName = b.goodsName + ", " + b.spec,
                                      batchNo = a.batchNo,
                                      extLocatorID = a.extLocatorID,
                                      maxNum = a.maxNum,
                                      locatorID = a.locatorID,
                                      minNum = a.minNum,
                                      updateMan = a.updateMan,
                                      updateTime = a.updateTime
                                  };

                //ViewModel类,存放结果.
                List<ViewModels.VM_LocatorGoods> ResultList = new List<ViewModels.VM_LocatorGoods>();
                foreach (var item in doubleQuery)
                {
                    ViewModels.VM_LocatorGoods TempObject = new ViewModels.VM_LocatorGoods();
                    TempObject.goodsID = item.goodsID;
                    TempObject.locatorID = item.locatorID;
                    TempObject.goodsName = item.goodsName;
                    TempObject.terminalID = item.terminalID;
                    TempObject.extLocatorID = item.extLocatorID;
                    TempObject.batchNo = item.batchNo;
                    TempObject.maxNum = item.maxNum;
                    TempObject.minNum = item.minNum;
                    TempObject.updateMan = item.updateMan;
                    TempObject.updateTime = item.updateTime;
                    ResultList.Add(TempObject);
                }
                return ResultList;
            }

        }

    }
}