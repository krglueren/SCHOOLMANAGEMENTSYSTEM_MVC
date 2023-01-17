using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Ogrenci
    {
        //Burada Ogrenci adında bir class'ımız var. Buradaki tanımladığımız değişkenler tablolarımızdaki verdiğimiz alan isimleri ile aynı olmalı. Class'ımızın adı da tablomuz ile aynı olmalı.
        [Required]
        public int Id { get; set; }

        [Required]
        public string AdSoyad { get; set; }

        [Required]
        public DateTime KayitTarih { get; set; }

        [Required]
        [MinLength(11, ErrorMessage = "11 haneliden küçük olamaz!")]
        [MaxLength(11, ErrorMessage = "11 haneliden büyük olamaz!")]
        public string OgrenciNo { get; set; }

        [Required]
        public DateTime DTarih { get; set; }

        [Required]
        public string Bolum { get; set; }


        //Buradaki class'daki bir değişkeni başka bir class'da kullanacağım için, kullanacağım class'daki ismi burada ICollection tipiyle tanımlıyorum. 
        [Required]
        public ICollection<OgrenciDers> OgrenciDersOgrenci { get; set; }
    }
}
