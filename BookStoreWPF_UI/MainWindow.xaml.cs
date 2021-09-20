using BookStoreService;
using BookStoreService.Dto;
using log4net;
using log4net.Config;
using Microsoft.Win32.SafeHandles;
using Ninject;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        private static IBSRepository _service;
        private BookStoreService.ReadFileMode fileMode => (bool)TextMode.IsChecked ? BookStoreService.ReadFileMode.Text : BookStoreService.ReadFileMode.Json;
        private IKernel _kernel;
        private readonly ILog log = LogManager.GetLogger(typeof(MainWindow));
        public MainWindow()
        {
            MDC.Set("machine", Environment.MachineName);
            AllocConsole();
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = Encoding.GetEncoding(MY_CODE_PAGE);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding) { AutoFlush = true };
            Console.SetOut(standardOutput);

            XmlConfigurator.Configure();

            InitializeComponent();
      
            _kernel = new StandardKernel(new ServiceModule());
            _service = fileMode == ReadFileMode.Text ? _kernel.Get<IBSRepository>("TextRepo") 
                : _kernel.Get<IBSRepository>("JsonRepo");

            Refresh();

        }

        private void Refresh()
        {
            if (_service != null)
            {
                log.Info($"GetAll(): get all Books from {fileMode} mode");
                var books = _service.GetAll().ToList();
                Books.ItemsSource = books;
            }    
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = Books.SelectedItem as BookDto;
            log.Info($"Delete book with Id={selectedItem.Id}");
            _service.DeleteBook(selectedItem.Id);
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
                log.Info($"Update book with Id={selectedItem.Id}");
                // Update list view
                _service.UpdateBook(dataContext);
                Refresh();
            }
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            PopupWindow popup = new PopupWindow();
            var newBook = new BookDto();
            popup.DataContext = newBook;
            var result = popup.ShowDialog();

            // Validation
            if (result.HasValue && result.Value)
            {
                try
                {
                    log.Info($"Add new book: Title={newBook.Title}, Author={newBook.Author}, Price={newBook.Price}");
                    _service.AddBook(newBook);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    MessageBox.Show(ex.Message);
                }
               
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
            if (_kernel == null)
            {
                _kernel = new StandardKernel(new ServiceModule());
            }

            _service = fileMode == ReadFileMode.Text ? _kernel.Get<IBSRepository>("TextRepo")
                : _kernel.Get<IBSRepository>("JsonRepo");
            Refresh();
        }
    }
}
