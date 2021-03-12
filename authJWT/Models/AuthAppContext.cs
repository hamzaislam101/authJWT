using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authJWT.Models
{
    public class AuthAppContext : DbContext
    {
        public AuthAppContext(DbContextOptions<AuthAppContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
