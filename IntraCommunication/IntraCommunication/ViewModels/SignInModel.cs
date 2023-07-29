using System.ComponentModel.DataAnnotations;

namespace IntraCommunication.ViewModels
{
    public class SignInModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
