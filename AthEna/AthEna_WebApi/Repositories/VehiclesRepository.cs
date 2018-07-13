using AthEna_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Repositories
{
    public class VehiclesRepository : InitialRepository
    {

        public dynamic GetVehicle(Guid vehicleId) //if the method is called with a 0 guid parameter...
        {
            try
            {
                if(vehicleId == Guid.Empty) //if the vehicleId is not specified...
                {
                    var vehiclesList = db.Vehicles
                                        .Select(s => new Vehicle
                                        {
                                            LicensePlate = s.LicensePlate,
                                            VehicleId = s.VehicleId
                                        }).ToList();

                    return vehiclesList;
                }
                else //if the vehicleId is specified, return the specific one...
                {
                    var vehicle = db.Vehicles.Where(w => w.VehicleId == vehicleId)
                                             .Select(s => new Vehicle
                                                {
                                                    LicensePlate = s.LicensePlate,
                                                    VehicleId = s.VehicleId
                                                }).FirstOrDefault();
                    return vehicle;
                }     
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic CreateVehicle(Vehicle newVehicle)
        {
            try
            {
                var vehicleToAdd = new Vehicle()
                {
                    VehicleId = new Guid(),
                    LicensePlate = newVehicle.LicensePlate   
                };
                db.Add(vehicleToAdd);
                var savingRes = db.SaveChanges();
                if (savingRes > 0)
                    return vehicleToAdd.VehicleId;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}
