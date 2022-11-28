using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.CameraServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface ICameraServer
  {
    IObservable<Unit> SetInformation(Information information);
    IObservable<Unit> SetInProgress(bool inProgress);
    IObservable<int> TakePhoto();
    IObservable<Unit> RespondTakePhoto(TakePhotoFeedback takePhotoFeedback, CaptureInfo captureInfo);
  }
}