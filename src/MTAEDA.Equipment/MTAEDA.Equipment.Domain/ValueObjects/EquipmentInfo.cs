using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Domain.ValueObjects
{
    public class EquipmentInfo
    {
        public string SerialNumber { get; private set; } = "";
        public string Name { get; private set; } = "";
        public string Description { get; private set; } = "";
        public string ModelNumber { get; private set; } = "";
        public string Manufacturer { get; private set; } = "";

        public static EquipmentInfo Create(string serialNumber, string name, string description, string modelNumber, string manufacturer)
        {
            return new EquipmentInfo()
            {
                SerialNumber = serialNumber,
                Name = name,
                Description = description,
                ModelNumber = modelNumber,
                Manufacturer = manufacturer

            };
        }
    }
}
