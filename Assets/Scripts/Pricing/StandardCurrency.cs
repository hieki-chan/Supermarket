﻿using System;
using UnityEngine;

namespace Supermarket.Pricing
{
    [Serializable]
    public struct StandardCurrency
    {
        public static readonly StandardCurrency zero = new StandardCurrency();

        public float Value { get => value; set { this.value = value; Rounded(); } }

        [SerializeField] private float value;

        public override string ToString()
        {
            return $"${Value:F2}";
        }

        /*        public static C ToStandard<C>(int value)
                {
                    return null;
                }*/

        public float Rounded()
        {
            return value = Mathf.Round(value * 100f) / 100f;
        }

        #region OVERRIDES
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return value.GetHashCode();
            }
        }

        #endregion

        #region OPERATORS

        public static StandardCurrency operator +(StandardCurrency value1, StandardCurrency value2)
        {
            return new StandardCurrency() { Value = value1.Value + value2.Value };
        }

        public static StandardCurrency operator -(StandardCurrency value1, StandardCurrency value2)
        {
            return new StandardCurrency() { Value = value1.Value - value2.Value };
        }

        public static StandardCurrency operator *(StandardCurrency value1, float f)
        {
            return new StandardCurrency() { Value = value1.Value * f };
        }

        public static bool operator ==(StandardCurrency val1, StandardCurrency val2)
        {
            return val1.Value == val2.Value;
        }

        public static bool operator !=(StandardCurrency val1, StandardCurrency val2)
        {
            return val1.Value != val2.Value;
        }

        public static bool operator >(StandardCurrency value1, StandardCurrency value2)
        {
            return value1.Value > value2.Value;
        }

        public static bool operator <(StandardCurrency value1, StandardCurrency value2)
        {
            return value1.Value < value2.Value;
        }

        #endregion

        #region IMPLICITS

        public static implicit operator float(StandardCurrency value)
        {
            return value.Value;
        }

        public static implicit operator StandardCurrency(float v)
        {
            return new StandardCurrency()
            {
                Value = v,
            };
        }

        public static implicit operator string(StandardCurrency c)
        {
            return c.ToString();
        }

        #endregion
    }
}
