using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Entity_Data_Model;

namespace Entity_Data_Model
{
    public class Operations
    {

        public int categoryId;
        public string categoryName;
        public string description;
        public int productID;
        public string productName;
        public int Unitprice;
        public int product_CategoryID;
       
        
        NORTHWNDEntities northwindEntities = new NORTHWNDEntities();



        public void mainMenu()
        {


            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("ADO.NET Entity Data Model");
                Console.WriteLine("Please choose an option");
                Console.WriteLine("1- CREATE");
                Console.WriteLine("2- READ");
                Console.WriteLine("3- UPDATE");
                Console.WriteLine("4- DELETE");
                Console.WriteLine("5- REDIS TIME");
                Console.WriteLine("6- SQL TIME");
                Console.WriteLine("7- EXIT");

                int choice = 0;

                while (true)
                {
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        if (choice >= 1 && choice <= 5)
                            break;
                        else
                            Console.WriteLine("Invalid number! Please enter a number between 1 and 5.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input! Please enter a valid number.");
                    }
                }

                if (choice == 7)
                {
                    exit = true;
                    continue;
                }

                string tableName;

                Console.WriteLine(" Category or Product ? ");
                tableName = Console.ReadLine();

                switch (choice)
                {
                    case 1:
                        if (tableName == "product" || tableName == "Product")
                        {
                            addProduct();
                        }
                        else
                        {
                            createCategory();
                        }

                        break;

                    case 2:
                        if (tableName == "product" || tableName == "Product")
                        {
                            readProduct();
                        }
                        else
                        {
                            readCategory();
                        }
                        break;

                    case 3:
                        if (tableName == "product" || tableName == "Product")
                        {
                            updateProduct();
                        }
                        else
                        {
                            updateCategory();
                        }
                        break;

                    case 4:
                        if (tableName == "product" || tableName == "Product")
                        {
                            deleteProduct();
                        }
                        else
                        {
                            deleteCategory();
                        }
                        break;
                    case 5:
                        ConnectRedis redis = new ConnectRedis();
                        redis.getDataFromRedis();
                        break;
                    case 6:
                        measureSqlTime();
                        break;

                    default:
                        Console.WriteLine("Invalid number!");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Exiting the program.");

        }


        public void createCategory()
        {
            Categories category = new Categories();
            Console.WriteLine("Enter  category name ");
            categoryName = Console.ReadLine();
            category.CategoryName = categoryName;

            Console.WriteLine("Enter description ");
            description = Console.ReadLine();
            category.Description = description;

            northwindEntities.Categories.Add(category);
            northwindEntities.SaveChanges();
            Console.WriteLine("Category inserted successfully. ");
            Console.ReadLine();
        }


        public void readCategory()
        {

            var LinqRead = from c in northwindEntities.Categories
                           select c;

            if (LinqRead.Any()) //if (LinqRead != null) programı kapatıyor çünkü boş koleksiyon döndürüyor ve foreach e direkt giriyor. Bu method
            {                   //Any() metodu, koleksiyonun içinde en az bir öğe olduğunu kontrol eder.
                foreach (var item in LinqRead)
                {
                    Console.WriteLine("Category Id: " + item.CategoryID);
                    Console.WriteLine("Category Name: " + item.CategoryName);
                    Console.WriteLine("Description: " + item.Description + "\n");
                }
            }
            else
            {
                Console.WriteLine("Record not found. ");
            }
        }




        public void updateCategory()
        {

            Console.WriteLine("Enter category id : ");
            categoryId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter  category name ");
            categoryName = Console.ReadLine();

            Console.WriteLine("Enter description ");
            description = Console.ReadLine();

            var LinqUpdate = (from c in northwindEntities.Categories
                              where c.CategoryID == categoryId
                              select c).FirstOrDefault(); // eşlesen ilk veriyi almayı sağlıyor

            if (LinqUpdate != null)
            {
                LinqUpdate.CategoryName = categoryName;
                LinqUpdate.Description = description;
                northwindEntities.SaveChanges();
                Console.WriteLine("Category updated successfully. ");

            }
            else
            {
                Console.WriteLine("Record not found. ");
            }


        }


        public void deleteCategory()
        {
            Console.WriteLine("Enter category id : ");
            categoryId = Convert.ToInt32(Console.ReadLine());



            var LinqResult = (from c in northwindEntities.Categories where c.CategoryID == categoryId select c).FirstOrDefault();

            if (LinqResult != null)
            {
                northwindEntities.Categories.Remove(LinqResult);
                northwindEntities.SaveChanges();
                Console.WriteLine("Category deleted successfully. ");
            }
            else
            {
                Console.WriteLine("Record not found. ");
            }
        }

        public void showMenu()
        {
            var LinqRead = from c in northwindEntities.Categories
                           select c;


            foreach (var item in LinqRead)
            {
                Console.WriteLine("Category Id: " + item.CategoryID);
                Console.WriteLine("Category Name: " + item.CategoryName);

            }



        }


        public void addProduct()
        {
            Products product = new Products();

            Console.WriteLine("Enter  product name ");
            productName = Console.ReadLine();
            product.ProductName = productName;

            Console.WriteLine("Enter unit price : ");
            Unitprice = Convert.ToInt32(Console.ReadLine());
            product.UnitPrice = Unitprice;

            Console.WriteLine("Please choose a category ID " + "\n");

            showMenu();

            product_CategoryID = Convert.ToInt32(Console.ReadLine());
            product.CategoryID = product_CategoryID;


            northwindEntities.Products.Add(product);
            northwindEntities.SaveChanges();
            Console.WriteLine("Product inserted successfully. ");
            Console.ReadLine();

        }




        public void readProduct()
        {

            var LinqRead = from c in northwindEntities.Products
                           select c;

            if (LinqRead.Any())
            {
                foreach (var item in LinqRead)
                {
                    Console.WriteLine("Product id: " + item.ProductID);
                    Console.WriteLine("Product Name: " + item.ProductName);
                    Console.WriteLine("Unit Price: " + item.UnitPrice + "\n");
                }
            }
            else
            {
                Console.WriteLine("Record not found. ");
            }
        }


        public void updateProduct()
        {

            Console.WriteLine("Enter Product id : ");
            productID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter  Product name ");
            productName = Console.ReadLine();

            Console.WriteLine("Enter Unit Price ");
            Unitprice = Convert.ToInt32(Console.ReadLine());

            var LinqUpdate = (from c in northwindEntities.Products
                              where c.ProductID == productID
                              select c).FirstOrDefault(); // eşlesen ilk veriyi almayı sağlıyor

            if (LinqUpdate != null)
            {
                LinqUpdate.ProductName = productName;
                LinqUpdate.UnitPrice = Unitprice;
                northwindEntities.SaveChanges();
                Console.WriteLine("Product updated successfully. ");

            }
            else
            {
                Console.WriteLine("Record not found. ");
            }


        }



        public void deleteProduct()
        {
            Console.WriteLine("Enter product id : ");
            productID = Convert.ToInt32(Console.ReadLine());



            var LinqResult = (from c in northwindEntities.Products where c.ProductID == productID select c).FirstOrDefault();

            if (LinqResult != null)
            {
                northwindEntities.Products.Remove(LinqResult);
                northwindEntities.SaveChanges();
                Console.WriteLine("Category deleted successfully. ");
            }
            else
            {
                Console.WriteLine("Record not found. ");
            }
        }

        public void measureSqlTime()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var product = (from c in northwindEntities.Products
                           where c.ProductID == 1
                           select c).FirstOrDefault();

            stopwatch.Stop();


            Console.WriteLine("Product Name: " + product.ProductName);


            Console.WriteLine("Sql time: " + stopwatch.ElapsedMilliseconds + " millisecond");
            Console.ReadLine();
        }






    }
}
