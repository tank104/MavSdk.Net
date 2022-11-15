using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.TelemetryServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface ITelemetryServer
  {
    IObservable<Unit> PublishPosition(Position position, VelocityNed velocityNed, Heading heading);
    IObservable<Unit> PublishHome(Position home);
    IObservable<Unit> PublishSysStatus(Battery battery, bool rcReceiverStatus, bool gyroStatus, bool accelStatus, bool magStatus, bool gpsStatus);
    IObservable<Unit> PublishExtendedSysState(VtolState vtolState, LandedState landedState);
    IObservable<Unit> PublishRawGps(RawGps rawGps, GpsInfo gpsInfo);
    IObservable<Unit> PublishBattery(Battery battery);
    IObservable<Unit> PublishStatusText(StatusText statusText);
    IObservable<Unit> PublishOdometry(Odometry odometry);
    IObservable<Unit> PublishPositionVelocityNed(PositionVelocityNed positionVelocityNed);
    IObservable<Unit> PublishGroundTruth(GroundTruth groundTruth);
    IObservable<Unit> PublishImu(Imu imu);
    IObservable<Unit> PublishScaledImu(Imu imu);
    IObservable<Unit> PublishRawImu(Imu imu);
    IObservable<Unit> PublishUnixEpochTime(ulong timeUs);
  }
}