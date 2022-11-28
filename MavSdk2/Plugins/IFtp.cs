using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.Ftp;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface IFtp
  {
    IObservable<Unit> Reset();
    IObservable<ProgressData> Download();
    IObservable<ProgressData> Upload();
    IObservable<List<string>> ListDirectory(string remoteDir);
    IObservable<Unit> CreateDirectory(string remoteDir);
    IObservable<Unit> RemoveDirectory(string remoteDir);
    IObservable<Unit> RemoveFile(string remoteFilePath);
    IObservable<Unit> Rename(string remoteFromPath, string remoteToPath);
    IObservable<bool> AreFilesIdentical(string localFilePath, string remoteFilePath);
    IObservable<Unit> SetRootDirectory(string rootDir);
    IObservable<Unit> SetTargetCompid(uint compid);
    IObservable<uint> GetOurCompid();
  }
}