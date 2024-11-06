using ProjectLayers.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BoatApp.WebApi.Models
{
    public class AddBoatRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public BoatTypes BoatTypes { get; set; }

        public List<int> FeatureIds { get; set; }
    }
}
