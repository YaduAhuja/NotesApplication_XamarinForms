using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedShell : TabbedPage
    {
        public TabbedShell()
        {
            InitializeComponent();
            Children.Add(new MainPage());
            Children.Add(new TasksPage());
        }
    }
}