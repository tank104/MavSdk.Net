using System.Runtime.InteropServices;


namespace DronePort.TelemetryLogger;

internal class Program
{
  private const string DllFilePath = @"mavsdk.dll";

  static async Task Main(string[] args)
  {


  }

  [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl)]
  private extern static int test(int number);

  public static int Test(int number)
  {
    return test(number);
  }
}