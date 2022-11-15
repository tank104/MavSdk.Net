using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Gimbal;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IGimbal
  {
    IObservable<Unit> SetPitchAndYaw(float pitchDeg, float yawDeg);
    IObservable<Unit> SetPitchRateAndYawRate(float pitchRateDegS, float yawRateDegS);
    IObservable<Unit> SetMode(GimbalMode gimbalMode);
    IObservable<Unit> SetRoiLocation(double latitudeDeg, double longitudeDeg, float altitudeM);
    IObservable<Unit> TakeControl(ControlMode controlMode);
    IObservable<Unit> ReleaseControl();
    IObservable<ControlStatus> Control();
  }
}