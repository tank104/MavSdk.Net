using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.ComponentInformationServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class ComponentInformationServer : IComponentInformationServer
  {
    private readonly ComponentInformationServerService.ComponentInformationServerServiceClient _componentInformationServerServiceClient;

    internal ComponentInformationServer(GrpcChannel channel)
    {
      _componentInformationServerServiceClient = new ComponentInformationServerService.ComponentInformationServerServiceClient(channel);
    }

    public IObservable<Unit> ProvideFloatParam(FloatParam param)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new ProvideFloatParamRequest();
        request.Param = param;
        var provideFloatParamResponse = _componentInformationServerServiceClient.ProvideFloatParam(request);
        var componentInformationServerResult = provideFloatParamResponse.ComponentInformationServerResult;
        if (componentInformationServerResult.Result == ComponentInformationServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new ComponentInformationServerException(componentInformationServerResult.Result, componentInformationServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<FloatParamUpdate> FloatParam()
    {
      return Observable.Using(() => _componentInformationServerServiceClient.SubscribeFloatParam(new SubscribeFloatParamRequest()),
      reader => Observable.Create(
        async (IObserver<FloatParamUpdate> observer) =>
        {
          try
          {
            while (await reader.ResponseStream.MoveNext(CancellationToken.None))
            {
              observer.OnNext(reader.ResponseStream.Current.ParamUpdate);
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

  public class ComponentInformationServerException : Exception
  {
    public ComponentInformationServerResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ComponentInformationServerException(ComponentInformationServerResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}