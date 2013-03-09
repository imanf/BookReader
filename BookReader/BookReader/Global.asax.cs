using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookReader
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Book", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseAlways<BookReader.Data.Models.BookReaderContext>());

            //Utilities.Import.ImportItem(@"C:\Projects\BookReader\BookReader\Files\Test\001_Quran.txt");
            //Utilities.Import.ImportItem(@"C:\Projects\BookReader\BookReader\Files\Test\002_Gems.txt");
            //Utilities.Import.ImportItem(@"C:\Projects\BookReader\BookReader\Files\Test\003_Bible_KJV.txt");
            //Utilities.Import.ImportItem(@"C:\Projects\BookReader\BookReader\Files\Target\004_Tabernacle.txt");
            //Utilities.Import.ImportItem(@"C:\Projects\BookReader\BookReader\Files\Prod\005_Iqan.txt");

            //Utilities.Import.ConvertToXML(@"C:\Projects\BookReader\BookReader\Files\Prod\001_Quran.txt", @"C:\Projects\BookReader\BookReader\Files\Prod\001_Quran.xml");
            //Utilities.Import.ConvertToXML(@"C:\Projects\BookReader\BookReader\Files\Prod\002_Gems.txt", @"C:\Projects\BookReader\BookReader\Files\Prod\002_Gems.xml");
            //Utilities.Import.ConvertToXML(@"C:\Projects\BookReader\BookReader\Files\Prod\003_Bible_KJV.txt", @"C:\Projects\BookReader\BookReader\Files\Prod\003_Bible_KJV.xml");
            //Utilities.Import.ConvertToXML(@"C:\Projects\BookReader\BookReader\Files\Prod\004_Tabernacle.txt", @"C:\Projects\BookReader\BookReader\Files\Prod\004_Tabernacle.xml");
            //Utilities.Import.ConvertToXML(@"C:\Projects\BookReader\BookReader\Files\Prod\005_Iqan.txt", @"C:\Projects\BookReader\BookReader\Files\Prod\005_Iqan.xml");
            
            //Utilities.Import.ImportXMLItem(@"C:\Projects\BookReader\BookReader\Files\Prod\001_Quran.xml");
            //Utilities.Import.ImportXMLItem(@"C:\Projects\BookReader\BookReader\Files\Test\002_Gems.xml");
            //Utilities.Import.ImportXMLItem(@"C:\Projects\BookReader\BookReader\Files\Test\003_Bible_KJV.xml");
            //Utilities.Import.ImportXMLItem(@"C:\Projects\BookReader\BookReader\Files\Target\004_Tabernacle.xml");
            //Utilities.Import.ImportXMLItem(@"C:\Projects\BookReader\BookReader\Files\Prod\005_Iqan.xml");

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

    }
}
