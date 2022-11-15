using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Camera;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface ICamera
  {
    IObservable<Unit> Prepare();
    IObservable<Unit> TakePhoto();
    IObservable<Unit> StartPhotoInterval(float intervalS);
    IObservable<Unit> StopPhotoInterval();
    IObservable<Unit> StartVideo();
    IObservable<Unit> StopVideo();
    IObservable<Unit> StartVideoStreaming();
    IObservable<Unit> StopVideoStreaming();
    IObservable<Unit> SetMode(Mode mode);
    IObservable<List<CaptureInfo>> ListPhotos(PhotosRange photosRange);
    IObservable<Mode> Mode();
    IObservable<Information> Information();
    IObservable<VideoStreamInfo> VideoStreamInfo();
    IObservable<CaptureInfo> CaptureInfo();
    IObservable<Mavsdk.Rpc.Camera.Status> Status();
    IObservable<List<Setting>> CurrentSettings();
    IObservable<List<SettingOptions>> PossibleSettingOptions();
    IObservable<Unit> SetSetting(Setting setting);
    IObservable<Setting> GetSetting(Setting setting);
    IObservable<Unit> FormatStorage();
    IObservable<Unit> SelectCamera(int cameraId);
  }
}