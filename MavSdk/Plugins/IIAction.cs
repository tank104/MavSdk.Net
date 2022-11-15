using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Action;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IAction
  {
    IObservable<Unit> Arm();
    IObservable<Unit> Disarm();
    IObservable<Unit> Takeoff();
    IObservable<Unit> Land();
    IObservable<Unit> Reboot();
    IObservable<Unit> Shutdown();
    IObservable<Unit> Terminate();
    IObservable<Unit> Kill();
    IObservable<Unit> ReturnToLaunch();
    IObservable<Unit> GotoLocation(double latitudeDeg, double longitudeDeg, float absoluteAltitudeM, float yawDeg);
    IObservable<Unit> DoOrbit(float radiusM, float velocityMs, OrbitYawBehavior yawBehavior, double latitudeDeg, double longitudeDeg, double absoluteAltitudeM);
    IObservable<Unit> Hold();
    IObservable<Unit> SetActuator(int index, float value);
    IObservable<Unit> TransitionToFixedwing();
    IObservable<Unit> TransitionToMulticopter();
    IObservable<float> GetTakeoffAltitude();
    IObservable<Unit> SetTakeoffAltitude(float altitude);
    IObservable<float> GetMaximumSpeed();
    IObservable<Unit> SetMaximumSpeed(float speed);
    IObservable<float> GetReturnToLaunchAltitude();
    IObservable<Unit> SetReturnToLaunchAltitude(float relativeAltitudeM);
    IObservable<Unit> SetCurrentSpeed(float speedMS);
  }
}