using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Mocap;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IMocap
  {
    IObservable<Unit> SetVisionPositionEstimate(VisionPositionEstimate visionPositionEstimate);
    IObservable<Unit> SetAttitudePositionMocap(AttitudePositionMocap attitudePositionMocap);
    IObservable<Unit> SetOdometry(Odometry odometry);
  }
}