using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Mavsdk.Rpc.Ftp;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public class Ftp
  {
    private readonly FtpService.FtpServiceClient _ftpServiceClient;

    internal Ftp(GrpcChannel channel)
    {
      _ftpServiceClient = new FtpService.FtpServiceClient(channel);
    }

        public IObservable<Unit> Reset()
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new ResetRequest();
            var resetResponse = _ftpServiceClient.Reset(request);
            var ftpResult = resetResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<ProgressData> Download()
        {
          return Observable.Using(() => _ftpServiceClient.SubscribeDownload(new SubscribeDownloadRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.FtpResult;
                  switch (result.Result)
                  {
                    case FtpResult.Types.Result.Success:
                    //case FtpResult.Types.Result.InProgress:
                    //case FtpResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.ProgressData);
                    break;
                    default:
                    observer.OnError(new FtpException(result.Result, result.ResultStr));
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

        public IObservable<ProgressData> Upload()
        {
          return Observable.Using(() => _ftpServiceClient.SubscribeUpload(new SubscribeUploadRequest()),
          reader => Observable.Create(
            async (IObserver<ProgressData> observer) =>
            {
              try
              {
                while (await reader.ResponseStream.MoveNext(CancellationToken.None))
                {
                  var result = reader.ResponseStream.Current.FtpResult;
                  switch (result.Result)
                  {
                    case FtpResult.Types.Result.Success:
                    //case FtpResult.Types.Result.InProgress:
                    //case FtpResult.Types.Result.Instruction:
                    observer.OnNext(reader.ResponseStream.Current.ProgressData);
                    break;
                    default:
                    observer.OnError(new FtpException(result.Result, result.ResultStr));
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

        public IObservable<List<string>> ListDirectory(string remoteDir)
        {
          return Observable.Create<List<string>>(observer =>
          {
            var request = new ListDirectoryRequest();
            request.RemoteDir = remoteDir;
            var listDirectoryResponse = _ftpServiceClient.ListDirectory(request);
            var ftpResult = listDirectoryResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnNext(listDirectoryResponse.Paths.ToList());
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> CreateDirectory(string remoteDir)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new CreateDirectoryRequest();
            request.RemoteDir = remoteDir;
            var createDirectoryResponse = _ftpServiceClient.CreateDirectory(request);
            var ftpResult = createDirectoryResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> RemoveDirectory(string remoteDir)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new RemoveDirectoryRequest();
            request.RemoteDir = remoteDir;
            var removeDirectoryResponse = _ftpServiceClient.RemoveDirectory(request);
            var ftpResult = removeDirectoryResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> RemoveFile(string remoteFilePath)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new RemoveFileRequest();
            request.RemoteFilePath = remoteFilePath;
            var removeFileResponse = _ftpServiceClient.RemoveFile(request);
            var ftpResult = removeFileResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> Rename(string remoteFromPath, string remoteToPath)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new RenameRequest();
            request.RemoteFromPath = remoteFromPath;
            request.RemoteToPath = remoteToPath;
            var renameResponse = _ftpServiceClient.Rename(request);
            var ftpResult = renameResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<bool> AreFilesIdentical(string localFilePath, string remoteFilePath)
        {
          return Observable.Create<bool>(observer =>
          {
            var request = new AreFilesIdenticalRequest();
            request.LocalFilePath = localFilePath;
            request.RemoteFilePath = remoteFilePath;
            var areFilesIdenticalResponse = _ftpServiceClient.AreFilesIdentical(request);
            var ftpResult = areFilesIdenticalResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnNext(areFilesIdenticalResponse.AreIdentical);
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetRootDirectory(string rootDir)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetRootDirectoryRequest();
            request.RootDir = rootDir;
            var setRootDirectoryResponse = _ftpServiceClient.SetRootDirectory(request);
            var ftpResult = setRootDirectoryResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<Unit> SetTargetCompid(uint compid)
        {
          return Observable.Create<Unit>(observer =>
          {
            var request = new SetTargetCompidRequest();
            request.Compid = compid;
            var setTargetCompidResponse = _ftpServiceClient.SetTargetCompid(request);
            var ftpResult = setTargetCompidResponse.FtpResult;
            if (ftpResult.Result == FtpResult.Types.Result.Success)
            {
              observer.OnCompleted();
            }
            else
            {
              observer.OnError(new FtpException(ftpResult.Result, ftpResult.ResultStr));
            }

            return Task.FromResult(Disposable.Empty);
          });
        }

        public IObservable<uint> GetOurCompid()
        {
          return Observable.Create<uint>(observer =>
          {
            var request = new GetOurCompidRequest();
            var getOurCompidResponse = _ftpServiceClient.GetOurCompid(request);
            observer.OnNext(getOurCompidResponse.Compid);

            observer.OnCompleted();
            return Task.FromResult(Disposable.Empty);
          });
        }
  }

  public class FtpException : Exception
  {
    public FtpResult.Types.Result Result { get; }
    public string ResultStr { get; }

    public FtpException(FtpResult.Types.Result result, string resultStr)
    {
      Result = result;
      ResultStr = resultStr;
    }
  }
}