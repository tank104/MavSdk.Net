using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Failure;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface IFailure
  {
    IObservable<Unit> Inject(FailureUnit failureUnit, FailureType failureType, int instance);
  }
}