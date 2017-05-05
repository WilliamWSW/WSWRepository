/**
* 命名空间: ISC_Web.Helpers
*
* 功 能： 自定义Html.Image函数,用于View在使用图片属性时直接调用此方法,避免和html语法混淆.
* 类 名： ImageHelper
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
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class ImageHelper
    {
        /// <summary>
        /// 自定义HtmlHelper Image
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <param name="alternateText"></param>
        /// <returns></returns>
        /// example:
        /// Html.Image("img1", ResolveUrl("~/Content/XBox.jpg"), "XBox Console")
        public static string Image(this HtmlHelper helper, string id, string url, string alternateText)
        {
            return Image(helper, id, url, alternateText, null);
        }

        /// <summary>
        /// 自定义HtmlHelper Image
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <param name="alternateText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        /// example:
        /// Html.Image("img1", ResolveUrl("~/Content/XBox.jpg"), "XBox Console", new {border="4px"})
        public static string Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes)
        {
            // Create tag builder
            var builder = new TagBuilder("img");

            // Create valid id
            builder.GenerateId(id);

            // Add attributes
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return builder.ToString(TagRenderMode.SelfClosing);
        }
    }
}