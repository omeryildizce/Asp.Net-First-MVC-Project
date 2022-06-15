using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcProject.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcEntities database = new MvcEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = database.Kategoriler.ToList();
            var degerler= database.Kategoriler.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Kategoriler kategoriler)
        {
            if (!ModelState.IsValid)
            {
                return View("Add");
            }
            database.Kategoriler.Add(kategoriler);
            database.SaveChanges();
            return View();
        }
        public ActionResult Delete(int id)
        {
            var kategori = database.Kategoriler.Find(id);
            database.Kategoriler.Remove(kategori);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var kategori = database.Kategoriler.Find(id);
            return View("Update", kategori);
        }
        public ActionResult Guncelle(Kategoriler parametre)
        {
            var kategori = database.Kategoriler.Find(parametre.KategoriId);
            kategori.KategoriAd = parametre.KategoriAd;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}