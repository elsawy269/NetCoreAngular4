using MessageBoard.BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.BackEnd
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options)
        {}

        public DbSet<Message> messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
