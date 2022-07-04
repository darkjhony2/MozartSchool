using Microsoft.AspNetCore.Identity;

namespace ColegioMozart.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public bool Enabled { get; set; }
}
