using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Shell;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IShell
  {
    IObservable<Unit> Send(string command);
    IObservable<string> Receive();
  }
}