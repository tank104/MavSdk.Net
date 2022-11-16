using Mavsdk.Rpc.Telemetry;

namespace Mavsdk.Extensions
{
  public static class HealthExtensions
  {
    #region Public Static Methods

    /// <summary>
    /// Checks that all statuses (except Armable) are ok.
    /// </summary>
    /// <param name="health">Health object</param>
    /// <returns>True is all checks ok, else false.</returns>
    public static bool IsOk(this Health health)
    {
      return health.IsGyrometerCalibrationOk && health.IsAccelerometerCalibrationOk && health.IsMagnetometerCalibrationOk &&
        health.IsGlobalPositionOk && health.IsLocalPositionOk;
    }

    #endregion
  }
}
