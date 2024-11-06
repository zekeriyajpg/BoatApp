using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLayers.Data.Entities
{
    public class SalesEntity : CoreEntity
    {
        public int BoatId { get; set; }
        public int UserId { get; set; }
        public DateTime SellDate { get; set; }



        public UserEntity User { get; set; }
        public BoatEntity Boat { get; set; }
             
    }

    public class SalesConfiguration : BaseConfiguration<SalesEntity>
    {
        public override void Configure(EntityTypeBuilder<SalesEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
