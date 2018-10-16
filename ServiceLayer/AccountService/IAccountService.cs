using DataContract.GenericDomains;

namespace ServiceLayer
{
    public interface IAccountService
    {
        User LoginUser(string userName, string password);
        User Register(User model);
    }
}
