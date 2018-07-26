using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class CartLine
    {
        public PhoneItem Phone_cart { get; set; }
        public int Quantity { get; set; } //количество товара одного вида
    }
}