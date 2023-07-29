using System;

namespace IntraCommunication.ViewModels
{
    public class UserProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public DateTime Dob { get; set; }
        public string Password { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }

    }
}
