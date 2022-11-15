using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Telemetry;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface ITelemetry
  {
    IObservable<Position> Position();
    IObservable<Position> Home();
    IObservable<bool> InAir();
    IObservable<LandedState> LandedState();
    IObservable<bool> Armed();
    IObservable<VtolState> VtolState();
    IObservable<Quaternion> AttitudeQuaternion();
    IObservable<EulerAngle> AttitudeEuler();
    IObservable<AngularVelocityBody> AttitudeAngularVelocityBody();
    IObservable<Quaternion> CameraAttitudeQuaternion();
    IObservable<EulerAngle> CameraAttitudeEuler();
    IObservable<VelocityNed> VelocityNed();
    IObservable<GpsInfo> GpsInfo();
    IObservable<RawGps> RawGps();
    IObservable<Battery> Battery();
    IObservable<FlightMode> FlightMode();
    IObservable<Health> Health();
    IObservable<RcStatus> RcStatus();
    IObservable<StatusText> StatusText();
    IObservable<ActuatorControlTarget> ActuatorControlTarget();
    IObservable<ActuatorOutputStatus> ActuatorOutputStatus();
    IObservable<Odometry> Odometry();
    IObservable<PositionVelocityNed> PositionVelocityNed();
    IObservable<GroundTruth> GroundTruth();
    IObservable<FixedwingMetrics> FixedwingMetrics();
    IObservable<Imu> Imu();
    IObservable<Imu> ScaledImu();
    IObservable<Imu> RawImu();
    IObservable<bool> HealthAllOk();
    IObservable<ulong> UnixEpochTime();
    IObservable<DistanceSensor> DistanceSensor();
    IObservable<ScaledPressure> ScaledPressure();
    IObservable<Heading> Heading();
    IObservable<Unit> SetRatePosition(double rateHz);
    IObservable<Unit> SetRateHome(double rateHz);
    IObservable<Unit> SetRateInAir(double rateHz);
    IObservable<Unit> SetRateLandedState(double rateHz);
    IObservable<Unit> SetRateVtolState(double rateHz);
    IObservable<Unit> SetRateAttitude(double rateHz);
    IObservable<Unit> SetRateCameraAttitude(double rateHz);
    IObservable<Unit> SetRateVelocityNed(double rateHz);
    IObservable<Unit> SetRateGpsInfo(double rateHz);
    IObservable<Unit> SetRateBattery(double rateHz);
    IObservable<Unit> SetRateRcStatus(double rateHz);
    IObservable<Unit> SetRateActuatorControlTarget(double rateHz);
    IObservable<Unit> SetRateActuatorOutputStatus(double rateHz);
    IObservable<Unit> SetRateOdometry(double rateHz);
    IObservable<Unit> SetRatePositionVelocityNed(double rateHz);
    IObservable<Unit> SetRateGroundTruth(double rateHz);
    IObservable<Unit> SetRateFixedwingMetrics(double rateHz);
    IObservable<Unit> SetRateImu(double rateHz);
    IObservable<Unit> SetRateScaledImu(double rateHz);
    IObservable<Unit> SetRateRawImu(double rateHz);
    IObservable<Unit> SetRateUnixEpochTime(double rateHz);
    IObservable<Unit> SetRateDistanceSensor(double rateHz);
    IObservable<GpsGlobalOrigin> GetGpsGlobalOrigin();
  }
}