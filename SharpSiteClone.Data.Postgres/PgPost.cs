using System.ComponentModel.DataAnnotations;

namespace SharpSiteClone.Data.Postgres;

public class PgPost
{
    [Required, Key, MaxLength(300)]
    public required string Slug { get; set; }
    [Required, MaxLength(200)]
    public required string Title { get; set; }
    [Required]
    public required string Content { get; set; } = string.Empty;
    [Required]
    public required DateTimeOffset Published { get; set; } = DateTimeOffset.MaxValue;
    
    // cast support from SharpSiteClone.Abstractions.Post to SharpSite.Data.Postgres.PgPost
    public static explicit operator PgPost(SharpSiteClone.Abstractions.Post post)
    {
        return new PgPost
        {
            Slug = post.Slug,
            Title = post.Title,
            Content = post.Content,
            Published = post.PublishedDate
        };
    }
    
    // cast support from SharpSite.Data.Postgres.PgPost to SharpSiteClone.Abstractions.Post
    public static explicit operator SharpSiteClone.Abstractions.Post(PgPost post)
    {
        return new SharpSiteClone.Abstractions.Post
        {
            Slug = post.Slug,
            Title = post.Title,
            Content = post.Content,
            PublishedDate = post.Published
        };
    }
}