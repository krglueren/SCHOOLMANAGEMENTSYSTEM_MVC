using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.EF;

namespace SchoolManagementSystem.Controllers
{
    public class OgrenciController : Controller
    {
        public IActionResult Index(string name)
        {
            //Burada Ogrenci sayfamızın anasayfasında, Database imizdeki Ogrenci tablosunu listeleyeceğimiz kodları yazdık.
            //Önce OKULDbEntities' tanımladık ve context üzerinden Ogrenci tablomuzu listeledik.
            var context = new OKULDbEntities();
            var OgrenciGstr = context.Ogrenci.ToList();

            if(!string.IsNullOrEmpty(name)) 
            {
                 OgrenciGstr = (List<Models.Ogrenci>)OgrenciGstr.Where(m=>m.AdSoyad.Contains(name)).ToList();
                 return View(OgrenciGstr);
            }
            
            return View(OgrenciGstr);
        }

        public IActionResult OgrenciEkle()
        {
            //Burada OgrenciEkle sayfamızı kullanıcıya iletmek için bir metod oluşturduk.
            return View();
        }

        //HttpPost ile metodun post olduğunu belirttik, yani ekleme işlemini arkada güvenli bir şekilde yapmamızı sağladık.
        [HttpPost]
        //Burada metod'umuzun isminin yanında parantez içinde yazdığımız kod bir parametredir. OgrenciEkle sayfamızdan gelen verileri metodumuzda kullanabilmemiz için parametre oluşturuyoruz.
        public IActionResult Ekle(Models.Ogrenci ogrenci)
        {
            //Burada yukarıdaki oluşturduğumuz OgrenciEkle sayfasından gelen form'un içerisinde yazdığımız linkin adı altında bir metod oluşturduk.
            //Bu metodun başına verileri post etmek için de, yani kullanıcının görmediği bir alanda verileri ekleyeceğimiz için [HttpPost] yazdık.
            //Burada if'in içerisine yazdığımız kodlar; "ogrenci parametresi üzerinden gelen değerler boş değil ise"  anlamına geliyor.
            if (ogrenci != null)
            {
                var context = new OKULDbEntities();
                //Burada OKULDbEntities olarak oluşturduğumuz context referansı ile Ogrenci tablomuza, class'ımıza metodumuza gelen parametreyi ekliyoruz ve sonra context.SaveChanges ile yaptığımız 
                //değişikliği database üzerinde de kaydetmiş oluyoruz. en son da index sayfasına bir link gönderiyoruz ki yapılan eklemeyi görebilelim.
                context.Ogrenci.Add(ogrenci);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        //Burada integer bir parametre oluşturuyoruz. Index sayfasında güncelle ve sil kısmında yazdığımız id ile , tıklamış olduğumuz verinin id sini bu metodda getiriyoruz.
        public IActionResult OgrenciSil(int id)
        {
            var context = new OKULDbEntities();
            //Burada Ogrenci tablomuzdaki gelen id nin ilk item'ını yani ilk kayıdını OgrncSil değişkenimize atıyoruz.
            var OgrncSil = context.Ogrenci.FirstOrDefault(x => x.Id == id);
            //Burada atadığımıız değişkeni Ogrenci tablomuzdan siliyoruz ve sonrasında kaydedip, index sayfasına liink atıyoruz.
            context.Ogrenci.Remove(OgrncSil);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Burada integer bir parametre oluşturuyoruz. index sayfamızdaki kayıtların yanında bulunan güncelleye basıldığında o kayıdın id sini bize getirmesi için bir parametredir bu.
        public IActionResult OgrenciGuncelle(int id)
        {
            var context = new OKULDbEntities();
            //Burada Ogrenci tablomuzun içindeki ilk Parametre ile gelen id değerini buluyoruz ve ekrana kullanıcıya gösteriyoruz.
            //buradaki parantez içine yazdığımız id bizim indexden gelen kaydın id sini bize getiren parametredir.
            var updt = context.Ogrenci.FirstOrDefault(x => x.Id == id);
            return View(updt);
        }

        [HttpPost]
        //Burada tekrardan Ogrenci class'ının altında bir ogrenci adında parametre tanımlıyoruz. Yukarıda oluşturduğumuz OgrenciGuncelle metodundan gelen kayıdı bu parametre ile alıyoruz.
        public IActionResult Guncelle(Models.Ogrenci ogrenci)
        {
            var context = new OKULDbEntities();
            //Burada Ogrenci tablosundan, getirdiğimiz parametre kaydının id sini siliyoruz yani o ilgili kayıdı siliyoruz
            //ve metoda getirdiğimiz parametreyi ogrenciler tablosundaki ilgili kayıdın üstüne güncelliyoruz. en sonda kaydedip index sayfasına link veriyoruz.
            var gncll = context.Ogrenci.FirstOrDefault(x => x.Id == ogrenci.Id);
            context.Ogrenci.Remove(gncll);
            context.Ogrenci.Add(ogrenci);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
