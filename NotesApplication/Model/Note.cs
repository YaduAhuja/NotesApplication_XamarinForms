using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace NotesApplication.Model
{
    public class Note : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _heading, _content;
        public string NoteHeading
        {
            get => _heading;
            set
            {
                value = value.Trim();
                if (value != null && !value.Equals(_heading))
                {
                    _heading = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoteHeading)));
                }
            }
        }

        public string NoteContent
        {
            get => _content;
            set
            {
                value = value.Trim();
                if (value != null && !value.Equals(_content))
                {
                    _content = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoteContent)));
                }
            }
        }

        public Note()
        {
            _heading = "";
            _content = "";
        }
    }
}
