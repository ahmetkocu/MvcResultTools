using MvcResultTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PdfResulSample.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// Show pdf file
        /// </summary>
        /// <returns></returns>
        public PdfResult PdfResultTest()
        {
            return new PdfResult(Url.Content("~/SampleFiles/PDF/pdf_test.pdf"));
        }
    }
}
