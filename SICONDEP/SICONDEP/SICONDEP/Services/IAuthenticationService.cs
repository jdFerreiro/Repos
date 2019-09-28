namespace SICONDEP.Services
{
    public interface IAuthenticationService
    {
        bool Login(string userCode, string userPassword);
        void Logout();
    }
}
