using Autofac;
using Prism.Common;
using Prism.Services;
using SecureBLE.ViewModels;
using SecureBLE.Views;

namespace SecureBLE.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            ContainerBuilder builder = new ContainerBuilder();


            builder.RegisterType<MainView>().AsSelf();

            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<ApplicationProvider>().As<IApplicationProvider>();

            builder.RegisterType<PageDialogService>().As<IPageDialogService>();

            return builder.Build();
        }
    }
}
