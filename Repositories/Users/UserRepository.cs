using disaster_management.Data;
using disaster_management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories.Users
{
    public class UserRepository : Repository<User>
    {
        private readonly DaDManagementContext context;
        public UserRepository(DbContextOptions<DaDManagementContext> context) : base(context)
        {
            this.context = new DaDManagementContext(context);
        }

        public async Task<User?> ValidateUser(string username, string password)
        {
            // Replace with hashed password comparison in production
            var user = await context.Users.SingleOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
            // Tìm nhóm của user
            var group = await context.UserGroups
                .FirstOrDefaultAsync(gr => gr.Users.Any(u => u.UserId == user.UserId));
            // Tìm role của nhóm
            var role = await context.UserRoles
                .FirstOrDefaultAsync(r => r.Groups.Any(g => g.GroupId == group.GroupId));
            group.Roles.Add(role);
            user.Groups.Add(group);
            
            return user;
        }


        internal async Task<IEnumerable<User>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    
    
    }
}
