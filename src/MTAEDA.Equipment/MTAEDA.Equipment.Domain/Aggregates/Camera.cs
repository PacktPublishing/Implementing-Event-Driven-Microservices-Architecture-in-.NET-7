using MTAEDA.Core.Interfaces;
using MTAEDA.Equipment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Domain.Aggregates
{
    public class Camera: IDomainAggregateRoot
    {
        public EquipmentInfo Model { get; private set; }
        public int CameraId { get; private set; }
        public int StationId { get; private set; }
        public int? TurnstileId { get; private set; }
    }
}
