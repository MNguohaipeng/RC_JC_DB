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
   public class HanderDataForXF_SYConfig : EntityTypeConfiguration<HanderDataForXF_SYEntity>
    {

        public HanderDataForXF_SYConfig()
        {
            ToTable("HanderDataForXF_SY");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        
        }

    }
}
