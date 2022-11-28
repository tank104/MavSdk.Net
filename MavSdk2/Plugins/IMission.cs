using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Mission;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IMission
  {
    IObservable<Unit> UploadMission(MissionPlan missionPlan);
    IObservable<ProgressData> UploadMissionWithProgress();
    IObservable<Unit> CancelMissionUpload();
    IObservable<MissionPlan> DownloadMission();
    IObservable<ProgressDataOrMission> DownloadMissionWithProgress();
    IObservable<Unit> CancelMissionDownload();
    IObservable<Unit> StartMission();
    IObservable<Unit> PauseMission();
    IObservable<Unit> ClearMission();
    IObservable<Unit> SetCurrentMissionItem(int index);
    IObservable<bool> IsMissionFinished();
    IObservable<MissionProgress> MissionProgress();
    IObservable<bool> GetReturnToLaunchAfterMission();
    IObservable<Unit> SetReturnToLaunchAfterMission(bool enable);
  }
}