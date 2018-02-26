using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatAPI.Models
{
    /*
     *  
     * The database context is the main class
     * that coordinates Entity Framework functionality
     * for a given model.  In this case, the UserModel.
     * 
     * This class is created by deriving from the 
     * Microsoft.EntityFramworkCore.DbContext
     */
    public class UserContext : DbContext
    {
        // Creating an instance of the DbContext class in the Class Constructor 
        public UserContext(DbContextOptions<UserContext> options): base(options) { }
        public DbSet<UserModel> Users { get; set; }
    }

    
}
