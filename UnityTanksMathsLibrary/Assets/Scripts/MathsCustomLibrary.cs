using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// REFERENCES
// https://stackoverflow.com/questions/33044848/c-sharp-lerping-from-position-to-position
// https://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
// http://james-ramsden.com/calculate-the-cross-product-c-code/
// https://en.wikipedia.org/wiki/Cross_product
// https://www.wolframalpha.com/input/?i=cross+product
// https://www.geeksforgeeks.org/approximation-algorithms/
// https://docs.unity3d.com/ScriptReference/Vector3.html
// https://docs.unity3d.com/ScriptReference/Vector2.html
// https://gamedev.stackexchange.com/questions/70252/how-can-i-project-a-vector-on-another-vector
// https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/types/namespaces
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct
// https://www.tutorialsteacher.com/csharp/csharp-multi-dimensional-array
// https://www.cuemath.com/algebra/transpose-of-a-matrix/

namespace MathsCustomLibrary
{
    public class DivideByZeroException : Exception
    {
        public DivideByZeroException() { }

        public DivideByZeroException(string message) : base(message) { }

        public DivideByZeroException(string message, Exception inner) : base(message, inner) { }
    }

    [SerializeField]
    public class Maths
    {
        // Negative infinity = -infinity(?)
        // Positive infinity = infinity(?)
        private const float pi = 3.1415926535897931f;
        private const float epsilon = Single.Epsilon;
        private const float deg2Rad = (pi * 2) / 360;
        private const float rad2Deg = 360 / (pi * 2);

        /// <summary>
        /// Returns positive infinity
        /// </summary>
        public const float Infinity = Single.PositiveInfinity;
        /// <summary>
        /// Returns Negative infinity
        /// </summary>
        public const float NegativeInfinity = Single.NegativeInfinity;

        /// <summary>
        /// Returns PI to 16 digits
        /// </summary>
        public static float PI { get => pi; }
        /// <summary>
        /// Returns the smallest positive double value that is greater than zero
        /// </summary>
        public static float Epsilon { get => epsilon; }
        /// <summary>
        /// Returns degrees to radius
        /// </summary>
        public static float Deg2Rad { get => deg2Rad; }
        /// <summary>
        /// Returns radius to degrees
        /// </summary>
        public static float Rad2Deg { get => rad2Deg; }

        /// <summary>
        /// Returns the higher of 2 numbers
        /// </summary>
        public static Func<float, float, float> Max = (float a, float b) => a > b ? a : b; // if a > b return a else b
        /// <summary>
        /// Returns the smallest of 2 numbers
        /// </summary>
        public static Func<float, float, float> Min = (float a, float b) => a < b ? a : b; // if a < b return a else b
        /// <summary>
        /// Clamps a number between 0 and 1
        /// </summary>
        public static Func<float, float> Clamp = (float a) => Min(Max(a, 0), 1);
        /// <summary>
        /// Clamps a number within a range
        /// </summary>
        public static Func<float, float, float, float> ClampRange = (float value, float min, float max) => Min(Max(value, min), max);
        /// <summary>
        /// Returns a clamped interpolated result between two floats
        /// </summary>
        public static Func<float, float, float, float> Lerp = (float a, float b, float t) => a + (b - a) * Clamp(t);
        /// <summary>
        /// Returns an un-clamped interpolated result between two floats
        /// </summary>
        public static Func<float, float, float, float> LerpUnclamped = (float a, float b, float t) => a + (b - a) * t;
        /// <summary>
        /// Returns float squared
        /// </summary>
        public static Func<float, float> Square = (float a) => a * a;
        /// <summary>
        /// Returns the absolute value of a number
        /// </summary>
        public static Func<float, float> Abs = (float a) => a < 0 ? a * -1 : a;
        /// <summary>
        /// Compares two floats to whether they are similar
        /// </summary>
        public static Func<float, float, bool> Approx = (float a, float b) => Abs(b - a) < Max(0.00001f * Max(Abs(a), Abs(b)), Epsilon * 8);

        /// <summary>
        /// Returns the square root of a number
        /// </summary>
        public static Func<float, float> Sqrt = (float a) =>
        {
            float root = 1;
            int i = 0;

            if (a > 0)
            {
                while (true)
                {
                    i++;
                    root = (a / root + root) / 2;
                    if (i == a + 1) { break; }
                }
            }
            return root;
        };

        // public float Acos(float f);

    }

    [SerializeField]
    public class Vector2
    {
        // Conversion to Unity
        public static implicit operator Vector2(UnityEngine.Vector2 uev)
            => new Vector2(uev.x, uev.y);

        public static implicit operator UnityEngine.Vector2(Vector2 v)
            => new UnityEngine.Vector2(v.x, v.y);

        private float mag;

        // Instancing
        public Vector2() : this(0, 0) { }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        // Convert
        /// <summary>
        /// Converts a Vector2 to a Vector3
        /// </summary>
        public Vector3 Vector3() => new Vector3(this.x, 0, this.y);

        // Operators
        public static Vector2 operator +(Vector2 a) => a; // Addition operator
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y); // Addition operator between 2 vectors
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y); // Subtraction operator between 2 vectors
        public static Vector2 operator *(Vector2 a, Vector2 b) => new Vector2(a.x * b.x, a.y * b.y); // Multiplication operator between 2 vectors
        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.x * b, a.y * b); // Multiplication operator between a vector and a float
        public static bool operator ==(Vector2 a, Vector2 b) => (a.x == b.x && a.y == b.y) ? true : false; // Equals check between 2 vectors
        //public static bool operator !=(Vector2 a, Vector2 b) => (a.x != b.y || a.y != b.y) ? true : false; // Does not equals check between 2 vectors
        public static bool operator !=(Vector2 a, Vector2 b) => (!(a.x == b.x) || !(a.y == b.y)) ? true : false; // Does not equals (2 vectors)

        public static Vector2 operator /(Vector2 a, Vector2 b) // Division operator between 2 vectors
            => (b.x == 0 || b.y == 0) ? throw new DivideByZeroException("You cannot divide by Zero") : new Vector2(a.x / b.x, a.y / b.y);

        public static Vector2 operator /(Vector2 a, float b) // Division operator vetween a vector and a float
            => (b == 0) ? throw new DivideByZeroException("You cannot divide by Zero") : new Vector2(a.x / b, a.y / b);

        // Properties
        /// <summary>
        /// Returns the magnitude of this vector
        /// </summary>
        public float magnitude { get => (Maths.Sqrt(X * X + Y * Y)); }
        /// <summary>
        /// Returns normalized magnitude of this vector
        /// </summary>
        public float normalized { get => this.mag = 1f; }
        /// <summary>
        /// Returns the X coordinate of this vector
        /// </summary>
        public float x { get => this.X; set => this.X = value; }
        /// <summary>
        /// Returns the Y coordinate of this vector
        /// </summary>
        public float y { get => this.Y; set => this.Y = value; }
        /// <summary>
        /// Returns the magnitude squared of this vector
        /// </summary>
        public float SqrMagnitude { get => this.mag * this.mag; }

        /// <summary>
        /// Returns the specific coordinate based on index
        /// </summary>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: { return this.X; }
                    case 1: { return this.Y; }
                    default: throw new IndexOutOfRangeException("Index out of range");
                }
            }
        }

        // Static properties
        /// <summary>
        /// Shorthand for writing Vector2(0, -1)
        /// </summary>
        public static Vector2 back { get => new Vector2(0, -1); }
        /// <summary>
        /// Shorthand for writing Vector2(-1, 0)
        /// </summary>
        public static Vector2 left { get => new Vector2(-1, 0); }
        /// <summary>
        /// Shorthand for writing Vector2(1, 0)
        /// </summary>
        public static Vector2 right { get => new Vector2(1, 0); }
        /// <summary>
        /// Shorthand for writing Vector2(0, 1)
        /// </summary>
        public static Vector2 up { get => new Vector2(0, 1); }
        /// <summary>
        /// Shorthand for writing Vector2(0, 0)
        /// </summary>
        public static Vector2 zero { get => new Vector2(0, 0); }
        /// <summary>
        /// Shorthand for writing Vector2(1, 1)
        /// </summary>
        public static Vector2 one { get => new Vector2(1, 1); }

        // Public methods
        /// <summary>
        /// Returns whether vector equals or not
        /// </summary>
        public bool Equals(Vector2 vec) => (vec.x == this.X && vec.y == this.Y) ? true : false;
        /// <summary>
        /// Converts Vector2 to string
        /// </summary>
        public string toString() => "(" + this.x + ", " + this.y + ")";
        /// <summary>
        /// Normalizes magnitude
        /// </summary>
        public void Normalize() => this.mag = 1f;

        /// <summary>
        /// Sets the X, Y coordinates of this Vector
        /// </summary>
        public void Set(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        // Static Methods
        /// <summary>
        /// Returns the dot product of 2 vectors
        /// </summary>
        public static Func<Vector2, Vector2, float> Dot = (Vector2 a, Vector2 b) => (a.x * b.x) + (a.y * b.y);
        /// <summary>
        /// Returns the distance between 2 vectors
        /// </summary>
        public static Func<Vector2, Vector2, float> Distance = (Vector2 a, Vector2 b) => (float)Maths.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));

        /// <summary>
        /// Returns a new Vector with the maximum values from 2 vectors
        /// </summary>
        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            float x = a.x > b.x ? a.x : a.x == b.x ? a.x : b.x;
            float y = a.y > b.y ? a.y : a.y == b.y ? a.y : b.y;

            return new Vector2(x, y);
        }
        /// <summary>
        /// Returns a new Vector with the minimum values from 2 vectors
        /// </summary>
        public static Vector2 Min(Vector2 a, Vector2 b)
        {
            float x = a.x < b.x ? a.x : a.x == b.x ? a.x : b.x;
            float y = a.y < b.y ? a.y : a.y == b.y ? a.y : b.x;

            return new Vector2(x, y);
        }

        /// <summary>
        /// Returns the angle in degrees between 2 vectors (from and to)
        /// </summary>
        public static float Angle(Vector2 from, Vector2 to)
        {
            float x = to.x - from.x;
            float y = to.y - from.y;

            return Mathf.Atan2(y, x) * (180 / Maths.PI);
        }

        /// <summary>
        /// Interpolates between vectors 
        /// </summary>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            float lx = Maths.Lerp(a.x, b.x, t);
            float ly = Maths.Lerp(a.y, b.y, t);

            return new Vector2(lx, ly);
        }

        /// <summary>
        /// Interpolates between vectors (unclamped)
        /// </summary>
        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            float lux = Maths.LerpUnclamped(a.y, b.y, t);
            float luy = Maths.LerpUnclamped(a.y, b.y, t);

            return new Vector2(lux, luy);
        }

        /// <summary>
        /// Returns a copy of a vector with it's magnitude clamped to maxLength
        /// </summary>
        public static Vector2 ClampMagnitude(Vector2 vector, float max)
        {
            float mag = vector.magnitude > max ? max : vector.magnitude;
            Vector2 vec = new Vector2();
            vec = vector;
            vec.mag = mag;

            return vec;
        }

        /// <summary>
        /// Moves a point towards a target
        /// </summary>
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistance)
        {
            Vector2 a = target - current;
            return (a.magnitude <= maxDistance || a.magnitude == 0f) ? target : current + a / a.magnitude * maxDistance;
        }
    }

    // Vector3 class
    [SerializeField]
    public class Vector3
    {
        // Conversion operator for Vector3
        public static implicit operator Vector3(UnityEngine.Vector3 uev)
            => new Vector3(uev.x, uev.y, uev.z);

        public static implicit operator UnityEngine.Vector3(Vector3 v)
            => new UnityEngine.Vector3(v.x, v.y, v.z);

        private float mag;

        // Instancing
        public Vector3() : this(0, 0, 0) { }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; } // X getter/setter
        public float Y { get; set; } // Y getter/setter
        public float Z { get; set; } // Z getter/setter

        // Static properties
        /// <summary>
        /// Shorthand for Vector3(0, 0, -1)
        /// </summary>
        public static Vector3 back { get => new Vector3(0, 0, -1f); }
        /// <summary>
        /// Shorthand for Vector3(0, -1, 0)
        /// </summary>
        public static Vector3 down { get => new Vector3(0, -1, 0); }
        /// <summary>
        /// Shorthand for Vector3(0, 0, 1)
        /// </summary>
        public static Vector3 forward { get => new Vector3(0, 0, 1); }
        /// <summary>
        /// Shorthand for Vector3(-1, 0, 0)
        /// </summary>
        public static Vector3 left { get => new Vector3(-1, 0, 0); }
        /// <summary>
        /// Shorthand for Vector3(1, 1, 1)
        /// </summary>
        public static Vector3 one { get => new Vector3(1, 1, 1); }
        /// <summary>
        /// Shorthand for Vector3(0, 0, 0)
        /// </summary>
        public static Vector3 zero { get => new Vector3(0, 0, 0); }
        /// <summary>
        /// Shorthand for Vector3(1, 0, 0)
        /// </summary>
        public static Vector3 right { get => new Vector3(1, 0, 0); }
        /// <summary>
        /// Shorthand for Vector3(0, 1, 0)
        /// </summary>
        public static Vector3 up { get => new Vector3(0, 1, 0); }


        // Public properties
        /// <summary>
        /// Returns the length of this vector
        /// </summary>
        public float magnitude { get => Maths.Sqrt(X * X + Y * Y + Z * Z); }
        /// <summary>
        /// Returns the length of this vector with a magnitude of 1
        /// </summary>
        //public Vector3 normalized { get => new Vector3(this.x, this.y, this.y).mag = 1f; }
        public Vector3 normalized()
        {
            Vector3 newV = new Vector3();
            newV = this;
            newV.mag = 1f;
            return newV;
        }
        /// <summary>
        /// Returns X coordinate of this vector
        /// </summary>
        public float x { get => this.X; set => this.X = value; }
        /// <summary>
        /// Returns Y coordinate of this vector
        /// </summary>
        public float y { get => this.Y; set => this.Y = value; }
        /// <summary>
        /// Returns Z coordinate of this vector
        /// </summary>
        public float z { get => this.Z; set => this.Z = value; }
        /// <summary>
        /// Returns the magnitude of this vector squared
        /// </summary>
        public float SqrMagnitude { get => this.mag * this.mag; }

        /// <summary>
        /// Returns the specific coordinate based on index
        /// </summary>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.X;
                    case 1: return this.Y;
                    case 2: return this.Z;
                    default: throw new IndexOutOfRangeException("Index out of range");
                }
            }
        }

        // Public methods
        /// <summary>
        /// Returns whether vector equals or not
        /// </summary>
        public bool Equals(Vector3 vector3) => (vector3.x == this.X && vector3.y == this.Y && vector3.z == this.Z) ? true : false;
        /// <summary>
        /// Converts Vector3 to string
        /// </summary>
        public string toString() => "(" + this.X + ", " + this.Y + ", " + this.Z + ")";
        /// <summary>
        /// Normalizes magnitude
        /// </summary>
        public void Normalize() => this.mag = 1f;
        /// <summary>
        /// Sets the X, Y, Z coordinates of this Vector
        /// </summary>
        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // Static methods
        /// <summary>
        /// Returns the angle in degrees between 2 vectors
        /// </summary>
        public static Func<Vector3, Vector3, float> Angle = (Vector3 from, Vector3 to) => Mathf.Atan2(to.y - from.y, to.x - from.x);
        /// <summary>
        /// Returns the dot product of 2 vectors
        /// </summary>
        public static Func<Vector3, Vector3, float> Dot = (Vector3 a, Vector3 b) => (a.x * b.x + a.y * b.y + a.z * b.z);
        /// <summary>
        /// Returns the distance between 2 vectors
        /// </summary>
        public static Func<Vector3, Vector3, float> Distance = (Vector3 a, Vector3 b) => (float)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        /// <summary>
        /// Returns the cross product of 2 vectors
        /// </summary>
        public static Func<Vector3, Vector3, Vector3> Cross = (Vector3 a, Vector3 b)
            => new Vector3(a.y * b.z - b.y * a.z, (a.x * a.z - b.x * a.z) * -1, a.x * b.y - b.x * a.y);

        /// <summary>
        /// Returns a new Vector with the maximum values from 2 vectors
        /// </summary>
        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            float x = a.x > b.x ? a.x : a.x == b.x ? a.x : b.x;
            float y = a.y > b.y ? a.y : a.y == b.y ? a.y : b.y;
            float z = a.x > b.x ? a.z : a.z == b.z ? a.z : b.z;
            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Returns a new Vector with the minimum values from 2 vectors
        /// </summary>
        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            float x = a.x < b.x ? a.x : a.x == b.x ? a.x : b.x;
            float y = a.y < b.y ? a.y : a.y == b.y ? a.y : b.y;
            float z = a.z < b.z ? a.z : a.z == b.z ? a.z : b.z;
            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Interpolates between vectors 
        /// </summary>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            float lx = Maths.Lerp(a.x, b.x, t);
            float ly = Maths.Lerp(a.y, b.y, t);
            float lz = Maths.Lerp(a.z, b.z, t);
            return new Vector3(lx, ly, lz);
        }
        /// <summary>
        /// Interpolates between vectors (unclamped)
        /// </summary>
        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            float lux = Maths.LerpUnclamped(a.x, b.x, t);
            float luy = Maths.LerpUnclamped(a.y, b.y, t);
            float luz = Maths.LerpUnclamped(a.z, b.z, t);
            return new Vector3(lux, luy, luz);
        }
        /// <summary>
        /// Returns a clamped magnitude to maxLength
        /// </summary>
        public static Vector3 ClampMag(Vector3 vector, float max)
        {
            Vector3 vec3 = new Vector3();
            vec3 = vector;
            vec3.mag = vector.magnitude > max ? max : vector.magnitude;
            return vec3;
        }
        /// <summary>
        /// Moves a point towards a target
        /// </summary>
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistance)
        {
            Vector3 a = target - current;
            return (a.magnitude <= maxDistance || a.magnitude == 0f) ? target : current + a / a.magnitude * maxDistance;
        }

        // Operators
        public static Vector3 operator +(Vector3 a) => a; // Addition operator
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); // Addition operator
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z); // Subtraction operator
        public static Vector3 operator *(Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z); // Multiplication operator between 2 vectors
        public static Vector3 operator *(Vector3 a, float b) => new Vector3(a.x * b, a.y * b, a.z * b); // Multiplication operator between a vector and a float
        public static bool operator ==(Vector3 a, Vector3 b) => (a.x == b.x && a.y == b.y && a.z == b.z) ? true : false; // Equates to (2 vectors)
        //public static bool operator !=(Vector3 a, Vector3 b) => (a.x != b.x || a.y != b.y || a.z != b.z) ? true : false; // Does not equate (2 vectors)
        public static bool operator !=(Vector3 a, Vector3 b) => (!(a.x == b.x) || !(a.y == b.y) || !(a.z == b.z)) ? true : false; // Does not equate (2 Vectors)
        public static bool operator <=(Vector3 a, Vector3 b) => (a.x <= b.x && a.y <= b.y && a.z <= b.z) ? true : false; // Is smaller than (2 vectors)
        public static bool operator <=(float a, Vector3 b) => (a <= b.x && a <= b.y && a <= b.z) ? true : false; // Is smaller than (float, vector)
        public static bool operator <=(Vector3 a, float b) => (a.x <= b && a.y <= b && a.z <= b) ? true : false; // Is smaller than (vector, float)
        public static bool operator >=(Vector3 a, Vector3 b) => (a.x >= b.x && a.y >= b.y && a.z >= b.z) ? true : false; // Is bigger than (2 vectors)
        public static bool operator >=(Vector3 a, float b) => (a.x >= b && a.y >= b && a.z >= b) ? true : false; // Is bigger than (vector, float)
        public static bool operator >=(float a, Vector3 b) => (a >= b.x && a >= b.y && a >= b.z) ? true : false; // Is bigger than (float, vector)


        public static Vector3 operator /(Vector3 a, Vector3 b) // Division operator between 2 vectors
            => ((b.x == 0) || (b.y == 0) || (b.z == 0)) ? throw new DivideByZeroException("You cannot divide by Zero") : new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);

        public static Vector3 operator /(Vector3 a, float b) // Division operator (vector, float)
            => (b == 0) ? throw new DivideByZeroException("you cannot divide by Zero") : new Vector3(a.x / b, a.y / b, a.z / b);

        /// <summary>
        /// Gradually changes a vector towards a desired goal over time
        /// </summary>
        public static float SmoothDamp(float current, float target, float currentVelo, float smoothTime, float maxSpeed = Maths.Infinity)
        {
            // Pretty much the same as SmoothDamp from Unity but with my own maths functions
            smoothTime = Maths.Max(0.0001f, smoothTime);
            float a = 2f / smoothTime;
            float x = a * Time.deltaTime;
            float b = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
            float change = current - target;
            float og = target;
            float maxChange = maxSpeed * smoothTime;
            change = Maths.ClampRange(change, -maxChange, maxChange);
            target = current - change;
            float temp = (currentVelo + a * change) * Time.deltaTime;
            currentVelo = (currentVelo - a * temp) * b;
            float output = target + (change + temp) * b;

            if (og - current > 0.0f == output > og)
            {
                output = og;
                currentVelo = (output - og) / Time.deltaTime;
            }

            return output;
        }


        // Functions that aren't completed:

        // OrthoNormalize
        // Project
        // ProjectOnPlane
        // Reflect
        // RotateTowards
        // Scale
        // SignedAngle
        // Slerp
        // SlerpUnclamped
    }

    [SerializeField]
    public class Vector4 // For Matrix4x4
    {
        // The Vector4 declaration so that I could create the Matrix4D

        public static implicit operator Vector4(UnityEngine.Vector4 uev)
            => new Vector4(uev.x, uev.y, uev.z, uev.w);

        public static implicit operator UnityEngine.Vector4(Vector4 v)
            => new UnityEngine.Vector4(v.x, v.y, v.z, v.w);

        private float mag;

        public Vector4() : this(0, 0, 0, 0) { }

        public Vector4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        // Privately set values
        private float X { get; set; }
        private float Y { get; set; }
        private float Z { get; set; }
        private float W { get; set; }

        // Getters/Setters for the previous values
        public float x { get => X; set => X = value; }
        public float y { get => Y; set => Y = value; }
        public float z { get => Z; set => Z = value; }
        public float w { get => W; set => W = value; }
    }

    [SerializeField]
    public struct Matrix2D
    {

        //     0    1
        //    ---------
        //  0| d00  d10
        //  1| d01  d11

        public float d00; // Entry point: 0, 0
        public float d10; // Entry point: 1, 0
        public float d01; // Entry point: 0, 1
        public float d11; // Entry point: 1, 1

        public Matrix2D(Vector2 c0, Vector2 c1)
        {
            //     0    1
            //    ---------
            //  0| c0.x c0.y
            //  1| c1.x c1.y

            // https://www.tutorialsteacher.com/csharp/csharp-multi-dimensional-array
            // I think this is correct??

            this.d00 = c0.x; this.d10 = c0.y;
            this.d01 = c1.x; this.d11 = c1.y;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return d00;
                    case 1: return d10;
                    case 2: return d01;
                    case 3: return d11;
                    default: throw new IndexOutOfRangeException("Index is out of range");
                }
            }
        }

        // Operators
        public static Matrix2D operator *(Matrix2D a, Matrix2D b) // Multiplying 2 dimensional vectors together
        {
            Matrix2D c;
            c.d00 = a.d00 * b.d00;
            c.d01 = a.d01 * b.d01;
            c.d10 = a.d10 * b.d10;
            c.d11 = a.d11 * b.d11;

            return c;
        }

        public static Matrix2D operator +(Matrix2D a, Matrix2D b) // Adding 2 dimensional vectors together
        {
            Matrix2D c;
            c.d00 = a.d00 + b.d00;
            c.d01 = a.d01 + b.d01;
            c.d10 = a.d10 + b.d10;
            c.d11 = a.d11 + b.d11;

            return c;
        }

        // Need a column/row finder??
        public static bool operator ==(Matrix2D a, Matrix2D b) => a.GetColumn(0) == b.GetColumn(0) && a.GetColumn(1) == b.GetColumn(1); // Check if 2 matrix2d are the same
        public static bool operator !=(Matrix2D a, Matrix2D b) => !(a == b); // With the == operator already working, check if 2 matrix2d are not the same
        // Public methods

        // For equals operator??
        public Vector2 GetRow(int index)
        {
            switch (index)
            {
                case 0: { return new Vector2(d00, d10); }
                case 1: { return new Vector2(d01, d11); }
                default: throw new IndexOutOfRangeException("Index out of range");
            }
        }

        // For equals operator??
        public Vector2 GetColumn(int index)
        {
            switch (index)
            {
                case 0: { return new Vector2(d00, d01); }
                case 1: { return new Vector2(d10, d11); }
                default: throw new IndexOutOfRangeException("Index out of range");
            }
        }

        public Matrix2D Transpose(Matrix2D a)
        {
            // https://www.cuemath.com/algebra/transpose-of-a-matrix/

            // A:
            //     0    1
            //    ---------
            //  0| d00  d10
            //  1| d01  d11

            // B:
            //     0    1
            //    ---------
            //  0| d00  d01
            //  1| d10  d11

            Matrix2D b;
            b.d00 = a.d00;
            b.d10 = a.d01;
            b.d01 = a.d10;
            b.d11 = a.d11;

            return b;
        }

        // Identity matrix:

        // 1 0
        // 0 1

        /// <summary>
        /// Returns the identity of a 2 dimensional Matrix
        /// </summary>
        public static Matrix2D Identity { get => new Matrix2D(new Vector2(1, 0), new Vector2(0, 1)); }

        // Zero Matrix

        // 0 0
        // 0 0

        /// <summary>
        /// Returns the Zero matrix of a 2 dimensional Matrix
        /// </summary>
        public static Matrix2D ZeroMatrix { get => new Matrix2D(new Vector2(0, 0), new Vector2(0, 0)); }

        /// <summary>
        /// Inverse of 2 dimensional vectors
        /// </summary>
        public Matrix2D Inverse(Matrix2D a)
        {
            Matrix2D b;
            b.d00 = a.d11;
            b.d11 = a.d00;
            b.d01 = a.d01 * -1;
            b.d10 = a.d10 * -1;

            float div = a.d00 * a.d11 - a.d01 * a.d10;

            Matrix2D c;
            c.d00 = b.d00 / div;
            c.d11 = b.d11 / div;
            c.d01 = b.d01 / div;
            c.d10 = b.d10 / div;

            return c;
        }
    }

    [SerializeField]
    public struct Matrix3D
    {
        //     0    1   2
        //    --------------
        //  0| d00 d10 d20
        //  1| d01 d11 d21
        //  2| d02 d12 d22

        public float d00; // Entry point: 0, 0
        public float d10; // Entry point: 1, 0
        public float d20; // Entry point: 2, 0
        public float d01; // Entry point: 0, 1
        public float d11; // Entry point: 1, 1
        public float d21; // Entry point: 2, 1
        public float d02; // Entry point: 0, 2
        public float d12; // Entry point: 1, 2
        public float d22; // Entry point: 2, 2

        // Constructor
        public Matrix3D(Vector3 a, Vector3 b, Vector3 c)
        {
            this.d00 = a.x; this.d10 = a.y; this.d20 = a.z;
            this.d01 = b.x; this.d11 = b.y; this.d21 = b.z;
            this.d02 = c.x; this.d12 = c.y; this.d22 = c.z;
        }

        /// <summary>
        /// Returns the value at index given
        /// or sets the value at given index
        /// </summary>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return d00;
                    case 1: return d10;
                    case 2: return d20;
                    case 3: return d01;
                    case 4: return d11;
                    case 5: return d21;
                    case 6: return d02;
                    case 7: return d12;
                    case 8: return d22;
                    default: throw new IndexOutOfRangeException("Index out of range");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: { d00 = value; break; }
                    case 1: { d10 = value; break; }
                    case 2: { d20 = value; break; }
                    case 3: { d01 = value; break; }
                    case 4: { d11 = value; break; }
                    case 5: { d21 = value; break; }
                    case 6: { d02 = value; break; }
                    case 7: { d12 = value; break; }
                    case 8: { d22 = value; break; }
                    default: throw new IndexOutOfRangeException("Index out of range");

                }
            }
        }

        /// <summary>
        /// Multiplies 2 3 dimensional Matrixes together
        /// </summary>
        public static Matrix3D operator *(Matrix3D a, Matrix3D b)
        {
            Matrix3D c;
            c.d00 = a.d00 * b.d00;
            c.d10 = a.d10 * b.d10;
            c.d20 = a.d20 * b.d20;
            c.d01 = a.d01 * b.d01;
            c.d11 = a.d11 * b.d11;
            c.d21 = a.d21 * b.d21;
            c.d02 = a.d02 * b.d02;
            c.d12 = a.d12 * b.d12;
            c.d22 = a.d22 * b.d22;

            return c;
        }
        
        /// <summary>
        /// Adds 2 3 dimensional Matrixes together
        /// </summary>
        public static Matrix3D operator +(Matrix3D a, Matrix3D b)
        {
            Matrix3D c;
            c.d00 = a.d00 + b.d00;
            c.d10 = a.d10 + b.d10;
            c.d20 = a.d20 + b.d20;
            c.d01 = a.d01 + b.d01;
            c.d11 = a.d11 + b.d11;
            c.d21 = a.d21 + b.d21;
            c.d02 = a.d02 + b.d02;
            c.d12 = a.d12 + b.d12;
            c.d22 = a.d22 + b.d22;
            return c;
        }

        // Checks if 2 Matrices equate
        public static bool operator ==(Matrix3D a, Matrix3D b) => a.GetColumn(0) == b.GetColumn(0) && a.GetColumn(1) == b.GetColumn(1) && a.GetColumn(2) == b.GetColumn(2);
        // Checks if 2 Matrices DO NOT equate
        public static bool operator !=(Matrix3D a, Matrix3D b) => !(a == b);

        /// <summary>
        /// Returns column at given index in form of Vector3
        /// </summary>
        public Vector3 GetColumn(int index)
        {
            switch (index)
            {
                case 0: { return new Vector3(d00, d01, d02); }
                case 1: { return new Vector3(d10, d11, d12); }
                case 2: { return new Vector3(d20, d21, d22); }
                default: throw new IndexOutOfRangeException("Index out of range");
            }
        }

        /// <summary>
        /// Returns row at given index in form of Vector3
        /// </summary>
        public Vector3 GetRow(int index)
        {
            switch (index)
            {
                case 0: { return new Vector3(d00, d10, d20); }
                case 1: { return new Vector3(d01, d11, d21); }
                case 3: { return new Vector3(d02, d12, d22); }
                default: throw new IndexOutOfRangeException("Index out of range");
            }
        }

        /// <summary>
        /// Transposes a Matrix3D
        /// </summary>
        public Matrix3D Transpose(Matrix3D a)
        {
            // A:
            //     0    1   2
            //    --------------
            //  0| d00 d10 d20
            //  1| d01 d11 d21
            //  2| d02 d12 d22

            // B:
            //     0    1   2
            //    --------------
            //  0| d00 d01 d02
            //  1| d10 d11 d12
            //  2| d20 d21 d22

            Matrix3D b;
            b.d00 = a.d00;
            b.d10 = a.d01;
            b.d20 = a.d02;
            b.d01 = a.d10;
            b.d02 = a.d20;
            b.d11 = a.d11;
            b.d21 = a.d12;
            b.d12 = a.d21;
            b.d22 = a.d22;

            return b;
        }

        /// <summary>
        /// Returns identity matrix of 3 dimensional Matrix
        /// </summary>
        public static Matrix3D Identity { get => new Matrix3D(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1)); }
        /// <summary>
        /// Returns zero matrix of 3 dimensional matrix
        /// </summary>
        public static Matrix3D ZeroMatrix { get => new Matrix3D(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0)); }

        /// <summary>
        /// Returns inverse of 3 dimensional matrix
        /// </summary>
        public Matrix3D Inverse(Matrix3D a)
        {
            // https://www.mathsisfun.com/algebra/matrix-inverse-minors-cofactors-adjugate.html

            //     0    1   2
            //    --------------
            //  0| d00 d10 d20
            //  1| d01 d11 d21
            //  2| d02 d12 d22

            Vector3 row1 = new Vector3( // Create row 1
                ((a.d11 * a.d22) - (a.d21 * a.d12)),
                ((a.d01 * a.d22) - (a.d21 * a.d02)),
                ((a.d01 * a.d12) - (a.d11 * a.d02)));

            Vector3 row2 = new Vector3( // Create row 2
                ((a.d10 * a.d22) - (a.d20 * a.d12)),
                ((a.d00 * a.d22) - (a.d20 * a.d02)),
                ((a.d00 * a.d12) - (a.d10 * a.d02)));

            Vector3 row3 = new Vector3( // Create row 3
                ((a.d10 * a.d21) - (a.d20 * a.d11)),
                ((a.d00 * a.d21) - (a.d20 * a.d01)),
                ((a.d00 * a.d11) - (a.d10 * a.d01)));

            Matrix3D b = new Matrix3D(row1, row2, row3); // Create new Matrix from previously created new rows

            // Makes every 2nd value negative
            b.d10 *= -1;
            b.d01 *= -1;
            b.d21 *= -1;
            b.d12 *= -1;

            // Transpose new Matrix
            b = this.Transpose(b);

            // Calculate determinent from first Matrix
            float determinant =
                (b.d00 * ((b.d11 * b.d22) - (b.d21 * b.d12)) - (b.d10 * ((b.d01 * b.d22) - (b.d21 * b.d02))) + (b.d20 * ((b.d01 * b.d12) - (b.d11 * b.d02))));

            // Divide by determinent
            b.d00 /= determinant;
            b.d10 /= determinant;
            b.d20 /= determinant;
            b.d01 /= determinant;
            b.d11 /= determinant;
            b.d21 /= determinant;
            b.d02 /= determinant;
            b.d12 /= determinant;
            b.d22 /= determinant;

            // Return new Matrix
            return b;
        }
    }

    public struct Matrix4D
    {
        // Conversion to Unity (Allows for compatibility)
        public static implicit operator Matrix4D(UnityEngine.Matrix4x4 mat)
        {
            return new Matrix4D(
                new Vector4(mat.m00, mat.m10, mat.m20, mat.m30),
                new Vector4(mat.m01, mat.m11, mat.m21, mat.m31),
                new Vector4(mat.m02, mat.m12, mat.m22, mat.m32),
                new Vector4(mat.m03, mat.m13, mat.m23, mat.m33)
                );
        }

        public static implicit operator UnityEngine.Matrix4x4(Matrix4D m)
        {
            return new UnityEngine.Matrix4x4(
                new UnityEngine.Vector4(m.d00, m.d10, m.d20, m.d30),
                new UnityEngine.Vector4(m.d01, m.d11, m.d21, m.d31),
                new UnityEngine.Vector4(m.d02, m.d12, m.d22, m.d32),
                new UnityEngine.Vector4(m.d03, m.d13, m.d23, m.d33)
                );
        }

        //     0    1   2   3
        //    ------------------
        //  0| d00 d10 d20 d30
        //  1| d01 d11 d21 d31
        //  2| d02 d12 d22 d32
        //  3| d03 d13 d23 d33

        public float d00;
        public float d10;
        public float d20;
        public float d30;

        public float d01;
        public float d11;
        public float d21;
        public float d31;

        public float d02;
        public float d12;
        public float d22;
        public float d32;

        public float d03;
        public float d13;
        public float d23;
        public float d33;

        // Constructor
        public Matrix4D(Vector4 a, Vector4 b, Vector4 c, Vector4 d)
        {
            this.d00 = a.x; this.d10 = a.y; this.d20 = a.z; this.d30 = a.w;
            this.d01 = b.x; this.d11 = b.y; this.d21 = b.z; this.d31 = b.w;
            this.d02 = c.x; this.d12 = c.y; this.d22 = c.z; this.d32 = c.w;
            this.d03 = d.x; this.d13 = d.y; this.d23 = d.z; this.d33 = d.w;
        }
        /// <summary>
        /// Returns value at given index from Matrix3D
        /// or Sets value at given index from Matrix3D
        /// </summary>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return d00;
                    case 1: return d10;
                    case 2: return d20;
                    case 3: return d30;

                    case 4: return d01;
                    case 5: return d11;
                    case 6: return d21;
                    case 7: return d31;

                    case 8: return d02;
                    case 9: return d12;
                    case 10: return d22;
                    case 11: return d32;

                    case 12: return d03;
                    case 13: return d13;
                    case 14: return d23;
                    case 15: return d33;

                    default: throw new IndexOutOfRangeException("Index out of range");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: { this.d00 = value; break; }
                    case 1: { this.d10 = value; break; }
                    case 2: { this.d20 = value; break; }
                    case 3: { this.d30 = value; break; }

                    case 4: { this.d01 = value; break; }
                    case 5: { this.d11 = value; break; }
                    case 6: { this.d21 = value; break; }
                    case 7: { this.d31 = value; break; }

                    case 8: { this.d02 = value; break; }
                    case 9: { this.d12 = value; break; }
                    case 10: { this.d22 = value; break; }
                    case 11: { this.d32 = value; break; }

                    case 12: { this.d03 = value; break; }
                    case 13: { this.d13 = value; break; }
                    case 14: { this.d23 = value; break; }
                    case 15: { this.d33 = value; break; }

                    default: throw new IndexOutOfRangeException("Index out of range");
                }
            }
        }

        /// <summary>
        /// Multiplies 2 4 Dimensional matrices together
        /// </summary>
        public static Matrix4D operator *(Matrix4D a, Matrix4D b)
        {
            Matrix4D c;
            c.d00 = a.d00 * b.d00;
            c.d10 = a.d01 * b.d01;
            c.d20 = a.d20 * b.d20;
            c.d30 = a.d30 * b.d30;

            c.d01 = a.d01 * b.d01;
            c.d11 = a.d11 * b.d11;
            c.d21 = a.d21 * b.d21;
            c.d31 = a.d31 * b.d31;

            c.d02 = a.d02 * b.d02;
            c.d12 = a.d12 * b.d12;
            c.d22 = a.d22 * b.d22;
            c.d32 = a.d32 * b.d32;

            c.d03 = a.d03 * b.d03;
            c.d13 = a.d13 * b.d13;
            c.d23 = a.d23 * b.d23;
            c.d33 = a.d33 * b.d33;

            return c;
        }

        /// <summary>
        /// Adds 2 4 dimensional Matrices together
        /// </summary>
        public static Matrix4D operator +(Matrix4D a, Matrix4D b)
        {
            Matrix4D c;
            c.d00 = a.d00 + b.d00;
            c.d10 = a.d01 + b.d01;
            c.d20 = a.d20 + b.d20;
            c.d30 = a.d30 + b.d30;

            c.d01 = a.d01 + b.d01;
            c.d11 = a.d11 + b.d11;
            c.d21 = a.d21 + b.d21;
            c.d31 = a.d31 + b.d31;

            c.d02 = a.d02 + b.d02;
            c.d12 = a.d12 + b.d12;
            c.d22 = a.d22 + b.d22;
            c.d32 = a.d32 + b.d32;

            c.d03 = a.d03 + b.d03;
            c.d13 = a.d13 + b.d13;
            c.d23 = a.d23 + b.d23;
            c.d33 = a.d33 + b.d33;

            return c;
        }

        /// <summary>
        /// Checks if 2 Matrix4D equate
        /// </summary>
        public static bool operator ==(Matrix4D a, Matrix4D b)
            => a.GetColumn(0) == b.GetColumn(0) && a.GetColumn(1) == b.GetColumn(1) && a.GetColumn(2) == b.GetColumn(2) && a.GetColumn(3) == b.GetColumn(3);

        /// <summary>
        /// Checks if 2 Matrix4D DO NOT equate
        /// </summary>
        public static bool operator !=(Matrix4D a, Matrix4D b) => !(a == b);

        //     0    1   2   3
        //    ------------------
        //  0| d00 d10 d20 d30
        //  1| d01 d11 d21 d31
        //  2| d02 d12 d22 d32
        //  3| d03 d13 d23 d33

        /// <summary>
        /// Get column from Matrix4D at given index
        /// </summary>
        public Vector4 GetColumn(int index)
        {
            switch (index)
            {
                case 0: { return new Vector4(d00, d01, d02, d03); }
                case 1: { return new Vector4(d10, d11, d12, d13); }
                case 2: { return new Vector4(d20, d21, d22, d23); }
                case 3: { return new Vector4(d30, d31, d32, d33); }

                default: throw new IndexOutOfRangeException("Index out of range");
            }
        }

        /// <summary>
        /// Get row from Matrix4D at given index
        /// </summary>
        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0: { return new Vector4(d00, d10, d20, d30); }
                case 1: { return new Vector4(d01, d11, d21, d31); }
                case 2: { return new Vector4(d02, d12, d22, d32); }
                case 3: { return new Vector4(d03, d13, d23, d23); }

                default: throw new IndexOutOfRangeException("Index out of range");
            }
        }

        /// <summary>
        /// Returns transpose of given Matrix4D
        /// </summary>
        public Matrix4D Transpose(Matrix4D a)
        {

            //     0    1   2   3
            //    ------------------
            //  0| d00 d10 d20 d30
            //  1| d01 d11 d21 d31
            //  2| d02 d12 d22 d32
            //  3| d03 d13 d23 d33

            Matrix4D b;
            b.d00 = a.d00;
            b.d10 = a.d01;
            b.d20 = a.d02;
            b.d30 = a.d03;
            b.d01 = a.d10;
            b.d02 = a.d20;
            b.d03 = a.d30;
            b.d11 = a.d11;
            b.d12 = a.d21;
            b.d13 = a.d31;
            b.d21 = a.d12;
            b.d31 = a.d13;
            b.d22 = a.d22;
            b.d32 = a.d23;
            b.d23 = a.d32;
            b.d33 = a.d33;

            return b;
        }

        /// <summary>
        /// Returns identity of Matrix4D
        /// </summary>
        public static Matrix4D Identity { get => new Matrix4D(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1)); }
        /// <summary>
        /// Returns Zero matrix of Matrix4D
        /// </summary>
        public static Matrix4D ZeroMatrix { get => new Matrix4D(new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0)); }

        /*
        public Matrix4D Inverse(Matrix4D a)
        {

            // http://www.euclideanspace.com/maths/algebra/matrix/functions/determinant/fourD/index.htm
            // https://stackoverflow.com/questions/2937702/i-want-to-find-determinant-of-4x4-matrix-in-c-sharp

            //     0    1   2   3
            //    ------------------
            //  0| d00 d10 d20 d30
            //  1| d01 d11 d21 d31
            //  2| d02 d12 d22 d32
            //  3| d03 d13 d23 d33


        }
        */
    }

    /*public class CustomPhys
    {
        public static bool RayCast(Vector3 origin, Vector3 direction, float maxDistance = Maths.Infinity)
        {

            float ox = origin.x;
            float oy = origin.y;
            float oz = origin.z;

            float dx = direction.x;
            float dy = direction.y;
            float dz = direction.z;

            bool xNeg = ox < 0 ? true : false;
            bool yNeg = oy < 0 ? true : false;
            bool zNeg = oz < 0 ? true : false;

            while (origin != direction)
            {
                Vector3 newPos = new Vector3();

                if (ox != dx)
                {
                    if (xNeg && ox < dx)
                    {
                        for (float i = ox; i == dx; i++)
                        {
                            
                        }
                }

                if (oy != dy)
                {

                }

                if(ox != dz)
                {

                }
            }


        } // RayCast func

    } // Physics */

} // namespace
