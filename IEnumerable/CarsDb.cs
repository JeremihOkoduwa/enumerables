
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace IEnumerable
{
    public class CarsDb : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}
