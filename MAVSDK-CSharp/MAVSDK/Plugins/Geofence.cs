using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Geofence;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class Geofence
  {
    private readonly GeofenceService.GeofenceServiceClient _geofenceServiceClient;

    internal Geofence(Channel channel)
    {
      _geofenceServiceClient = new GeofenceService.GeofenceServiceClient(channel);
    }

        public IObservable<Unit> UploadGeofence(List<Polygon> polygons)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new UploadGeofenceRequest();
            request.Polygons.AddRange(polygons);
            var uploadGeofenceResponse = _geofenceServiceClient.UploadGeofence(request);
            var geofenceResult = uploadGeofenceResponse.GeofenceResult;
            if (geofenceResult.Result == GeofenceResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new GeofenceException(geofenceResult.Result, geofenceResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> ClearGeofence()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new ClearGeofenceRequest();
            var clearGeofenceResponse = _geofenceServiceClient.ClearGeofence(request);
            var geofenceResult = clearGeofenceResponse.GeofenceResult;
            if (geofenceResult.Result == GeofenceResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new GeofenceException(geofenceResult.Result, geofenceResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class GeofenceException : Exception
  {
    public GeofenceResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public GeofenceException(GeofenceResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}