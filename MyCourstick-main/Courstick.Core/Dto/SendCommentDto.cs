using Courstick.Core.Models;

namespace Courstick.Core.Dto;

public class SendCommentDto
{
    public User User { get; set; }
    public string Text { get; set; }
    public string? TimeStamp { get; set; }
}