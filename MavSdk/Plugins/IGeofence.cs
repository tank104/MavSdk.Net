using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Geofence;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IGeofence
  {
    IObservable<Unit> UploadGeofence(List<Polygon> polygons);
    IObservable<Unit> ClearGeofence();
  }
}