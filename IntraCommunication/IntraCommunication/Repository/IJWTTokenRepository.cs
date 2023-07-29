using IntraCommunication.ViewModels;
using Newtonsoft.Json.Linq;

namespace IntraCommunication.Repository
{
    public interface IJWTTokenRepository
    {
        Tokens Authenticate(SignInModel user);
    }
}
