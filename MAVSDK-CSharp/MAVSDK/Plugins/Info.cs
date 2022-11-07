using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Info;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class Info
  {
    private readonly InfoService.InfoServiceClient _infoServiceClient;

    internal Info(Channel channel)
    {
      _infoServiceClient = new InfoService.InfoServiceClient(channel);
    }

        public IObservable<FlightInfo> GetFlightInformation()
        {
          return Observable.Create<FlightInfo>(observer =>
          {
            var request = new GetFlightInformationRequest();
            var getFlightInformationResponse = _infoServiceClient.GetFlightInformation(request);
            var infoResult = getFlightInformationResponse.InfoResult;
            if (infoResult.Result == InfoResult.Types.Result.Success)
            {
              observer.OnNext(getFlightInformationResponse.FlightInfo);
            }
            else
            {
              observer.OnError(new InfoException(infoResult.Result, infoResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Identification> GetIdentification()
        {
          return Observable.Create<Identification>(observer =>
          {
            var request = new GetIdentificationRequest();
            var getIdentificationResponse = _infoServiceClient.GetIdentification(request);
            var infoResult = getIdentificationResponse.InfoResult;
            if (infoResult.Result == InfoResult.Types.Result.Success)
            {
              observer.OnNext(getIdentificationResponse.Identification);
            }
            else
            {
              observer.OnError(new InfoException(infoResult.Result, infoResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Product> GetProduct()
        {
          return Observable.Create<Product>(observer =>
          {
            var request = new GetProductRequest();
            var getProductResponse = _infoServiceClient.GetProduct(request);
            var infoResult = getProductResponse.InfoResult;
            if (infoResult.Result == InfoResult.Types.Result.Success)
            {
              observer.OnNext(getProductResponse.Product);
            }
            else
            {
              observer.OnError(new InfoException(infoResult.Result, infoResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Version> GetVersion()
        {
          return Observable.Create<Version>(observer =>
          {
            var request = new GetVersionRequest();
            var getVersionResponse = _infoServiceClient.GetVersion(request);
            var infoResult = getVersionResponse.InfoResult;
            if (infoResult.Result == InfoResult.Types.Result.Success)
            {
              observer.OnNext(getVersionResponse.Version);
            }
            else
            {
              observer.OnError(new InfoException(infoResult.Result, infoResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<double> GetSpeedFactor()
        {
          return Observable.Create<double>(observer =>
          {
            var request = new GetSpeedFactorRequest();
            var getSpeedFactorResponse = _infoServiceClient.GetSpeedFactor(request);
            var infoResult = getSpeedFactorResponse.InfoResult;
            if (infoResult.Result == InfoResult.Types.Result.Success)
            {
              observer.OnNext(getSpeedFactorResponse.SpeedFactor);
            }
            else
            {
              observer.OnError(new InfoException(infoResult.Result, infoResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class InfoException : Exception
  {
    public InfoResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public InfoException(InfoResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}