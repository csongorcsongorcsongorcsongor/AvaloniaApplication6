using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using AvaloniaApplication6.Models;
using AvaloniaApplication6.Persistence;
using AvaloniaApplication6.ViewModels;
using AvaloniaApplication6.Views;

namespace AvaloniaApplication6;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation. made by csongor
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);
        TextDataAccess dataAccess = new TextDataAccess();
        MainModel mainModel = new MainModel(dataAccess);
        MainViewModel viewModel = new MainViewModel(mainModel);

        MainView view = new MainView();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel,
                Content = view
            };
           

            viewModel.SaveEvent += async (s, e) =>
            {
                TopLevel topLevel = TopLevel.GetTopLevel(view);
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Save Car",
                    DefaultExtension = "txt",
                });
                if (file is not null)
                {
                    mainModel.Save(file.Path.AbsolutePath);
                }
            };

            viewModel.LoadEvent += async (s, e) =>
            {
                TopLevel topLevel = TopLevel.GetTopLevel(view);
                var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Load Car",
                    AllowMultiple = false,
                });

                if (file is not null)
                {
                    await mainModel.Load(file[0].Path.AbsolutePath);
                }
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = viewModel
            };
        }


        base.OnFrameworkInitializationCompleted();
    }
}
