using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantaC.Models
{
    public class Post
    {
        [Key]
        public string PostId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        [Required]
        public string PostName { get; set; }

        [Required]
        public string PostSubject { get; set; }

        public string PostImage { get; set; }        

        [Required]
        [DataType(DataType.MultilineText)]
        public string PostContent { get; set; }

        public virtual List<Comment> PostComments { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}