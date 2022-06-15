using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        MvcEntities database = new MvcEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SatisKontrol()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SatisKontrol(Satıslar parametre)
        {
            database.Satıslar.Add(parametre);
            database.SaveChanges();

            return View("Index");

        }
    }
}