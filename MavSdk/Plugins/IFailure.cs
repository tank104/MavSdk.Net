using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Failure;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IFailure
  {
    IObservable<Unit> Inject(FailureUnit failureUnit, FailureType failureType, int instance);
  }
}