﻿using InventoryAppData.Models;

namespace InventoryASP.Models.Employee
{
    public class CheckoutModel
    {
        public Checkout Checkout { get; set; }       
        public bool IsSelected { get; set; }
    }
}