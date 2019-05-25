using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using tema1.Services;
using tema1.ViewModels;

namespace tema1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService commentService;
        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        /// <summary>
        /// Gets All Comments
        /// </summary>
        /// <param name="filter">a filter to be applied</param>
        /// <returns>A list of filtered comments</returns>
        // GET: api/Comments
        [HttpGet]
        public IEnumerable<CommentGetModel> Get([FromQuery]string filter)
        {
            return commentService.GetAll(filter);
        }

    }
}