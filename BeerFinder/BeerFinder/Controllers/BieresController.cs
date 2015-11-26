﻿using BeerFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerFinder.Controllers
{
    public class BieresController : Controller
    {
        //
        // GET: /Bieres/
        public ActionResult Index()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////
        // TYPES
        ////////////////////////////////////////////////////////////
        public ActionResult ListerTypes()
        {
            TypesTable table = new TypesTable(Session["Database"]);
            table.SelectAll();

            return View(table.ToList());
        }

        [HttpGet]
        public ActionResult AjouterTypes()
        {
            return View(new TypesRecord());
        }

        [HttpPost]
        public ActionResult AjouterTypes(TypesRecord type)
        {
            if (ModelState.IsValid)
            {
                TypesTable table = new TypesTable(Session["Database"]);
                table.type = type;
                table.Insert();
                return RedirectToAction("Index", "Bieres");
            }
            return View(type);
        }

        public ActionResult EditerTypes(String Id)
        {
            TypesTable types = new TypesTable(Session["Database"]);
            if (types.SelectByID(Id))
                return View(types.type);
            else
                return RedirectToAction("ListerTypes", "Bieres");
        }

        [HttpPost]
        public ActionResult EditerTypes(TypesRecord record)
        {
            TypesTable table = new TypesTable(Session["Database"]);
            if (ModelState.IsValid)
            {
                if (table.SelectByID(record.Id))
                {
                    table.type = record;
                    table.Update();
                    return RedirectToAction("ListerTypes", "Bieres");
                }
            }
            return View(record);
        }

        public ActionResult SupprimerTypes(String Id)
        {
            TypesTable table = new TypesTable(Session["Database"]);
            table.DeleteRecordByID(Id);
            return RedirectToAction("ListerTypes", "Bieres");
        }

        ////////////////////////////////////////////////////////////
        // BIÈRES
        ////////////////////////////////////////////////////////////
        public ActionResult ListerBieres()
        {
            BieresTable table = new BieresTable(Session["Database"]);
            table.SelectAll();

            return View(table.ToList());
        }

        [HttpGet]
        public ActionResult AjouterBieres()
        {
            BieresRecord biere = new BieresRecord();
            TypesTable types = new TypesTable(Session["Database"]);
            types.SelectAll();
            biere.ListeTypes = types.ToList();
            return View(biere);
        }

        [HttpPost]
        public ActionResult AjouterBieres(BieresRecord biere)
        {
            if (ModelState.IsValid)
            {
                BieresTable table = new BieresTable(Session["Database"]);
                table.biere = biere;
                table.Insert();
                return RedirectToAction("Index", "Bieres");
            }
            return View(biere);
        }

        [HttpGet]
        public ActionResult EditerBieres(String Id)
        {
            BieresTable bieres = new BieresTable(Session["Database"]);
            TypesTable types = new TypesTable(Session["Database"]);
            types.SelectAll();
            bieres.biere.ListeTypes = types.ToList();
            if (bieres.SelectByID(Id))
                return View(bieres.biere);
            else
                return RedirectToAction("ListerBieres", "Bieres");
        }

        [HttpPost]
        public ActionResult EditerBieres(BieresRecord record)
        {
            BieresTable table = new BieresTable(Session["Database"]);
            if (ModelState.IsValid)
            {
                if (table.SelectByID(record.Id.ToString()))
                {
                    table.biere = record;
                    table.Update();
                    return RedirectToAction("ListerBieres", "Bieres");
                }
            }
            return View(record);
        }

        public ActionResult SupprimerBieres(String Id)
        {
            BieresTable table = new BieresTable(Session["Database"]);
            table.DeleteRecordByID(Id);
            return RedirectToAction("ListerBieres", "Bieres");
        }
    }
}