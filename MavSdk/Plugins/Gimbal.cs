using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Gimbal;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class Gimbal : IGimbal
  {
    private readonly GimbalService.GimbalServiceClient _gimbalServiceClient;

    internal Gimbal(GrpcChannel channel)
    {
      _gimbalServiceClient = new GimbalService.GimbalServiceClient(channel);
    }

    public IObservable<Unit> SetPitchAndYaw(float pitchDeg, float yawDeg)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetPitchAndYawRequest();
        request.PitchDeg = pitchDeg;
        request.YawDeg = yawDeg;
        var setPitchAndYawResponse = _gimbalServiceClient.SetPitchAndYaw(request);
        var gimbalResult = setPitchAndYawResponse.GimbalResult;
        if (gimbalResult.Result == GimbalResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new GimbalException(gimbalResult.Result, gimbalResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetPitchRateAndYawRate(float pitchRateDegS, float yawRateDegS)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetPitchRateAndYawRateRequest();
        request.PitchRateDegS = pitchRateDegS;
        request.YawRateDegS = yawRateDegS;
        var setPitchRateAndYawRateResponse = _gimbalServiceClient.SetPitchRateAndYawRate(request);
        var gimbalResult = setPitchRateAndYawRateResponse.GimbalResult;
        if (gimbalResult.Result == GimbalResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new GimbalException(gimbalResult.Result, gimbalResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetMode(GimbalMode gimbalMode)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetModeRequest();
        request.GimbalMode = gimbalMode;
        var setModeResponse = _gimbalServiceClient.SetMode(request);
        var gimbalResult = setModeResponse.GimbalResult;
        if (gimbalResult.Result == GimbalResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new GimbalException(gimbalResult.Result, gimbalResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetRoiLocation(double latitudeDeg, double longitudeDeg, float altitudeM)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRoiLocationRequest();
        request.LatitudeDeg = latitudeDeg;
        request.LongitudeDeg = longitudeDeg;
        request.AltitudeM = altitudeM;
        var setRoiLocationResponse = _gimbalServiceClient.SetRoiLocation(request);
        var gimbalResult = setRoiLocationResponse.GimbalResult;
        if (gimbalResult.Result == GimbalResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new GimbalException(gimbalResult.Result, gimbalResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> TakeControl(ControlMode controlMode)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new TakeControlRequest();
        request.ControlMode = controlMode;
        var takeControlResponse = _gimbalServiceClient.TakeControl(request);
        var gimbalResult = takeControlResponse.GimbalResult;
        if (gimbalResult.Result == GimbalResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new GimbalException(gimbalResult.Result, gimbalResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> ReleaseControl()
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new ReleaseControlRequest();
        var releaseControlResponse = _gimbalServiceClient.ReleaseControl(request);
        var gimbalResult = releaseControlResponse.GimbalResult;
        if (gimbalResult.Result == GimbalResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new GimbalException(gimbalResult.Result, gimbalResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<ControlStatus> Control()
    {
      return Observable.Using(() => _gimbalServiceClient.SubscribeControl(new SubscribeControlRequest()),
      reader => Observable.Create(
        async (IObserver<ControlStatus> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.ControlStatus);
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
  }

  public class GimbalException : Exception
  {
    public GimbalResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public GimbalException(GimbalResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}