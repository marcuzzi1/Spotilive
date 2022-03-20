using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotiliveTryHard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotiliveTryHard.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartsPage : ContentPage
    {
        public ChartsPage()
        {
            BindingContext = new ChartsViewModel();
            InitializeComponent();
        }
    }
}