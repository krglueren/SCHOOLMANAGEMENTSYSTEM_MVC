using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class OkulYonetim
    {
        //Burada OkulYonetim adında bir class'ımız var. Buradaki tanımladığımız değişkenler tablolarımızdaki verdiğimiz alan isimleri ile aynı olmalı. Class'ımızın adı da tablomuz ile aynı olmalı.
        [Required]
        public int Id { get; set; }

        [Required]
        public string AdSoyad { get; set; }

        [Required]
        public string Gorevi { get; set; }

        [Required]
        public string YonetimTip { get; set; }


        //Buradaki class'daki bir değişkeni başka bir class'da kullanacağım için, kullanacağım class'daki ismi burada ICollection tipiyle tanımlıyorum. 
        public ICollection<Ders> Derss { get; set; }
    }
}
