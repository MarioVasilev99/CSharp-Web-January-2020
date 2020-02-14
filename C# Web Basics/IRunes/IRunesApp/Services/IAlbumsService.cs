namespace IRunesApp.Services
{
    using IRunesApp.ViewModels.Albums;
    using System.Collections.Generic;

    public interface IAlbumsService
    {
        void CreateAlbum(string name, string cover);

        IEnumerable<AlbumInfoViewModel> GetAll();

        AlbumDetailsViewModel GetDetails(string id);
    }
}
