using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NotesApplication.Model;
using NotesApplication.ViewModel;
using NotesApplication.Service;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TaskDetail : ContentPage
    {
        private Tasks CurrentTask;
        private readonly TaskPageViewModel TaskPageViewModel;
        public bool isPresentInList;

        public TaskDetail(Tasks Tasks, TaskPageViewModel TaskPageViewModel, bool isPresentInList)
        {
            InitializeComponent();

            CurrentTask = Tasks;
            Debug.WriteLine(CurrentTask);
            this.isPresentInList = isPresentInList;
            TasksHeadingView.Text = CurrentTask.TaskContent;
            TaskCheckBox.IsChecked = CurrentTask.isChecked;
            this.TaskPageViewModel = TaskPageViewModel;
        }

        private void TasksHeadingView_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = (Entry)sender;
            CurrentTask.TaskContent = view.Text;
            refreshTasksList();
        }

        private void refreshTasksList()
        {
            if (isPresentInList && CurrentTask.TaskContent.Length == 0)
            {
                TaskService.getTaskService().removeSubscription(CurrentTask);
                Device.BeginInvokeOnMainThread(() =>
                {
                    TaskPageViewModel.TasksList.Remove(CurrentTask);
                });
                isPresentInList = false;
            }

            if (!isPresentInList && CurrentTask.TaskContent.Length > 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    TaskPageViewModel.TasksList.Add(CurrentTask);
                });
                isPresentInList = true;
            }
        }

        private void TasksCheckBox_Changed(object sender, CheckedChangedEventArgs e)
        {
            var view = (CheckBox)sender;
            CurrentTask.isChecked = view.IsChecked;
        }

        private void DeleteTasks_Clicked(object sender, System.EventArgs e)
        {
            clearTasks();
            goBack();
        }

        private void ClearTasks_Clicked(object sender, System.EventArgs e)
        {
            clearTasks();
        }

        private void clearTasks()
        {
            TasksHeadingView.Text = "";
        }
        private void goBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}