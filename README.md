# MavSdk.NET
MavSdk client for C#. https://mavsdk.mavlink.io

(Originally forked from https://github.com/mavlink/MavSdk-CSharp)

Things to still do:
- [x] Add/Update latest proto
- [x] Upgrade to Grpc.Net
- [x] Remove console app, and restructure code to be library only
- [ ] Add Unit Tests

To get started:
1. Download and run latest mavsdk_server_bin.exe (or other OS equivilent) from [MavSdk Releases](https://github.com/mavlink/MavSdk/releases) (this repo currently tested on v1.4.7).
1. Run your Simulator and HITL/SITL (tested with [AirSim](https://github.com/tank104/AirSim) and [PX4](https://microsoft.github.io/AirSim/px4_sitl_wsl2/))
1. Create a new .NET 6 project
1. Install latest package `dotnet add package MavSdk.NET --version x.x.x`
1. Use example code below
```cs
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Mavsdk.Rpc.Mission;

namespace Mavsdk.CSharp.ConsoleClient
{
  class Program
  {
    private const string Host = "localhost";
    private const int Port = 50051;

    static async Task Main()
    {
      /*
       * Print the relative altitude.
       *
       * Note:
       *     - round it to the first decimal
       *     - emit an event only when the value changes
       *     - discard the altitudes lower than 0
       */
      var drone = new MavSdkSystem(new Uri($"http://{Host}:{Port}"));
      var tcs = new TaskCompletionSource<bool>();

      drone.Telemetry.Position()
          .Select(position => Math.Round(position.RelativeAltitudeM, 1))
          .DistinctUntilChanged()
          .Where(altitude => altitude >= 0)
          .Subscribe(Observer.Create<double>(altitude => Console.WriteLine($"altitude: {altitude}"), _ => { }));

      // Print the takeoff altitude.
      drone.Action.GetTakeoffAltitude()
          .Do(altitude => Console.WriteLine($"Takeoff altitude: {altitude}"))
          .Subscribe();

      // Print mission progress + end program when flown to completion
      drone.Mission.MissionProgress()
          .Subscribe(mp =>
          {
            Console.WriteLine($"Mission progress - item #{mp.Current + 1}");
            if ((mp.Current == (mp.Total - 1)) && (mp.Total > 0))
            {
              tcs.SetResult(true);
            }
          });

      // Upload and fly a mission
      var missionPlan = await GetSampleMissionPlan(drone);
      drone.Mission.UploadMission(missionPlan)
          .Concat(drone.Mission.SetReturnToLaunchAfterMission(true))
          .Concat(drone.Action.Arm())
          .Concat(drone.Mission.StartMission())
          .Subscribe(_ => { });

      //wait until the mission finishes (from MissionProgress subscription)
      await tcs.Task;
    }

    private static async Task<MissionPlan> GetSampleMissionPlan(MavSdkSystem drone)
    {
      var missionPlan = new MissionPlan();
      var dronePosition = await drone.Telemetry.Position().FirstAsync();
      var missionItem = new MissionItem();
      missionItem.IsFlyThrough = true;
      missionItem.SpeedMS = 2;
      missionItem.RelativeAltitudeM = 5;
      missionItem.LatitudeDeg = dronePosition.LatitudeDeg;
      missionItem.LongitudeDeg = dronePosition.LongitudeDeg;

      for (int i = 0; i < 3; i++)
      {
        missionItem = missionItem.Clone();
        if (i % 2 == 0)
          missionItem.LatitudeDeg += 0.0001;
        else
          missionItem.LatitudeDeg -= 0.0001;

        missionItem.LongitudeDeg += 0.0001;
        missionPlan.MissionItems.Add(missionItem);
      }

      return missionPlan;
    }
  }
}
```
