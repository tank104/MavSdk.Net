using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Calibration;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Calibration
  {
    private readonly CalibrationService.CalibrationServiceClient _calibrationServiceClient;

    internal Calibration(GrpcChannel channel)
    {
      _calibrationServiceClient = new CalibrationService.CalibrationServiceClient(channel);
    }

        public IObservable<ProgressData> CalibrateGyro()
        {
          return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateGyro(new SubscribeCalibrateGyroRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.CalibrationResult;
                  switch (result.Result)
                  {
                    case CalibrationResult.Types.Result.Success:
                    //case CalibrationResult.Types.Result.InProgress:
                    //case CalibrationResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.ProgressData);
                    break;
                    default:
                    observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                    break;
                  }
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

        public IObservable<ProgressData> CalibrateAccelerometer()
        {
          return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateAccelerometer(new SubscribeCalibrateAccelerometerRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.CalibrationResult;
                  switch (result.Result)
                  {
                    case CalibrationResult.Types.Result.Success:
                    //case CalibrationResult.Types.Result.InProgress:
                    //case CalibrationResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.ProgressData);
                    break;
                    default:
                    observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                    break;
                  }
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

        public IObservable<ProgressData> CalibrateMagnetometer()
        {
          return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateMagnetometer(new SubscribeCalibrateMagnetometerRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.CalibrationResult;
                  switch (result.Result)
                  {
                    case CalibrationResult.Types.Result.Success:
                    //case CalibrationResult.Types.Result.InProgress:
                    //case CalibrationResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.ProgressData);
                    break;
                    default:
                    observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                    break;
                  }
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

        public IObservable<ProgressData> CalibrateLevelHorizon()
        {
          return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateLevelHorizon(new SubscribeCalibrateLevelHorizonRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.CalibrationResult;
                  switch (result.Result)
                  {
                    case CalibrationResult.Types.Result.Success:
                    //case CalibrationResult.Types.Result.InProgress:
                    //case CalibrationResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.ProgressData);
                    break;
                    default:
                    observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                    break;
                  }
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

        public IObservable<ProgressData> CalibrateGimbalAccelerometer()
        {
          return Observable.Using(() => _calibrationServiceClient.SubscribeCalibrateGimbalAccelerometer(new SubscribeCalibrateGimbalAccelerometerRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.CalibrationResult;
                  switch (result.Result)
                  {
                    case CalibrationResult.Types.Result.Success:
                    //case CalibrationResult.Types.Result.InProgress:
                    //case CalibrationResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.ProgressData);
                    break;
                    default:
                    observer.OnError(new CalibrationException(result.Result, result.ResultStr));
                    break;
                  }
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

        public IObservable<Unit> Cancel()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new CancelRequest();
            var cancelResponse = _calibrationServiceClient.Cancel(request);
            var calibrationResult = cancelResponse.CalibrationResult;
            if (calibrationResult.Result == CalibrationResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new CalibrationException(calibrationResult.Result, calibrationResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class CalibrationException : Exception
  {
    public CalibrationResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public CalibrationException(CalibrationResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}