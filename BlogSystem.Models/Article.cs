﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Article : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
