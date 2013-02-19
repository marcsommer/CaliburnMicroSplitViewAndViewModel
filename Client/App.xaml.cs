using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Client.Views;
using Common.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace Client
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "Client.Views",
                DefaultSubNamespaceForViewModels = "Common.ViewModels"
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);

            container = new WinRTContainer();
            container.RegisterWinRTServices();

            // Register the ViewModels
            container.PerRequest<MainViewModel>();

            MessageBinder.SpecialValues.Add("$account", context =>
            {
                if (context == null || context.EventArgs == null)
                    return null;

                return
                    ((ItemClickEventArgs)context.EventArgs).ClickedItem as AccountViewModel;
            });
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();
            assemblies.Add(typeof(MainViewModel).GetTypeInfo().Assembly);

            return assemblies;
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainView>();
        }
    }
}
