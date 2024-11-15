﻿using System.Text;
using SharpSiteClone.Abstractions;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedParameter.Local
// ReSharper disable InconsistentNaming

public static class ProgramExtensions_Sitemap
{
    public static WebApplication MapSiteMap(this WebApplication app)
    {
        app.MapGet("/sitemap.xml", async (
                IHostEnvironment env, 
                HttpContext context, 
                IPostRepository postRepository) =>
            {

                var host = context.Request.Host.Value;
                var lastModDate = DateTime.Now.Date;

                var baseXml = $"""
                               <?xml version="1.0" encoding="UTF-8"?>
                               <urlset xmlns="https://www.sitemaps.org/schemas/sitemap/0.9">
                                 <url>
                                   <loc>https://{host}</loc>
                                   <lastmod>{lastModDate:yyyy-MM-dd}</lastmod>
                                 </url>
                               """;
                var sb = new StringBuilder(baseXml);
                
                var posts = await postRepository.GetPosts();
                foreach (var post in posts)
                {
                    var postXml = $"""
                                  	<url>
                                  		<loc>https://{host}{post.ToUrl()}</loc>
                                  		<lastmod>{post.PublishedDate:yyyy-MM-dd}</lastmod>
                                  	</url>
                                  """;
                    sb.Append(postXml);
                }

                // append post URLs
                sb.Append("</urlset>");

                context.Response.ContentType = "application/xml";
                await context.Response.WriteAsync(sb.ToString());
            })
            .CacheOutput(policy =>
            {
                policy.Tag("sitemap");
                policy.Expire(TimeSpan.FromMinutes(30));
            }
        );
        
        return app;
    }

}