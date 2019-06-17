using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentGroupFacultyApp.Startup))]
namespace StudentGroupFacultyApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
