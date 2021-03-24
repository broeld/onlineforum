using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<UserProfile>
    {
    }
}
