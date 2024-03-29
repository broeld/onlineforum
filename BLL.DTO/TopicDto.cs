﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public ICollection<ThreadDto> Threads { get; set; }
    }
}
