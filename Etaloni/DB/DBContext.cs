using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etaloni.DB
{
    public class DBEtalonContext : DbContext
    {
        public DbSet<Etalon> Etalons { get; set; }
        public DBEtalonContext() : base("LocalDB") {}
    }
}
