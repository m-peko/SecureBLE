﻿using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using SecureBLE.Startup;
using SecureBLE.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SecureBLE
{
	public partial class App : Application
	{
		public static IAdapter BLEAdapter { get; set; }

		public App()
		{
			InitializeComponent();

		    var bootstrapper = new Bootstrapper();
		    var container = bootstrapper.Bootstrap();

		    MainPage = new NavigationPage(container.Resolve<MainView>());

            BLEAdapter = CrossBluetoothLE.Current.Adapter;
		}

		protected override void OnStart()
		{}

		protected override void OnSleep()
		{}

		protected override void OnResume()
		{}
	}
}
