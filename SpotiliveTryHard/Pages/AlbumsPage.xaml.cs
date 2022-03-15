﻿using SpotiliveTryHard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotiliveTryHard.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumsPage : ContentPage
    {
        public AlbumsPage()
        {
            this.BindingContext = AlbumsViewModel.Instance;
            InitializeComponent();
        }
    }
}