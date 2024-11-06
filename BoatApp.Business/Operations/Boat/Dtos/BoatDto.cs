using ProjectLayers.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.Operations.Boat.Dtos
{
    public class BoatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public BoatTypes BoatTypes { get; set; }

        public List<BoatFeatureDto> Features { get; set; }
    }
}
