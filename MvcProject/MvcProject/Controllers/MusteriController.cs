using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcEntities database = new MvcEntities();
        public ActionResult Index(string parametre)
        {
            var degerler = from veri in database.Musteriler select veri;
            if (!string.IsNullOrEmpty(parametre))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(parametre));

            }
            return View(degerler.ToList());
            //var degerler = database.Musteriler.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult MusteriAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MusteriAdd(Musteriler musteriler)
        {
            if (!ModelState.IsValid)
            {
                return View("MusteriAdd");
            }
            database.Musteriler.Add(musteriler);
            database.SaveChanges();
            return View();
        }
        public ActionResult Delete(int id)
        {
            var musteri = database.Musteriler.Find(id);
            database.Musteriler.Remove(musteri);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var musteri = database.Musteriler.Find(id);
            return View("Update",musteri);
        }
        public ActionResult Guncelle(Musteriler parametre)
        {
            var musteri = database.Musteriler.Find(parametre.MusteriID);
            musteri.MusteriAd = parametre.MusteriAd;
            musteri.MusteriSoyad = parametre.MusteriSoyad;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}