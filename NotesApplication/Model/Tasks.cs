using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace NotesApplication.Model
{
    public class Tasks : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _content;
        private bool _isChecked;
        private TextDecorations _decoration;

        public string TaskContent
        {
            get => _content;
            set
            {
                value = value.Trim();
                if (value != null && !value.Equals(_content))
                {
                    _content = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaskContent)));
                }
            }
        }

        public bool isChecked
        {
            get => _isChecked;
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    if (_isChecked)
                    {
                        Decoration = TextDecorations.Strikethrough;
                    }else
                    {
                        Decoration = TextDecorations.None;
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(isChecked)));
                }
            }
        }

        public TextDecorations Decoration
        {
            get => _decoration;
            set
            {
                if(value != _decoration)
                {
                    _decoration = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Decoration)));
                }
            }
        }

        public Tasks()
        {
            _content = "";
            _isChecked = false;
        }
    }
}
