using System;
using System.Collections.Generic;
using System.Reactive;
using Mavsdk.Rpc.TrackingServer;

using Version = Mavsdk.Rpc.Info.Version;

namespace MavSdk.Plugins
{
  public interface ITrackingServer
  {
    IObservable<Unit> SetTrackingPointStatus(TrackPoint trackedPoint);
    IObservable<Unit> SetTrackingRectangleStatus(TrackRectangle trackedRectangle);
    IObservable<Unit> SetTrackingOffStatus();
    IObservable<TrackPoint> TrackingPointCommand();
    IObservable<TrackRectangle> TrackingRectangleCommand();
    IObservable<int> TrackingOffCommand();
    IObservable<Unit> RespondTrackingPointCommand(CommandAnswer commandAnswer);
    IObservable<Unit> RespondTrackingRectangleCommand(CommandAnswer commandAnswer);
    IObservable<Unit> RespondTrackingOffCommand(CommandAnswer commandAnswer);
  }
}