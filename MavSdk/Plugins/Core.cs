using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Core;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class Core : ICore
  {
    private readonly CoreService.CoreServiceClient _coreServiceClient;

    internal Core(GrpcChannel channel)
    {
      _coreServiceClient = new CoreService.CoreServiceClient(channel);
    }

    public IObservable<ConnectionState> ConnectionState()
    {
      return Observable.Using(() => _coreServiceClient.SubscribeConnectionState(new SubscribeConnectionStateRequest()),
      reader => Observable.Create(
        async (IObserver<ConnectionState> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.ConnectionState);
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

    public IObservable<Unit> SetMavlinkTimeout(double timeoutS)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SetMavlinkTimeoutRequest();
        request.TimeoutS = timeoutS;
        _coreServiceClient.SetMavlinkTimeout(request);
        observer.OnCompleted();

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  
}