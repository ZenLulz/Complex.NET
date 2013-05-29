﻿/*
* Complex.NET
* https://github.com/ZenLulz/Complex.NET
*
* Copyright 2013 ZenLulz ~ Jämes Ménétrey
* Released under the MIT license
*
* Date: 2013-05-29
*/

using System;
using System.Runtime.InteropServices;

namespace Binarysharp.Maths
{
    /// <summary>
    /// Represents a complex number.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Complex : IEquatable<Complex>
    {
        #region Properties
        #region Argument
        /// <summary>
        /// Gets the argument of the complex number.
        /// </summary>
        public double Argument
        {
            get { return Math.Atan2(Imaginary, Real); }
        }
        #endregion
        #region Imaginary
        /// <summary>
        /// Gets the imaginary part of the complex number.
        /// </summary>
        public double Imaginary { get; private set; }
        #endregion
        #region Modulus
        /// <summary>
        /// Gets the modulus of the complex number.
        /// </summary>
        public double Modulus
        {
            get { return Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2)); }
        }
        #endregion
        #region Quadrant
        /// <summary>
        /// Gets a value indicating in which quadrant the complex number is.
        /// </summary>
        public int Quadrant
        {
            get
            {
                if (Real >= 0 && Imaginary >= 0)
                    return 1;
                if (Real < 0 && Imaginary >= 0)
                    return 2;
                if (Real < 0 && Imaginary < 0)
                    return 3;
                return 4;
            }
        }
        #endregion
        #region Real
        /// <summary>
        /// Gets the real part of the complex number.
        /// </summary>
        public double Real { get; private set; }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of <see cref="Complex"/> using the specified real and imaginary values.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        /// <param name="imaginary">The imaginary part of the complex number.</param>
        public Complex(double real, double imaginary)
            : this()
        {
            // Save the parameters
            Real = real;
            Imaginary = imaginary;
        }
        #endregion

        #region Methods
        #region Conjugate
        /// <summary>
        /// Computes the conjugate of a complex number and returns the result.
        /// </summary>
        /// <returns>The conjugate of the complex number.</returns>
        public Complex Conjugate()
        {
            return new Complex(Real, -Imaginary);
        }
        #endregion
        #region Equals
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        public bool Equals(Complex other)
        {
            return Imaginary.Equals(other.Imaginary) && Real.Equals(other.Real);
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Complex && Equals((Complex)obj);
        }
        #endregion
        #region FromPolarCoordinates (static)
        /// <summary>
        /// Creates a complex number from a point's polar coordinates.
        /// </summary>
        /// <param name="modulus">The magnitude, which is the distance from the origin (the intersection of the x-axis and the y-axis) to the number.</param>
        /// <param name="argument">The phase, which is the angle from the line to the horizontal axis, measured in radians.</param>
        /// <returns>A complex number.</returns>
        public static Complex FromPolarCoordinates(double modulus, double argument)
        {
            return new Complex(Math.Round(modulus * Math.Cos(argument), 15), Math.Round(modulus * Math.Sin(argument), 15));
        }
        #endregion
        #region GetHashCode
        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Imaginary.GetHashCode() * 397) ^ Real.GetHashCode();
            }
        }
        #endregion
        #region Operator Overloading
        /// <summary>
        /// Overloads the equality operator.
        /// </summary>
        public static bool operator ==(Complex left, Complex right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Overloads the inequality operator.
        /// </summary>
        public static bool operator !=(Complex left, Complex right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// Overloads the addition operator.
        /// </summary>
        public static Complex operator +(Complex left, Complex right)
        {
            return new Complex(left.Real + right.Real, left.Imaginary + right.Imaginary);
        }
        /// <summary>
        /// Overloads the subtraction operator.
        /// </summary>
        public static Complex operator -(Complex left, Complex right)
        {
            return new Complex(left.Real - right.Real, left.Imaginary - right.Imaginary);
        }
        /// <summary>
        /// Overloads the multiplication operator.
        /// </summary>
        public static Complex operator *(Complex left, Complex right)
        {
            var real = (left.Real * right.Real) - (left.Imaginary * right.Imaginary);
            var imaginary = (left.Real * right.Imaginary) + (left.Imaginary * right.Real);
            return new Complex(real, imaginary);
        }
        /// <summary>
        /// Overloads the division operator.
        /// </summary>
        public static Complex operator /(Complex left, Complex right)
        {
            var numerator = left * right.Conjugate();
            var denominator = Math.Pow(right.Real, 2) + Math.Pow(right.Imaginary, 2);
            return new Complex(numerator.Real / denominator, numerator.Imaginary / denominator);
        }
        #endregion
        #region ToString
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            // If the imaginary number is missing
            if (Imaginary.Equals(0))
                return string.Format("{0}", Real);
            // If the real number is missing
            if (Real.Equals(0))
                return string.Format("{0}i", Imaginary);
            // If both exist
            return string.Format("{0}{1}{2}i", Real, Imaginary > 0 ? "+" : "", Imaginary);
        }
        #endregion
        #endregion
    }
}