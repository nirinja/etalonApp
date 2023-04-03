using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etaloni.DB
{
    public class DBHelper : DropCreateDatabaseAlways<DBEtalonContext>
    {
        public static List<Etalon> ShowEtalon()
        {
            using (var context = new DBEtalonContext())
            {
                return context.Etalons.Select(x => x).OrderBy(x => x.NextCalibration).ToList();
            }
        }

        public static int CreateCal(int id, string indication, string manufacturer, string model, string designation, 
            string serialNumber, int interval,  DateTime lastCalibration, DateTime nextCalibration, string proider)
        {
            using (var context = new DBEtalonContext())
            {
                var etalonToCreate = new Etalon
                {
                    EtalonId = id,
                    Indication = indication,
                    Manufacturer = manufacturer,
                    Model = model,
                    Designation = designation,
                    SerialNumber = serialNumber,
                    Interval = interval,
                    LastCalibration = lastCalibration,
                    NextCalibration = nextCalibration,
                    Proider = proider,                 
                };
                context.Etalons.Add(etalonToCreate);
                context.SaveChanges();
                return etalonToCreate.EtalonId;
            }
        }

        public static void DeleteEtalon(int id)
        {
            using (var context = new DBEtalonContext())
            {
                var etalon = context.Etalons.Find(id);

                if (etalon != null)
                {
                    context.Etalons.Remove(etalon);
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateEtalon(int id, string indication, string manufacturer, string model, string designation,
            string serialNumber, int interval, DateTime lastCalibration, DateTime nextCalibration, string proider)
        {
            using (var context = new DBEtalonContext())
            {
                var etalonOrig = context.Etalons.Find(id);

                if (etalonOrig != null)
                {
                    etalonOrig.Indication = indication;
                    etalonOrig.Manufacturer = manufacturer;
                    etalonOrig.Manufacturer = manufacturer;
                    etalonOrig.Model = model;
                    etalonOrig.Designation = designation;
                    etalonOrig.SerialNumber = serialNumber;
                    etalonOrig.Interval = interval;
                    etalonOrig.LastCalibration = lastCalibration;
                    etalonOrig.NextCalibration = nextCalibration;
                    etalonOrig.Proider = proider;
                    context.SaveChanges();
                }
            }
        }

        #region CRUDHELPER
        //public static int CRUDHELPER(int id)
        //{
        //    using (var context = new TilnoviEtaloniDBContext())
        //    {
        //        //create
        //        var sacaleToCreate = new Scale();
        //        //sacaleToCreate.ScaleAccuracy = scaleAccuracy;
        //        //sacaleToCreate.ScaleName = scaleName;
        //        context.Scale.Add(sacaleToCreate);
        //        context.SaveChanges();

        //        //read
        //        var sacaleToRead = context.Scale.Find(id);
        //        /*sacaleToRead = context.Scale.Where(x => x.ScaleId == id).FirstOrDefault();
        //        sacaleToRead = context.Scale.FirstOrDefault(x => x.ScaleId == id);
        //        sacaleToRead = context.Scale.First(x => x.ScaleId == id);
        //        sacaleToRead = (from x in context.Scale
        //                        where x.ScaleId == id
        //                        select x).FirstOrDefault();*/

        //        //read child 
        //        var sacaleWithChildrenToRead = context.Scale.Include(x => x.Weights).FirstOrDefault(x => x.ScaleId == id);
        //        var sacaleWeightsListToRead = sacaleWithChildrenToRead.Weights;

        //        //update
        //        var sacaleToUpdate = context.Scale.Find(id);
        //        //sacaleToUpdate.ScaleAccuracy = updatedScaleAccuracy;
        //        context.SaveChanges();

        //        //delete
        //        var sacaleToDelete = context.Scale.Find(id);
        //        context.Scale.Remove(sacaleToDelete);
        //        context.SaveChanges();

        //        return 0;
        //    }
        //}
        #endregion
    }
}
