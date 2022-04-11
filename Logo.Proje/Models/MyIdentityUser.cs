using Microsoft.AspNetCore.Identity;

namespace Logo.Proje.Models
{
    public class MyIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long identityNo { get; set; }
        public bool hasCar { get; set; }
        public string carPlate { get; set; }

    }
}
