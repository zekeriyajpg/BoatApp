using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLayers.Data.Entities
{
    public class BoatFeatureEntity : CoreEntity
    {
        
        public int BoatId { get; set; }
        public int FeatureId { get; set; }


        public BoatEntity Boat { get; set; }
        public FeatureEntity Feature { get; set; }
    }

    public class BoatFeatureConfiguration : BaseConfiguration<BoatFeatureEntity>
    {
        public override void Configure(EntityTypeBuilder<BoatFeatureEntity> builder)
        {
            builder.Ignore(x=>x.Id);
            builder.HasKey("BoatId", "FeatureId");
            base.Configure(builder);
        }
    }



}
