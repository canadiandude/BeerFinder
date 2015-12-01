using BeerFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerFinder.Controllers
{
    public class SelectionsController : Controller
    {
        //
        // GET: /Selections/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AfficherSelections(string Id)
        {
            BieresTable bieres = new BieresTable(Session["Database"]);
            bieres.SelectFromSelection(Id);

            BarsTable bars = new BarsTable(Session["Database"]);
            bars.SelectByID(Id);
            Session["Bar"] = bars;

            SelectionTable selection = new SelectionTable(Session["Database"]);
            Session["ListePrix"] = selection.ListPrix(Id);

            return View(bieres.ToList());
        }

        [HttpGet]
        public ActionResult AjouterSelection()
        {
            SelectionsRecord selection = new SelectionsRecord();
            selection.IdBar = ((BarsTable)Session["Bar"]).bar.Id;

            BieresTable bieres = new BieresTable(Session["Database"]);
            bieres.SelectAll();
            selection.ListeBieres = bieres.ToList();

            return View(selection);
        }

        [HttpPost]
        public ActionResult AjouterSelection(SelectionsRecord selection)
        {
            selection.IdBar = ((BarsTable)Session["Bar"]).bar.Id;
            if (ModelState.IsValid)
            {
                SelectionTable table = new SelectionTable(Session["Database"]);
                table.Selection = selection;
                table.Insert();
                return RedirectToAction("AfficherSelections/" + ((BarsTable)Session["Bar"]).bar.Id.ToString());
            }
            else
                return View(selection);
            
        }
	}
}