using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.ParamServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public class ParamServer : IParamServer
  {
    private readonly ParamServerService.ParamServerServiceClient _paramServerServiceClient;

    internal ParamServer(GrpcChannel channel)
    {
      _paramServerServiceClient = new ParamServerService.ParamServerServiceClient(channel);
    }

    public IObservable<int> RetrieveParamInt(string name)
    {
      return Observable.Create<int>(observer =>
      {
        var request = new RetrieveParamIntRequest();
        request.Name = name;
        var retrieveParamIntResponse = _paramServerServiceClient.RetrieveParamInt(request);
        var paramServerResult = retrieveParamIntResponse.ParamServerResult;
        if (paramServerResult.Result == ParamServerResult.Types.Result.Success)
        {
          observer.OnNext(retrieveParamIntResponse.Value);
        }
        else
        {
          observer.OnError(new ParamServerException(paramServerResult.Result, paramServerResult.ResultStr));
        }

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> ProvideParamInt(string name, int value)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new ProvideParamIntRequest();
        request.Name = name;
        request.Value = value;
        var provideParamIntResponse = _paramServerServiceClient.ProvideParamInt(request);
        var paramServerResult = provideParamIntResponse.ParamServerResult;
        if (paramServerResult.Result == ParamServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new ParamServerException(paramServerResult.Result, paramServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<float> RetrieveParamFloat(string name)
    {
      return Observable.Create<float>(observer =>
      {
        var request = new RetrieveParamFloatRequest();
        request.Name = name;
        var retrieveParamFloatResponse = _paramServerServiceClient.RetrieveParamFloat(request);
        var paramServerResult = retrieveParamFloatResponse.ParamServerResult;
        if (paramServerResult.Result == ParamServerResult.Types.Result.Success)
        {
          observer.OnNext(retrieveParamFloatResponse.Value);
        }
        else
        {
          observer.OnError(new ParamServerException(paramServerResult.Result, paramServerResult.ResultStr));
        }

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> ProvideParamFloat(string name, float value)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new ProvideParamFloatRequest();
        request.Name = name;
        request.Value = value;
        var provideParamFloatResponse = _paramServerServiceClient.ProvideParamFloat(request);
        var paramServerResult = provideParamFloatResponse.ParamServerResult;
        if (paramServerResult.Result == ParamServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new ParamServerException(paramServerResult.Result, paramServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<string> RetrieveParamCustom(string name)
    {
      return Observable.Create<string>(observer =>
      {
        var request = new RetrieveParamCustomRequest();
        request.Name = name;
        var retrieveParamCustomResponse = _paramServerServiceClient.RetrieveParamCustom(request);
        var paramServerResult = retrieveParamCustomResponse.ParamServerResult;
        if (paramServerResult.Result == ParamServerResult.Types.Result.Success)
        {
          observer.OnNext(retrieveParamCustomResponse.Value);
        }
        else
        {
          observer.OnError(new ParamServerException(paramServerResult.Result, paramServerResult.ResultStr));
        }

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<Unit> ProvideParamCustom(string name, string value)
    {
      return Observable.Create<Unit>(observer =>
      {
        var request = new ProvideParamCustomRequest();
        request.Name = name;
        request.Value = value;
        var provideParamCustomResponse = _paramServerServiceClient.ProvideParamCustom(request);
        var paramServerResult = provideParamCustomResponse.ParamServerResult;
        if (paramServerResult.Result == ParamServerResult.Types.Result.Success)
        {
          observer.OnCompleted();
        }
        else
        {
          observer.OnError(new ParamServerException(paramServerResult.Result, paramServerResult.ResultStr));
        }

        return Task.FromResult(Disposable.Empty);
      });
    }

    public IObservable<AllParams> RetrieveAllParams()
    {
      return Observable.Create<AllParams>(observer =>
      {
        var request = new RetrieveAllParamsRequest();
        var retrieveAllParamsResponse = _paramServerServiceClient.RetrieveAllParams(request);
        observer.OnNext(retrieveAllParamsResponse.Params);

        observer.OnCompleted();
        return Task.FromResult(Disposable.Empty);
      });
    }
  }

  public class ParamServerException : Exception
  {
    public ParamServerResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ParamServerException(ParamServerResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}