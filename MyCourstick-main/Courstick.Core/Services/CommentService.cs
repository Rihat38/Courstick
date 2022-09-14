using Courstick.Core.Dto;
using Courstick.Core.Interfaces;
using Courstick.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Courstick.Core.Services;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;

    public CommentService(ICommentRepository commentRepository, Microsoft.AspNetCore.Identity.UserManager<User> userManager)
    {
        _commentRepository = commentRepository;
        _userManager = userManager;
    }

    public async Task<List<SendCommentDto>> GetComments(int id)
    {
        var comments = await _commentRepository.GetCommentsByCourseIdAsync(id);
        List<SendCommentDto> commentDto = new List<SendCommentDto>();
        foreach (var comment in comments)
        {
            SendCommentDto item = new SendCommentDto();
            item.Text = comment.Text;
            item.User = await _userManager.FindByIdAsync(comment.UserId.ToString());
            commentDto.Add(item);
        }

        return commentDto;
    }

    public async Task<Comment> AddComment(CommentDto comment, User user)
    {
        Comment com = new Comment();
        com.courseid = comment.CourseId;
        com.Text = comment.Text;
        com.UserId = user.Id;
        com.CreatedDate = new DateTime();
        
        var result = await _commentRepository.AddAsync(com);
        await _commentRepository.SaveChangesAsync();
        return result;
    } 
}