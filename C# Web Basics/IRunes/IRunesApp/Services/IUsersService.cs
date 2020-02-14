namespace IRunesApp.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void Register(string username, string password, string email);

        bool UsernameExists(string username);

        bool EmailExists(string email);

        string GetUsername(string id);
    }
}
