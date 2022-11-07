using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Offboard;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class Offboard
  {
    private readonly OffboardService.OffboardServiceClient _offboardServiceClient;

    internal Offboard(GrpcChannel channel)
    {
      _offboardServiceClient = new OffboardService.OffboardServiceClient(channel);
    }

        public IObservable<Unit> Start()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartRequest();
            var startResponse = _offboardServiceClient.Start(request);
            var offboardResult = startResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> Stop()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StopRequest();
            var stopResponse = _offboardServiceClient.Stop(request);
            var offboardResult = stopResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<bool> IsActive()
        {
          return Observable.Create<bool>(observer =>
          {
            var request = new IsActiveRequest();
            var isActiveResponse = _offboardServiceClient.IsActive(request);
            observer.OnNext(isActiveResponse.IsActive);

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetAttitude(Attitude attitude)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetAttitudeRequest();
            request.Attitude = attitude;
            var setAttitudeResponse = _offboardServiceClient.SetAttitude(request);
            var offboardResult = setAttitudeResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetActuatorControl(ActuatorControl actuatorControl)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetActuatorControlRequest();
            request.ActuatorControl = actuatorControl;
            var setActuatorControlResponse = _offboardServiceClient.SetActuatorControl(request);
            var offboardResult = setActuatorControlResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetAttitudeRate(AttitudeRate attitudeRate)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetAttitudeRateRequest();
            request.AttitudeRate = attitudeRate;
            var setAttitudeRateResponse = _offboardServiceClient.SetAttitudeRate(request);
            var offboardResult = setAttitudeRateResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetPositionNed(PositionNedYaw positionNedYaw)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetPositionNedRequest();
            request.PositionNedYaw = positionNedYaw;
            var setPositionNedResponse = _offboardServiceClient.SetPositionNed(request);
            var offboardResult = setPositionNedResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetPositionGlobal(PositionGlobalYaw positionGlobalYaw)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetPositionGlobalRequest();
            request.PositionGlobalYaw = positionGlobalYaw;
            var setPositionGlobalResponse = _offboardServiceClient.SetPositionGlobal(request);
            var offboardResult = setPositionGlobalResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetVelocityBody(VelocityBodyYawspeed velocityBodyYawspeed)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetVelocityBodyRequest();
            request.VelocityBodyYawspeed = velocityBodyYawspeed;
            var setVelocityBodyResponse = _offboardServiceClient.SetVelocityBody(request);
            var offboardResult = setVelocityBodyResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetVelocityNed(VelocityNedYaw velocityNedYaw)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetVelocityNedRequest();
            request.VelocityNedYaw = velocityNedYaw;
            var setVelocityNedResponse = _offboardServiceClient.SetVelocityNed(request);
            var offboardResult = setVelocityNedResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetPositionVelocityNed(PositionNedYaw positionNedYaw, VelocityNedYaw velocityNedYaw)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetPositionVelocityNedRequest();
            request.PositionNedYaw = positionNedYaw;
            request.VelocityNedYaw = velocityNedYaw;
            var setPositionVelocityNedResponse = _offboardServiceClient.SetPositionVelocityNed(request);
            var offboardResult = setPositionVelocityNedResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetAccelerationNed(AccelerationNed accelerationNed)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetAccelerationNedRequest();
            request.AccelerationNed = accelerationNed;
            var setAccelerationNedResponse = _offboardServiceClient.SetAccelerationNed(request);
            var offboardResult = setAccelerationNedResponse.OffboardResult;
            if (offboardResult.Result == OffboardResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new OffboardException(offboardResult.Result, offboardResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class OffboardException : Exception
  {
    public OffboardResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public OffboardException(OffboardResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}