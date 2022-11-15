using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Param;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IParam
  {
    IObservable<int> GetParamInt(string name);
    IObservable<Unit> SetParamInt(string name, int value);
    IObservable<float> GetParamFloat(string name);
    IObservable<Unit> SetParamFloat(string name, float value);
    IObservable<string> GetParamCustom(string name);
    IObservable<Unit> SetParamCustom(string name, string value);
    IObservable<AllParams> GetAllParams();
  }
}