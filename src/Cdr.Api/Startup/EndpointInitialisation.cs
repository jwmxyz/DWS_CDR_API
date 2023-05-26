namespace Cdr.Api.Startup
{
    public static class EndpointInitialisation
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
