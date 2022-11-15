using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Rtk;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IRtk
  {
    IObservable<Unit> SendRtcmData(RtcmData rtcmData);
  }
}