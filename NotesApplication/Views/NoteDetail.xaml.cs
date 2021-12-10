using System.ComponentModel;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotesApplication.Model;
using NotesApplication.Service;
using NotesApplication.ViewModel;

namespace NotesApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDetail : ContentPage
    {
        private Note currentNote;
        private readonly MainPageViewModel mainPageViewModel;
        public bool isPresentInList;
        public NoteDetail(Note note, MainPageViewModel mainPageViewModel, bool isPresentInList)
        {
            InitializeComponent();

            currentNote = note;
            Debug.WriteLine(currentNote);
            this.isPresentInList = isPresentInList;
            NoteHeadingView.Text = note.NoteHeading.ToString();
            NoteContentView.Text = note.NoteContent.ToString();
            this.mainPageViewModel = mainPageViewModel;
        }

        private void NoteHeadingView_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = (Entry)sender;
            currentNote.NoteHeading = view.Text;
            refreshNotesList();
        }

        private void NoteContentView_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = (Editor)sender;
            currentNote.NoteContent = view.Text;
            refreshNotesList();
        }

        private void refreshNotesList()
        {
            if(isPresentInList && currentNote.NoteContent.Length == 0 && currentNote.NoteHeading.Length == 0)
            {
                NoteService.getNoteService().removeSubscription(currentNote);
                Device.BeginInvokeOnMainThread(() =>
                {
                    mainPageViewModel.Notes.Remove(currentNote);
                });
                isPresentInList = false;
            }

            if(!isPresentInList && (currentNote.NoteContent.Length > 0 || currentNote.NoteHeading.Length > 0))
            {  
                Device.BeginInvokeOnMainThread(() =>
                {
                    mainPageViewModel.Notes.Add(currentNote);
                });
                isPresentInList = true;
            }
        }

        private void DeleteNote_Clicked(object sender, System.EventArgs e)
        {
            clearNote();
            goBack();
        }

        private void ClearNote_Clicked(object sender, System.EventArgs e)
        {
            clearNote();
        }

        private void clearNote()
        {
            NoteHeadingView.Text = "";
            NoteContentView.Text = "";
        }
        private void goBack() {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}