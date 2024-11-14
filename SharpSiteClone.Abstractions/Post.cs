using System.ComponentModel.DataAnnotations;

namespace SharpSiteClone.Abstractions;

/// <summary>
/// A blog post
/// </summary>
public class Post
{
    [Key, Required, MaxLength(300)]
    public required string Slug { get; set; } = String.Empty;
    
    [Required, MaxLength(200)]
    public required string Title { get; set; } = String.Empty;
    
    [Required]
    public required string Content { get; set; } = String.Empty;
    
    /// <summary>
    /// The date the article will be published. If the date is in the future, the article will not be displayed.
    /// </summary>
    /// <value></value>
    [Required]
    public required DateTimeOffset PublishedDate { get; set; } = DateTimeOffset.MaxValue;
    
    public static string GetSlug(string title)
    {
        var slug = title.ToLower().Replace(" ", "-");
        slug = System.Web.HttpUtility.UrlEncode(slug);
        return slug;
    }

    public Uri ToUrl()
    {
        return new Uri($"/{PublishedDate.UtcDateTime:yyyyMMdd}/{Slug}", UriKind.Relative);
    }
}