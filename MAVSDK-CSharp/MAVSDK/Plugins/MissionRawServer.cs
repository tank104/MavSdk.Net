using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.MissionRawServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class MissionRawServer
  {
    private readonly MissionRawServerService.MissionRawServerServiceClient _missionRawServerServiceClient;

    internal MissionRawServer(Channel channel)
    {
      _missionRawServerServiceClient = new MissionRawServerService.MissionRawServerServiceClient(channel);
    }

        public IObservable<MissionPlan> IncomingMission()
        {
          return Observable.Using(() => _missionRawServerServiceClient.SubscribeIncomingMission(new SubscribeIncomingMissionRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<MissionPlan> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.MissionRawServerResult;
              switch (result.Result)
              {
                case MissionRawServerResult.Types.Result.Success:
                //case MissionRawServerResult.Types.Result.InProgress:
                //case MissionRawServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.MissionPlan);
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
            }));
        }

        public IObservable<MissionItem> CurrentItemChanged()
        {
          return Observable.Using(() => _missionRawServerServiceClient.SubscribeCurrentItemChanged(new SubscribeCurrentItemChangedRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<MissionItem> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              observer.OnNext(reader.Current.MissionItem);
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
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
          return Observable.Using(() => _missionRawServerServiceClient.SubscribeClearAll(new SubscribeClearAllRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<uint> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              observer.OnNext(reader.Current.ClearType);
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
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