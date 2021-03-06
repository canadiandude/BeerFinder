﻿using BeerFinder.Models;
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
            if (!String.IsNullOrEmpty(Id))
            {
                String orderby = "";

                if (Session["SortBy_Selection"] != null)
                   orderby = (String)Session["SortBy_Selection"] + " " + (String)Session["SortOrder"];

                BieresTable bieres = new BieresTable(Session["Database"]);
                bieres.SelectFromSelection(Id, orderby);

                BarsTable bars = new BarsTable(Session["Database"]);
                bars.SelectByID(Id);
                Session["Bar"] = bars;

                SelectionTable selection = new SelectionTable(Session["Database"]);
                Session["ListePrix"] = selection.ListPrix(Id,orderby);
               

                return View(bieres.ToList());
            }
            else
                return RedirectToAction("ListerBars", "Bars");
        }

        [HttpGet]
        public ActionResult AjouterSelections()
        {
            SelectionsRecord selection = new SelectionsRecord();
            selection.IdBar = ((BarsTable)Session["Bar"]).bar.Id;

            BieresTable bieres = new BieresTable(Session["Database"]);
            bieres.SelectAll("Brasserie, NomBiere");
            selection.ListeBieres = bieres.ToList();

            return View(selection);
        }

        [HttpPost]
        public ActionResult AjouterSelections(SelectionsRecord selection)
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
            {
                BieresTable bieres = new BieresTable(Session["Database"]);
                bieres.SelectAll("Brasserie, NomBiere");
                selection.ListeBieres = bieres.ToList();

                return View(selection);
            }
        }

        public ActionResult SupprimerSelections(String id)
        {
            SelectionTable table = new SelectionTable(Session["Database"]);
            table.DeleteRecordByID(id);
            return RedirectToAction("AfficherSelections", ((BarsTable)Session["Bar"]).bar.Id);
        }

        [HttpGet]
        public ActionResult EditerSelections(String Id)
        {
            SelectionTable table = new SelectionTable(Session["Database"]);
            table.SelectByID(Id);
            BieresTable bieres = new BieresTable(Session["Database"]);
            bieres.SelectByID(table.Selection.IdBiere);
            table.Selection.ListeBieres = new List<BieresRecord>();
            table.Selection.ListeBieres.Add(bieres.biere);
            return View(table.Selection);
        }

        [HttpPost]
        public ActionResult EditerSelections(SelectionsRecord selection)
        {
            if (ModelState.IsValid)
            {
                SelectionTable table = new SelectionTable(Session["Database"]);
                table.Selection = selection;
                table.Update();
                return RedirectToAction("AfficherSelections", "Selections", new { Id = ((BarsTable)Session["Bar"]).bar.Id });
            }
            else
            {
                BieresTable bieres = new BieresTable(Session["Database"]);
                bieres.SelectByID(selection.IdBiere);
                selection.ListeBieres = new List<BieresRecord>();
                selection.ListeBieres.Add(bieres.biere);
                return View(selection);
            }
        }
        [HttpGet]
        public ActionResult Trier(String id, String sortBy)
        {

            if (Session["SortBy_Selection"] == null)
            {
                Session["SortBy_Selection"] = sortBy;
                Session["SortOrder"] = "ASC";
            }
            else
            {
                if ((String)Session["SortBy_Selection"] == sortBy)
                {
                    if ((String)Session["sortOrder"] == "ASC")
                        Session["SortOrder"] = "DESC";
                    else
                        Session["SortOrder"] = "ASC";
                }
                else
                {
                    Session["SortBy_Selection"] = sortBy;
                    Session["SortOrder"] = "ASC";
                }
            }
            return RedirectToAction("AfficherSelections", "Selections", new { Id = id });
        }
    }
}