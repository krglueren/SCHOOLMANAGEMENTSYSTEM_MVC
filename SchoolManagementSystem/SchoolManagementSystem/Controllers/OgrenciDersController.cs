using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.EF;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class OgrenciDersController : Controller
    {
        public IActionResult Index(string name)
        {
            //Burada OgrenciDers tablomuzdaki verileri listelemek için bize lazım olan kodları yazıyoruz.
            //Ders ve Ogrenci tablolarımızı listelememizin sebebi ise OgrenciDers tablosunda diğer listelediğimiz tabloların bilgilerinin de olması
            var context = new OKULDbEntities();
            var OgrenciDersLstl = context.OgrenciDers.ToList();
            var DersListele = context.Ders.ToList();
            var OgrenciListele = context.Ogrenci.ToList();

            if (!string.IsNullOrEmpty(name))
            {
                OgrenciDersLstl = (List<Models.OgrenciDers>)OgrenciDersLstl.Where(m => m.DersDef.Ad.Contains(name)).ToList();
                return View(OgrenciDersLstl);
            }

            return View(OgrenciDersLstl);

        }

        //Burada OgrenciDersEkle sayfasını kullanıcı ile buluşturuyoruz.
        public IActionResult OgrenciDersEkle()
        {
            return View();
        }

        [HttpPost]
        //Burada post metodumuzla tablomuza Sayfadan gelen değerleri ekliyoruz. parametre ile bunu gerçekleştiriyoruz.
        public IActionResult Ekle(Models.OgrenciDers ogrenciders)
        {

            if (ogrenciders != null)
            {
                var context = new OKULDbEntities();
                var Derslst = context.Ders.ToList();
                var Ogrencilst = context.Ogrenci.ToList();

                //Burada OgrenciDers class'ımızı referans gösterip altındaki değerlerine parametre ile getirdiğimiz değerleri eşitliyoruz.
                var YeniKyt = new OgrenciDers();
                //Burada OgrenciDers tablosunu listelediğimiz view kısmında yazdığımız DersDef.Ad ı bulup ekliyoruz.
                var IdDegeri = context.Ders.FirstOrDefault(x => x.Ad == ogrenciders.DersDef.Ad);
                YeniKyt.DersDef = IdDegeri;
                //Aynı şekilde ogrenci için de aynı işlemi yapıyoruz
                var IdDegeri2 = context.Ogrenci.FirstOrDefault(x => x.AdSoyad == ogrenciders.OgrenciDef.AdSoyad);
                YeniKyt.OgrenciDef = IdDegeri2;
                //En son da OgrenciDers tablomuza bu kayıdı ekleyip kaydediyoruz ve index sayfamıza yönlendiriyoruz.
                context.OgrenciDers.Add(YeniKyt);
                context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View();
        }

        //Burada İndex sayfamızdan gelen değeri id parametresi ile metodumuza getirip siliyoruz, kaydedip index sayfamıza yönlendiriyoruz.
        public IActionResult OgrenciDersSil(int id)
        {
            var context = new OKULDbEntities();
            var OgrncDrsSil = context.OgrenciDers.FirstOrDefault(x => x.Id == id);
            context.OgrenciDers.Remove(OgrncDrsSil);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Burada integer bir parametre oluşturuyoruz. index sayfamızdaki kayıtların yanında bulunan güncelleye basıldığında o kayıdın id sini bize getirmesi için bir parametredir bu.
        public IActionResult OgrenciDersGuncelle(int id)
        {
            var context = new OKULDbEntities();
            //Burada Ders tablomuzun içindeki ilk Parametre ile gelen id değerini buluyoruz ve ekrana kullanıcıya gösteriyoruz.
            //buradaki parantez içine yazdığımız id bizim indexden gelen kaydın id sini bize getiren parametredir.
            var updt = context.OgrenciDers.FirstOrDefault(x => x.Id == id);
            //Burada ise Ogrenci ve Ders class'ımızdaki veriyi güncelle sayfamızda göstereceğimiz için listeliyoruz. OgrenciDers sayfasını da listeliyoruz. 
            var OkulYonetimlstl = context.OgrenciDers.ToList();
            var OgrenciList = context.Ogrenci.ToList();
            var DersList = context.Ders.ToList();
            return View(updt);
        }

        [HttpPost]
        //Burada tekrardan OgrenciDers class'ının altında bir ogrenciders adında parametre tanımlıyoruz. Yukarıda oluşturduğumuz OgrenciDersGuncelle metodundan gelen kayıdı bu parametre ile alıyoruz ve kullanıcıya kayıtı gösteriyoruz.
        public IActionResult Guncelle(Models.OgrenciDers ogrenciders)
        {
            var context = new OKULDbEntities();
            //Burada parametre ile gelen kayıdın id sini bulup OgrenciDers tablomuzdan siliyoruz ve yeni kayıt ekliyoruz.
            var gncll = context.OgrenciDers.FirstOrDefault(x => x.Id == ogrenciders.Id);
            context.OgrenciDers.Remove(gncll);
            //Burada OgrenciDers class'ını referans gösteriyoruz ve altındaki değişkenleri parametre ile gelen kayıda eşitliyoruz. 
            var YeniOgrenciDers = new OgrenciDers();
            YeniOgrenciDers.Id = ogrenciders.Id;
            //Burada listeleme sayfamızda yazdığımız DersDef.Ad ve OgrenciDef.AdSoyad 'ı n değerlerini bulup ekliyoruz. sonra OgrenciDers tablomuza ekleyip kaydediyoruz ve en son index sayfamıza yönlendiriyoruz.
            var IdDegeri = context.Ders.FirstOrDefault(x => x.Ad == ogrenciders.DersDef.Ad);
            YeniOgrenciDers.DersDef = IdDegeri;
            var IdDegeri2 = context.Ogrenci.FirstOrDefault(x => x.AdSoyad == ogrenciders.OgrenciDef.AdSoyad);
            YeniOgrenciDers.OgrenciDef = IdDegeri2;
            context.OgrenciDers.Add(YeniOgrenciDers);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
