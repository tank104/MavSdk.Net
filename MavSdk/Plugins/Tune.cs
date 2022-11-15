using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Tune;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Tune
  {
    private readonly TuneService.TuneServiceClient _tuneServiceClient;

    internal Tune(GrpcChannel channel)
    {
      _tuneServiceClient = new TuneService.TuneServiceClient(channel);
    }

        public IObservable<Unit> PlayTune(TuneDescription tuneDescription)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new PlayTuneRequest();
            request.TuneDescription = tuneDescription;
            var playTuneResponse = _tuneServiceClient.PlayTune(request);
            var tuneResult = playTuneResponse.TuneResult;
            if (tuneResult.Result == TuneResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new TuneException(tuneResult.Result, tuneResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class TuneException : Exception
  {
    public TuneResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public TuneException(TuneResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}