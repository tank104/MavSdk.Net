using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Rtk;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IRtk
  {
    IObservable<Unit> SendRtcmData(RtcmData rtcmData);
  }
}