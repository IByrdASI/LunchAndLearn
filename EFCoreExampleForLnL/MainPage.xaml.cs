using EFCoreExampleForLnL.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreExampleForLnL;

public partial class MainPage : ContentPage
{
    private MainPageViewModel? _viewModel;

    public MainPage()
    {
        InitializeComponent();
        Loaded += OnPageLoaded;
    }

    private async void OnPageLoaded(object? sender, EventArgs e)
    {
        if (_viewModel == null)
        {
            _viewModel = MauiProgram.Services.GetService<MainPageViewModel>();

            if (_viewModel != null)
            {
                BindingContext = _viewModel;
                await _viewModel.LoadDataAsync();
            }
        }
    }
}
