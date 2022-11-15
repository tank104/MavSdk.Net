using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Shell;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Shell : IShell
  {
    private readonly ShellService.ShellServiceClient _shellServiceClient;

    internal Shell(GrpcChannel channel)
    {
      _shellServiceClient = new ShellService.ShellServiceClient(channel);
    }

    public IObservable<Unit> Send(string command)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SendRequest();
        request.Command = command;
        var sendResponse = _shellServiceClient.Send(request);
        var shellResult = sendResponse.ShellResult;
        if (shellResult.Result == ShellResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new ShellException(shellResult.Result, shellResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<string> Receive()
    {
      return Observable.Using(() => _shellServiceClient.SubscribeReceive(new SubscribeReceiveRequest()),
      reader => Observable.Create(
        async (IObserver<string> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.Data);
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
  }

  public class ShellException : Exception
  {
    public ShellResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ShellException(ShellResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}