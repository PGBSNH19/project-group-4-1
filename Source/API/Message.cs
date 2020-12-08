using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

    }
}
