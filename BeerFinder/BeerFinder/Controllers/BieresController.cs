using BeerFinder.Models;
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
        public ActionResult AjouterType()
        {
            return View(new TypesRecord());
        }

        [HttpPost]
        public ActionResult AjouterType(TypesRecord type)
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

        public ActionResult EditerType(String Id)
        {
            TypesTable types = new TypesTable(Session["Database"]);
            if (types.SelectByID(Id))
                return View(types.type);
            else
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
	}
}