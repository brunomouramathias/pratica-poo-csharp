using System;
using PraticaPoo.Domain.ValueObjects;
using PraticaPoo.Domain;

namespace PraticaPoo.Domain.Entities
{
    /// <summary>
    /// Represents a product available for sale. A product has a name, a price and a quantity
    /// in stock. Negative prices or negative quantities are not allowed. Business rules
    /// such as insufficient stock are enforced via <see cref="DomainException"/>.
    /// </summary>
    public class Product
    {
        public Guid Id { get; }
        private string _name;
        private Money _price;
        private int _quantity;

        public string Name
        {
            get => _name;
            set
            {
                _name = string.IsNullOrWhiteSpace(value)
                    ? throw new ArgumentException("Name must not be empty.", nameof(value))
                    : value.Trim();
            }
        }

        public Money Price
        {
            get => _price;
            set => _price = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// The quantity in stock. Always non‑negative.
        /// </summary>
        public int Quantity => _quantity;

        public Product(string name, Money price, int initialQuantity = 0)
        {
            Id = Guid.NewGuid();
            Name = name; // uses setter guard
            Price = price ?? throw new ArgumentNullException(nameof(price));
            if (initialQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialQuantity), initialQuantity, "Initial quantity must be non‑negative.");
            _quantity = initialQuantity;
        }

        /// <summary>
        /// Increases the quantity in stock by the specified amount. Negative values are not allowed.
        /// </summary>
        public void AddStock(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount to add must be non‑negative.");
            _quantity += amount;
        }

        /// <summary>
        /// Removes the specified amount from stock. Throws a <see cref="DomainException"/>
        /// if there is insufficient stock. Negative values are not allowed.
        /// </summary>
        public void RemoveStock(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount to remove must be non‑negative.");
            if (amount > _quantity)
                throw new DomainException($"Insufficient stock. Requested: {amount}, available: {_quantity}.");
            _quantity -= amount;
        }

        /// <summary>
        /// Returns a human‑readable summary of the product.
        /// </summary>
        public override string ToString() => $"{Name}: {Price} (Stock: {Quantity})";
    }
}
