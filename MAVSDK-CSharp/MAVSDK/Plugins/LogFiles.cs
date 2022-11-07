using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.LogFiles;

using Version = Mavsdk.Rpc.Info.Version;

namespace MAVSDK.Plugins
{
  public class LogFiles
  {
    private readonly LogFilesService.LogFilesServiceClient _logFilesServiceClient;

    internal LogFiles(GrpcChannel channel)
    {
      _logFilesServiceClient = new LogFilesService.LogFilesServiceClient(channel);
    }

        public IObservable<List<Entry>> GetEntries()
        {
          return Observable.Create<List<Entry>>(observer =>
          {
            var request = new GetEntriesRequest();
            var getEntriesResponse = _logFilesServiceClient.GetEntries(request);
            var logFilesResult = getEntriesResponse.LogFilesResult;
            if (logFilesResult.Result == LogFilesResult.Types.Result.Success)
            {
              observer.OnNext(getEntriesResponse.Entries.ToList());
            }
            else
            {
              observer.OnError(new LogFilesException(logFilesResult.Result, logFilesResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<ProgressData> DownloadLogFile()
        {
          return Observable.Using(() => _logFilesServiceClient.SubscribeDownloadLogFile(new SubscribeDownloadLogFileRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.LogFilesResult;
                  switch (result.Result)
                  {
                    case LogFilesResult.Types.Result.Success:
                    //case LogFilesResult.Types.Result.InProgress:
                    //case LogFilesResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.Progress);
                    break;
                    default:
                    observer.OnError(new LogFilesException(result.Result, result.ResultStr));
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

        public IObservable<ProgressData> DownloadLogFile(Entry entry, string path)
        {
          return Observable.Create<ProgressData>(observer =>
          {
            var request = new DownloadLogFileRequest();
            request.Entry = entry;
            request.Path = path;
            var downloadLogFileResponse = _logFilesServiceClient.DownloadLogFile(request);
            var logFilesResult = downloadLogFileResponse.LogFilesResult;
            if (logFilesResult.Result == LogFilesResult.Types.Result.Success)
            {
              observer.OnNext(downloadLogFileResponse.Progress);
            }
            else
            {
              observer.OnError(new LogFilesException(logFilesResult.Result, logFilesResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> EraseAllLogFiles()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new EraseAllLogFilesRequest();
            var eraseAllLogFilesResponse = _logFilesServiceClient.EraseAllLogFiles(request);
            var logFilesResult = eraseAllLogFilesResponse.LogFilesResult;
            if (logFilesResult.Result == LogFilesResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new LogFilesException(logFilesResult.Result, logFilesResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class LogFilesException : Exception
  {
    public LogFilesResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public LogFilesException(LogFilesResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}