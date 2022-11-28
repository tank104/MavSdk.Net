using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.CameraServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class CameraServer : ICameraServer
  {
    private readonly CameraServerService.CameraServerServiceClient _cameraServerServiceClient;

    internal CameraServer(GrpcChannel channel)
    {
      _cameraServerServiceClient = new CameraServerService.CameraServerServiceClient(channel);
    }

    public IObservable<Unit> SetInformation(Information information)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetInformationRequest();
        request.Information = information;
        var setInformationResponse = _cameraServerServiceClient.SetInformation(request);
        var cameraServerResult = setInformationResponse.CameraServerResult;
        if (cameraServerResult.Result == CameraServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new CameraServerException(cameraServerResult.Result, cameraServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> SetInProgress(bool inProgress)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetInProgressRequest();
        request.InProgress = inProgress;
        var setInProgressResponse = _cameraServerServiceClient.SetInProgress(request);
        var cameraServerResult = setInProgressResponse.CameraServerResult;
        if (cameraServerResult.Result == CameraServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new CameraServerException(cameraServerResult.Result, cameraServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<int> TakePhoto()
    {
      return Observable.Using(() => _cameraServerServiceClient.SubscribeTakePhoto(new SubscribeTakePhotoRequest()),
      reader => Observable.Create(
        async (IObserver<int> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Index);
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

    public IObservable<Unit> RespondTakePhoto(TakePhotoFeedback takePhotoFeedback, CaptureInfo captureInfo)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new RespondTakePhotoRequest();
        request.TakePhotoFeedback = takePhotoFeedback;
        request.CaptureInfo = captureInfo;
        var respondTakePhotoResponse = _cameraServerServiceClient.RespondTakePhoto(request);
        var cameraServerResult = respondTakePhotoResponse.CameraServerResult;
        if (cameraServerResult.Result == CameraServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new CameraServerException(cameraServerResult.Result, cameraServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class CameraServerException : Exception
  {
    public CameraServerResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public CameraServerException(CameraServerResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}