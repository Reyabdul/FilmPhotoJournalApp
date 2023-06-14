using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilmPhotoJournalApp.Startup))]
namespace FilmPhotoJournalApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
