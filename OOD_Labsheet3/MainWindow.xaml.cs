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

namespace OOD_Labsheet3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Ex1
        //Ex 1 - Customer Names
        private void btnQueryEx1_Click(object sender, RoutedEventArgs e)
        {
            //var query = from c in db.Customers
            //            select c.CompanyName;
            var query = db.Customers.Select(c => c.CompanyName);

            lbxCustomersEx1.ItemsSource = query.ToList();
        }
        #endregion

        #region Ex2
        //Ex 2 - Customer Objects
        private void btnQueryEx2_Click(object sender, RoutedEventArgs e)
        {
            //var query = from c in db.Customers
            //            select c;
            var query = db.Customers.Select(c => c);

            dgCustomersEx2.ItemsSource = query.ToList();
        }
        #endregion
    }
}
