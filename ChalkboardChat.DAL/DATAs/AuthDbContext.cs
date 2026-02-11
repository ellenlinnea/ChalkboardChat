using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChalkboardChat.DAL.DATAs
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            // "options innehåller connection string och inställningar för EF Core."
            // "Vi skickar vidare options till bas-klassen så EF vet hur den ska koppla mot DB."
        }
    }
}
