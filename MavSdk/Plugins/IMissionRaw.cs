using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.MissionRaw;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IMissionRaw
  {
    IObservable<Unit> UploadMission(List<MissionItem> missionItems);
    IObservable<Unit> UploadGeofence(List<MissionItem> missionItems);
    IObservable<Unit> UploadRallyPoints(List<MissionItem> missionItems);
    IObservable<Unit> CancelMissionUpload();
    IObservable<List<MissionItem>> DownloadMission();
    IObservable<Unit> CancelMissionDownload();
    IObservable<Unit> StartMission();
    IObservable<Unit> PauseMission();
    IObservable<Unit> ClearMission();
    IObservable<Unit> SetCurrentMissionItem(int index);
    IObservable<MissionProgress> MissionProgress();
    IObservable<bool> MissionChanged();
    IObservable<MissionImportData> ImportQgroundcontrolMission(string qgcPlanPath);
  }
}