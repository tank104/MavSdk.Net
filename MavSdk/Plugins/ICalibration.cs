using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Calibration;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface ICalibration
  {
    IObservable<ProgressData> CalibrateGyro();
    IObservable<ProgressData> CalibrateAccelerometer();
    IObservable<ProgressData> CalibrateMagnetometer();
    IObservable<ProgressData> CalibrateLevelHorizon();
    IObservable<ProgressData> CalibrateGimbalAccelerometer();
    IObservable<Unit> Cancel();
  }
}