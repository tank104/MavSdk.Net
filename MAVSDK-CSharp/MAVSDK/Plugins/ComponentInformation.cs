using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.ComponentInformation;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class ComponentInformation
  {
    private readonly ComponentInformationService.ComponentInformationServiceClient _componentInformationServiceClient;

    internal ComponentInformation(GrpcChannel channel)
    {
      _componentInformationServiceClient = new ComponentInformationService.ComponentInformationServiceClient(channel);
    }

        public IObservable<List<FloatParam>> AccessFloatParams()
        {
          return Observable.Create<List<FloatParam>>(observer =>
          {
            var request = new AccessFloatParamsRequest();
            var accessFloatParamsResponse = _componentInformationServiceClient.AccessFloatParams(request);
            var componentInformationResult = accessFloatParamsResponse.ComponentInformationResult;
            if (componentInformationResult.Result == ComponentInformationResult.Types.Result.Success)
            {
              observer.OnNext(accessFloatParamsResponse.Params.ToList());
            }
            else
            {
              observer.OnError(new ComponentInformationException(componentInformationResult.Result, componentInformationResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<FloatParamUpdate> FloatParam()
        {
          return Observable.Using(() => _componentInformationServiceClient.SubscribeFloatParam(new SubscribeFloatParamRequest()),
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

  public class ComponentInformationException : Exception
  {
    public ComponentInformationResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ComponentInformationException(ComponentInformationResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}