using BackEnd;
using DataContract.GenericDomains;

namespace ServiceLayer
{
    public class AccountService : IAccountService
    {
        private UserRepository _userRepository = new UserRepository();

        public void AddUser(User model)
        {
            _userRepository.AddEntity(model);

        }

        public User LoginUser(string userName, string password)
        {
            return  _userRepository.GetEntityByFilter(x => x.Name == userName && x.Password == password);
        }
        public User Register(User model)
        {
            return _userRepository.AddEntity(model);
        }

    }
}
