using Microsoft.AspNetCore.Builder;

namespace Minimal_API_Crud
{
    public static class Apiroute
    {
        public static void RouteMethod(this WebApplication app)
        {
            app.ProductsMethod();
            app.StudentMethod();
        }
    }
}
