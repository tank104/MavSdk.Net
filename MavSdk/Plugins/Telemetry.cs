using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Telemetry;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class Telemetry : ITelemetry
  {
    private readonly TelemetryService.TelemetryServiceClient _telemetryServiceClient;

    internal Telemetry(GrpcChannel channel)
    {
      _telemetryServiceClient = new TelemetryService.TelemetryServiceClient(channel);
    }

    public IObservable<Position> Position()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribePosition(new SubscribePositionRequest()),
      reader => Observable.Create(
        async (IObserver<Position> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Position);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Position> Home()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeHome(new SubscribeHomeRequest()),
      reader => Observable.Create(
        async (IObserver<Position> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Home);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<bool> InAir()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeInAir(new SubscribeInAirRequest()),
      reader => Observable.Create(
        async (IObserver<bool> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.IsInAir);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<LandedState> LandedState()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeLandedState(new SubscribeLandedStateRequest()),
      reader => Observable.Create(
        async (IObserver<LandedState> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.LandedState);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<bool> Armed()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeArmed(new SubscribeArmedRequest()),
      reader => Observable.Create(
        async (IObserver<bool> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.IsArmed);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<VtolState> VtolState()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeVtolState(new SubscribeVtolStateRequest()),
      reader => Observable.Create(
        async (IObserver<VtolState> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.VtolState);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Quaternion> AttitudeQuaternion()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeAttitudeQuaternion(new SubscribeAttitudeQuaternionRequest()),
      reader => Observable.Create(
        async (IObserver<Quaternion> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.AttitudeQuaternion);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<EulerAngle> AttitudeEuler()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeAttitudeEuler(new SubscribeAttitudeEulerRequest()),
      reader => Observable.Create(
        async (IObserver<EulerAngle> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.AttitudeEuler);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<AngularVelocityBody> AttitudeAngularVelocityBody()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeAttitudeAngularVelocityBody(new SubscribeAttitudeAngularVelocityBodyRequest()),
      reader => Observable.Create(
        async (IObserver<AngularVelocityBody> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.AttitudeAngularVelocityBody);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Quaternion> CameraAttitudeQuaternion()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeCameraAttitudeQuaternion(new SubscribeCameraAttitudeQuaternionRequest()),
      reader => Observable.Create(
        async (IObserver<Quaternion> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.AttitudeQuaternion);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<EulerAngle> CameraAttitudeEuler()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeCameraAttitudeEuler(new SubscribeCameraAttitudeEulerRequest()),
      reader => Observable.Create(
        async (IObserver<EulerAngle> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.AttitudeEuler);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<VelocityNed> VelocityNed()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeVelocityNed(new SubscribeVelocityNedRequest()),
      reader => Observable.Create(
        async (IObserver<VelocityNed> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.VelocityNed);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<GpsInfo> GpsInfo()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeGpsInfo(new SubscribeGpsInfoRequest()),
      reader => Observable.Create(
        async (IObserver<GpsInfo> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.GpsInfo);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<RawGps> RawGps()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeRawGps(new SubscribeRawGpsRequest()),
      reader => Observable.Create(
        async (IObserver<RawGps> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.RawGps);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Battery> Battery()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeBattery(new SubscribeBatteryRequest()),
      reader => Observable.Create(
        async (IObserver<Battery> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Battery);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<FlightMode> FlightMode()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeFlightMode(new SubscribeFlightModeRequest()),
      reader => Observable.Create(
        async (IObserver<FlightMode> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.FlightMode);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Health> Health()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeHealth(new SubscribeHealthRequest()),
      reader => Observable.Create(
        async (IObserver<Health> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Health);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<RcStatus> RcStatus()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeRcStatus(new SubscribeRcStatusRequest()),
      reader => Observable.Create(
        async (IObserver<RcStatus> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.RcStatus);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<StatusText> StatusText()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeStatusText(new SubscribeStatusTextRequest()),
      reader => Observable.Create(
        async (IObserver<StatusText> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.StatusText);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<ActuatorControlTarget> ActuatorControlTarget()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeActuatorControlTarget(new SubscribeActuatorControlTargetRequest()),
      reader => Observable.Create(
        async (IObserver<ActuatorControlTarget> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.ActuatorControlTarget);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<ActuatorOutputStatus> ActuatorOutputStatus()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeActuatorOutputStatus(new SubscribeActuatorOutputStatusRequest()),
      reader => Observable.Create(
        async (IObserver<ActuatorOutputStatus> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.ActuatorOutputStatus);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Odometry> Odometry()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeOdometry(new SubscribeOdometryRequest()),
      reader => Observable.Create(
        async (IObserver<Odometry> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Odometry);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<PositionVelocityNed> PositionVelocityNed()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribePositionVelocityNed(new SubscribePositionVelocityNedRequest()),
      reader => Observable.Create(
        async (IObserver<PositionVelocityNed> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.PositionVelocityNed);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<GroundTruth> GroundTruth()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeGroundTruth(new SubscribeGroundTruthRequest()),
      reader => Observable.Create(
        async (IObserver<GroundTruth> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.GroundTruth);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<FixedwingMetrics> FixedwingMetrics()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeFixedwingMetrics(new SubscribeFixedwingMetricsRequest()),
      reader => Observable.Create(
        async (IObserver<FixedwingMetrics> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.FixedwingMetrics);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Imu> Imu()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeImu(new SubscribeImuRequest()),
      reader => Observable.Create(
        async (IObserver<Imu> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Imu);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Imu> ScaledImu()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeScaledImu(new SubscribeScaledImuRequest()),
      reader => Observable.Create(
        async (IObserver<Imu> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Imu);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Imu> RawImu()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeRawImu(new SubscribeRawImuRequest()),
      reader => Observable.Create(
        async (IObserver<Imu> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Imu);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<bool> HealthAllOk()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeHealthAllOk(new SubscribeHealthAllOkRequest()),
      reader => Observable.Create(
        async (IObserver<bool> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.IsHealthAllOk);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<ulong> UnixEpochTime()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeUnixEpochTime(new SubscribeUnixEpochTimeRequest()),
      reader => Observable.Create(
        async (IObserver<ulong> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.TimeUs);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<DistanceSensor> DistanceSensor()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeDistanceSensor(new SubscribeDistanceSensorRequest()),
      reader => Observable.Create(
        async (IObserver<DistanceSensor> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.DistanceSensor);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<ScaledPressure> ScaledPressure()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeScaledPressure(new SubscribeScaledPressureRequest()),
      reader => Observable.Create(
        async (IObserver<ScaledPressure> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.ScaledPressure);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Heading> Heading()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeHeading(new SubscribeHeadingRequest()),
      reader => Observable.Create(
        async (IObserver<Heading> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.HeadingDeg);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Altitude> Altitude()
    {
      return Observable.Using(() => _telemetryServiceClient.SubscribeAltitude(new SubscribeAltitudeRequest()),
      reader => Observable.Create(
        async (IObserver<Altitude> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Altitude);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Unit> SetRatePosition(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRatePositionRequest();
        request.RateHz = rateHz;
        var setRatePositionResponse = _telemetryServiceClient.SetRatePosition(request);
        var telemetryResult = setRatePositionResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateHome(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateHomeRequest();
        request.RateHz = rateHz;
        var setRateHomeResponse = _telemetryServiceClient.SetRateHome(request);
        var telemetryResult = setRateHomeResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateInAir(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateInAirRequest();
        request.RateHz = rateHz;
        var setRateInAirResponse = _telemetryServiceClient.SetRateInAir(request);
        var telemetryResult = setRateInAirResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateLandedState(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateLandedStateRequest();
        request.RateHz = rateHz;
        var setRateLandedStateResponse = _telemetryServiceClient.SetRateLandedState(request);
        var telemetryResult = setRateLandedStateResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateVtolState(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateVtolStateRequest();
        request.RateHz = rateHz;
        var setRateVtolStateResponse = _telemetryServiceClient.SetRateVtolState(request);
        var telemetryResult = setRateVtolStateResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateAttitudeQuaternion(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateAttitudeQuaternionRequest();
        request.RateHz = rateHz;
        var setRateAttitudeQuaternionResponse = _telemetryServiceClient.SetRateAttitudeQuaternion(request);
        var telemetryResult = setRateAttitudeQuaternionResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateAttitudeEuler(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateAttitudeEulerRequest();
        request.RateHz = rateHz;
        var setRateAttitudeEulerResponse = _telemetryServiceClient.SetRateAttitudeEuler(request);
        var telemetryResult = setRateAttitudeEulerResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateCameraAttitude(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateCameraAttitudeRequest();
        request.RateHz = rateHz;
        var setRateCameraAttitudeResponse = _telemetryServiceClient.SetRateCameraAttitude(request);
        var telemetryResult = setRateCameraAttitudeResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateVelocityNed(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateVelocityNedRequest();
        request.RateHz = rateHz;
        var setRateVelocityNedResponse = _telemetryServiceClient.SetRateVelocityNed(request);
        var telemetryResult = setRateVelocityNedResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateGpsInfo(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateGpsInfoRequest();
        request.RateHz = rateHz;
        var setRateGpsInfoResponse = _telemetryServiceClient.SetRateGpsInfo(request);
        var telemetryResult = setRateGpsInfoResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateBattery(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateBatteryRequest();
        request.RateHz = rateHz;
        var setRateBatteryResponse = _telemetryServiceClient.SetRateBattery(request);
        var telemetryResult = setRateBatteryResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateRcStatus(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateRcStatusRequest();
        request.RateHz = rateHz;
        var setRateRcStatusResponse = _telemetryServiceClient.SetRateRcStatus(request);
        var telemetryResult = setRateRcStatusResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateActuatorControlTarget(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateActuatorControlTargetRequest();
        request.RateHz = rateHz;
        var setRateActuatorControlTargetResponse = _telemetryServiceClient.SetRateActuatorControlTarget(request);
        var telemetryResult = setRateActuatorControlTargetResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateActuatorOutputStatus(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateActuatorOutputStatusRequest();
        request.RateHz = rateHz;
        var setRateActuatorOutputStatusResponse = _telemetryServiceClient.SetRateActuatorOutputStatus(request);
        var telemetryResult = setRateActuatorOutputStatusResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateOdometry(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateOdometryRequest();
        request.RateHz = rateHz;
        var setRateOdometryResponse = _telemetryServiceClient.SetRateOdometry(request);
        var telemetryResult = setRateOdometryResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRatePositionVelocityNed(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRatePositionVelocityNedRequest();
        request.RateHz = rateHz;
        var setRatePositionVelocityNedResponse = _telemetryServiceClient.SetRatePositionVelocityNed(request);
        var telemetryResult = setRatePositionVelocityNedResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateGroundTruth(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateGroundTruthRequest();
        request.RateHz = rateHz;
        var setRateGroundTruthResponse = _telemetryServiceClient.SetRateGroundTruth(request);
        var telemetryResult = setRateGroundTruthResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateFixedwingMetrics(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateFixedwingMetricsRequest();
        request.RateHz = rateHz;
        var setRateFixedwingMetricsResponse = _telemetryServiceClient.SetRateFixedwingMetrics(request);
        var telemetryResult = setRateFixedwingMetricsResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateImu(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateImuRequest();
        request.RateHz = rateHz;
        var setRateImuResponse = _telemetryServiceClient.SetRateImu(request);
        var telemetryResult = setRateImuResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateScaledImu(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateScaledImuRequest();
        request.RateHz = rateHz;
        var setRateScaledImuResponse = _telemetryServiceClient.SetRateScaledImu(request);
        var telemetryResult = setRateScaledImuResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateRawImu(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateRawImuRequest();
        request.RateHz = rateHz;
        var setRateRawImuResponse = _telemetryServiceClient.SetRateRawImu(request);
        var telemetryResult = setRateRawImuResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateUnixEpochTime(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateUnixEpochTimeRequest();
        request.RateHz = rateHz;
        var setRateUnixEpochTimeResponse = _telemetryServiceClient.SetRateUnixEpochTime(request);
        var telemetryResult = setRateUnixEpochTimeResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateDistanceSensor(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateDistanceSensorRequest();
        request.RateHz = rateHz;
        var setRateDistanceSensorResponse = _telemetryServiceClient.SetRateDistanceSensor(request);
        var telemetryResult = setRateDistanceSensorResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRateAltitude(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateAltitudeRequest();
        request.RateHz = rateHz;
        var setRateAltitudeResponse = _telemetryServiceClient.SetRateAltitude(request);
        var telemetryResult = setRateAltitudeResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<GpsGlobalOrigin> GetGpsGlobalOrigin()
    {
      return Observable.Create<GpsGlobalOrigin>(observer =>
      {
        var request = new GetGpsGlobalOriginRequest();
        var getGpsGlobalOriginResponse = _telemetryServiceClient.GetGpsGlobalOrigin(request);
        var telemetryResult = getGpsGlobalOriginResponse.TelemetryResult;
        if (telemetryResult.Result == TelemetryResult.Types.Result.Success)
        {
          observer.OnNext(getGpsGlobalOriginResponse.GpsGlobalOrigin);
        }
        else
        {
          observer.OnError(new TelemetryException(telemetryResult.Result, telemetryResult.ResultStr));
        }

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class TelemetryException : Exception
  {
    public TelemetryResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public TelemetryException(TelemetryResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}