using Autofac;
using Autofac.Core;
using Prism.Common;
using Prism.Services;
using SecureBLE.Services;
using SecureBLE.Services.Interfaces;
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

			builder.RegisterType<DevicesView>().AsSelf();
			builder.RegisterType<DevicesViewModel>().AsSelf();

            builder.RegisterType<ServicesView>()
                .As<ServicesView>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(object) && pi.Name == "selectedDevice",
                        (pi, ctx) => "selectedDevice"));

            builder.RegisterType<ServicesViewModel>()
                .As<ServicesViewModel>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(object) && pi.Name == "selectedDevice",
                        (pi, ctx) => "selectedDevice"));

            builder.RegisterType<ServicesView>().AsSelf();
            builder.RegisterType<ServicesViewModel>().AsSelf();

            builder.RegisterType<CharacteristicsView>()
                .As<CharacteristicsView>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(object) && pi.Name == "selectedService",
                        (pi, ctx) => "selectedService"));

            builder.RegisterType<CharacteristicsViewModel>()
                .As<CharacteristicsViewModel>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(object) && pi.Name == "selectedService",
                        (pi, ctx) => "selectedService"));

            builder.RegisterType<CharacteristicsView>().AsSelf();
            builder.RegisterType<CharacteristicsViewModel>().AsSelf();


            builder.RegisterType<CommunicationView>()
                .As<CommunicationView>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(object) && pi.Name == "selectedCharacteristic",
                        (pi, ctx) => "selectedCharacteristic"));

            builder.RegisterType<CommunicationViewModel>()
                .As<CommunicationViewModel>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(object) && pi.Name == "selectedCharacteristic",
                        (pi, ctx) => "selectedCharacteristic"));

            builder.RegisterType<CommunicationView>().AsSelf();
            builder.RegisterType<CommunicationViewModel>().AsSelf();

            builder.RegisterType<ApplicationProvider>().As<IApplicationProvider>();

			builder.RegisterType<PageDialogService>().As<IPageDialogService>();

            builder.RegisterType<CommunicationService>().As<ICommunicationService>();

            return builder.Build();
        }
    }
}
