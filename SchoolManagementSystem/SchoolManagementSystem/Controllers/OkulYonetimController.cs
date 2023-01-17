using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.EF;

namespace SchoolManagementSystem.Controllers
{
    public class OkulYonetimController : Controller
    {
        //Burada OkulYonetim tablomuzdaki kayıtları kullanıcıya göstereceğimiz kodları yazıyoruz.
        public IActionResult Index(string name)
        {
            var context = new OKULDbEntities();
            var OkulYonetimLstl = context.OkulYonetim.ToList();

            if (!string.IsNullOrEmpty(name))
            {
                OkulYonetimLstl = (List<Models.OkulYonetim>)OkulYonetimLstl.Where(m => m.YonetimTip.Contains(name)).ToList();
                return View(OkulYonetimLstl);
            }

            return View(OkulYonetimLstl);
        }

        //Burada Tablomuza yeni ekleyeceğimiz için kullanıcıdan kayıt alacak sayfası kullanıcıyla buluşturuyoruz.
        public IActionResult OkulYonetimEkle()
        {
            return View();
        }

        [HttpPost]
        //Burada OkulYonetimEkle sayfamızdan gelen kayıdı oluşturduğumuz parametre ile metodumuza aktarıyoruz.
        public IActionResult Ekle(Models.OkulYonetim okulyonetim)
        {
            //Eğerki gelen değer boş değil ise parametreyi tablomuza ekliyoruz ve kaydedip index sayfamıza yönlendiriyoruz.
            if (okulyonetim != null)
            {
                var context = new OKULDbEntities();
                context.OkulYonetim.Add(okulyonetim);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        //Burada id isimli parametre oluşturuyoruz. index sayfasından gelen kayıdın id sini bu parametre ile metoda çağırıyoruz.
        public IActionResult OkulYonetimSil(int id)
        {
            //Burada ise parametre ile gelen id yi OkulYonetim tablomuzdan siliyoruzi kaydedip index sayfamıza yönlendiriyoruz.
            var context = new OKULDbEntities();
            var dlt = context.OkulYonetim.FirstOrDefault(x => x.Id == id);
            context.OkulYonetim.Remove(dlt);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Burada index sayfamızdan gelen kayıdın id sini parametre ile metodumuza çağırıyoruz.
        public IActionResult OkulYonetimGuncelle(int id)
        {
            //gelen id yi OkulYonetim tablomuzda bulup kullanıcıya güncelleme yapması için gösteriyoruz.
            var context = new OKULDbEntities();
            var updt = context.OkulYonetim.FirstOrDefault(x => x.Id == id);
            return View(updt);
        }

        [HttpPost]
        //Burada güncelleme sayfasından gelen kayıdı parametre ile meteodumuza çağırıyoruz.
        public IActionResult Guncelle(Models.OkulYonetim calisan)
        {
            var context = new OKULDbEntities();
            //Çağırdığımız parametrenin id sini OkulYonetim tablomuzdan silip tekrardan parametreyi tekrardan tablomuza ekliyoruz, kaydedip index sayfasına yönlendiriyoruz.
            var gncll = context.OkulYonetim.FirstOrDefault(x => x.Id == calisan.Id);
            context.OkulYonetim.Remove(gncll);
            context.OkulYonetim.Add(calisan);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Burada ise arama panelinden gelen değer için bir parametre tanımlıyoruz.
        public IActionResult OkulYonetimYonetimTipi(string p)
        {
            var context = new OKULDbEntities();
            //Burada gelen parametre girdiğimiz hangi değere eşit ise o parametrenin kayıdını kullanıcıyla buluşturuyoruz.
            if (p == "Öğretmen" || p == "öğretmen")
            {
                var ara = context.OkulYonetim.Where(d => d.YonetimTip == "Öğretmen").ToList();
                return View(ara.ToList());
            }
            else if (p == "İdare" || p == "idare")
            {
                var ara = context.OkulYonetim.Where(d => d.YonetimTip == "İdare").ToList();
                return View(ara.ToList());
            }
            else if (p == "Öğrenci İşleri" || p == "öğrenci işleri")
            {
                var ara = context.OkulYonetim.Where(d => d.YonetimTip == "Öğrenci İşleri").ToList();
                return View(ara.ToList());
            }

            return View();

        }
    }
}
