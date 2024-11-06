using BoatApp.Business.Operations.Feature.Dtos;
using BoatApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.Operations.Feature
{
    public interface IFeatureService 
    {

        Task<ServiceMessage> AddFeature(AddFeatureDto feature);
    }
}
