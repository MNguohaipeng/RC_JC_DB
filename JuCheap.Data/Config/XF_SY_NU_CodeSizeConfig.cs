using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuCheap.Data.Config
{
    public class XF_SY_NU_CodeSizeConfig : EntityTypeConfiguration<JuCheap.Entity.XF_SY_NU_CodeSizeEntity>
    {

        public XF_SY_NU_CodeSizeConfig()
        {
            ToTable("XF_SY_NAN_ChiMa");
            HasKey(item => item.Id);
            Property(item => item.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

 

        }
    }
}
