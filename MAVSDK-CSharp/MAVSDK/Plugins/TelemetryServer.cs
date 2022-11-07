using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.TelemetryServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class TelemetryServer
  {
    private readonly TelemetryServerService.TelemetryServerServiceClient _telemetryServerServiceClient;

    internal TelemetryServer(GrpcChannel channel)
    {
      _telemetryServerServiceClient = new TelemetryServerService.TelemetryServerServiceClient(channel);
    }

        public IObservable<Unit> PublishPosition(Position position, VelocityNed velocityNed, Heading heading)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishPositionRequest();
            request.Position = position;
            request.VelocityNed = velocityNed;
            request.Heading = heading;
            var publishPositionResponse = _telemetryServerServiceClient.PublishPosition(request);
            var telemetryServerResult = publishPositionResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishHome(Position home)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishHomeRequest();
            request.Home = home;
            var publishHomeResponse = _telemetryServerServiceClient.PublishHome(request);
            var telemetryServerResult = publishHomeResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishSysStatus(Battery battery, bool rcReceiverStatus, bool gyroStatus, bool accelStatus, bool magStatus, bool gpsStatus)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishSysStatusRequest();
            request.Battery = battery;
            request.RcReceiverStatus = rcReceiverStatus;
            request.GyroStatus = gyroStatus;
            request.AccelStatus = accelStatus;
            request.MagStatus = magStatus;
            request.GpsStatus = gpsStatus;
            var publishSysStatusResponse = _telemetryServerServiceClient.PublishSysStatus(request);
            var telemetryServerResult = publishSysStatusResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishExtendedSysState(VtolState vtolState, LandedState landedState)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishExtendedSysStateRequest();
            request.VtolState = vtolState;
            request.LandedState = landedState;
            var publishExtendedSysStateResponse = _telemetryServerServiceClient.PublishExtendedSysState(request);
            var telemetryServerResult = publishExtendedSysStateResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishRawGps(RawGps rawGps, GpsInfo gpsInfo)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishRawGpsRequest();
            request.RawGps = rawGps;
            request.GpsInfo = gpsInfo;
            var publishRawGpsResponse = _telemetryServerServiceClient.PublishRawGps(request);
            var telemetryServerResult = publishRawGpsResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishBattery(Battery battery)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishBatteryRequest();
            request.Battery = battery;
            var publishBatteryResponse = _telemetryServerServiceClient.PublishBattery(request);
            var telemetryServerResult = publishBatteryResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishStatusText(StatusText statusText)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishStatusTextRequest();
            request.StatusText = statusText;
            var publishStatusTextResponse = _telemetryServerServiceClient.PublishStatusText(request);
            var telemetryServerResult = publishStatusTextResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishOdometry(Odometry odometry)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishOdometryRequest();
            request.Odometry = odometry;
            var publishOdometryResponse = _telemetryServerServiceClient.PublishOdometry(request);
            var telemetryServerResult = publishOdometryResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishPositionVelocityNed(PositionVelocityNed positionVelocityNed)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishPositionVelocityNedRequest();
            request.PositionVelocityNed = positionVelocityNed;
            var publishPositionVelocityNedResponse = _telemetryServerServiceClient.PublishPositionVelocityNed(request);
            var telemetryServerResult = publishPositionVelocityNedResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishGroundTruth(GroundTruth groundTruth)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishGroundTruthRequest();
            request.GroundTruth = groundTruth;
            var publishGroundTruthResponse = _telemetryServerServiceClient.PublishGroundTruth(request);
            var telemetryServerResult = publishGroundTruthResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishImu(Imu imu)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishImuRequest();
            request.Imu = imu;
            var publishImuResponse = _telemetryServerServiceClient.PublishImu(request);
            var telemetryServerResult = publishImuResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishScaledImu(Imu imu)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishScaledImuRequest();
            request.Imu = imu;
            var publishScaledImuResponse = _telemetryServerServiceClient.PublishScaledImu(request);
            var telemetryServerResult = publishScaledImuResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishRawImu(Imu imu)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishRawImuRequest();
            request.Imu = imu;
            var publishRawImuResponse = _telemetryServerServiceClient.PublishRawImu(request);
            var telemetryServerResult = publishRawImuResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PublishUnixEpochTime(ulong timeUs)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PublishUnixEpochTimeRequest();
            request.TimeUs = timeUs;
            var publishUnixEpochTimeResponse = _telemetryServerServiceClient.PublishUnixEpochTime(request);
            var telemetryServerResult = publishUnixEpochTimeResponse.TelemetryServerResult;
            if (telemetryServerResult.Result == TelemetryServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TelemetryServerException(telemetryServerResult.Result, telemetryServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class TelemetryServerException : Exception
  {
    public TelemetryServerResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public TelemetryServerException(TelemetryServerResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}