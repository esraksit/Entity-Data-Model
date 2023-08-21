using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Diagnostics;
using Entity_Data_Model;


namespace Entity_Data_Model
{
    public class ConnectRedis
    {
        NORTHWNDEntities northwindEntities = new NORTHWNDEntities();
        Operations operations = new Operations();

        ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect("localhost");

        public void saveRedis()
        {
            var LinqRead = from c in northwindEntities.Products
                           select c;

            IDatabase db = redisConn.GetDatabase();

            operations.readProduct();

            foreach (var product in LinqRead)
            {

                if (db.KeyExists($"product:{product.ProductID}") == false)
                {

                    db.StringSet($"product:{product.ProductID}", product.ProductName);
                    Console.WriteLine($"Product {product.ProductName} added to Redis.");
                }
                else
                {
                    Console.WriteLine($"Product {product.ProductName} already exists in Redis.");
                }
            }


        }


        public void getDataFromRedis()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            IDatabase db = redisConn.GetDatabase();

            // Example: Fetching data with a specific key from Redis
            RedisValue productName = db.StringGet("product:1"); // Assuming product ID is 1

            if (!productName.IsNullOrEmpty)
            {
                Console.WriteLine("Product Name from Redis: " + productName);
            }
            else
            {
                Console.WriteLine("Product not found in Redis.");
            }

            stopwatch.Stop();
            Console.WriteLine("Redis time: " + stopwatch.ElapsedMilliseconds + " millisecond");
            Console.WriteLine();

        }




    }
}
