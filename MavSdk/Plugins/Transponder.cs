using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Transponder;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class Transponder : ITransponder
  {
    private readonly TransponderService.TransponderServiceClient _transponderServiceClient;

    internal Transponder(GrpcChannel channel)
    {
      _transponderServiceClient = new TransponderService.TransponderServiceClient(channel);
    }

    public IObservable<AdsbVehicle> ObserveTransponder()
    {
      return Observable.Using(() => _transponderServiceClient.SubscribeTransponder(new SubscribeTransponderRequest()),
      reader => Observable.Create(
        async (IObserver<AdsbVehicle> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Transponder);
            }
            observer.OnCompleted();
          }
          catch (Exception ex)
          {
            observer.OnError(ex);
          }
        }
      ));
    }

    public IObservable<Unit> SetRateTransponder(double rateHz)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetRateTransponderRequest();
        request.RateHz = rateHz;
        var setRateTransponderResponse = _transponderServiceClient.SetRateTransponder(request);
        var transponderResult = setRateTransponderResponse.TransponderResult;
        if (transponderResult.Result == TransponderResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new TransponderException(transponderResult.Result, transponderResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class TransponderException : Exception
  {
    public TransponderResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public TransponderException(TransponderResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}