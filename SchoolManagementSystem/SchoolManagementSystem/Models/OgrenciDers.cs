using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class OgrenciDers
    {
        //Burada OgrenciDers adında bir class'ımız var. Buradaki tanımladığımız değişkenler tablolarımızdaki verdiğimiz alan isimleri ile aynı olmalı. Class'ımızın adı da tablomuz ile aynı olmalı.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        public int DersId { get; set; }

        [Required]
        public int OgrenciId { get; set; }


        //Tablolar arası ilişki kuracağumız için başka bir class'ı buraya o class'ın ismiyle tanımlıyoruz. 
        public Ogrenci OgrenciDef { get; set; }

        public Ders DersDef { get; set; }
    }
}
