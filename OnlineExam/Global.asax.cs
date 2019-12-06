using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineExam
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //public MvcApplication()
        //{
        //    AuthorizeRequest += new EventHandler(MvcApplication_AuthorizeRequest);
        //}

        //void MvcApplication_AuthorizeRequest(object sender, EventArgs e)
        //{
        //    var id = Context.User.Identity as FormsIdentity;
        //    if (id != null && id.IsAuthenticated)
        //    {
        //        var roles = id.Ticket.UserData.Split(',');
        //        Context.User = new GenericPrincipal(id, roles);
        //    }
        //}
    }
}
