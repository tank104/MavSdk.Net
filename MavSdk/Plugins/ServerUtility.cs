using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.ServerUtility;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class ServerUtility : IServerUtility
  {
    private readonly ServerUtilityService.ServerUtilityServiceClient _serverUtilityServiceClient;

    internal ServerUtility(GrpcChannel channel)
    {
      _serverUtilityServiceClient = new ServerUtilityService.ServerUtilityServiceClient(channel);
    }

    public IObservable<Unit> SendStatusText(StatusTextType type, string text)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new SendStatusTextRequest();
        request.Type = type;
        request.Text = text;
        var sendStatusTextResponse = _serverUtilityServiceClient.SendStatusText(request);
        var serverUtilityResult = sendStatusTextResponse.ServerUtilityResult;
        if (serverUtilityResult.Result == ServerUtilityResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new ServerUtilityException(serverUtilityResult.Result, serverUtilityResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class ServerUtilityException : Exception
  {
    public ServerUtilityResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ServerUtilityException(ServerUtilityResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}