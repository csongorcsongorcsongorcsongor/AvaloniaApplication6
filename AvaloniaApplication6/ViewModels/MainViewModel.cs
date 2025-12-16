using System;
using System.Collections.ObjectModel;
using AvaloniaApplication6.Models;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication6.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private MainModel _model;
    public string ModelInput { get; set; }
    public string TypeInput { get; set; }
    public int AgeInput { get; set; }
    public int MilesInput { get; set; }

    public ObservableCollection<Car> Cars { get; set; }
    public RelayCommand AddCarCommand { get; set; }

    public RelayCommand SaveCommand { get; set; }
    public RelayCommand LoadCommand { get; set; }
    public event EventHandler SaveEvent;
    public event EventHandler LoadEvent;

    public MainViewModel(MainModel model)
    {
        _model = model;
        _model.CarCreated += _model_CarCreated;
        Cars = new ObservableCollection<Car>();
        AddCarCommand = new RelayCommand(AddCar);
       
        SaveCommand = new RelayCommand(() => { SaveEvent?.Invoke(this, EventArgs.Empty); });
        LoadCommand = new RelayCommand(() => { LoadEvent?.Invoke(this, EventArgs.Empty); });
    }


    private void _model_CarCreated(object? sender, CarEventArgs e)
    {
        Car carr = new Car(e.Car.Model, e.Car.Type, e.Car.Age, e.Car.Miles);
        Cars.Add(carr);
    }
    private void AddCar()
    {
        Car carr = new Car(ModelInput, TypeInput, AgeInput, MilesInput);
        Cars.Add(carr);
    }
}
