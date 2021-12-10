using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;

using Xamarin.Forms;

using NotesApplication.Model;
using NotesApplication.Service;
using NotesApplication.Views;

namespace NotesApplication.ViewModel
{
    public class TaskPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Tasks _mySelectedItem;
        public ObservableCollection<Tasks> TasksList { get; }

        public Tasks MySelectedItem
        {
            get => _mySelectedItem;
            set
            {
                if (value != null)
                {
                    _mySelectedItem = null;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MySelectedItem)));
                    MoveToDetailPage(value, true);
                }
            }
        }

        public ICommand AddTasks { get; }
        public ICommand ClearAllTasks { get; }

        public TaskPageViewModel()
        {
            TasksList = TaskService.getTaskService().TasksList;
            _mySelectedItem = null;
            AddTasks = new Command(() => MoveToDetailPage(new Tasks(), false));
            ClearAllTasks = new Command(() => TasksList.Clear());
        }

        private void MoveToDetailPage(Tasks Tasks, bool isPresentInList)
        {
            var page = new TaskDetail(Tasks, this, isPresentInList);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
