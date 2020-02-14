namespace IRunesApp.Controllers
{
    using IRunesApp.Services;
    using IRunesApp.ViewModels.Users;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            var userId = usersService.GetUserId(input.Username, input.Password);
            if (userId != null)
            {
                this.SignIn(userId);
                return this.Redirect("/");
            }

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Email))
            {
                return this.Error("Email cannot be empty!");
            }

            if (input.Username.Length < 4 || input.Username.Length > 10)
            {
                return this.Error("Username must be between 4 and 20 characters!");
            }

            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Username must be between 6 and 20 characters!");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Password should match!");
            }

            if (usersService.UsernameExists(input.Username))
            {
                return this.Error("Username already exists.");
            }

            if (usersService.EmailExists(input.Email))
            {
                return this.Error("Email already in use.");
            }

            this.usersService.Register(input.Username, input.Password, input.Email);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
