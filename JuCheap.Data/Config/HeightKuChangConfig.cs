using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Data.Config
{
  public  class HeightKuChangConfig : EntityTypeConfiguration<JuCheap.Entity.HeightKuChangEntity>
    {
        public HeightKuChangConfig()
        {
            ToTable("HeightKuChang");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
 
            //Property(item => item.FrontLength).HasColumnType("varchar").IsRequired();
            //Property(item => item.NetBust).IsRequired();
            //Property(item => item.FinishedBust).IsRequired();
            //Property(item => item.InWaist).IsRequired();
            //Property(item => item.FinishedHem_NoFork).IsRequired();
            //Property(item => item.FinishedHem_SplitEnds).IsRequired();
            //Property(item => item.ShoulderWidth).IsRequired();
            //Property(item => item.Size_Code).IsRequired();
            //Property(item => item.Sleecve_Show).IsRequired();
            //Property(item => item.CreateDateTime).IsRequired();

        }
    }
}
