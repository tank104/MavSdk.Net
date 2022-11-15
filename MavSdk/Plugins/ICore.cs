using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Core;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface ICore
  {
    IObservable<ConnectionState> ConnectionState();
    IObservable<Unit> SetMavlinkTimeout(double timeoutS);
  }
}