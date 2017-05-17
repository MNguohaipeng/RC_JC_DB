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
  public class DictionariesConfig : EntityTypeConfiguration<DictionariesEntity>
    {
        public DictionariesConfig()
        {
            ToTable("Dictionaries");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
  

        }

    }
}
