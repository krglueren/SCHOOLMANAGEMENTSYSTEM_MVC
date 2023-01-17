using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.EF
{
    //Class DbContext çatısı altında olduğu için burada class'dan sonra Dbcontext'i belirtiyoruz.
    public class OKULDbEntities : DbContext
    {
        public OKULDbEntities()
        {

        }
        //Bu alt tarafta yazdığımız kodlar standart sql bağlama kodlarıdır. Üzerinden değişiklikler yapılabilir bu yüzden farklı kodlar da yazılabilir.
        public OKULDbEntities(DbContextOptions<OKULDbEntities> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Initial Catalog=OKUL;Data Source=KRGLUEREN;TrustServerCertificate=True;Persist Security Info=True;User ID=krglueren;Password=1234erenkoroglu");
        }

        //Burada Sql database'imizdeki tablo isimlerini çağırıyoruz.
        public DbSet<Models.OkulYonetim> OkulYonetim { get; set; }

        public DbSet<Models.Ogrenci> Ogrenci { get; set; }

        public DbSet<Models.Ders> Ders { get; set; }

        public DbSet<Models.OgrenciDers> OgrenciDers { get; set; }



        //Burada ilişkili olan tablolar arasındaki bağlantıları kurduğumuz kodlar mevcut.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Ders>()
                .HasOne(e => e.OkulYonetimDef)
                .WithMany(c => c.Derss)
                .HasForeignKey(s => s.OkulYonetimId);

            modelBuilder.Entity<Models.OgrenciDers>()
                .HasOne(e => e.OgrenciDef)
                .WithMany(c => c.OgrenciDersOgrenci)
                .HasForeignKey(s => s.OgrenciId);

            modelBuilder.Entity<Models.OgrenciDers>()
                .HasOne(e => e.DersDef)
                .WithMany(c => c.OgreciDersDers)
                .HasForeignKey(s => s.DersId);

        }
    }
}
