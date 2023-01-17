using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.EF;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class DersController : Controller
    {
        public IActionResult Index(string name)
        {
            //Burada Ders tablosunu kullanıcıya sunduğumuz kodlar mevcut.
            var context = new OKULDbEntities();
            var DersLst = context.Ders.ToList();
            var OkulYonetimlstl = context.OkulYonetim.ToList();

            if (!string.IsNullOrEmpty(name))
            {
                DersLst = (List<Models.Ders>)DersLst.Where(m => m.Ad.Contains(name)).ToList();
                return View(DersLst);
            }
            return View(DersLst);
        }

        public IActionResult DersEkle()
        {
            //Burada ders ekleme sayfasını kullanıcıyla buluşturmak için bir metod açtık.
            return View();
        }

        [HttpPost]
        //Burada Ekle diye bir metod oluşturduk. Parantez içine de yukarıdaki DersEkle metodundan gelecek olan kayıdı metodda kullanabilmemiz için bir parametre tanımladık.
        public IActionResult Ekle(Models.Ders ders)
        {
            if (ders != null)
            {
                var context = new OKULDbEntities();
                var OkulYonetimLst = context.OkulYonetim.ToList();

                //Burada Ders class'ına bir referans oluşturduk. Class'ın değişkenlerine ulaşabilmemiz için. sonrasında bu referansa verdiğimiz isim ile class ın tek tek değerlerine,
                //bize parametre olarak gelen kayıdın class'a karşılık gelen item larını eşitliyoruz.
                var YeniDers = new Ders();
                YeniDers.Ad = ders.Ad;
                YeniDers.Kredisi = ders.Kredisi;
                //Burada normalde OkulYonetimId yazmamız gerekiyor. Fakat biz DersEkle sayfasında item'ın değerine OkulYonetimDef.AdSoyad dediğimiz için bu değeri ekliyoruz. !!!Burayı hocaya sor!!!
                var IdDegeri = context.OkulYonetim.FirstOrDefault(x => x.AdSoyad == ders.OkulYonetimDef.AdSoyad);
                //Soru:Normalde alt taraftaki gibi çağırdığımız değerin(Models.Ders ders)(OkulYonetimDef.AdSoyad) id sini almamız gerekmiyor mu hocam?
                //var IdDegeri = context.OkulYonetim.FirstOrDefault(x => x.Id == ders.OkulYonetimDef.Id);
                YeniDers.OkulYonetimDef = IdDegeri;
                context.Ders.Add(YeniDers);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        //Burada integer bir parametre oluşturuyoruz. Index sayfasında güncelle ve sil kısmında yazdığımız id ile , tıklamış olduğumuz verinin id sini bu metodda getiriyoruz.
        public IActionResult DersSil(int id)
        {
            var context = new OKULDbEntities();
            //Burada Ders tablomuzdaki gelen id nin ilk item'ını yani ilk kayıdını DrsSil değişkenimize atıyoruz.
            var DrsSil = context.Ders.FirstOrDefault(x => x.Id == id);
            //Burada atadığımıız değişkeni Ders tablomuzdan siliyoruz ve sonrasında kaydedip, index sayfasına liink atıyoruz.
            context.Ders.Remove(DrsSil);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Burada integer bir parametre oluşturuyoruz. index sayfamızdaki kayıtların yanında bulunan güncelleye basıldığında o kayıdın id sini bize getirmesi için bir parametredir bu.
        public IActionResult DersGuncelle(int id)
        {
            var context = new OKULDbEntities();
            //Burada Ders tablomuzun içindeki ilk Parametre ile gelen id değerini buluyoruz ve ekrana kullanıcıya gösteriyoruz.
            //buradaki parantez içine yazdığımız id bizim indexden gelen kaydın id sini bize getiren parametredir.
            var updt = context.Ders.FirstOrDefault(x => x.Id == id);
            //Burada ise OkulYonetim class'ımızdaki veriyi güncelle sayfamızda göstereceğimiz için OkulYonetim tablomuzu da listeliyoruz.
            var OkulYonetimlstl = context.OkulYonetim.ToList();
            return View(updt);
        }

        [HttpPost]
        //Burada tekrardan Ders class'ının altında bir ders adında parametre tanımlıyoruz. Yukarıda oluşturduğumuz DersGuncelle metodundan gelen kayıdı bu parametre ile alıyoruz ve kullanıcıya kayıtı gösteriyoruz.
        public IActionResult Guncelle(Models.Ders ders)
        {
            var context = new OKULDbEntities();
            //Burada Ders tablosundan, getirdiğimiz parametre kaydının id sini siliyoruz yani o ilgili kayıdı siliyoruz
            //ve metoda getirdiğimiz parametreyi Ders tablosundaki ilgili kayıdın üstüne güncelliyoruz. en sonda kaydedip index sayfasına link veriyoruz.
            var gncll = context.Ders.FirstOrDefault(x => x.Id == ders.Id);
            context.Ders.Remove(gncll);
            //Burada sildiğimiz kayıdın üstüne o kayıdın Id si ile yeni bir kayıt oluşturuyoruz ve güncelleme işlemini gerçekleştiriyoruz.
            var YeniDers = new Ders();
            YeniDers.Id = ders.Id;
            YeniDers.Ad = ders.Ad;
            YeniDers.Kredisi = ders.Kredisi;
            //Burada normalde OkulYonetimId yazmamız gerekiyor. Fakat biz DersGuncelle sayfasında item'ın değerine OkulYonetimDef.AdSoyad dediğimiz için bu değeri ekliyoruz. !!!Burayı hocaya sor!!!
            var IdDegeri = context.OkulYonetim.FirstOrDefault(x => x.AdSoyad == ders.OkulYonetimDef.AdSoyad);
            //Soru:Normalde alt taraftaki gibi çağırdığımız değerin(Models.Ders ders)(OkulYonetimDef.AdSoyad) id sini almamız gerekmiyor mu hocam?
            //var IdDegeri = context.OkulYonetim.FirstOrDefault(x => x.Id == ders.OkulYonetimDef.Id);
            YeniDers.OkulYonetimDef = IdDegeri;
            context.Ders.Add(YeniDers);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
