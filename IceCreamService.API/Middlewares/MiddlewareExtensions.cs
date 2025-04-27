using IceCreamService.Core.Middleware;

namespace IceCreamService.API.Middlewares;
public static class MiddlewareExtensions
{
    public static WebApplication UseApiMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CorsPolicy");
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}