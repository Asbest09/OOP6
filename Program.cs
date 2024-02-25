using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOP6
{
    class Program
    {
        static void Main(string[] args)
        {
            const string BuyProduct = "buy";
            const string ShowProducts = "show";
            const string Exit = "exit";

            Seller seller = new Seller();

            bool isWorking = true;

            Console.WriteLine("Введите ваш баланс");
            int money = Convert.ToInt32(Console.ReadLine());

            while(isWorking)
            {
                Console.WriteLine($"Введите {BuyProduct} для покупки товара, {ShowProducts} для просмотра инвенторя, {Exit} для выхода");
                string input = Console.ReadLine();

                switch (input)
                {
                    case BuyProduct:
                        money = seller.SellProduct(money);
                        break;
                    case ShowProducts:
                        seller.ShowProducts();
                        break;
                    case Exit:
                        Console.WriteLine("Программа завершена");

                        isWorking = false;

                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    class Seller
    {
        public List<Product> PlayerProducts = new List<Product>();
        public List<Product> SellerProducts = new List<Product>() { new Product(50, "Батон", "10.02.24"), new Product(200, "Сметана", "07.02.24"), new Product(1000, "Конструктор", "02.12.23"), new Product(2000000, "Hyundai Solaris", "27.04.23"), new Product(500000000, "Сабля Наполеона", "1812") };

        public void ShowProducts(bool itIsPlayer = true)
        {
            if(itIsPlayer)
            {
                if (PlayerProducts.Count == 0)
                    Console.WriteLine("Нет предметов");
                else
                {
                    for (int i = 0; i < PlayerProducts.Count; i++)
                        PlayerProducts[i].ShowProduct(i);
                }
            }
            else
            {
                if (SellerProducts.Count == 0)
                    Console.WriteLine("Нет предметов");
                else
                {
                    for (int i = 0; i < SellerProducts.Count; i++)
                        SellerProducts[i].ShowProduct(i);
                }
            }
        }
        public int SellProduct(int money)
        {
            Console.WriteLine("Введите индекс товара");
            ShowProducts(false);
            int index = Convert.ToInt32(Console.ReadLine()) - 1;

            if(index >= SellerProducts.Count || index < 0 || money < SellerProducts[index].Price)
                Console.WriteLine("Неверный индекс или Вам не хватает средств");
            else
            {
                money -= SellerProducts[index].Price;

                Console.WriteLine("Вы купили " + SellerProducts[index].Name + " за " + SellerProducts[index].Price + ", ваш баланс - " + money);

                PlayerProducts.Add(SellerProducts[index]);
                SellerProducts.RemoveAt(index);
            }

            return money;
        }
    }

    class Product
    {
        public int Price { get; private set; }
        public string Name { get; private set; }
        public string DateOfManufacture { get; private set; }

        public Product(int price, string name, string dateOfManufacture) 
        {
            Price = price;
            Name = name;
            DateOfManufacture = dateOfManufacture;
        }

        public void ShowProduct(int i)
        {  
            Console.WriteLine($"{i + 1}. {Name} - {Price} - {DateOfManufacture}");
        }
    }
}
