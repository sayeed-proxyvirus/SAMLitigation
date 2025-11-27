using Microsoft.AspNetCore.Mvc;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.AuthorizeAttribute;
using SAMLitigation.Models.ViewModel;
using SAMLitigation.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace SAMLitigation.Controllers
{
    //[CustomAuthorize]
    public class HomeController : Controller
    {  
        private readonly DashboardService dashboardService;

        public HomeController(SAMDbContext context, DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            DashboardViewModel viewModel = new DashboardViewModel();

            //viewModel = dashboardService.GetDashboardAllCount();
            //PrepareMenuTree();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //public void PrepareMenuTree() 
        //{
        //    try 
        //    {
        //        var items = new List<Node>
        //        {
        //            new Node { Code = "A", ParentCode = null },
        //            new Node { Code = "B", ParentCode = "A" },
        //            new Node { Code = "C", ParentCode = "A" },

        //            new Node { Code = "D", ParentCode = "B" },
        //            new Node { Code = "E", ParentCode = "B" },

        //            new Node { Code = "F", ParentCode = "C" },

        //            new Node { Code = "G", ParentCode = null },
        //            new Node { Code = "H", ParentCode = "G" },
        //        };

        //        BuildTree(items);

        //    }
        //    catch 
        //    {
        //        throw;
        //    }
        //}

        //public List<Node> BuildTree(List<Node> nodes)
        //{
        //    // Build lookup for faster access
        //    var lookup = nodes.ToDictionary(x => x.Code);

        //    List<Node> rootNodes = new List<Node>();

        //    foreach (var node in nodes)
        //    {
        //        if (string.IsNullOrEmpty(node.ParentCode))
        //        {
        //            // Root node
        //            rootNodes.Add(node);
        //        }
        //        else if (lookup.TryGetValue(node.ParentCode, out var parent))
        //        {
        //            parent.Children.Add(node); // add child to parent
        //        }
        //    }

        //    return rootNodes;
        //}

    }
}
