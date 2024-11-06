using ProjectLayers.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Data.Entities
{
    public class SettingEntity : CoreEntity
    {
        public bool MaintenenceMode { get; set; }
    }
}
