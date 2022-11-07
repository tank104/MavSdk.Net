using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Shell;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class Shell
  {
    private readonly ShellService.ShellServiceClient _shellServiceClient;

    internal Shell(Channel channel)
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
          return Observable.Using(() => _shellServiceClient.SubscribeReceive(new SubscribeReceiveRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<string> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              observer.OnNext(reader.Current.Data);
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
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