using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.ManualControl;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class ManualControl
  {
    private readonly ManualControlService.ManualControlServiceClient _manualControlServiceClient;

    internal ManualControl(GrpcChannel channel)
    {
      _manualControlServiceClient = new ManualControlService.ManualControlServiceClient(channel);
    }

        public IObservable<Unit> StartPositionControl()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartPositionControlRequest();
            var startPositionControlResponse = _manualControlServiceClient.StartPositionControl(request);
            var manualControlResult = startPositionControlResponse.ManualControlResult;
            if (manualControlResult.Result == ManualControlResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ManualControlException(manualControlResult.Result, manualControlResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> StartAltitudeControl()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new StartAltitudeControlRequest();
            var startAltitudeControlResponse = _manualControlServiceClient.StartAltitudeControl(request);
            var manualControlResult = startAltitudeControlResponse.ManualControlResult;
            if (manualControlResult.Result == ManualControlResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ManualControlException(manualControlResult.Result, manualControlResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetManualControlInput(float x, float y, float z, float r)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetManualControlInputRequest();
            request.X = x;
            request.Y = y;
            request.Z = z;
            request.R = r;
            var setManualControlInputResponse = _manualControlServiceClient.SetManualControlInput(request);
            var manualControlResult = setManualControlInputResponse.ManualControlResult;
            if (manualControlResult.Result == ManualControlResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ManualControlException(manualControlResult.Result, manualControlResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class ManualControlException : Exception
  {
    public ManualControlResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ManualControlException(ManualControlResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}