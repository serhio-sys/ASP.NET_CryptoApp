using CryptoApp.Models;
using CryptoApp.Services;

namespace CryptoApp.Middalwares
{
    public class UserMiddalware
    {
        public readonly UserService userService;
        public readonly RequestDelegate requestDelegate;

        public UserMiddalware(RequestDelegate requestDelegate, UserService userService)
        {
            this.requestDelegate = requestDelegate;
            this.userService = userService;
        }

        public async Task InvokeAsync(HttpContext context, DatabaseContext databaseContext)
        {
            int id;
            int.TryParse(context.Session.GetString("Id"), out id);
            if (id != 0)
            {
                userService.user = databaseContext.Users.FirstOrDefault(it => it.Id == id);
            }
            await requestDelegate.Invoke(context);
        }
    }
}
