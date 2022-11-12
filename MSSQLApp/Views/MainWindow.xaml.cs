using MSSQLApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using static MSSQLApp.ViewModels.MainViewModel;

namespace MSSQLApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService<MainViewModel>();
        }

        private void PolaczClick(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as MainViewModel;
            if(dc != null)
            {
                dc.Password = this.passwordBox.Password;
                dc.BtnOperationCommand.Execute((int)OperationEnum.Polacz);
            }
        }

        private void TestujClick(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as MainViewModel;
            if (dc != null)
            {
                dc.Password = this.passwordBox.Password;
                dc.BtnOperationCommand.Execute((int)OperationEnum.Testuj);
            }
        }
    }
}
