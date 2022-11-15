using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.ServerUtility;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IServerUtility
  {
    IObservable<Unit> SendStatusText(StatusTextType type, string text);
  }
}