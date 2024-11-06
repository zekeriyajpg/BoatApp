using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectLayers.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLayers.Data.Entities
{
    public class BoatEntity : CoreEntity
    {
        
        public String Model { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public BoatTypes BoatType { get; set; }

        public ICollection<BoatFeatureEntity> BoatFeatures { get; set; }

        public ICollection<SalesEntity> Sales { get; set; }
    }
    public class BoatConfiguration : BaseConfiguration<BoatEntity>
    {
        public override void Configure(EntityTypeBuilder<BoatEntity> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(80);
            base.Configure(builder);
        }
    }



}
