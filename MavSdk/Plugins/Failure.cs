using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Failure;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Failure : IFailure
  {
    private readonly FailureService.FailureServiceClient _failureServiceClient;

    internal Failure(GrpcChannel channel)
    {
      _failureServiceClient = new FailureService.FailureServiceClient(channel);
    }

    public IObservable<Unit> Inject(FailureUnit failureUnit, FailureType failureType, int instance)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new InjectRequest();
        request.FailureUnit = failureUnit;
        request.FailureType = failureType;
        request.Instance = instance;
        var injectResponse = _failureServiceClient.Inject(request);
        var failureResult = injectResponse.FailureResult;
        if (failureResult.Result == FailureResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new FailureException(failureResult.Result, failureResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class FailureException : Exception
  {
    public FailureResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public FailureException(FailureResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}