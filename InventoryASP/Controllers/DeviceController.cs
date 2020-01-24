﻿using InventoryAppData;
using InventoryAppData.Models;
using InventoryASP.Models.Device;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryASP.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDevice _deviceService;

        public DeviceController(IDevice deviceService)
        {
            _deviceService = deviceService;
        }

        public IActionResult Index()
        {
            var devices = _deviceService.GetAll();

            var listingResult = devices.Select(device => new DeviceListingModel
            {
                Id = device.Id,
                DeviceType = device.Type,
                DeviceName = device.Name,
                DeviceModel = device.DeviceModel,
                DeviceManufacturer = device.Manufacturer,
                SerialNumber = device.SerialNumber,
                HolderId = GetHolderId(device.Id),
                HolderName = GetHolderFullName(device.Id)
            });

            var model = new DeviceIndexModel
            {
                Devices = listingResult
            };

            return View(model);
        }

        // Форма добавления нового устройства
        [HttpGet]
        public IActionResult NewDevice()
        {
            var model = new NewDeviceModel();            

            return PartialView(model);
        }

        // Добавляем новое устройство
        [HttpPost]
        public async Task<IActionResult> AddNewDevice(NewDeviceModel model)
        {
            var device = BuildNewDevice(model);

            await _deviceService.Add(device);
            return RedirectToAction("Index", "Device");
        }

        // Получаем список свободного оборудования
        public IActionResult GetFreeDeviceList(int employeeId)
        {
            var devices = _deviceService.GetAllFreeDevices();

            var listingResult = devices.Select(device => new DeviceListingModel
            {
                Id = device.Id,
                DeviceType = device.Type,
                DeviceName = device.Name,
                DeviceModel = device.DeviceModel,
                DeviceManufacturer = device.Manufacturer,
                SerialNumber = device.SerialNumber,
                HolderName = GetHolderFullName(device.Id)
            }).ToList();

            var model = new FreeDeviceListModel
            {
                Devices = listingResult,
                EmployeeId = employeeId
            };

            return PartialView(model);
        }

        private int GetHolderId(int id)
        {
            var holder = _deviceService.GetCurrentHolder(id);

            var holderId = holder?.Id ?? 0;

            return holderId;
        }

        // Формируем фамилию и инициалы
        private string GetHolderFullName(int id)
        {
            var holder = _deviceService.GetCurrentHolder(id);
            if (holder == null)
                return "";

            var holderFullName = new StringBuilder()
                .Append(holder.LastName)
                .Append(" ")
                .Append(holder.Name.First())
                .Append(".")
                .Append(holder.Patronymic.First())
                .Append(".")
                .ToString();

            return holderFullName;
        }
        // Формируем новое устройство
        private Device BuildNewDevice(NewDeviceModel model)
        {
            var device = new Device
            {
                Name = model.Name,
                Type = model.Type,
                SerialNumber = model.SerialNumber,
                Manufacturer = model.Manufacturer,
                DeviceModel = model.DeviceModel,
                Description = model.Description
            };

            return device;
        }
    }
}
