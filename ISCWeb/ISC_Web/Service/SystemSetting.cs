/**
* 命名空间: ISC_Web.Service
*
* 功 能： 自定义系统设置相关的功能.
* 类 名： SystemSetting
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

namespace ISC_Web.Service
{
    public class SystemSetting
    {
        /// <summary>
        /// 获取客户端屏幕分辨率，返回PageList单页行数。
        /// </summary>
        /// <param name="hh">屏幕高度</param>
        /// <returns></returns>
        public static int GetScreen(string hh)
        {
            int EndScreen = 3;
            if (hh==null)
            {
                return 10;
            }
            if (hh.Substring(0,1).Equals("X"))
            {
                var Screen = Convert.ToInt32(hh.Substring(1, hh.Length-1));
                if (Screen < 768)
                {
                    EndScreen = 9;
                }
                else if (Screen == 768)
                {
                    EndScreen = 10;
                }
                else if (Screen > 768 && Screen <= 900)
                {
                    EndScreen = 15;
                }
                else if (Screen > 900 && Screen <= 1024)
                {
                    EndScreen = 18;
                }
                else if (Screen > 1024 && Screen <= 1050)
                {
                    EndScreen = 19;
                }
                else if (Screen > 1050 && Screen <= 1080)
                {
                    EndScreen = 20;
                }
                else 
                {
                    EndScreen = 21;
                }
            }
            else
            {
                var Screen = Convert.ToDecimal(hh);
                EndScreen = (int)Math.Round(Screen / 64);
            }

            return EndScreen;
        }
    }
}