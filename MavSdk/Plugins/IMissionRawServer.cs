using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.MissionRawServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IMissionRawServer
  {
    IObservable<MissionPlan> IncomingMission();
    IObservable<MissionItem> CurrentItemChanged();
    IObservable<Unit> SetCurrentItemComplete();
    IObservable<uint> ClearAll();
  }
}