namespace EFData.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int HecklerId { get; set; }
        public Heckler Heckler { get; set; }
    }
}