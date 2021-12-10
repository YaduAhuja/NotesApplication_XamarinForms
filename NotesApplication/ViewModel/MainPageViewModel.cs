using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

using System.Windows.Input;
using System.Diagnostics;

using NotesApplication.Model;
using NotesApplication.Service;
using NotesApplication.Views;

namespace NotesApplication.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Note _mySelectedItem;
        private string _searchBarText;

        public bool _searchBarInUse, _searchBarNotInUse;
        public ObservableCollection<Note> SearchNotes { get;}
        public ObservableCollection<Note> Notes { get;}

        public Note MySelectedItem
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

        public bool SearchBarInUse
        {
            get => _searchBarInUse;
            set
            { 
                if (value != _searchBarInUse) {
                    _searchBarInUse = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchBarInUse)));
                }
            }
        }
        public bool SearchBarNotInUse
        {
            get => _searchBarNotInUse;
            set
            {
                if (value != _searchBarNotInUse)
                {
                    _searchBarNotInUse = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchBarNotInUse)));
                }
            }
        }



        public string SearchBarText
        {
            get => _searchBarText;
            set
            {
                value = value.Trim();
                if (value != null && !value.Equals(SearchBarText))
                {
                    Debug.WriteLine(value.Length);
                    SearchNotes.Clear();
                    _searchBarText = value;
                    
                    foreach(Note n in Notes)
                    {
                        var SearchToChecked = value.ToLower();
                        if(n.NoteHeading.ToLower().Contains(SearchToChecked) || 
                            n.NoteContent.ToLower().Contains(SearchToChecked))
                        {
                            SearchNotes.Add(n);
                        }
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchBarText)));
                }

                if(_searchBarText.Length > 0)
                {
                    SearchBarInUse = true;
                    SearchBarNotInUse = false;
                }else
                {
                    SearchBarInUse = false;
                    SearchBarNotInUse = true;
                }
            }
        }

        public ICommand AddNote { get; }
        public ICommand ClearAllNotes { get; }
        public ICommand CancelSearch { get; }

        public MainPageViewModel()
        {   
            //Member Initialization
            Notes = NoteService.getNoteService().NotesList;
            SearchNotes = new ObservableCollection<Note>();

            
            _mySelectedItem = null;
            _searchBarNotInUse = true;
            _searchBarInUse = false;

            //Command Bindings
            AddNote = new Command(() => MoveToDetailPage(new Note(), false));
            ClearAllNotes = new Command(() => Notes.Clear());
            CancelSearch = new Command(() => {
                SearchBarText = "";
                Debug.WriteLine("Search Cancelled");
            });
        }

        private void MoveToDetailPage(Note note, bool isPresentInList)
        {
            var page = new NoteDetail(note, this, isPresentInList);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
