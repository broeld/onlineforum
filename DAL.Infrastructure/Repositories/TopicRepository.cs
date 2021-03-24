using DAL.Domain;
using DAL.Infrastructure.Repositories.Generic;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure.Repositories
{
    public class TopicRepository : GenericRepository<Topic>, ITopicRepository
    {
    }
}
