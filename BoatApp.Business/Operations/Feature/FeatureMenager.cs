using BoatApp.Business.Operations.Feature.Dtos;
using BoatApp.Business.Types;
using BoatApp.Data.Repositories;
using BoatApp.Data.UnitOfWork;
using ProjectLayers.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.Operations.Feature
{
    public class FeatureMenager : IFeatureService
    {

        private readonly IUnitOfWork _unitofWork;
        private readonly IRepository<FeatureEntity> _repository;

        public FeatureMenager(IUnitOfWork unitOfWork, IRepository<FeatureEntity> repository)
        {
            _repository = repository;
            _unitofWork = unitOfWork;
        }
        public async Task<ServiceMessage> AddFeature(AddFeatureDto feature)
        {
            var hasFeature = _repository.GetAll(x=>x.Title.ToLower()== feature.Title.ToLower()).Any();
            if (hasFeature)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Özellik zaten bulunuyor."
                };
            }

            var featureEntity = new FeatureEntity
            {
                Title = feature.Title
            };
            _repository.Add(featureEntity);

            try
            {
                await _unitofWork.SaveChangesAsync();
                
            }
            catch (Exception ) 
            {
                throw new Exception("Özellik kaydında bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };
        }

    }
}
