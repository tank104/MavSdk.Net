using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.TrackingServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class TrackingServer : ITrackingServer
  {
    private readonly TrackingServerService.TrackingServerServiceClient _trackingServerServiceClient;

    internal TrackingServer(GrpcChannel channel)
    {
      _trackingServerServiceClient = new TrackingServerService.TrackingServerServiceClient(channel);
    }

    public IObservable<Unit> SetTrackingPointStatus(TrackPoint trackedPoint)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetTrackingPointStatusRequest();
        request.TrackedPoint = trackedPoint;
        _trackingServerServiceClient.SetTrackingPointStatus(request);
        observer.OnCompleted();

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetTrackingRectangleStatus(TrackRectangle trackedRectangle)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetTrackingRectangleStatusRequest();
        request.TrackedRectangle = trackedRectangle;
        _trackingServerServiceClient.SetTrackingRectangleStatus(request);
        observer.OnCompleted();

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetTrackingOffStatus()
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetTrackingOffStatusRequest();
        _trackingServerServiceClient.SetTrackingOffStatus(request);
        observer.OnCompleted();

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<TrackPoint> TrackingPointCommand()
    {
      return Observable.Using(() => _trackingServerServiceClient.SubscribeTrackingPointCommand(new SubscribeTrackingPointCommandRequest()),
      reader => Observable.Create(
        async (IObserver<TrackPoint> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.TrackPoint);
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

    public IObservable<TrackRectangle> TrackingRectangleCommand()
    {
      return Observable.Using(() => _trackingServerServiceClient.SubscribeTrackingRectangleCommand(new SubscribeTrackingRectangleCommandRequest()),
      reader => Observable.Create(
        async (IObserver<TrackRectangle> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.TrackRectangle);
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

    public IObservable<int> TrackingOffCommand()
    {
      return Observable.Using(() => _trackingServerServiceClient.SubscribeTrackingOffCommand(new SubscribeTrackingOffCommandRequest()),
      reader => Observable.Create(
        async (IObserver<int> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Dummy);
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

    public IObservable<Unit> RespondTrackingPointCommand(CommandAnswer commandAnswer)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new RespondTrackingPointCommandRequest();
        request.CommandAnswer = commandAnswer;
        var respondTrackingPointCommandResponse = _trackingServerServiceClient.RespondTrackingPointCommand(request);
        var trackingServerResult = respondTrackingPointCommandResponse.TrackingServerResult;
        if (trackingServerResult.Result == TrackingServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TrackingServerException(trackingServerResult.Result, trackingServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> RespondTrackingRectangleCommand(CommandAnswer commandAnswer)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new RespondTrackingRectangleCommandRequest();
        request.CommandAnswer = commandAnswer;
        var respondTrackingRectangleCommandResponse = _trackingServerServiceClient.RespondTrackingRectangleCommand(request);
        var trackingServerResult = respondTrackingRectangleCommandResponse.TrackingServerResult;
        if (trackingServerResult.Result == TrackingServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TrackingServerException(trackingServerResult.Result, trackingServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> RespondTrackingOffCommand(CommandAnswer commandAnswer)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new RespondTrackingOffCommandRequest();
        request.CommandAnswer = commandAnswer;
        var respondTrackingOffCommandResponse = _trackingServerServiceClient.RespondTrackingOffCommand(request);
        var trackingServerResult = respondTrackingOffCommandResponse.TrackingServerResult;
        if (trackingServerResult.Result == TrackingServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TrackingServerException(trackingServerResult.Result, trackingServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class TrackingServerException : Exception
  {
    public TrackingServerResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public TrackingServerException(TrackingServerResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}