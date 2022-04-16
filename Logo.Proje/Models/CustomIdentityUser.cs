using Microsoft.AspNetCore.Identity;

namespace Logo.Proje.Models
{
    public class CustomIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long IdentityNo { get; set; }
        public bool HasCar { get; set; }
        public string CarPlate { get; set; }
    }
}
