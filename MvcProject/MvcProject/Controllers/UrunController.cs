using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models.Entity;
namespace MvcProject.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcEntities database = new MvcEntities();

        public ActionResult Index()
        {
            var degerler = database.Urunler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunAdd()
        {
            List<SelectListItem> degerler = (from i in database.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()

                                             }).ToList();
            ViewBag.Degerler = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunAdd(Urunler urunler)
        {
            var kategori = database.Kategoriler.Where(m => m.KategoriId == urunler.Kategoriler.KategoriId).FirstOrDefault();
            urunler.Kategoriler = kategori;
            database.Urunler.Add(urunler);
            database.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            var urun = database.Urunler.Find(id);
            database.Urunler.Remove(urun);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var urun = database.Urunler.Find(id);
            List<SelectListItem> degerler = (from i in database.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()

                                             }).ToList();
            ViewBag.Degerler = degerler;
            return View("Update", urun);


        }
        public ActionResult Guncelle(Urunler parametre)
        {
            var urun = database.Urunler.Find(parametre.UrunId);
            urun.UrunAd = parametre.UrunAd;
            urun.Marka = parametre.Marka;
            urun.Stok = parametre.Stok;
            urun.Fiyat = parametre.Fiyat;
            // urun.Kategoriler = parametre.Kategoriler;
            var kategori = database.Kategoriler.Where(m => m.KategoriId == parametre.Kategoriler.KategoriId).FirstOrDefault();
            urun.UrunKategori = kategori.KategoriId;
            database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}