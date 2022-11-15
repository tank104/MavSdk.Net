using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.ParamServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IParamServer
  {
    IObservable<int> RetrieveParamInt(string name);
    IObservable<Unit> ProvideParamInt(string name, int value);
    IObservable<float> RetrieveParamFloat(string name);
    IObservable<Unit> ProvideParamFloat(string name, float value);
    IObservable<string> RetrieveParamCustom(string name);
    IObservable<Unit> ProvideParamCustom(string name, string value);
    IObservable<AllParams> RetrieveAllParams();
  }
}