namespace Ubam.Evolution.Presentation.Startups;

public static class ExceptionHandlingMiddleware
{
    public static void ConfigureMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStatusCodePages(context =>
        {
            if (context.HttpContext.Response.StatusCode == 404)
                context.HttpContext.Response.Redirect("/Error/NotFound");

            return Task.CompletedTask;
        });
    }
}