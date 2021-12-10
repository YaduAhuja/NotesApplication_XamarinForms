using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Collections.Specialized;

using Xamarin.Essentials;

using NotesApplication.Model;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NotesApplication.Service
{
    public class NoteService
    {
        public ObservableCollection<Note> NotesList { get; }
        private static NoteService reference;
        private List<Note> getNotesList()
        {
            string s = getCollection().Result;
            List<Note> cons = JsonConvert.DeserializeObject<List<Note>>(s);
            Debug.WriteLine("Get Notes List" + cons);
            return cons;
        }

        
        public static NoteService getNoteService()
        {
            if(reference == null)
            {
                reference = new NoteService();
            }

            return reference;
        }
        private NoteService()
        {
            NotesList = new ObservableCollection<Note>(getNotesList());
            NotesList.CollectionChanged += new NotifyCollectionChangedEventHandler(NotesList_CollectionChanged);
            SubsribeToNotes();
        }

        private void SubsribeToNotes()
        {
            foreach(Note note in NotesList)
            {
                note.PropertyChanged -= Note_PropertyChanged;
                note.PropertyChanged += Note_PropertyChanged;
            }
        }

        public void removeSubscription(Note note)
        {
            if(note == null) { return; }
            note.PropertyChanged -= Note_PropertyChanged;
        }

        private void Note_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("Note Property Changed");
            saveCollection();
        }

        private void NotesList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("Collection Property Changed");
            SubsribeToNotes();
            saveCollection();
        }

        private string buildSaveData()
        {
            
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for(int i = 0; i < NotesList.Count -1; i++)
            {
                sb.Append(JsonConvert.SerializeObject(NotesList[i])).Append(",");
            }

            if(NotesList.Count > 0)
            {
                sb.Append(JsonConvert.SerializeObject(NotesList[NotesList.Count - 1]));
            }
            sb.Append("]");
            Debug.WriteLine("Build Save Data"+sb);
            return sb.ToString();
        }

        private async void saveCollection()
        {
            try
            {
                await SecureStorage.SetAsync("Notes", buildSaveData());
            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private async Task<string> getCollection()
        {
            try
            {
                string ret = await SecureStorage.GetAsync("Notes");
                if(ret != null && ret.Length > 0)
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
