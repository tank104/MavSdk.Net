using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.FollowMe;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class FollowMe
  {
    private readonly FollowMeService.FollowMeServiceClient _followMeServiceClient;

    internal FollowMe(GrpcChannel channel)
    {
      _followMeServiceClient = new FollowMeService.FollowMeServiceClient(channel);
    }

        public IObservable<Config> GetConfig()
        {
          return Observable.Create<Config>(observer =>
          {
            var request = new GetConfigRequest();
            var getConfigResponse = _followMeServiceClient.GetConfig(request);
            observer.OnNext(getConfigResponse.Config);

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetConfig(Config config)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetConfigRequest();
            request.Config = config;
            var setConfigResponse = _followMeServiceClient.SetConfig(request);
            var followMeResult = setConfigResponse.FollowMeResult;
            if (followMeResult.Result == FollowMeResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FollowMeException(followMeResult.Result, followMeResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<bool> IsActive()
        {
          return Observable.Create<bool>(observer =>
          {
            var request = new IsActiveRequest();
            var isActiveResponse = _followMeServiceClient.IsActive(request);
            observer.OnNext(isActiveResponse.IsActive);

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetTargetLocation(TargetLocation location)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetTargetLocationRequest();
            request.Location = location;
            var setTargetLocationResponse = _followMeServiceClient.SetTargetLocation(request);
            var followMeResult = setTargetLocationResponse.FollowMeResult;
            if (followMeResult.Result == FollowMeResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FollowMeException(followMeResult.Result, followMeResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<TargetLocation> GetLastLocation()
        {
          return Observable.Create<TargetLocation>(observer =>
          {
            var request = new GetLastLocationRequest();
            var getLastLocationResponse = _followMeServiceClient.GetLastLocation(request);
            observer.OnNext(getLastLocationResponse.Location);

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> Start()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartRequest();
            var startResponse = _followMeServiceClient.Start(request);
            var followMeResult = startResponse.FollowMeResult;
            if (followMeResult.Result == FollowMeResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FollowMeException(followMeResult.Result, followMeResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> Stop()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StopRequest();
            var stopResponse = _followMeServiceClient.Stop(request);
            var followMeResult = stopResponse.FollowMeResult;
            if (followMeResult.Result == FollowMeResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FollowMeException(followMeResult.Result, followMeResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class FollowMeException : Exception
  {
    public FollowMeResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public FollowMeException(FollowMeResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}