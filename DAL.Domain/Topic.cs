using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domain
{
    public class Topic : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
