using System.Xml.Linq;

namespace AspNetCoreDashboardBackend.Middlewares;

public class UserIdSetMiddleware
{
    private readonly RequestDelegate _next;

    public UserIdSetMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value != null
            && context.Request.Path.Value.Contains("/api/dashboard/dashboards/")
            && context.Request.Method == "GET"
            )
        {
            string userId = "";

            if (context.Request.Headers.TryGetValue("userid", out var headerValue))
            {
                userId = headerValue.ToString();
            }
            
            var dashboardName = context.Request.Path.Value.Split("/").Last();
            SetUserId(dashboardName, userId);
        }

        await _next(context);
    }

    private void SetUserId(string dashboardFileName, string userId)
    {
        var xmlFilePath = $"/root/dashboards/{dashboardFileName}.xml";

        var xmlDoc = XDocument.Load(xmlFilePath);

        var userIdParameter = xmlDoc.Descendants("Parameter")
            .FirstOrDefault(p => p.Attribute("Name")?.Value == "@UserId");

        if (userIdParameter != null)
        {
            userIdParameter.Value = userId;
        }

        xmlDoc.Save(xmlFilePath);
    }
}