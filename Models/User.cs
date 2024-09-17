using Microsoft.AspNetCore.Identity;

namespace ABCMoneyTransfer.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
    }
}
