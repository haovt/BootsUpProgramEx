using BookStoreService;
using BookStoreService.Dto;
using Ninject;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreWPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IBSService _service;
        private FileMode fileMode => (bool)TextMode.IsChecked ? FileMode.Text : FileMode.Json;

        public MainWindow()
        {
            InitializeComponent();

            IKernel kernel = new StandardKernel(new ServiceModule());
            _service = kernel.Get<IBSService>();
            Refresh();

        }

        private void Refresh()
        {
            if (_service != null)
            {
                var books = _service.GetAll(fileMode).ToList();
                Books.ItemsSource = books;
            }    
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = Books.SelectedItem as BookDto;
            _service.DeleteBook(selectedItem.Id, fileMode);
            Refresh();
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = Books.SelectedItem as BookDto;
            PopupWindow popup = new PopupWindow();
            var dataContext = selectedItem.Copy();

            popup.DataContext = dataContext;
            var result = popup.ShowDialog();

            if (result.HasValue && result.Value)
            {
                // Update list view
                _service.UpdateBook(dataContext, fileMode);
                Refresh();
            }
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            PopupWindow popup = new PopupWindow();
            var newBook = new BookDto();
            popup.DataContext = newBook;
            var result = popup.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _service.AddBook(newBook, fileMode);
                Refresh();
            }
     
        }

        private void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
