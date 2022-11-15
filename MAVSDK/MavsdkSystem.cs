using System;
using Grpc.Net.Client;
using MAVSDK.Plugins;
using Action = MAVSDK.Plugins.Action;

namespace MAVSDK
{
  public class MavsdkSystem : IMavsdkSystem
  {
    #region Constructors

    public MavsdkSystem(Uri address)
    {
      _channel = GrpcChannel.ForAddress(address);

      Action = new Action(_channel);
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

    public Action Action { get; }
    public ActionServer ActionServer { get; }
    public Calibration Calibration { get; }
    public Camera Camera { get; }
    public ComponentInformation ComponentInformation { get; }
    public ComponentInformationServer ComponentInformationServer { get; }
    public Core Core { get; }
    public Failure Failure { get; }
    public FollowMe FollowMe { get; }
    public Ftp Ftp { get; }
    public Geofence Geofence { get; }
    public Gimbal Gimbal { get; }
    public Info Info { get; }
    public LogFiles LogFiles { get; }
    public ManualControl ManualControl { get; }
    public Mission Mission { get; }
    public MissionRaw MissionRaw { get; }
    public MissionRawServer MissionRawServer { get; }
    public Mocap Mocap { get; }
    public Offboard Offboard { get; }
    public Param Param { get; }
    public ParamServer ParamServer { get; }
    public Rtk Rtk { get; }
    public ServerUtility ServerUtility { get; }
    public Shell Shell { get; }
    public Telemetry Telemetry { get; }
    public TelemetryServer TelemetryServer { get; }
    public TrackingServer TrackingServer { get; }
    public Transponder Transponder { get; }
    public Tune Tune { get; }

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