using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Mocap;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Mocap : IMocap
  {
    private readonly MocapService.MocapServiceClient _mocapServiceClient;

    internal Mocap(GrpcChannel channel)
    {
      _mocapServiceClient = new MocapService.MocapServiceClient(channel);
    }

    public IObservable<Unit> SetVisionPositionEstimate(VisionPositionEstimate visionPositionEstimate)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetVisionPositionEstimateRequest();
        request.VisionPositionEstimate = visionPositionEstimate;
        var setVisionPositionEstimateResponse = _mocapServiceClient.SetVisionPositionEstimate(request);
        var mocapResult = setVisionPositionEstimateResponse.MocapResult;
        if (mocapResult.Result == MocapResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MocapException(mocapResult.Result, mocapResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetAttitudePositionMocap(AttitudePositionMocap attitudePositionMocap)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetAttitudePositionMocapRequest();
        request.AttitudePositionMocap = attitudePositionMocap;
        var setAttitudePositionMocapResponse = _mocapServiceClient.SetAttitudePositionMocap(request);
        var mocapResult = setAttitudePositionMocapResponse.MocapResult;
        if (mocapResult.Result == MocapResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MocapException(mocapResult.Result, mocapResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetOdometry(Odometry odometry)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetOdometryRequest();
        request.Odometry = odometry;
        var setOdometryResponse = _mocapServiceClient.SetOdometry(request);
        var mocapResult = setOdometryResponse.MocapResult;
        if (mocapResult.Result == MocapResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MocapException(mocapResult.Result, mocapResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class MocapException : Exception
  {
    public MocapResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public MocapException(MocapResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}