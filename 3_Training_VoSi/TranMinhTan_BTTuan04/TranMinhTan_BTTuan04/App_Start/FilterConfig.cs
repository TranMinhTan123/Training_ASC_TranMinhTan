using System.Web;
using System.Web.Mvc;

namespace TranMinhTan_BTTuan04
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}