using System.ComponentModel.DataAnnotations;

namespace BoatApp.WebApi.Models
{
    public class FeatureRequest
    {
        [Required]
        [Length(10 , 30)]
        public string Title { get; set; }
    }
}
