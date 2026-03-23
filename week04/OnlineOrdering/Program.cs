using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create first order with USA customer
        Address address1 = new Address("123 Main Street", "Salt Lake City", "UT", "USA");
        Customer customer1 = new Customer("John Smith", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "P001", 999.99, 1));
        order1.AddProduct(new Product("Mouse", "P002", 25.50, 2));
        order1.AddProduct(new Product("Keyboard", "P003", 75.00, 1));

        // Create second order with international customer
        Address address2 = new Address("456 Oak Avenue", "Toronto", "Ontario", "Canada");
        Customer customer2 = new Customer("Emily Johnson", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Headphones", "P004", 149.99, 1));
        order2.AddProduct(new Product("Monitor", "P005", 299.00, 2));

        // Create third order with USA customer
        Address address3 = new Address("789 Pine Road", "New York", "NY", "USA");
        Customer customer3 = new Customer("Sarah Williams", address3);

        Order order3 = new Order(customer3);
        order3.AddProduct(new Product("Webcam", "P006", 89.99, 1));
        order3.AddProduct(new Product("USB Hub", "P007", 35.00, 3));
        order3.AddProduct(new Product("HDMI Cable", "P008", 15.99, 2));

        // Display order details
        List<Order> orders = new List<Order> { order1, order2, order3 };

        foreach (Order order in orders)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine();
            Console.WriteLine($"Total Price: ${order.GetTotalCost():F2}");
            Console.WriteLine("=================================================");
            Console.WriteLine();
        }
    }
}