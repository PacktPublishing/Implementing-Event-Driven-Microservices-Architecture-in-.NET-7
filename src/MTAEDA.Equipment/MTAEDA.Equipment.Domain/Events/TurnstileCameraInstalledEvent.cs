using MTAEDA.Core.Interfaces;
using MTAEDA.Equipment.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Domain.Events
{
    public class TurnstileCameraInstalledEvent : IDomainEvent
    {
        public int TurnstileId { get; set; }
        public DateTime InstallDate { get; set; }
        public int CameraId { get; set; }

        public string? DomainData { get; set; }

        public TurnstileCameraInstalledEvent (int turnstileId, int cameraId, DateTime installDate)
        {
            TurnstileId = turnstileId;
            CameraId = cameraId;
            InstallDate = installDate;
        }
    }
}
