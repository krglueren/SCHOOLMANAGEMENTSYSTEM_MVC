using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class Ders
    {
        //Burada Ders adında bir class'ımız var. Buradaki tanımladığımız değişkenler tablolarımızdaki verdiğimiz alan isimleri ile aynı olmalı. Class'ımızın adı da tablomuz ile aynı olmalı.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; }

        [Required]
        public string Kredisi { get; set; }

        [Required]
        public int OkulYonetimId { get; set; }

        //Tablolar arası ilişki kuracağumız için başka bir class'ı buraya o class'ın ismiyle tanımlıyoruz. 
        public OkulYonetim OkulYonetimDef { get; set; }

        //Buradaki class'daki bir değişkeni başka bir class'da kullanacağım için, kullanacağım class'daki ismi burada ICollection tipiyle tanımlıyorum. 
        public ICollection<OgrenciDers> OgreciDersDers { get; set; }
    }
}
