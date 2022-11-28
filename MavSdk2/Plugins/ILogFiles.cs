using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.LogFiles;

using Version = Mavsdk.Rpc.Info.Version;

namespace Mavsdk.Plugins
{
  public interface ILogFiles
  {
    IObservable<List<Entry>> GetEntries();
    IObservable<ProgressData> DownloadLogFile();
    IObservable<ProgressData> DownloadLogFile(Entry entry, string path);
    IObservable<Unit> EraseAllLogFiles();
  }
}