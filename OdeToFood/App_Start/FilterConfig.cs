using System.Web;
using System.Web.Mvc;

namespace OdeToFood
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //HandleErrorAttribute is meant for showing a friendly error page to the user
            filters.Add(new HandleErrorAttribute());
        }
    }
}
