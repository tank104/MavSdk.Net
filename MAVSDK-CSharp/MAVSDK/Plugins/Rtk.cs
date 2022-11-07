using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mavsdk.Rpc.Rtk;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class Rtk
  {
    private readonly RtkService.RtkServiceClient _rtkServiceClient;

    internal Rtk(Channel channel)
    {
      _rtkServiceClient = new RtkService.RtkServiceClient(channel);
    }

        public IObservable<Unit> SendRtcmData(RtcmData rtcmData)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SendRtcmDataRequest();
            request.RtcmData = rtcmData;
            var sendRtcmDataResponse = _rtkServiceClient.SendRtcmData(request);
            var rtkResult = sendRtcmDataResponse.RtkResult;
            if (rtkResult.Result == RtkResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new RtkException(rtkResult.Result, rtkResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class RtkException : Exception
  {
    public RtkResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public RtkException(RtkResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}