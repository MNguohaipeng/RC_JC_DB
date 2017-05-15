
using JuCheap.Entity.Base;

namespace JuCheap.Entity //修改名字空间
{
    public class SleeveEntity : BaseEntity
    {

        public string Code { get; set; }

        public decimal  Length { get; set; }

        public int FK_CoatSize_ID { get; set; }

    }
}