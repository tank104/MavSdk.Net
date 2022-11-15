using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Camera;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Camera
  {
    private readonly CameraService.CameraServiceClient _cameraServiceClient;

    internal Camera(GrpcChannel channel)
    {
      _cameraServiceClient = new CameraService.CameraServiceClient(channel);
    }

        public IObservable<Unit> Prepare()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PrepareRequest();
            var prepareResponse = _cameraServiceClient.Prepare(request);
            var cameraResult = prepareResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> TakePhoto()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new TakePhotoRequest();
            var takePhotoResponse = _cameraServiceClient.TakePhoto(request);
            var cameraResult = takePhotoResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StartPhotoInterval(float intervalS)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartPhotoIntervalRequest();
            request.IntervalS = intervalS;
            var startPhotoIntervalResponse = _cameraServiceClient.StartPhotoInterval(request);
            var cameraResult = startPhotoIntervalResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StopPhotoInterval()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StopPhotoIntervalRequest();
            var stopPhotoIntervalResponse = _cameraServiceClient.StopPhotoInterval(request);
            var cameraResult = stopPhotoIntervalResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StartVideo()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartVideoRequest();
            var startVideoResponse = _cameraServiceClient.StartVideo(request);
            var cameraResult = startVideoResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StopVideo()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StopVideoRequest();
            var stopVideoResponse = _cameraServiceClient.StopVideo(request);
            var cameraResult = stopVideoResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StartVideoStreaming()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartVideoStreamingRequest();
            var startVideoStreamingResponse = _cameraServiceClient.StartVideoStreaming(request);
            var cameraResult = startVideoStreamingResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StopVideoStreaming()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StopVideoStreamingRequest();
            var stopVideoStreamingResponse = _cameraServiceClient.StopVideoStreaming(request);
            var cameraResult = stopVideoStreamingResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetMode(Mode mode)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetModeRequest();
            request.Mode = mode;
            var setModeResponse = _cameraServiceClient.SetMode(request);
            var cameraResult = setModeResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<List<CaptureInfo>> ListPhotos(PhotosRange photosRange)
        {
          return Observable.Create<List<CaptureInfo>>(observer =>
          {
            var request = new ListPhotosRequest();
            request.PhotosRange = photosRange;
            var listPhotosResponse = _cameraServiceClient.ListPhotos(request);
            var cameraResult = listPhotosResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnNext(listPhotosResponse.CaptureInfos.ToList());
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Mode> Mode()
        {
          return Observable.Using(() => _cameraServiceClient.SubscribeMode(new SubscribeModeRequest()),
          reader => Observable.Create(
            async (IObserver<Mode> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.Mode);
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

        public IObservable<Information> Information()
        {
          return Observable.Using(() => _cameraServiceClient.SubscribeInformation(new SubscribeInformationRequest()),
          reader => Observable.Create(
            async (IObserver<Information> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.Information);
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

        public IObservable<VideoStreamInfo> VideoStreamInfo()
        {
          return Observable.Using(() => _cameraServiceClient.SubscribeVideoStreamInfo(new SubscribeVideoStreamInfoRequest()),
          reader => Observable.Create(
            async (IObserver<VideoStreamInfo> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.VideoStreamInfo);
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

        public IObservable<CaptureInfo> CaptureInfo()
        {
          return Observable.Using(() => _cameraServiceClient.SubscribeCaptureInfo(new SubscribeCaptureInfoRequest()),
          reader => Observable.Create(
            async (IObserver<CaptureInfo> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.CaptureInfo);
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

        public IObservable<Mavsdk.Rpc.Camera.Status> Status()
        {
          return Observable.Using(() => _cameraServiceClient.SubscribeStatus(new SubscribeStatusRequest()),
          reader => Observable.Create(
            async (IObserver<Mavsdk.Rpc.Camera.Status> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.CameraStatus);
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

        public IObservable<List<Setting>> CurrentSettings()
        {
          return Observable.Using(() => _cameraServiceClient.SubscribeCurrentSettings(new SubscribeCurrentSettingsRequest()),
          reader => Observable.Create(
            async (IObserver<List<Setting>> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.CurrentSettings.ToList());
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

        public IObservable<List<SettingOptions>> PossibleSettingOptions()
        {
          return Observable.Using(() => _cameraServiceClient.SubscribePossibleSettingOptions(new SubscribePossibleSettingOptionsRequest()),
          reader => Observable.Create(
            async (IObserver<List<SettingOptions>> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  observer.OnNext(reader.ResponseStream.Current.SettingOptions.ToList());
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

        public IObservable<Unit> SetSetting(Setting setting)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetSettingRequest();
            request.Setting = setting;
            var setSettingResponse = _cameraServiceClient.SetSetting(request);
            var cameraResult = setSettingResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Setting> GetSetting(Setting setting)
        {
          return Observable.Create<Setting>(observer =>
          {
            var request = new GetSettingRequest();
            request.Setting = setting;
            var getSettingResponse = _cameraServiceClient.GetSetting(request);
            var cameraResult = getSettingResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnNext(getSettingResponse.Setting);
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> FormatStorage()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new FormatStorageRequest();
            var formatStorageResponse = _cameraServiceClient.FormatStorage(request);
            var cameraResult = formatStorageResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SelectCamera(int cameraId)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SelectCameraRequest();
            request.CameraId = cameraId;
            var selectCameraResponse = _cameraServiceClient.SelectCamera(request);
            var cameraResult = selectCameraResponse.CameraResult;
            if (cameraResult.Result == CameraResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CameraException(cameraResult.Result, cameraResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class CameraException : Exception
  {
    public CameraResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public CameraException(CameraResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}