using MTAEDA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Domain.Events
{
    public class TurnstileCameraUninstalledEvent : IDomainEvent
    {
        public string? DomainData { get; set; }
        public int TurnstileId { get; set; }
        public DateTime UninstallDate { get; set; }
        public int CameraId { get; set; }

        public TurnstileCameraUninstalledEvent(int turnstileId, int cameraId, DateTime uninstallDate)
        {
            TurnstileId = turnstileId;
            CameraId = cameraId;
            UninstallDate = uninstallDate;
        }
    }
}
