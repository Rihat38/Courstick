using Courstick.Core.Models;

namespace Courstick.Dto;

public class CourseInfoDto
{
    public string Name { get; set; }
    public string SmallDescription { get; set; }
    public string Description { get; set; }
    public User Author { get; set; }
    public double Price { get; set; }
    public int? Id { get; set; }
    public double? Rating { get; set; }
    public List<Page>? Page { get; set; }
    public Status? Status { get; set; }
    public List<Tag>? Tag { get; set; }
    public string ImageUrl { get; set; }
}