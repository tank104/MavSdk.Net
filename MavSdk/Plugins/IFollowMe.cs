using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.FollowMe;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IFollowMe
  {
    IObservable<Config> GetConfig();
    IObservable<Unit> SetConfig(Config config);
    IObservable<bool> IsActive();
    IObservable<Unit> SetTargetLocation(TargetLocation location);
    IObservable<TargetLocation> GetLastLocation();
    IObservable<Unit> Start();
    IObservable<Unit> Stop();
  }
}