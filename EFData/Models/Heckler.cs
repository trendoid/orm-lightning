using System.Collections.Generic;

namespace EFData.Models
{
    public class Heckler
    {
        public int HecklerId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Comment> Comments { get; set; }
    }
}