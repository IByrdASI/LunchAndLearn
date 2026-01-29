using EFCoreExampleForLnL.ViewModels;

namespace EFCoreExampleForLnL;

public partial class MainPage : ContentPage
{
    private MainPageViewModel? _viewModel;

    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel == null)
        {
            _viewModel = Handler?.MauiContext?.Services.GetService<MainPageViewModel>();
            if (_viewModel != null)
            {
                BindingContext = _viewModel;
                await _viewModel.LoadDataAsync();
            }
        }
    }
}
