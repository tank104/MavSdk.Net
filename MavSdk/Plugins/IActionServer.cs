using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.ActionServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IActionServer
  {
    IObservable<ArmDisarm> ArmDisarm();
    IObservable<FlightMode> FlightModeChange();
    IObservable<bool> Takeoff();
    IObservable<bool> Land();
    IObservable<bool> Reboot();
    IObservable<bool> Shutdown();
    IObservable<bool> Terminate();
    IObservable<Unit> SetAllowTakeoff(bool allowTakeoff);
    IObservable<Unit> SetArmable(bool armable, bool forceArmable);
    IObservable<Unit> SetDisarmable(bool disarmable, bool forceDisarmable);
    IObservable<Unit> SetAllowableFlightModes(AllowableFlightModes flightModes);
    IObservable<AllowableFlightModes> GetAllowableFlightModes();
  }
}