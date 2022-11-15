using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Tune;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface ITune
  {
    IObservable<Unit> PlayTune(TuneDescription tuneDescription);
  }
}