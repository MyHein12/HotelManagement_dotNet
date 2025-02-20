using System.Web.Mvc;

namespace HotelManagement.Admin
{
    public class DashboardAreaRegistration : DashboardAreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Dashboard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)  
        {
            context.MapRoute(
                "Dashboard_default",
                "Dasboard/{controller}/{action}/{id}",
                new {action = "Index", id = UrlPamameter.Optional}
            );
        }
    }
}