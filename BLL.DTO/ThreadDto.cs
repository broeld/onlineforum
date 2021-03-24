using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
        public class ThreadDto
        {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsOpen { get; set; }
        public DateTime ThreadOpenedDate { get; set; }
        public DateTime? ThreadClosedDate { get; set; }

        public int UserProfileId { get; set; }
        public UserDto UserProfile { get; set; }

        public int TopicId { get; set; }
        public TopicDto Topic { get; set; }

        public ICollection<PostDto> Posts { get; set; }
    }
}
