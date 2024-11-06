using ProjectLayers.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.Operations.Boat.Dtos
{
    public class AddBoatDto
    {
        
        public string Name { get; set; }
        
        public string Model { get; set; }
        
        public int Price { get; set; }
        
        public BoatTypes BoatTypes { get; set; }

        public List<int> FeatureIds { get; set; }
    }
}
