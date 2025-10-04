using System;
using PraticaPoo.Domain.ValueObjects;

namespace PraticaPoo.Domain.Entities
{
    /// <summary>
    /// Represents a single line of an order, referencing a <see cref="Product"/> and the
    /// quantity purchased. Calculates the subtotal for this item by multiplying the
    /// product price by the quantity.
    /// </summary>
    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public OrderItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be positive.");
            Quantity = quantity;
        }

        /// <summary>
        /// Calculates the subtotal for this order item.
        /// </summary>
        public Money SubTotal() => Product.Price.Multiply(Quantity);

        public override string ToString() => $"{Product.Name} x{Quantity}: {SubTotal()}";
    }
}
