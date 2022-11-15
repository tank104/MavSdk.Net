using MavSdk.Plugins;
using System;

namespace MavSdk
{
  public interface IMavSdkSystem : IDisposable
  {
    #region Public Properties

    Plugins.Action Action { get; }

    ActionServer ActionServer { get; }

    Calibration Calibration { get; }

    Camera Camera { get; }

    ComponentInformation ComponentInformation { get; }

    ComponentInformationServer ComponentInformationServer { get; }

    Core Core { get; }

    Failure Failure { get; }

    FollowMe FollowMe { get; }

    Ftp Ftp { get; }

    Geofence Geofence { get; }

    Gimbal Gimbal { get; }

    Info Info { get; }

    LogFiles LogFiles { get; }

    ManualControl ManualControl { get; }

    Mission Mission { get; }

    MissionRaw MissionRaw { get; }

    MissionRawServer MissionRawServer { get; }

    Mocap Mocap { get; }

    Offboard Offboard { get; }

    Param Param { get; }

    ParamServer ParamServer { get; }

    Rtk Rtk { get; }

    ServerUtility ServerUtility { get; }

    Shell Shell { get; }

    Telemetry Telemetry { get; }

    TelemetryServer TelemetryServer { get; }

    TrackingServer TrackingServer { get; }

    Transponder Transponder { get; }

    Tune Tune { get; }

    #endregion
  }
}