using BookStoreService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace BookStoreWPF_UI
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PopupWindow()
        {
            InitializeComponent();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {




            this.DialogResult = true;
            this.Close();
        }
    }
}
