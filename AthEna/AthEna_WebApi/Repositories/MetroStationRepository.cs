using AthEna_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Repositories
{
    public class MetroStationRepository : InitialRepository
    {

        public dynamic GetMetroStation(Guid metroStationId) //if the method is called with a 0 guid parameter...
        {
            try
            {
                if (metroStationId == Guid.Empty) //if the vehicleId is not specified...
                {
                    var metroStationList = db.MetroStations
                                        .Select(s => new MetroStation
                                        {
                                            StationId = s.StationId,
                                            StationName = s.StationName,
                                            IsOnLine = s.IsOnLine,
                                            IsAlsoOnLine = s.IsAlsoOnLine
                                        }).ToList();

                    return metroStationList;
                }
                else //if the metroStationId is specified, return the specific one...
                {
                    var metroStation = db.MetroStations.Where(w => w.StationId == metroStationId)
                                             .Select(s => new MetroStation
                                             {
                                                 StationName = s.StationName,
                                                 IsOnLine = s.IsOnLine,
                                                 IsAlsoOnLine = s.IsAlsoOnLine
                                             }).FirstOrDefault();
                    return metroStation;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic CreateMetroStation(MetroStation newMetroStation)
        {
            try
            {
                var metroStationToAdd = new MetroStation()
                {
                    StationId = new Guid(),
                    StationName = newMetroStation.StationName,
                    IsOnLine = newMetroStation.IsOnLine,
                    IsAlsoOnLine = newMetroStation.IsAlsoOnLine
                };
                db.Add(metroStationToAdd);
                var savingRes = db.SaveChanges();
                if (savingRes > 0)
                    return metroStationToAdd.StationId;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
