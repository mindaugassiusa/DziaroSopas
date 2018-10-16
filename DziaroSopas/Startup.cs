using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DziaroSopas.Startup))]
namespace DziaroSopas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
