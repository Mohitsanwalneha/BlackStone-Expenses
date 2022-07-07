using System.Web;
using System.Web.Mvc;

namespace BlackStone_Expenses
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
