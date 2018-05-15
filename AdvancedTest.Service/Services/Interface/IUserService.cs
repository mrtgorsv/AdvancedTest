using AdvancedTest.Data.Model;

namespace AdvancedTest.Service.Services.Interface
{
    public interface IUserService
    {
        User LogIn(string login, string password);
    }
}
