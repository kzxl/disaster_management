﻿using disaster_management.Data;
using disaster_management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories.Users
{
    public class UserGroupsRepository : Repository<UserGroup>
    {
        public UserGroupsRepository(DbContextOptions<DaDManagementContext> context) : base(context)
        {
        }

        internal async Task<IEnumerable<UserGroup>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
