using BeerFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerFinder.Controllers
{
    public class BarsController : Controller
    {
        //
        // GET: /Bars/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListerBars()
        {
            BarsTable bars = new BarsTable(Session["Database"]);
            bars.SelectAll();
            return View(bars.ToList());
        }
       
        [HttpGet]
        public ActionResult AjouterBars()
        {
            return View(new BarsRecord());
        }
        [HttpPost]
        public ActionResult AjouterBars(BarsRecord bar)
        {
            if (ModelState.IsValid)
            {
                BarsTable table = new BarsTable(Session["Database"]);                
                table.bar = bar;
                table.bar.UploadLogo(Request);
                table.Insert();
                return RedirectToAction("ListerBars", "Bars");
            }
            return View(bar);
        }

           [HttpGet]
         public ActionResult SupprimerBar(String id)
        {
            BarsTable table = new BarsTable(Session["Database"]);
            table.DeleteRecordByID(id);

            return RedirectToAction("ListerBars", "Bars");
        }
           [HttpGet]
        public ActionResult EditerBar(String id)
           {
               BarsTable table = new BarsTable(Session["Database"]);
               if (table.SelectByID(id))
                   return View(table.bar);
               else
                   return RedirectToAction("ListerBars", "Bars");

           }

           [HttpPost]
           public ActionResult EditerBar(BarsRecord bar)
           {
               if (ModelState.IsValid)
               {
                   BarsTable table = new BarsTable(Session["Database"]);
                   table.bar = bar;
                   table.bar.UploadLogo(Request);
                   table.Update();
                   return RedirectToAction("ListerBars", "Bars");
               }

               return View(bar);
           }
	}
}