using MavSdk.Plugins;
using System;

namespace MavSdk
{
  public interface IMavSdkSystem : IDisposable
  {
    #region Public Properties

    IAction Action { get; }

    IActionServer ActionServer { get; }

    ICalibration Calibration { get; }

    ICamera Camera { get; }

    IComponentInformation ComponentInformation { get; }

    IComponentInformationServer ComponentInformationServer { get; }

    ICore Core { get; }

    IFailure Failure { get; }

    IFollowMe FollowMe { get; }

    IFtp Ftp { get; }

    IGeofence Geofence { get; }

    IGimbal Gimbal { get; }

    IInfo Info { get; }

    ILogFiles LogFiles { get; }

    IManualControl ManualControl { get; }

    IMission Mission { get; }

    IMissionRaw MissionRaw { get; }

    IMissionRawServer MissionRawServer { get; }

    IMocap Mocap { get; }

    IOffboard Offboard { get; }

    IParam Param { get; }

    IParamServer ParamServer { get; }

    IRtk Rtk { get; }

    IServerUtility ServerUtility { get; }

    IShell Shell { get; }

    ITelemetry Telemetry { get; }

    ITelemetryServer TelemetryServer { get; }

    ITrackingServer TrackingServer { get; }

    ITransponder Transponder { get; }

    ITune Tune { get; }

    #endregion
  }
}