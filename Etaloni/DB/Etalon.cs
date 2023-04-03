using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etaloni.DB
{
    public class Etalon
    {
        [Key]
        public int EtalonId { get; set; }
        public string Indication { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Designation { get; set; }
        public string SerialNumber { get; set; }
        public string Proider { get; set; }
        public int Interval { get; set; }
        public DateTime LastCalibration { get; set; }
        public DateTime NextCalibration { get; set; }

        //enable-migrations
        //add-migration Init
        //update-database -verbose
    }
}
