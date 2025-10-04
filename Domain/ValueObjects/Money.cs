using System;

namespace PraticaPoo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with currency. This class is immutable and all
    /// operations return new instances. Negative amounts are not allowed; use subtraction
    /// operations to model debits instead of creating a Money with a negative amount.
    /// </summary>
    public sealed class Money : IEquatable<Money>
    {
        /// <summary>
        /// The numeric amount of money.
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// The ISO currency code (e.g. "BRL", "USD"). Stored as uppercase.
        /// </summary>
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency code must not be null or whitespace.", nameof(currency));
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount must be non‑negative.");

            Amount = amount;
            Currency = currency.ToUpperInvariant();
        }

        /// <summary>
        /// Adds two monetary values. Currencies must match.
        /// </summary>
        public Money Add(Money other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        /// <summary>
        /// Subtracts another monetary value from this one. Currencies must match and the
        /// result cannot be negative.
        /// </summary>
        public Money Subtract(Money other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            EnsureSameCurrency(other);
            if (other.Amount > Amount)
                throw new ArgumentOutOfRangeException(nameof(other), other.Amount, "Cannot subtract more than the current amount.");
            return new Money(Amount - other.Amount, Currency);
        }

        /// <summary>
        /// Multiplies this monetary value by a scalar. The result cannot be negative.
        /// </summary>
        public Money Multiply(decimal factor)
        {
            if (factor < 0)
                throw new ArgumentOutOfRangeException(nameof(factor), factor, "Factor must be non‑negative.");
            return new Money(Amount * factor, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            if (!string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Cannot operate on two Money instances with different currencies (\"{Currency}\" vs \"{other.Currency}\").");
        }

        public override bool Equals(object? obj) => Equals(obj as Money);

        public bool Equals(Money? other)
        {
            if (other is null) return false;
            return Amount == other.Amount && string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency.ToUpperInvariant());
        }

        public override string ToString()
        {
            // Use string interpolation with escaped braces to satisfy the patch parser.
            return string.Format("{0:0.00} {1}", Amount, Currency);
        }

        public static bool operator ==(Money? left, Money? right) => Equals(left, right);
        public static bool operator !=(Money? left, Money? right) => !Equals(left, right);
    }
}
