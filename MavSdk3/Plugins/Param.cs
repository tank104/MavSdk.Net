using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Param;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Param
  {
    private readonly ParamService.ParamServiceClient _paramServiceClient;

    internal Param(GrpcChannel channel)
    {
      _paramServiceClient = new ParamService.ParamServiceClient(channel);
    }

        public IObservable<int> GetParamInt(string name)
        {
          return Observable.Create<int>(observer =>
          {
            var request = new GetParamIntRequest();
            request.Name = name;
            var getParamIntResponse = _paramServiceClient.GetParamInt(request);
            var paramResult = getParamIntResponse.ParamResult;
            if (paramResult.Result == ParamResult.Types.Result.Success)
            {
              observer.OnNext(getParamIntResponse.Value);
            }
            else
            {
              observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetParamInt(string name, int value)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetParamIntRequest();
            request.Name = name;
            request.Value = value;
            var setParamIntResponse = _paramServiceClient.SetParamInt(request);
            var paramResult = setParamIntResponse.ParamResult;
            if (paramResult.Result == ParamResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<float> GetParamFloat(string name)
        {
          return Observable.Create<float>(observer =>
          {
            var request = new GetParamFloatRequest();
            request.Name = name;
            var getParamFloatResponse = _paramServiceClient.GetParamFloat(request);
            var paramResult = getParamFloatResponse.ParamResult;
            if (paramResult.Result == ParamResult.Types.Result.Success)
            {
              observer.OnNext(getParamFloatResponse.Value);
            }
            else
            {
              observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetParamFloat(string name, float value)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetParamFloatRequest();
            request.Name = name;
            request.Value = value;
            var setParamFloatResponse = _paramServiceClient.SetParamFloat(request);
            var paramResult = setParamFloatResponse.ParamResult;
            if (paramResult.Result == ParamResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<string> GetParamCustom(string name)
        {
          return Observable.Create<string>(observer =>
          {
            var request = new GetParamCustomRequest();
            request.Name = name;
            var getParamCustomResponse = _paramServiceClient.GetParamCustom(request);
            var paramResult = getParamCustomResponse.ParamResult;
            if (paramResult.Result == ParamResult.Types.Result.Success)
            {
              observer.OnNext(getParamCustomResponse.Value);
            }
            else
            {
              observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetParamCustom(string name, string value)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetParamCustomRequest();
            request.Name = name;
            request.Value = value;
            var setParamCustomResponse = _paramServiceClient.SetParamCustom(request);
            var paramResult = setParamCustomResponse.ParamResult;
            if (paramResult.Result == ParamResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ParamException(paramResult.Result, paramResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<AllParams> GetAllParams()
        {
          return Observable.Create<AllParams>(observer =>
          {
            var request = new GetAllParamsRequest();
            var getAllParamsResponse = _paramServiceClient.GetAllParams(request);
            observer.OnNext(getAllParamsResponse.Params);

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class ParamException : Exception
  {
    public ParamResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ParamException(ParamResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}