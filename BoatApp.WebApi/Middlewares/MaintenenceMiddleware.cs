using BoatApp.Business.Operations.Setting;

namespace BoatApp.WebApi.Middlewares
{
    public class MaintenenceMiddleware
    {
        private readonly RequestDelegate _next;
       
        public MaintenenceMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            var _settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintenenceMode = _settingService.GetMaintenanceState();

            if (context.Request.Path.StartsWithSegments("/api/auth/login")|| context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _next(context);
                return;
            }

            if (maintenenceMode)
            {
                await context.Response.WriteAsync("Şu anda hizmet veremiyoruz");
            }
            else
            {
                await _next(context); 
            }
        }
    }
}
