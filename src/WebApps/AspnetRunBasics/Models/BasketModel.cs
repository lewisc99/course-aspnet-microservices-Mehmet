﻿using AspnetRunBasics.Models;
using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    public class BasketModel
    {

        public string UserName { get; set; }
        public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
        public decimal TotalPrice { get; set; }
    }
}
