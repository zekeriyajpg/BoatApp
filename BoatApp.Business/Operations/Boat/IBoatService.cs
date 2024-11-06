using BoatApp.Business.Operations.Boat.Dtos;
using BoatApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Business.Operations.Boat
{
    public interface IBoatService
    {
        Task<ServiceMessage> AddBoat(AddBoatDto boat);
        Task<BoatDto> GetBoat(int id);
        Task<List<BoatDto>> GetBoats();
        Task<ServiceMessage> AdjustPrices(int id, int changeTo);
        Task<ServiceMessage> DeleteBoat(int id);
        Task<ServiceMessage> UpdateBoat(UpdateBoatDto boat);
    }
}
