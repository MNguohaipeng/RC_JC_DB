using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using JuCheap.Entity;
namespace JuCheap.Data.Config
{
   public class Cs_dataConfig: EntityTypeConfiguration<Cs_dataEntity>
    {
        public Cs_dataConfig()
        {
            ToTable("Cs_data");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.Orderid).HasColumnType("nvarchar").IsRequired().HasMaxLength(36);
            Property(item => item.Option).HasColumnType("int").IsRequired();
            Property(item => item.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.Note).IsRequired();

        }
    }
}
