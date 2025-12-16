using System;
using System.Collections.ObjectModel;
using AvaloniaApplication6.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication6.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly MainModel _model;

    [ObservableProperty] private string modelInput = "";
    [ObservableProperty] private string typeInput = "";
    [ObservableProperty] private int ageInput;
    [ObservableProperty] private int milesInput;

    public RelayCommand SaveCommand { get; }
    public RelayCommand LoadCommand { get; }

    public event EventHandler? SaveEvent;
    public event EventHandler? LoadEvent;

    public MainViewModel(MainModel model)
    {
        _model = model;
        _model.CarLoaded += Model_CarLoaded;

        SaveCommand = new RelayCommand(OnSave);
        LoadCommand = new RelayCommand(() => LoadEvent?.Invoke(this, EventArgs.Empty));
    }

    private void OnSave()
    {
        var car = new Car(ModelInput, TypeInput, AgeInput, MilesInput);
        _model.SetCar(car);
        SaveEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Model_CarLoaded(object? sender, CarEventArgs e)
    {
        ModelInput = e.Car.Model;
        TypeInput = e.Car.Type;
        AgeInput = e.Car.Age;
        MilesInput = e.Car.Miles;
    }
}

