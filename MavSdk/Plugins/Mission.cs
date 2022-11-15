using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Mission;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Mission : IMission
  {
    private readonly MissionService.MissionServiceClient _missionServiceClient;

    internal Mission(GrpcChannel channel)
    {
      _missionServiceClient = new MissionService.MissionServiceClient(channel);
    }

    public IObservable<Unit> UploadMission(MissionPlan missionPlan)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new UploadMissionRequest();
        request.MissionPlan = missionPlan;
        var uploadMissionResponse = _missionServiceClient.UploadMission(request);
        var missionResult = uploadMissionResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<ProgressData> UploadMissionWithProgress()
    {
      return Observable.Using(() => _missionServiceClient.SubscribeUploadMissionWithProgress(new SubscribeUploadMissionWithProgressRequest()),
      reader => Observable.Create(
        async (IObserver<ProgressData> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              var result = reader.ResponseStream.Current.MissionResult;
              switch (result.Result)
              {
                case MissionResult.Types.Result.Success:
                //case MissionResult.Types.Result.InProgress:
                //case MissionResult.Types.Result.Instruction:
                observer.OnNext(reader.ResponseStream.Current.ProgressData);
                break;
                default:
                observer.OnError(new MissionException(result.Result, result.ResultStr));
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

    public IObservable<Unit> CancelMissionUpload()
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new CancelMissionUploadRequest();
        var cancelMissionUploadResponse = _missionServiceClient.CancelMissionUpload(request);
        var missionResult = cancelMissionUploadResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<MissionPlan> DownloadMission()
    {
      return Observable.Create<MissionPlan>(observer =>
      {
        var request = new DownloadMissionRequest();
        var downloadMissionResponse = _missionServiceClient.DownloadMission(request);
        var missionResult = downloadMissionResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnNext(downloadMissionResponse.MissionPlan);
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<ProgressDataOrMission> DownloadMissionWithProgress()
    {
      return Observable.Using(() => _missionServiceClient.SubscribeDownloadMissionWithProgress(new SubscribeDownloadMissionWithProgressRequest()),
      reader => Observable.Create(
        async (IObserver<ProgressDataOrMission> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              var result = reader.ResponseStream.Current.MissionResult;
              switch (result.Result)
              {
                case MissionResult.Types.Result.Success:
                //case MissionResult.Types.Result.InProgress:
                //case MissionResult.Types.Result.Instruction:
                observer.OnNext(reader.ResponseStream.Current.ProgressData);
                break;
                default:
                observer.OnError(new MissionException(result.Result, result.ResultStr));
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

    public IObservable<Unit> CancelMissionDownload()
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new CancelMissionDownloadRequest();
        var cancelMissionDownloadResponse = _missionServiceClient.CancelMissionDownload(request);
        var missionResult = cancelMissionDownloadResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> StartMission()
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new StartMissionRequest();
        var startMissionResponse = _missionServiceClient.StartMission(request);
        var missionResult = startMissionResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> PauseMission()
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new PauseMissionRequest();
        var pauseMissionResponse = _missionServiceClient.PauseMission(request);
        var missionResult = pauseMissionResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> ClearMission()
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new ClearMissionRequest();
        var clearMissionResponse = _missionServiceClient.ClearMission(request);
        var missionResult = clearMissionResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetCurrentMissionItem(int index)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetCurrentMissionItemRequest();
        request.Index = index;
        var setCurrentMissionItemResponse = _missionServiceClient.SetCurrentMissionItem(request);
        var missionResult = setCurrentMissionItemResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<bool> IsMissionFinished()
    {
      return Observable.Create<bool>(observer =>
      {
        var request = new IsMissionFinishedRequest();
        var isMissionFinishedResponse = _missionServiceClient.IsMissionFinished(request);
        var missionResult = isMissionFinishedResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnNext(isMissionFinishedResponse.IsFinished);
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<MissionProgress> MissionProgress()
    {
      return Observable.Using(() => _missionServiceClient.SubscribeMissionProgress(new SubscribeMissionProgressRequest()),
      reader => Observable.Create(
        async (IObserver<MissionProgress> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.MissionProgress);
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

    public IObservable<bool> GetReturnToLaunchAfterMission()
    {
      return Observable.Create<bool>(observer =>
      {
        var request = new GetReturnToLaunchAfterMissionRequest();
        var getReturnToLaunchAfterMissionResponse = _missionServiceClient.GetReturnToLaunchAfterMission(request);
        var missionResult = getReturnToLaunchAfterMissionResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnNext(getReturnToLaunchAfterMissionResponse.Enable);
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetReturnToLaunchAfterMission(bool enable)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetReturnToLaunchAfterMissionRequest();
        request.Enable = enable;
        var setReturnToLaunchAfterMissionResponse = _missionServiceClient.SetReturnToLaunchAfterMission(request);
        var missionResult = setReturnToLaunchAfterMissionResponse.MissionResult;
        if (missionResult.Result == MissionResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new MissionException(missionResult.Result, missionResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class MissionException : Exception
  {
    public MissionResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public MissionException(MissionResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}