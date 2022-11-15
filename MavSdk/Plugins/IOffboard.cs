using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Offboard;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IOffboard
  {
    IObservable<Unit> Start();
    IObservable<Unit> Stop();
    IObservable<bool> IsActive();
    IObservable<Unit> SetAttitude(Attitude attitude);
    IObservable<Unit> SetActuatorControl(ActuatorControl actuatorControl);
    IObservable<Unit> SetAttitudeRate(AttitudeRate attitudeRate);
    IObservable<Unit> SetPositionNed(PositionNedYaw positionNedYaw);
    IObservable<Unit> SetPositionGlobal(PositionGlobalYaw positionGlobalYaw);
    IObservable<Unit> SetVelocityBody(VelocityBodyYawspeed velocityBodyYawspeed);
    IObservable<Unit> SetVelocityNed(VelocityNedYaw velocityNedYaw);
    IObservable<Unit> SetPositionVelocityNed(PositionNedYaw positionNedYaw, VelocityNedYaw velocityNedYaw);
    IObservable<Unit> SetAccelerationNed(AccelerationNed accelerationNed);
  }
}