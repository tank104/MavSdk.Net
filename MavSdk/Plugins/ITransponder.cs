using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Transponder;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface ITransponder
  {
    IObservable<AdsbVehicle> ObserveTransponder();
    IObservable<Unit> SetRateTransponder(double rateHz);
  }
}