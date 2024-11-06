using Azure.Core;
using BoatApp.Business.Operations.Boat.Dtos;
using BoatApp.Business.Types;
using BoatApp.Data.Repositories;
using BoatApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectLayers.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BoatApp.Business.Operations.Boat
{
    public class BoatMenager : IBoatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<BoatEntity> _boatRepository;
        private readonly IRepository<BoatFeatureEntity> _boatFeatureRepository;

        public BoatMenager(IUnitOfWork unitOfWork, IRepository<BoatEntity> boatRepository, IRepository<BoatFeatureEntity> boatFeatureRepository)
        {
            _unitOfWork = unitOfWork;
            _boatRepository = boatRepository;
            _boatFeatureRepository = boatFeatureRepository;
        }

        public async Task<ServiceMessage> AddBoat(AddBoatDto boat)
        {
            var hasBoat = _boatRepository.GetAll(x => x.Name.ToLower() == boat.Name.ToLower()).Any();


            if (hasBoat)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Gemi Mevcut"
                };
            }

            await _unitOfWork.BeginTransaction();

            var boatEntity = new BoatEntity
            {
                Name = boat.Name,
                Model = boat.Model,
                Price = boat.Price,
                BoatType = boat.BoatTypes,
            };

            _boatRepository.Add(boatEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Gemi kaydı sırasında hata oldu");
            }

            foreach (var featurId in boat.FeatureIds)
            {
                var boatFeature = new BoatFeatureEntity
                {
                    BoatId = boatEntity.Id,
                    FeatureId = featurId,
                };

                _boatFeatureRepository.Add(boatFeature);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Gemi özellikleri eklenirken hata oldu süreç başa sarıldı");
            }

            return new ServiceMessage
            {
                IsSucced = true

            };



        }

        public async Task<ServiceMessage> AdjustPrices(int id, int changeTo)
        {
            var boat = _boatRepository.GetById(id);
            if (boat is null)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Bu id ile eşleşen gemi yok"

                };
            }
            boat.Price = changeTo;
            _boatRepository.Update(boat);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Değer Değiştirilrken Bir Hata oluştu");
            }
            return new ServiceMessage
            {
                IsSucced = true
            };
        }

        public async Task<ServiceMessage> DeleteBoat(int id)
        {
            var boat = _boatRepository.GetById(id);
            if (boat is null )
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "böyle bir gemi yok"
                };
            }

            _boatRepository.Delete(id);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("silme işlemi tamamlanmadı");
            }


            return new ServiceMessage
            {
                IsSucced = true
            };
        }

        public async Task<BoatDto> GetBoat(int id)
            {
                var boat = await _boatRepository.GetAll(x => x.Id == id)
                    .Select(x => new BoatDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Model = x.Model,
                        Price = x.Price,
                        BoatTypes = x.BoatType,
                        Features = x.BoatFeatures.Select(s => new BoatFeatureDto
                        {
                            Id = s.Id,
                            Title = s.Feature.Title

                        }).ToList()



                    }).FirstOrDefaultAsync();
                return boat;




            }

            public async Task<List<BoatDto>> GetBoats()
            {
                var boats = await _boatRepository.GetAll()
                    .Select(x => new BoatDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Model = x.Model,
                        Price = x.Price,
                        BoatTypes = x.BoatType,
                        Features = x.BoatFeatures.Select(s => new BoatFeatureDto
                        {
                            Id = s.Id,
                            Title = s.Feature.Title

                        }).ToList()



                    }).ToListAsync();
                return boats;
            }

        public async Task<ServiceMessage> UpdateBoat(UpdateBoatDto boat)
        {
            var boatEntity = _boatRepository.GetById(boat.Id);
            if (boatEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Gemi bulunamadı"
                };
            }

            await _unitOfWork.BeginTransaction();

            boatEntity.Name = boat.Name;
            boatEntity.Model = boat.Model;
            boatEntity.BoatType = boat.BoatType;

           _boatRepository.Update(boatEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Gemi Bilgileri Güncellenirken bir hata oluştu");
            }

            var boatFeatures = _boatFeatureRepository.GetAll(x=>x.BoatId==x.BoatId).ToList();
            foreach (var boatFeature in boatFeatures)
            {
                _boatFeatureRepository.Delete(boatFeature, false);
            }

            foreach (var featureId in boat.FeatureIds)
            {
                var boatFeature = new BoatFeatureEntity
                {
                    BoatId = boatEntity.Id,
                    FeatureId = featureId
                };
                _boatFeatureRepository.Add(boatFeature);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch 
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Gemi Bilgileri GÜncellenirken Bir Hata Oluştu İşlemler Geri Alındı");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };




        }
    }
}
