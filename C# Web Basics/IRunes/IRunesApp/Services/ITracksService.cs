namespace IRunesApp.Services
{
    using IRunesApp.ViewModels.Tracks;

    public interface ITracksService
    {
        void CreateTrack(string albumId, string name, string link, decimal price);

        TrackDetailsViewModel GetTrackDetails(string trackId, string albumId);
    }
}
