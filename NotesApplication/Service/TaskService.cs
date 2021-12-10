using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Collections.Specialized;

using Xamarin.Essentials;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Threading.Tasks;

using NotesApplication.Model;

namespace NotesApplication.Service
{
    class TaskService
    {
        public ObservableCollection<Tasks> TasksList { get; }
        private static TaskService reference;
        private List<Tasks> getTasksList()
        {
            string s = getCollection().Result;
            List<Tasks> cons = JsonConvert.DeserializeObject<List<Tasks>>(s);
            Debug.WriteLine("Get Tasks List" + s);
            return cons;
        }


        public static TaskService getTaskService()
        {
            if (reference == null)
            {
                reference = new TaskService();
            }

            return reference;
        }
        private TaskService()
        {
            TasksList = new ObservableCollection<Tasks>(getTasksList());
            TasksList.CollectionChanged += new NotifyCollectionChangedEventHandler(TasksList_CollectionChanged);
            SubsribeToTasks();
        }

        private void SubsribeToTasks()
        {
            foreach (Tasks task in TasksList)
            {
                task.PropertyChanged -= Tasks_PropertyChanged;
                task.PropertyChanged += Tasks_PropertyChanged;
            }
        }

        public void removeSubscription(Tasks Tasks)
        {
            if (Tasks == null) { return; }
            Tasks.PropertyChanged -= Tasks_PropertyChanged;
        }

        private void Tasks_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("Tasks Property Changed");
            saveCollection();
        }

        private void TasksList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("Collection Property Changed");
            SubsribeToTasks();
            saveCollection();
        }

        private string buildSaveData()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < TasksList.Count - 1; i++)
            {
                sb.Append(JsonConvert.SerializeObject(TasksList[i])).Append(",");
            }

            if (TasksList.Count > 0)
            {
                sb.Append(JsonConvert.SerializeObject(TasksList[TasksList.Count - 1]));
            }
            sb.Append("]");
            Debug.WriteLine("Build Save Data" + sb);
            return sb.ToString();
        }

        private async void saveCollection()
        {
            try
            {
                await SecureStorage.SetAsync("Tasks", buildSaveData());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private async Task<string> getCollection()
        {
            try
            {
                string ret = await SecureStorage.GetAsync("Tasks");
                if (ret != null && ret.Length > 0)
                {
                    return ret;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return "[]";
        }
    }
}
