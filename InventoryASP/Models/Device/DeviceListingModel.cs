﻿namespace InventoryASP.Models.Device
{
    public class DeviceListingModel
    {
        public int Id { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceManufacturer { get; set; }
        public string SerialNumber { get; set; }
        public string HolderName { get; set; }
    }
}
