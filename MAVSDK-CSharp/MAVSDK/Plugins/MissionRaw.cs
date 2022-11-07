using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.MissionRaw;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class MissionRaw
  {
    private readonly MissionRawService.MissionRawServiceClient _missionRawServiceClient;

    internal MissionRaw(Channel channel)
    {
      _missionRawServiceClient = new MissionRawService.MissionRawServiceClient(channel);
    }

        public IObservable<Unit> UploadMission(List<MissionItem> missionItems)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new UploadMissionRequest();
            request.MissionItems.AddRange(missionItems);
            var uploadMissionResponse = _missionRawServiceClient.UploadMission(request);
            var missionRawResult = uploadMissionResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> CancelMissionUpload()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new CancelMissionUploadRequest();
            var cancelMissionUploadResponse = _missionRawServiceClient.CancelMissionUpload(request);
            var missionRawResult = cancelMissionUploadResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<List<MissionItem>> DownloadMission()
        {
          return Observable.Create<List<MissionItem>>(observer =>
          {
            var request = new DownloadMissionRequest();
            var downloadMissionResponse = _missionRawServiceClient.DownloadMission(request);
            var missionRawResult = downloadMissionResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnNext(downloadMissionResponse.MissionItems.ToList());
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> CancelMissionDownload()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new CancelMissionDownloadRequest();
            var cancelMissionDownloadResponse = _missionRawServiceClient.CancelMissionDownload(request);
            var missionRawResult = cancelMissionDownloadResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StartMission()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartMissionRequest();
            var startMissionResponse = _missionRawServiceClient.StartMission(request);
            var missionRawResult = startMissionResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> PauseMission()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PauseMissionRequest();
            var pauseMissionResponse = _missionRawServiceClient.PauseMission(request);
            var missionRawResult = pauseMissionResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> ClearMission()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new ClearMissionRequest();
            var clearMissionResponse = _missionRawServiceClient.ClearMission(request);
            var missionRawResult = clearMissionResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
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
            var setCurrentMissionItemResponse = _missionRawServiceClient.SetCurrentMissionItem(request);
            var missionRawResult = setCurrentMissionItemResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<MissionProgress> MissionProgress()
        {
          return Observable.Using(() => _missionRawServiceClient.SubscribeMissionProgress(new SubscribeMissionProgressRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<MissionProgress> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              observer.OnNext(reader.Current.MissionProgress);
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<bool> MissionChanged()
        {
          return Observable.Using(() => _missionRawServiceClient.SubscribeMissionChanged(new SubscribeMissionChangedRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<bool> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              observer.OnNext(reader.Current.MissionChanged);
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<MissionImportData> ImportQgroundcontrolMission(string qgcPlanPath)
        {
          return Observable.Create<MissionImportData>(observer =>
          {
            var request = new ImportQgroundcontrolMissionRequest();
            request.QgcPlanPath = qgcPlanPath;
            var importQgroundcontrolMissionResponse = _missionRawServiceClient.ImportQgroundcontrolMission(request);
            var missionRawResult = importQgroundcontrolMissionResponse.MissionRawResult;
            if (missionRawResult.Result == MissionRawResult.Types.Result.Success)
            {
              observer.OnNext(importQgroundcontrolMissionResponse.MissionImportData);
            }
            else
            {
              observer.OnError(new MissionRawException(missionRawResult.Result, missionRawResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class MissionRawException : Exception
  {
    public MissionRawResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public MissionRawException(MissionRawResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}