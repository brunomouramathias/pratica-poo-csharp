using System;
using System.Collections.Generic;
using System.Linq;
using PraticaPoo.Domain.ValueObjects;

namespace PraticaPoo.Domain.Entities
{
    /// <summary>
    /// Represents an order containing multiple <see cref="OrderItem"/>s. Supports adding
    /// items and computing the total. This class enforces that each product appears only once.
    /// </summary>
    public class Order
    {
        private readonly List<OrderItem> _items = new();

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Guid Id { get; } = Guid.NewGuid();

        public void AddItem(Product product, int quantity)
        {
            if (product is null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be positive.");

            // If the product is already in the order, update the quantity rather than duplicating the line.
            var existingItem = _items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existingItem != null)
            {
                // Remove the old item and add a new one with the combined quantity. Using a new instance
                // ensures immutability of the OrderItem type.
                _items.Remove(existingItem);
                var newQuantity = existingItem.Quantity + quantity;
                _items.Add(new OrderItem(product, newQuantity));
            }
            else
            {
                _items.Add(new OrderItem(product, quantity));
            }
        }

        /// <summary>
        /// Calculates the total value of this order by summing all subtotals.
        /// </summary>
        public Money Total()
        {
            if (_items.Count == 0)
            {
                // A zero-valued money object in a default currency (e.g. BRL). In a real system the
                // currency would likely be inferred from user context or order metadata.
                return new Money(0, "BRL");
            }
            var firstCurrency = _items[0].SubTotal().Currency;
            decimal sum = 0;
            foreach (var item in _items)
            {
                var sub = item.SubTotal();
                if (!string.Equals(sub.Currency, firstCurrency, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Cannot compute total for items with different currencies.");
                sum += sub.Amount;
            }
            return new Money(sum, firstCurrency);
        }

        public override string ToString() => $"Order {Id}: {string.Join(", ", _items.Select(i => i.ToString()))} (Total: {Total()})";
    }
}
