using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Info;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IInfo
  {
    IObservable<FlightInfo> GetFlightInformation();
    IObservable<Identification> GetIdentification();
    IObservable<Product> GetProduct();
    IObservable<Version> GetVersion();
    IObservable<double> GetSpeedFactor();
  }
}