using AthEna_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthEna_WebApi.Repositories
{
    public class RouteRepository : InitialRepository
    {
        public dynamic GetRoute(Guid routeId) //if the method is called with a 0 guid parameter...
        {
            try
            {
                if (routeId == Guid.Empty) //if the vehicleId is not specified...
                {
                    var routeList = db.Routes
                                        .Select(s => new Route
                                        {
                                            RouteId = s.RouteId,
                                            RouteCodeNum = s.RouteCodeNum,
                                            RouteStartPoint = s.RouteStartPoint,
                                            RouteEndPoint = s.RouteEndPoint
                                        }).ToList();

                    return routeList;
                }
                else //if the routeId is specified, return the specific one...
                {
                    var route = db.Routes.Where(w => w.RouteId == routeId)
                                             .Select(s => new Route
                                             {
                                                 RouteId = s.RouteId,
                                                 RouteCodeNum = s.RouteCodeNum,
                                                 RouteStartPoint = s.RouteStartPoint,
                                                 RouteEndPoint = s.RouteEndPoint
                                             }).FirstOrDefault();
                    return route;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public dynamic CreateRoute(Route newRoute)
        {
            try
            {
                var routeToAdd = new Route()
                {
                    RouteId = new Guid(),
                    RouteCodeNum = newRoute.RouteCodeNum,
                    RouteStartPoint = newRoute.RouteStartPoint,
                    RouteEndPoint = newRoute.RouteEndPoint
                };
                db.Add(routeToAdd);
                var savingRes = db.SaveChanges();
                if (savingRes > 0)
                    return routeToAdd.RouteId;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
