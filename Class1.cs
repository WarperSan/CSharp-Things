﻿using System;
using System.Collections.Generic;
using System.Linq;
using static Gauthier.CustomExceptions;

namespace Gauthier
{
    public class Addons
    {
        /// <summary>
        /// Utilities for Vectors
        /// </summary>
        public class VectorUtils
        {
            public class Point
            {
                public double X { get; set; }
                public double Y { get; set; }

                public Point(double x, double y)
                {
                    X = x;
                    Y = y;
                }
            }

            /// <returns>Distance between the two given points</returns>
            public static double DistanceBetweenTwoPoints(Point point1, Point point2)
            {
                return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
            }

            /// <summary>
            /// Calculates an in between point
            /// </summary>
            /// <param name="a">Origin point</param>
            /// <param name="b">Destination point</param>
            /// <param name="distance">Max distance</param>
            /// <returns>In between point</returns>
            public static Point MoveTowardsPoint(Point a, Point b, int distance)
            {
                Point vector = new Point(b.X - a.X, b.Y - a.Y);
                Point newPos = new Point(distance * Math.Sign(vector.X) + a.X, distance * Math.Sign(vector.Y) + a.Y);

                if (Math.Abs(vector.X) <= distance)
                    newPos.X = b.X;

                if (Math.Abs(vector.Y) <= distance)
                    newPos.Y = b.Y;

                return newPos;
            }
        }

        /// <summary>
        /// Utilites for using the Console
        /// </summary>
        public class ConsoleUtils
        {
            /// <summary>
            /// Shows the given message and asks in the Console for an answer
            /// </summary>
            /// <typeparam name="T">Type of the answer</typeparam>
            /// <param name="message">Message to show upon asking</param>
            /// <returns>User's input of type T</returns>
            /// <exception cref="NullReferenceException">Final Answer is Null or Empty</exception>
            public static T AskUser<T>(string message, ConsoleColor color = ConsoleColor.White)
            {
                string answer;
                bool result;

                do
                {
                    ShowConsoleMessage(message, true, color);
                    answer = Console.ReadLine();

                    if (string.IsNullOrEmpty(answer))
                        result = false;
                    else
                        result = CanValueConvertToType<T>(answer);

                    if (!result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"The string \"{answer}\" cannot be convert to {typeof(T)}");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                } while (!result);

                if (string.IsNullOrEmpty(answer)) throw new NullReferenceException("The given answer is either null or empty");

                return (T)Convert.ChangeType(answer, typeof(T));
            }

            /// <summary>
            /// Shows the given message and asks in the Console for an answer
            /// </summary>
            /// <param name="message">Message to show upon asking</param>
            /// <returns>User's input</returns>
            /// <exception cref="NullReferenceException">Final Answer is Null or Empty</exception>
            public static string AskUser(string message, ConsoleColor color = ConsoleColor.White)
            {
                string answer;
                bool result;

                do
                {
                    ShowConsoleMessage(message, true, color);
                    answer = Console.ReadLine();

                    result = !string.IsNullOrEmpty(answer);
                    if (!result)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Please input a string with at least a length of 1");
                        Console.ResetColor();
                    }

                    Console.WriteLine();
                } while (!result);

                if (string.IsNullOrEmpty(answer)) throw new NullReferenceException("The given answer is either null or empty");

                return answer;
            }

            /// <summary>
            /// Affiche la question donnée en écrivant la question dans la couleur assignée aux questions (visuellement plus agréable, mais nécessaire)
            /// </summary>
            /// <param name="message">Message à afficher</param>
            public static void ShowConsoleMessage(object message, bool useWriteLine = true, ConsoleColor messageColor = ConsoleColor.White)
            {
                Console.ForegroundColor = messageColor;
                Console.Write(message);
                if (useWriteLine) Console.WriteLine();
                Console.ResetColor();
            }

            /// <summary>
            /// Shows a menu with the given options
            /// </summary>
            /// <param name="options">All the available options</param>
            /// <param name="clearConsole">Does the menu cause a clear of the console ?</param>
            /// <param name="updownChar">Character used for the top and bottom row</param>
            /// <param name="sideChar">Character used for the side row</param>
            /// <returns>Index of the selected option</returns>
            /// <exception cref="InvalidArrayLength"></exception>
            public static int ShowMenu(string[] options, bool clearConsole = true, char updownChar = '-', char sideChar = '|')
            {
                if (options.Length == 0)
                    throw new InvalidArrayLength("The given array doesn't contain any item");

                int[] lengths = new int[options.Length];
                for (int i = 0; i < options.Length; i++)
                {
                    options[i] = $"{sideChar} {i + 1}. {options[i]}";
                    lengths[i] = options[i].Length;
                }

                // Find longest line
                int longestOption = NumberUtils.Max(lengths).Key;

                for (int i = 0; i < options.Length; i++)
                {
                    int lengthDiff = longestOption + 1 - options[i].Length;
                    for (int j = 0; j <= lengthDiff; j++)
                    {
                        options[i] += " ";
                    }

                    options[i] += sideChar;
                }

                int index = 0;

                do
                {
                    if (clearConsole)
                        Console.Clear();

                    for (int i = 0; i < options[0].Length; i++)
                    {
                        Console.Write(updownChar);
                    }
                    Console.WriteLine();

                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.WriteLine(options[i]);
                    }

                    for (int i = 0; i < options[0].Length; i++)
                    {
                        Console.Write(updownChar);
                    }
                    Console.WriteLine();
                    Console.WriteLine();

                    index = AskUser<int>("Enter the desired option's number");
                } while (index <= 0 || index > options.Length);

                if (clearConsole)
                    Console.Clear();
                return index;
            }
        }

        /// <summary>
        /// Verify if the given value can be converted to the given type
        /// </summary>
        /// <typeparam name="T">Type to convert the value to</typeparam>
        /// <param name="value">Value to convert</param>
        /// <returns>Can be converted</returns>
        public static bool CanValueConvertToType<T>(object value)
        {
            try
            {
                T test = (T)Convert.ChangeType(value, typeof(T));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Utilities for using Arrays
        /// </summary>
        public class ArrayUtils
        {
            /// <returns>Do the given arrays have the same items in the same order ?</returns>
            public static bool IsAbsoluteSame<T>(T[] array1, T[] array2)
            {
                if (array1.Length != array2.Length)
                    return false;

                for (int i = 0; i < array1.Length; i++)
                {
                    if (array1[i].ToString() != array2[i].ToString())
                        return false;
                }
                return true;
            }

            /// <returns>Do the given arrays have the same items ?</returns>
            public static bool IsSame<T>(T[] array1, T[] array2)
            {
                T[] tempArray1 = (T[])array1.Clone();
                T[] tempArray2 = (T[])array2.Clone();

                Array.Sort(tempArray1);
                Array.Sort(tempArray2);

                return IsAbsoluteSame(tempArray1, tempArray2);
            }

            /// <summary>
            /// Print the given array with the given name
            /// </summary>
            public static void ShowArray<T>(T[] array, string arrayName = "Array")
            {
                ConsoleUtils.ShowConsoleMessage(arrayName + " { ", false);
                for (int i = 0; i < array.Length; i++)
                {
                    ConsoleUtils.ShowConsoleMessage($"{array[i]} ", false, ConsoleColor.Blue);
                }
                ConsoleUtils.ShowConsoleMessage("}");
            }

            /// <returns>Returns an array of the given length that only contains the given value</returns>
            public static T[] InitiliaseArrayToValue<T>(T value, int length)
            {
                if (length == 0)
                    return new T[] { };

                T[] tempArray = new T[length];

                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempArray[i] = value;
                }

                return tempArray;
            }

            /// <summary>
            /// Swaps two items together
            /// </summary>
            /// <param name="index1">Index of the first item</param>
            /// <param name="index2">Index of the second item</param>
            /// <exception cref="IndexOutOfRangeException"></exception>
            public static void InterchangeCells<T>(T[] array, int index1, int index2)
            {
                if (array.Length < 2)
                    return;

                if (index1 == index2)
                    return;

                if (!NumberUtils.IsInBounds(0, array.Length - 1, index1))
                    throw new IndexOutOfRangeException();

                if (!NumberUtils.IsInBounds(0, array.Length - 1, index2))
                    throw new IndexOutOfRangeException();

                T tempValue = array[index1];
                array[index1] = array[index2];
                array[index2] = tempValue;
            }

            /// <returns>Selected portion of the given array</returns>
            public static T[] GetArrayPortion<T>(T[] array, int startIndex, int length)
            {
                if (array.Length == 0)
                    return new T[] { };

                if (array.Length < length)
                    throw new InvalidArrayLength("The given array is shorter than the given length of the segment");

                if (array.Length < startIndex + length)
                    throw new IndexOutOfRangeException();

                T[] arrayPortion = new T[length];
                Array.Copy(array, startIndex, arrayPortion, 0, length);
                return arrayPortion;
            }
        }

        /// <summary>
        /// Utilities for using Numbers
        /// </summary>
        public class NumberUtils
        {
            /// <returns>Display of the given number (Last Display is for 10^78)</returns>
            /// <exception cref="InvalidTypeException"></exception>
            public static string SimplifyNumber<T>(T number)
            {
                if (!IsNumericType(number))
                    throw new InvalidTypeException("Given number isn't a number");

                string result = number.ToString().Split(',')[0];
                string[] letters = new string[] { "K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "d", "U", "D", "!", "@", "#", "$", "%", "^", "&", "*", "[", "]", "{", "}", ";" };

                if (result.Length < 4)
                    return result;

                int index = 0;
                while (result.Length - 3 * (index + 1) > 3) { index++; }

                int commaPos = result.Length - 3 * (index + 1);
                string rest = result.Substring(commaPos, 4 - commaPos);

                while (rest.Substring(rest.Length - 1, 1) == "0")
                {
                    if (rest.Length == 1)
                    {
                        rest = "";
                        break;
                    }

                    rest = rest.Substring(0, rest.Length - 1);
                }

                string extension = letters[index];

                // Simplified version ? => A -> B -> C -> ... -> AA -> AB -> AC

                if (rest.Length == 0)
                    return result.Substring(0, commaPos) + rest + extension;

                return result.Substring(0, commaPos) + "," + rest + extension;
            }

            /// <summary>
            /// Checks if the given array has at least one item and if the array contains numbers
            /// </summary>
            /// <exception cref="InvalidArrayLength"></exception>
            /// <exception cref="InvalidTypeException"></exception>
            private static void NumericArrayCheck<T>(T[] array)
            {
                if (array.Length == 0)
                    throw new InvalidArrayLength("Given array has a length of 0");

                if (!IsNumericType(array[0]))
                    throw new InvalidTypeException("The given array doesn't contain numbers");
            }

            /// <summary>
            /// Checks if the given value is within the two bounds
            /// </summary>
            /// <returns>Is the value within the two bounds ?</returns>
            /// <exception cref="InvalidTypeException"></exception>
            /// <exception cref="InvalidBoundsException"></exception>
            public static bool IsInBounds<T>(T minBound, T maxBound, T number)
            {
                if (!IsNumericType(minBound))
                    throw new InvalidTypeException("Min Bound doesn't represent a number");

                if (!IsNumericType(maxBound))
                    throw new InvalidTypeException("Max Bound doesn't represent a number");

                if (!IsNumericType(number))
                    throw new InvalidTypeException("The given value doesn't represent a number");

                if (IsEqual(minBound, maxBound))
                {
                    if (IsEqual(minBound, number))
                        return true;
                    else
                        throw new InvalidBoundsException("Min Bound can't be equal to Max Bound");
                }

                if (IsGreaterThan(minBound, maxBound))
                    throw new InvalidBoundsException("Min Bound is greater than Max Bound");

                if (IsLessThan(maxBound, minBound))
                    throw new InvalidBoundsException("Max Bound is less than Min Bound");

                return IsGreaterThanOrEqual(number, minBound) && IsLessThanOrEqual(number, maxBound);
            }

            /// <summary>
            /// Chceks if number is prime
            /// </summary>
            /// <returns>Is the number prime ?</returns>
            public static bool IsPrimeNumber(int number)
            {
                if (Math.Sign(number) == -1)
                    return false;

                int multiplierCount = 0;
                int i = 1;

                while (multiplierCount < 2 && i < number)
                {
                    if (number % i == 0 && number != i)
                    {
                        multiplierCount++;
                    }

                    i++;
                }

                return multiplierCount == 2;
            }

            /// <returns>Biggest number between the given numbers</returns>
            public static T Max<T>(T obj1, T obj2)
            {


                if (IsGreaterThanOrEqual(obj1, obj2))
                    return obj1;
                return obj2;
            }

            /// <summary>
            /// Finds the biggest number in the given array
            /// </summary>
            /// <param name="array">Array to check in</param>
            /// <returns>KeyValuePair<index, biggest number></returns>
            /// <exception cref="InvalidArrayLength"></exception>
            /// <exception cref="InvalidTypeException"></exception>
            public static KeyValuePair<T, int[]> Max<T>(T[] array)
            {
                NumericArrayCheck(array);

                KeyValuePair<T, int[]> biggestNumberUtils = new KeyValuePair<T, int[]>(array[0], new int[] { 0 });

                if (array.Length != 1)
                {
                    for (int i = 1; i < array.Length; i++)
                    {
                        if (IsGreaterThan(array[i], biggestNumberUtils.Key))
                            biggestNumberUtils = new KeyValuePair<T, int[]>(array[i], new int[] { i });
                        else if (IsEqual(array[i], biggestNumberUtils.Key))
                            biggestNumberUtils = new KeyValuePair<T, int[]>(array[i], biggestNumberUtils.Value.Append(i).ToArray());
                    }
                }
                return biggestNumberUtils;
            }

            /// <returns>Smallest number between the given numbers</returns>
            public static T Min<T>(T obj1, T obj2)
            {
                if (IsLessThanOrEqual(obj1, obj2))
                    return obj1;
                return obj2;
            }

            /// <returns>Smallest number in the given array</returns>
            public static KeyValuePair<T, int[]> Min<T>(T[] array)
            {
                NumericArrayCheck(array);

                KeyValuePair<T, int[]> smallestNumberUtils = new KeyValuePair<T, int[]>(array[0], new int[] { 0 });

                if (array.Length != 1)
                {
                    for (int i = 1; i < array.Length; i++)
                    {
                        if (IsLessThan(array[i], smallestNumberUtils.Key))
                            smallestNumberUtils = new KeyValuePair<T, int[]>(array[i], new int[] { i });
                        else if (IsEqual(array[i], smallestNumberUtils.Key))
                            new KeyValuePair<T, int[]>(array[i], smallestNumberUtils.Value.Append(i).ToArray());
                    }
                }
                return smallestNumberUtils;
            }

            /// <returns>Sum of the given array's items</returns>
            public static T Sum<T>(T[] numbers)
            {
                NumericArrayCheck(numbers);

                dynamic somme = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    somme += (dynamic)numbers[i];
                }

                return somme;
            }

            /// <returns>Average of the given array's items</returns>
            public static T Average<T>(T[] numbers)
            {
                NumericArrayCheck(numbers);

                return Sum(numbers) / (dynamic)numbers.Length;
            }

            /// <summary>
            /// Removes every even numbers of the array
            /// </summary>
            /// <param name="array">Array to remove the items from</param>
            /// <returns>Sorted array</returns>
            /// <exception cref="InvalidArrayLength"></exception>
            /// <exception cref="InvalidTypeException"></exception>
            public static void RemoveEvenNumbers<T>(ref T[] array)
            {
                NumericArrayCheck(array);

                List<T> evenNumberUtils = new List<T> { };

                for (int i = 0; i < array.Length; i++)
                {
                    if (!IsEven(array[i]))
                        evenNumberUtils.Add(array[i]);
                }

                array = evenNumberUtils.ToArray();
            }

            /// <summary>
            /// Removes every odd numbers of the array
            /// </summary>
            /// <param name="array">Array to remove the items from</param>
            /// <returns>Sorted array</returns>
            /// <exception cref="InvalidArrayLength"></exception>
            /// <exception cref="InvalidTypeException"></exception>
            public static void RemoveOddNumbers<T>(ref T[] array)
            {
                NumericArrayCheck(array);

                List<T> oddNumberUtils = new List<T> { };

                for (int i = 0; i < array.Length; i++)
                {
                    if (IsEven(array[i]))
                        oddNumberUtils.Add(array[i]);
                }

                array = oddNumberUtils.ToArray();
            }

            // Numeric Type Check: https://stackoverflow.com/questions/1749966/c-sharp-how-to-determine-whether-a-type-is-a-number/1750093#1750093
            /// <summary>
            /// Checks if the given object is a number
            /// </summary>
            /// <param name="o">Object to check</param>
            /// <returns>Is the object a number ?</returns>
            public static bool IsNumericType(object o)
            {
                switch (Type.GetTypeCode(o.GetType()))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        return true;
                    default:
                        return false;
                }
            }

            // Operator Logic: https://stackoverflow.com/a/11616132
            /// <summary>
            /// Checks if the two objects are numbers and calculates the difference
            /// </summary>
            /// <param name="obj1">First number</param>
            /// <param name="obj2">Second number</param>
            /// <returns>Result of obj1 - obj2</returns>
            /// <exception cref="InvalidTypeException"></exception>
            private static int CompareTwoNumberUtils<T>(T obj1, T obj2)
            {
                if (!IsNumericType(obj1))
                    throw new InvalidTypeException("Object 1 isn't a number");

                if (!IsNumericType(obj2))
                    throw new InvalidTypeException("Object 2 isn't a number");

                return ((IComparable)obj1).CompareTo(obj2);
            }

            /// <summary>
            /// Checks if the first number is greater than the second (X > Y)
            /// </summary>
            /// <param name="obj1">X</param>
            /// <param name="obj2">Y</param>
            /// <returns>Is X greater than Y ?</returns>
            public static bool IsGreaterThan<T>(T obj1, T obj2)
            {
                return CompareTwoNumberUtils(obj1, obj2) > 0;
            }

            /// <summary>
            /// Checks if the given object is an even number
            /// </summary>
            /// <param name="obj">Object to verify</param>
            /// <returns>Is the object an even number ?</returns>
            /// <exception cref="InvalidTypeException"></exception>
            public static bool IsEven<T>(T obj)
            {
                if (!IsNumericType(obj))
                    throw new InvalidTypeException("The given object isn't a number");

                string value = obj.ToString();

                return int.Parse(value.Substring(value.Length - 1, 1)) % 2 == 0;
            }

            /// <summary>
            /// Checks if the first number is greater than or equal to the second (X >= Y)
            /// </summary>
            /// <param name="obj1">X</param>
            /// <param name="obj2">Y</param>
            /// <returns>Is X greater than or equal to Y ?</returns>
            public static bool IsGreaterThanOrEqual<T>(T obj1, T obj2)
            {
                return CompareTwoNumberUtils(obj1, obj2) >= 0;
            }

            /// <summary>
            /// Checks if the first number is less than the second (X < Y)
            /// </summary>
            /// <param name="obj1">X</param>
            /// <param name="obj2">Y</param>
            /// <returns>Is X less than Y ?</returns>
            public static bool IsLessThan<T>(T obj1, T obj2)
            {
                return CompareTwoNumberUtils(obj1, obj2) < 0;
            }

            /// <summary>
            /// Checks if the first number is less than or equal to the second (X =< Y)
            /// </summary>
            /// <param name="obj1">X</param>
            /// <param name="obj2">Y</param>
            /// <returns>Is X less than or equal to Y ?</returns>
            public static bool IsLessThanOrEqual<T>(T obj1, T obj2)
            {
                return CompareTwoNumberUtils(obj1, obj2) <= 0;
            }

            /// <summary>
            /// Checks if the first number equal to the second (X == Y)
            /// </summary>
            /// <param name="obj1">X</param>
            /// <param name="obj2">Y</param>
            /// <returns>Is X equal to Y ?</returns>
            public static bool IsEqual<T>(T obj1, T obj2)
            {
                return CompareTwoNumberUtils(obj1, obj2) == 0;
            }
        }
    }

    public class CustomExceptions
    {
        public class InvalidArrayLength : SystemException
        {
            public InvalidArrayLength(string message) : base(message) { }
        }

        public class InvalidTypeException : SystemException
        {
            public InvalidTypeException(string message) : base(message) { }
        }

        public class InvalidBoundsException : SystemException
        {
            public InvalidBoundsException(string message) : base(message) { }
        }

        public class ArrayComparaisonException : SystemException
        {
            public ArrayComparaisonException(string message) : base(message) { }
        }
    }
}