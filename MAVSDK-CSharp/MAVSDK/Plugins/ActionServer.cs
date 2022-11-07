using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.ActionServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class ActionServer
  {
    private readonly ActionServerService.ActionServerServiceClient _actionServerServiceClient;

    internal ActionServer(Channel channel)
    {
      _actionServerServiceClient = new ActionServerService.ActionServerServiceClient(channel);
    }

        public IObservable<ArmDisarm> ArmDisarm()
        {
          return Observable.Using(() => _actionServerServiceClient.SubscribeArmDisarm(new SubscribeArmDisarmRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<ArmDisarm> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.ActionServerResult;
              switch (result.Result)
              {
                case ActionServerResult.Types.Result.Success:
                //case ActionServerResult.Types.Result.InProgress:
                //case ActionServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.Arm);
                break;
                default:
                observer.OnError(new ActionServerException(result.Result, result.ResultStr));
                break;
              }
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<FlightMode> FlightModeChange()
        {
          return Observable.Using(() => _actionServerServiceClient.SubscribeFlightModeChange(new SubscribeFlightModeChangeRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<FlightMode> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.ActionServerResult;
              switch (result.Result)
              {
                case ActionServerResult.Types.Result.Success:
                //case ActionServerResult.Types.Result.InProgress:
                //case ActionServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.FlightMode);
                break;
                default:
                observer.OnError(new ActionServerException(result.Result, result.ResultStr));
                break;
              }
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<bool> Takeoff()
        {
          return Observable.Using(() => _actionServerServiceClient.SubscribeTakeoff(new SubscribeTakeoffRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<bool> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.ActionServerResult;
              switch (result.Result)
              {
                case ActionServerResult.Types.Result.Success:
                //case ActionServerResult.Types.Result.InProgress:
                //case ActionServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.Takeoff);
                break;
                default:
                observer.OnError(new ActionServerException(result.Result, result.ResultStr));
                break;
              }
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<bool> Land()
        {
          return Observable.Using(() => _actionServerServiceClient.SubscribeLand(new SubscribeLandRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<bool> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.ActionServerResult;
              switch (result.Result)
              {
                case ActionServerResult.Types.Result.Success:
                //case ActionServerResult.Types.Result.InProgress:
                //case ActionServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.Land);
                break;
                default:
                observer.OnError(new ActionServerException(result.Result, result.ResultStr));
                break;
              }
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<bool> Reboot()
        {
          return Observable.Using(() => _actionServerServiceClient.SubscribeReboot(new SubscribeRebootRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<bool> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.ActionServerResult;
              switch (result.Result)
              {
                case ActionServerResult.Types.Result.Success:
                //case ActionServerResult.Types.Result.InProgress:
                //case ActionServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.Reboot);
                break;
                default:
                observer.OnError(new ActionServerException(result.Result, result.ResultStr));
                break;
              }
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<bool> Shutdown()
        {
          return Observable.Using(() => _actionServerServiceClient.SubscribeShutdown(new SubscribeShutdownRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<bool> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.ActionServerResult;
              switch (result.Result)
              {
                case ActionServerResult.Types.Result.Success:
                //case ActionServerResult.Types.Result.InProgress:
                //case ActionServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.Shutdown);
                break;
                default:
                observer.OnError(new ActionServerException(result.Result, result.ResultStr));
                break;
              }
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<bool> Terminate()
        {
          return Observable.Using(() => _actionServerServiceClient.SubscribeTerminate(new SubscribeTerminateRequest()).ResponseStream,
          reader => Observable.Create(
            async (IObserver<bool> observer) =>
            {
            try
            {
              while (await reader.MoveNext())
              {
              var result = reader.Current.ActionServerResult;
              switch (result.Result)
              {
                case ActionServerResult.Types.Result.Success:
                //case ActionServerResult.Types.Result.InProgress:
                //case ActionServerResult.Types.Result.Instruction:
                observer.OnNext(reader.Current.Terminate);
                break;
                default:
                observer.OnError(new ActionServerException(result.Result, result.ResultStr));
                break;
              }
              }
              observer.OnCompleted();
            }
            catch (Exception ex)
            {
              observer.OnError(ex);
            }
            }));
        }

        public IObservable<Unit> SetAllowTakeoff(bool allowTakeoff)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetAllowTakeoffRequest();
            request.AllowTakeoff = allowTakeoff;
            var setAllowTakeoffResponse = _actionServerServiceClient.SetAllowTakeoff(request);
            var actionServerResult = setAllowTakeoffResponse.ActionServerResult;
            if (actionServerResult.Result == ActionServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ActionServerException(actionServerResult.Result, actionServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetArmable(bool armable, bool forceArmable)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetArmableRequest();
            request.Armable = armable;
            request.ForceArmable = forceArmable;
            var setArmableResponse = _actionServerServiceClient.SetArmable(request);
            var actionServerResult = setArmableResponse.ActionServerResult;
            if (actionServerResult.Result == ActionServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ActionServerException(actionServerResult.Result, actionServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetDisarmable(bool disarmable, bool forceDisarmable)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetDisarmableRequest();
            request.Disarmable = disarmable;
            request.ForceDisarmable = forceDisarmable;
            var setDisarmableResponse = _actionServerServiceClient.SetDisarmable(request);
            var actionServerResult = setDisarmableResponse.ActionServerResult;
            if (actionServerResult.Result == ActionServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ActionServerException(actionServerResult.Result, actionServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetAllowableFlightModes(AllowableFlightModes flightModes)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetAllowableFlightModesRequest();
            request.FlightModes = flightModes;
            var setAllowableFlightModesResponse = _actionServerServiceClient.SetAllowableFlightModes(request);
            var actionServerResult = setAllowableFlightModesResponse.ActionServerResult;
            if (actionServerResult.Result == ActionServerResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new ActionServerException(actionServerResult.Result, actionServerResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<AllowableFlightModes> GetAllowableFlightModes()
        {
          return Observable.Create<AllowableFlightModes>(observer =>
          {
            var request = new GetAllowableFlightModesRequest();
            var getAllowableFlightModesResponse = _actionServerServiceClient.GetAllowableFlightModes(request);
            observer.OnNext(getAllowableFlightModesResponse.FlightModes);

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class ActionServerException : Exception
  {
    public ActionServerResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public ActionServerException(ActionServerResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}