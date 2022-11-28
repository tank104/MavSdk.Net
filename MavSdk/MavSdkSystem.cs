using System;
using Grpc.Net.Client;
using Mavsdk.Plugins;

namespace Mavsdk
{
  public class MavSdkSystem : IMavSdkSystem
  {
    #region Constructors
    public MavSdkSystem(Uri address)
    {
      _channel = GrpcChannel.ForAddress(address);

      Action = new Plugins.Action(_channel);
      ActionServer = new ActionServer(_channel);
      Calibration = new Calibration(_channel);
      Camera = new Camera(_channel);
      ComponentInformation = new ComponentInformation(_channel);
      ComponentInformationServer = new ComponentInformationServer(_channel);
      Core = new Core(_channel);
      Failure = new Failure(_channel);
      FollowMe = new FollowMe(_channel);
      Ftp = new Ftp(_channel);
      Geofence = new Geofence(_channel);
      Gimbal = new Gimbal(_channel);
      Info = new Info(_channel);
      LogFiles = new LogFiles(_channel);
      ManualControl = new ManualControl(_channel);
      Mission = new Mission(_channel);
      MissionRaw = new MissionRaw(_channel);
      MissionRawServer = new MissionRawServer(_channel);
      Mocap = new Mocap(_channel);
      Offboard = new Offboard(_channel);
      Param = new Param(_channel);
      ParamServer = new ParamServer(_channel);
      Rtk = new Rtk(_channel);
      ServerUtility = new ServerUtility(_channel);
      Shell = new Shell(_channel);
      Telemetry = new Telemetry(_channel);
      TelemetryServer = new TelemetryServer(_channel);
      TrackingServer = new TrackingServer(_channel);
      Transponder = new Transponder(_channel);
      Tune = new Tune(_channel);
    }

    #endregion

    #region Public Properties

    public IAction Action { get; }

    public IActionServer ActionServer { get; }

    public ICalibration Calibration { get; }

    public ICamera Camera { get; }

    public IComponentInformation ComponentInformation { get; }

    public IComponentInformationServer ComponentInformationServer { get; }

    public ICore Core { get; }

    public IFailure Failure { get; }

    public IFollowMe FollowMe { get; }

    public IFtp Ftp { get; }

    public IGeofence Geofence { get; }

    public IGimbal Gimbal { get; }

    public IInfo Info { get; }

    public ILogFiles LogFiles { get; }

    public IManualControl ManualControl { get; }

    public IMission Mission { get; }

    public IMissionRaw MissionRaw { get; }

    public IMissionRawServer MissionRawServer { get; }

    public IMocap Mocap { get; }

    public IOffboard Offboard { get; }

    public IParam Param { get; }

    public IParamServer ParamServer { get; }

    public IRtk Rtk { get; }

    public IServerUtility ServerUtility { get; }

    public IShell Shell { get; }

    public ITelemetry Telemetry { get; }

    public ITelemetryServer TelemetryServer { get; }

    public ITrackingServer TrackingServer { get; }

    public ITransponder Transponder { get; }

    public ITune Tune { get; }

    #endregion

    #region Public Methods

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

    #region Protected Members

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposed && disposing)
      {
        try
        {
          _channel.ShutdownAsync().Wait(60000);
        }
        catch (Exception)
        {
          //ignored
        }
        _disposed = true;
      }
    }

    #endregion

    #region Private Members

    private bool _disposed = false;
    private readonly GrpcChannel _channel;

    #endregion
  }
}