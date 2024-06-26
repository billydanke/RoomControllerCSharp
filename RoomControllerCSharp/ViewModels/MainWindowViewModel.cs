using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using RoomControllerCSharp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomControllerCSharp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand TriggerPanelCommand { get; }

        public MainWindowViewModel()
        {
            TriggerPanelCommand = ReactiveCommand.Create(() =>
            {
                TriggerPane();
            });

            SelectedListItem = Items.FirstOrDefault();
        }

        #region Clock Bar Stuff

        private string _clockBarTime = TimeManager.GetDayAndTime();

        public string ClockBarTime
        {
            get => _clockBarTime;
            set => this.RaiseAndSetIfChanged(ref _clockBarTime, value);
        }

        #endregion

        private bool _isPaneOpen = false;
        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
        }

        private ViewModelBase _currentPage = new HomePageViewModel();
        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }

        private ListItemTemplate? _selectedListItem;
        public ListItemTemplate? SelectedListItem
        {
            get => _selectedListItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedListItem, value);
                if (value is null) return;
                var instance = Activator.CreateInstance(value.ModelType);
                if (instance is null) return;
                CurrentPage = (ViewModelBase)instance;
            }
        }

        public ObservableCollection<ListItemTemplate> Items { get; } = new()
        {
            new ListItemTemplate(typeof(HomePageViewModel), "Home", "HomeRegular"),
            new ListItemTemplate(typeof(MainLightsViewModel), "Main Lights", "LightbulbRegular"),
            new ListItemTemplate(typeof(ShelfLightsViewModel), "Shelf Lights", "TableRegular"),
            new ListItemTemplate(typeof(AccessoriesViewModel), "Accessories", "OptionsRegular"),
            new ListItemTemplate(typeof(AlarmsPageViewModel), "Alarms", "ClockAlarmRegular"),
            new ListItemTemplate(typeof(SettingsPageViewModel), "Settings", "SettingsRegular"),
        };

        private void TriggerPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
    }

    public class ListItemTemplate
    {

        public ListItemTemplate(Type type, string label, string iconKey)
        {
            ModelType = type;
            Label = label;
            //Label = type.Name.Replace("ViewModel", "");

            Application.Current!.TryFindResource(iconKey, out var res);
            ListItemIcon = (StreamGeometry)res!;
        }

        public string Label { get; }
        public Type ModelType { get; }
        public StreamGeometry ListItemIcon { get; }
    }
}
