using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.MissionRawServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class MissionRawServer
  {
    private readonly MissionRawServerService.MissionRawServerServiceClient _missionRawServerServiceClient;

    internal MissionRawServer(GrpcChannel channel)
    {
      _missionRawServerServiceClient = new MissionRawServerService.MissionRawServerServiceClient(channel);
    }

        public IObservable<MissionPlan> IncomingMission()
        {
          return Observable.Using(() => _missionRawServerServiceClient.SubscribeIncomingMission(new SubscribeIncomingMissionRequest()),
          reader => Observable.Create(
            async (IObserver<MissionPlan> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.MissionRawServerResult;
                  switch (result.Result)
                  {
                    case MissionRawServerResult.Types.Result.Success:
                    //case MissionRawServerResult.Types.Result.InProgress:
                    //case MissionRawServerResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.MissionPlan);
                    break;
                    default:
                    observer.OnError(new MissionRawServerException(result.Result, result.ResultStr));
                    break;
                  }
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

        public IObservable<MissionItem> CurrentItemChanged()
        {
          return Observable.Using(() => _missionRawServerServiceClient.SubscribeCurrentItemChanged(new SubscribeCurrentItemChangedRequest()),
          reader => Observable.Create(
            async (IObserver<MissionItem> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.MissionItem);
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

        public IObservable<Unit> SetCurrentItemComplete()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetCurrentItemCompleteRequest();
            _missionRawServerServiceClient.SetCurrentItemComplete(request);
            observer.OnCompleted();

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<uint> ClearAll()
        {
          return Observable.Using(() => _missionRawServerServiceClient.SubscribeClearAll(new SubscribeClearAllRequest()),
          reader => Observable.Create(
            async (IObserver<uint> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.ClearType);
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

  public class MissionRawServerException : Exception
  {
    public MissionRawServerResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public MissionRawServerException(MissionRawServerResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}