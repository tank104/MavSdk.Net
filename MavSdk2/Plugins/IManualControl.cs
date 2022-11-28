using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.ManualControl;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IManualControl
  {
    IObservable<Unit> StartPositionControl();
    IObservable<Unit> StartAltitudeControl();
    IObservable<Unit> SetManualControlInput(float x, float y, float z, float r);
  }
}