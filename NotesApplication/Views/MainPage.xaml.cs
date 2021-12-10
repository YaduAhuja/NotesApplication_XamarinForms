using NotesApplication.Model;
using NotesApplication.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
        }
        /*
        protected override void OnAppearing()
        {
            //Write the code of your page here
            base.OnAppearing();
            Debug.WriteLine("Hehehe");
            Note temp = Notes[Notes.Count - 1];
            Notes.RemoveAt(Notes.Count - 1);
            Notes.Add(temp);
            
        }*/
        private void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            SearchBarButton.IsVisible = true;
        }

        private void SearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            SearchBarButton.IsVisible = false;
        }

        /*
        private void NotesView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var view = (CollectionView)sender;
            var note = (Note)view.SelectedItem;
            if(note == null) { return; }
            NoteDetail DetailPage = new NoteDetail(note);
            view.SelectedItem = null;
            Navigation.PushAsync(DetailPage);
            Debug.WriteLine(note.Title);
        } */
    }
}