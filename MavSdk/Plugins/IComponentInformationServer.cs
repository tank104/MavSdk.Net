using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.ComponentInformationServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IComponentInformationServer
  {
    IObservable<Unit> ProvideFloatParam(FloatParam param);
    IObservable<FloatParamUpdate> FloatParam();
  }
}