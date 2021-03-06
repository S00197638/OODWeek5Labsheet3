﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        #region GitHub Repo Link
        //https://github.com/S00197638/OODWeek5Labsheet3
        #endregion

        #region ShowProducts Method
        private void ShowProducts(DataGrid currentGrid)
        {
            //Query
            //var query = from p in db.Products
            //            where p.Category.CategoryName.Equals("Beverages")
            //            orderby p.ProductID descending
            //            select new
            //            {
            //                p.ProductID,
            //                p.ProductName,
            //                p.Category.CategoryName,
            //                p.UnitPrice
            //            };

            //Lambda
            var query = db.Products
                .Where(p => p.Category.CategoryName.Equals("Beverages"))
                .OrderByDescending(p => p.ProductID)
                .Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.Category.CategoryName,
                    p.UnitPrice
                });

            currentGrid.ItemsSource = query.ToList();
        }
        #endregion

        #region Ex1
        //Ex 1 - Customer Names
        private void btnQueryEx1_Click(object sender, RoutedEventArgs e)
        {
            //Query
            //var query = from c in db.Customers
            //            select c.CompanyName;

            //Lambda
            var query = db.Customers.Select(c => c.CompanyName);

            lbxCustomersEx1.ItemsSource = query.ToList();
        }
        #endregion

        #region Ex2
        //Ex 2 - Customer Objects
        private void btnQueryEx2_Click(object sender, RoutedEventArgs e)
        {
            //Query
            //var query = from c in db.Customers
            //            select c;

            //Lambda
            var query = db.Customers.Select(c => c);

            dgCustomersEx2.ItemsSource = query.ToList();
        }
        #endregion

        #region Ex3
        //Ex 3 - Order Information
        private void btnQueryEx3_Click(object sender, RoutedEventArgs e)
        {
            //Query
            //var query = from o in db.Orders
            //            where o.Customer.City.Equals("London") 
            //            || o.Customer.City.Equals("Paris") 
            //            || o.Customer.Country.Equals("USA")
            //            orderby o.Customer.CompanyName
            //            select new
            //            {
            //                CustomerName = o.Customer.CompanyName,
            //                City = o.Customer.City,
            //                Address = o.ShipAddress
            //            };

            //Lambda
            var query = db.Orders
                .Where(o => o.Customer.City.Equals("London")
                        || o.Customer.City.Equals("Paris")
                        || o.Customer.Country.Equals("USA"))
                .OrderBy(o => o.Customer.CompanyName)
                .Select(o => new 
                { 
                    CustomerName = o.Customer.CompanyName, 
                    City = o.Customer.City, 
                    Address = o.ShipAddress 
                });

            dgCustomersEx3.ItemsSource = query.ToList().Distinct();
        }
        #endregion

        #region Ex4
        //Ex 4 - Product Information
        private void btnQueryEx4_Click(object sender, RoutedEventArgs e)
        {
            //Query
            //var query = from p in db.Products
            //            where p.Category.CategoryName.Equals("Beverages")
            //            orderby p.ProductID descending
            //            select new
            //            {
            //                p.ProductID,
            //                p.ProductName,
            //                p.Category.CategoryName,
            //                p.UnitPrice
            //            };

            //Lambda
            //var query = db.Products
            //    .Where(p => p.Category.CategoryName.Equals("Beverages"))
            //    .OrderByDescending(p => p.ProductID)
            //    .Select(p => new
            //    {
            //        p.ProductID,
            //        p.ProductName,
            //        p.Category.CategoryName,
            //        p.UnitPrice
            //    });

            //dgCustomersEx4.ItemsSource = query.ToList();

            ShowProducts(dgCustomersEx4);//As this is the same as above
        }
        #endregion

        #region Ex5
        //Ex 5 - Insert Information
        private void btnQueryEx5_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product()
            {
                ProductName = "Kickapoo Jungle Joy Juice",
                UnitPrice = 12.49m,
                CategoryID = 1
            };

            db.Products.Add(p);
            db.SaveChanges();

            ShowProducts(dgCustomersEx5);
        }
        #endregion

        #region Ex6
        //Ex 6 - Update Product Information
        private void btnQueryEx6_Click(object sender, RoutedEventArgs e)
        {
            //Lambda
            Product p1 = (db.Products
                .Where(p => p.ProductName.StartsWith("Kick"))
                .Select(p => p)).First();

            p1.UnitPrice = 100m;

            db.SaveChanges();
            ShowProducts(dgCustomersEx6);
        }
        #endregion

        #region Ex7
        //Ex 7 - Multiple Update
        private void btnQueryEx7_Click(object sender, RoutedEventArgs e)
        {
            //Query
            //var products = from p in db.Products
            //               where p.ProductName.StartsWith("Kick")
            //               select p;

            //Lambda
            var products = (db.Products
                .Where(p => p.ProductName.StartsWith("Kick"))
                .Select(p => p));

            foreach (var item in products)
                item.UnitPrice = 100m;

            db.SaveChanges();
            ShowProducts(dgCustomersEx7);
        }
        #endregion

        #region Ex8
        //Ex 8 - Delete
        private void btnQueryEx8_Click(object sender, RoutedEventArgs e)
        {
            //Query
            //var products = from p in db.Products
            //               where p.ProductName.StartsWith("Kick")
            //               select p;

            //Lambda
            var products = (db.Products
                .Where(p => p.ProductName.StartsWith("Kick"))
                .Select(p => p));

            db.Products.RemoveRange(products);
            db.SaveChanges();
            ShowProducts(dgCustomersEx8);
        }
        #endregion

        #region Ex9
        //Ex 9 - Stored Procedure
        private void btnQueryEx9_Click(object sender, RoutedEventArgs e)
        {
            var query = db.Customers_By_City("London");

            dgCustomersEx9.ItemsSource = query.ToList();
        }
        #endregion
    }
}
