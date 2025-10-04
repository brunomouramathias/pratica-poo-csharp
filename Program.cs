using System;
using PraticaPoo.Domain.Entities;
using PraticaPoo.Domain.ValueObjects;
using PraticaPoo.Utils;

namespace PraticaPoo
{
    internal static class Program
    {
        private static void Main()
        {
            // Create a product with an initial stock. This will throw if the initial quantity is negative.
            var product = new Product("Café Especial", new Money(25.90m, "BRL"), initialQuantity: 50);

            Console.WriteLine(product);

            // Demonstrate adding and removing stock.
            product.AddStock(20);
            product.RemoveStock(10);
            Console.WriteLine($"After adjustments: {product}");

            // Try to remove more stock than available to show domain exception handling.
            try
            {
                product.RemoveStock(1000);
            }
            catch (DomainException ex)
            {
                Console.WriteLine($"Domain rule violation: {ex.Message}");
            }

            // Build an order with two items.
            var order = new Order();
            order.AddItem(product, 2);
            var otherProduct = new Product("Filtro de Papel", new Money(5.50m, "BRL"), 200);
            order.AddItem(otherProduct, 5);
            Console.WriteLine(order);

            // Show use of extension methods.
            int x = 5;
            Console.WriteLine($"{x} squared is {x.Square()} and clamped between 0 and 10 is {x.Clamp(0, 10)}");
        }
    }
}
