using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.ComponentInformation;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IComponentInformation
  {
    IObservable<List<FloatParam>> AccessFloatParams();
    IObservable<FloatParamUpdate> FloatParam();
  }
}