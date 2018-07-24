using Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Store.DataBaseManager
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("ConnectionDefault") { }
        public DbSet<PhoneItem> Phones { get; set; }
    }
}