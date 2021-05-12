using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ThreadModel> Threads { get; set; }
    }
}
