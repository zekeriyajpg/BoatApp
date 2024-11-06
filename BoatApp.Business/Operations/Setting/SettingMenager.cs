using BoatApp.Data.Entities;
using BoatApp.Data.Repositories;
using BoatApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.Operations.Setting
{
    public class SettingMenager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingMenager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        public bool GetMaintenanceState()
        {
            var maintenanceState = _settingRepository.GetById(1).MaintenenceMode;
            return maintenanceState;
        }

        public async Task ToggleMaintenence()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenenceMode = !setting.MaintenenceMode;

            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception) 
            {
                throw new Exception("bakım durumu güncellenirken bir hata oluştu");
            }
        }
    }
}
