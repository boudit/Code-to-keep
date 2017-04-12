//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Validatable{T}.cs" company="Eurofins">
//    Copyright (c) Eurofins. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Extensions.Types
{
    using System;

    using Extensions.Primitives;

    public class Validatable<T> : IComparable, IComparable<Validatable<T>>, IComparable<T>
        where T : struct, IComparable
    {
        #region Constructors and Destructors

        internal Validatable(T value)
        {
            this.InternalValue = value;
        }

        internal Validatable(T? value)
        {
            this.InternalValue = value;
        }

        internal Validatable(string parsedText)
        {
            this.ParsedText = parsedText;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get a value indicating if this instance has been created from an existing <see cref="T"/> value.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.InternalValue.HasValue;
            }
        }

        /// <summary>
        /// Get the <see cref="T"/> value retrieved from ParsedText property.
        /// <para><see cref="InvalidOperationException" /> : throws when IsValid is False.</para>
        /// </summary>
        public T Value
        {
            get
            {
                if (!this.IsValid)
                {
                    throw new InvalidOperationException();
                }

                return this.InternalValue.Value;
            }
        }

        /// <summary>
        /// Get the string value used to parse the <see cref="T"/> value.
        /// </summary>
        public string ParsedText { get; private set; }

        internal T? InternalValue { get; set; }

        #endregion

        #region Implicit Operators

        public static implicit operator T(Validatable<T> value)
        {
            return value.Value;
        }

        public static implicit operator Validatable<T>(T value)
        {
            return new Validatable<T>(value);
        }

        public static implicit operator Validatable<T>(T? value)
        {
            return new Validatable<T>(value);
        }

        public static implicit operator Validatable<T>(string stringValue)
        {
            var result = new Validatable<T>(stringValue);

            T value;

            if (stringValue.TryConvert(out value))
            {
                result.InternalValue = value;
            }

            return result;
        }

        public static implicit operator string(Validatable<T> value)
        {
            return value.ToString();
        }

        public static bool operator ==(Validatable<T> wrapper, T value)
        {
            if (wrapper == null || !wrapper.IsValid)
            {
                return false;
            }

            return wrapper.Value.Equals(value);
        }

        public static bool operator !=(Validatable<T> wrapper, T value)
        {
            return !(wrapper == value);
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return this.IsValid ? this.Value.GetHashCode() : this.ParsedText.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            return this.CompareTo(obj as Validatable<T>);
        }

        public int CompareTo(T other)
        {
            return this.CompareTo((Validatable<T>)other);
        }

        public int CompareTo(Validatable<T> other)
        {
            if (other == null || !other.IsValid)
            {
                return -1;
            }

            if (!this.IsValid)
            {
                return 1;
            }

            return this.Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            if (!this.IsValid)
            {
                throw new InvalidOperationException();
            }

            return this.Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (!this.IsValid)
            {
                return false;
            }

            var typedObj = obj as Validatable<T>;
            if (typedObj == null || !typedObj.IsValid)
            {
                return false;
            }

            return this.Value.Equals(typedObj.Value);
        }

        #endregion
    }
}