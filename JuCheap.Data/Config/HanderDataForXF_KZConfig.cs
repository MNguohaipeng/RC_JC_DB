using JuCheap.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Data.Config
{
   public class HanderDataForXF_KZConfig : EntityTypeConfiguration<HanderDataForXF_KZEntity>
    {
        public HanderDataForXF_KZConfig()
        {
            ToTable("HanderDataForXF_KZ");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
