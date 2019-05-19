using tema1.Models;

namespace tema1.ViewModels
{
    public class CommentGetModel
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int ExpenseId { get; set; }

        //public static CommentGetModel FromComment(Comment comment)
        //{
        //    return new CommentGetModel
        //    {
        //       ID = comment.Id,
        //       Text = comment.Text,
        //       Important = comment.Important,
        //       //ExpenseId = comment.ExpenseId
        //    };
        //}
    }
}
