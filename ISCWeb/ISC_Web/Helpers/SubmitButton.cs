/**
* 命名空间: ISC_Web.Helpers
*
* 功 能： 自定义Html.Submit函数,用于View使用submit属性时直接调用此方法,避免和html语法混淆.
* 类 名： SubmitButton
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
namespace System.Web.Mvc
{
    public static class SubmitButton
    {
        /// <summary>
        /// 自定义HtmlHelper Submit按钮
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="value">value</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static MvcHtmlString Submit(this HtmlHelper htmlHelper, string value,string id)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttribute("type", "submit");
            builder.MergeAttribute("value", value);
            builder.MergeAttribute("id", id);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}