﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using Newtonsoft.Json;

namespace TWYK.Core
{
    public static class Extensions
    {
        #region Strings

        /// <summary>
        /// Indicates whether the string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if the string is null or empty, or if consists exclusively of white-space characters.</returns>
        public static bool IsNullOrWhiteSpace(this string str) {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Indicates whether the string is null or empty.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if the string is null or empty.</returns>
        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Replaces the format item in the string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns></returns>
        public static string FormatString(
            this string str,
            params object[] args
        ) {
            return string.Format(str, args);
        }

        /// <summary>
        /// Capitalizes each word in the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="currentCulture">If true uses the current culture otherwise the invariant culture. Default: false.</param>
        /// <returns>a string with each word capitalized.</returns>
        public static string ToTitleCase(
            this string str,
            bool currentCulture = false
        ) {
            // If the string is empty or too short
            // returns the string intact.
            if (str.IsNullOrWhiteSpace() || str.Length < 2) {
                return str;
            }

            return currentCulture
                ? CultureInfo
                    .CurrentCulture
                    .TextInfo
                    .ToTitleCase(str)
                : CultureInfo
                    .InvariantCulture
                    .TextInfo
                    .ToTitleCase(str);
        }

        /// <summary>
        /// Capitalizes and joins each word in the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="currentCulture">If true uses the current culture otherwise the invariant culture. Default: false.</param>
        /// <returns>a string with each word capitalized and joined.</returns>
        public static string ToCamelCase(
            this string str,
            bool currentCulture = false
        ) {
            var result = str.ToTitleCase(currentCulture);
            return result
                .Replace(" ", "")
                .Replace("_", "")
                .Replace("-", "")
                .Replace(".", "");
        }

        /// <summary>
        /// Capitalizes, all except the first word, and joins each word in the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="currentCulture">If true uses the current culture otherwise the invariant culture. Default: false.</param>
        /// <returns>a string with each word capitalized, except the first word, and joined.</returns>
        public static string ToPascalCase(
            this string str,
            bool currentCulture = false
        ) {
            var result = str.ToCamelCase(currentCulture);
            return new StringBuilder()
                .Append(result.Substring(0, 1).ToLower())
                .Append(result.Substring(1))
                .ToString();
        }

        /// <summary>
        /// Mask with asterix (*) any string except the last char(s)
        /// </summary>
        /// <param name="str">The string to mask</param>
        /// <param name="lastChars">The number of remaining char(s)</param>
        /// <returns>A masked string</returns>
        public static string MaskExceptLastChars(
            this string str,
            int lastChars = 3
        ) {
            if (str == null) {
                return null;
            }

            var newString = "";

            if (str.Length >= lastChars) {
                for (var i = 0; i < str.Length - lastChars; i++) {
                    newString += "*";
                }

                newString += str.Substring(str.Length - lastChars);
            }
            else {
                newString = str;
            }

            return newString;
        }

        /// <summary>
        /// Mask email to form a M****@****ny.com string
        /// </summary>
        /// <param name="email">Email to mask</param>
        /// <param name="lastChars">Last char(s) of domain name</param>
        /// <returns>Masked email</returns>
        public static string MaskEmails(
            this string email,
            int lastChars = 2
        ) {
            if (email == null) {
                return null;
            }

            var splitEmail = email.Split('@');

            // Mask Account Name
            var firstAccountChar = email.Substring(0, 1);
            var maksedAccountName = "";
            for (var i = 1; i < splitEmail[0].Length; i++) {
                maksedAccountName += "*";
            }

            var accountName = firstAccountChar + maksedAccountName;

            // Mask Domain name
            var dotPos = splitEmail[1].LastIndexOf('.');
            var domainSuffix = splitEmail[1].Substring(dotPos);
            var domainName = splitEmail[1].Substring(0, dotPos);

            var maskedEmail = accountName + "@" + domainName.MaskExceptLastChars(lastChars) + domainSuffix;

            return maskedEmail;
        }

        /// <summary>
        /// Removes the last char(s) of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">The number of char that will be removed. Default: 1.</param>
        /// <returns>the string with the last char(s) removed.</returns>
        public static string RemoveLast(
            this string str,
            int length = 1
        ) {
            return str.Substring(str.Length - length);
        }

        /// <summary>
        /// Removes the first char(s) of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">The number of char that will be removed. Default: 1.</param>
        /// <returns>the string with the first char(s) removed.</returns>
        public static string RemoveFirst(
            this string str,
            int length = 1
        ) {
            return str.Substring(0, length);
        }

        /// <summary>
        /// Creates a Regex with SingleLine and IgnoreCase options.
        /// If isCompile is set to true adds Compile option.
        /// </summary>
        /// <param name="pattern">The Regex pattern.</param>
        /// <param name="isCompile">Indicates if regex will have Compile option. Default: false.</param>
        /// <returns>a Regex with SingleLine and IgnoreCase options.</returns>
        public static Regex ToSingleLineCaseInsensitiveRegex(
            this string pattern,
            bool isCompile = false
        ) {
            return isCompile
                ? new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase)
                : new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }

        public static string TruncateHtml(this string input, int length = 300,
            string ommission = "...")
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}" + ommission, input.Substring(0, (iNextSpace > 0) ?
                iNextSpace : length).Trim());
        }

        public static string StripTags(this string markup)
        {
            try
            {
                StringReader sr = new StringReader(markup);
                XPathDocument doc;
                using (XmlReader xr = XmlReader.Create(sr,
                           new XmlReaderSettings()
                           {
                               ConformanceLevel = ConformanceLevel.Fragment
                               // for multiple roots
                           }))
                {
                    doc = new XPathDocument(xr);
                }

                return doc.CreateNavigator().Value; // .Value is similar to .InnerText of  
                //  XmlDocument or JavaScript's innerText
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion Strings

        #region Nulls

        /// <summary>
        /// Indicates if the struct is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>true if the struct's value is null.</returns>
        public static bool IsNull<T>(this T? value) where T : struct {
            return value == null;
        }

        /// <summary>
        /// Indicates if the class is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>true if the class's value is null.</returns>
        public static bool IsNull<T>(this T value) where T : class {
            return value == null;
        }

        /// <summary>
        /// Indicates if the struct is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>true if the struct's value is not null.</returns>
        public static bool IsNotNull<T>(this T? value) where T : struct {
            return value != null;
        }

        /// <summary>
        /// Indicates if the class is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>true if the class's value is not null.</returns>
        public static bool IsNotNull<T>(this T value) where T : class {
            return value != null;
        }

        /// <summary>
        /// Indicates if the struct is null or has default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>true if the value of the struct is null or its default value.</returns>
        public static bool IsNullOrDefault<T>(this T? value) where T : struct {
            return default(T).Equals(value.GetValueOrDefault());
        }

        /// <summary>
        /// Indicates if the struct is not null or has default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>true if the value of the struct is not null or its default value.</returns>
        public static bool IsNotNullOrDefault<T>(this T? value) where T : struct {
            return !default(T).Equals(value.GetValueOrDefault());
        }

        #endregion Nulls

        #region IEnumerable

        /// <summary>
        /// Indicates if the enumerable is null or has no items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>true if the enumerable is null or has no items.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) {
            return enumerable == null || enumerable.Count() == 0;
        }

        /// <summary>
        /// Indicates if the enumerable is not null and has at least one item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>true if the enumerable is not null and has at least one item.</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable) {
            return enumerable != null && enumerable.Count() > 0;
        }

        #endregion IEnumerable

        #region ChangeType

        /// <summary>
        /// Tries to cast the object to the given type.
        /// Throws exception if do not succeed.
        /// </summary>
        /// <typeparam name="T">The type that the object will be casted to.</typeparam>
        /// <param name="obj"></param>
        /// <returns>the object in the given type.</returns>
        public static T To<T>(this object obj) {
            return (T)obj;
        }

        /// <summary>
        /// Tries to cast the object to the given type.
        /// Returns null if do not succeed.
        /// </summary>
        /// <typeparam name="T">The type that the object will be casted to.</typeparam>
        /// <param name="obj"></param>
        /// <returns>the object in the given type or null if do not succeed.</returns>
        public static T ToOrNull<T>(this object obj) where T : class {
            T result = null;

            result = (T)obj;

            return result;
        }

        /// <summary>
        /// Tries to cast the object to the given type.
        /// Returns default value of given type if do not succeed.
        /// </summary>
        /// <typeparam name="T">The type that the object will be casted to.</typeparam>
        /// <param name="obj"></param>
        /// <returns>the object in the given type or the default value of the given type if do not succeed.</returns>
        public static T ToOrDefault<T>(this object obj) {
            var result = default(T);
            result = (T)obj;

            return result;
        }

        #endregion ChangeType

        #region Jsons

        /// <summary>
        /// Serializes the obj to json string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>the object serialized to json string.</returns>
        public static string ToJson<T>(this T obj) {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes the string to the given type.
        /// </summary>
        /// <typeparam name="T">The type that the json string will be deserialized to.</typeparam>
        /// <param name="json"></param>
        /// <returns>the string deserialized to the given type.</returns>
        public static T FromJson<T>(this string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T FromJson<T>(this object json) {
            return JsonConvert.DeserializeObject<T>(json.ToString());
        }

        #endregion Jsons

        #region Numbers

        /// <summary>
        /// Calculates the percentage of the number to the given total.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total">The total number</param>
        /// <returns>the percentage of the number to the given total.</returns>
        public static double PercentageOf(
            this double value,
            double total
        ) {
            return value / total * 100;
        }

        /// <summary>
        /// Calculates the percentage of the number to the given total.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total">The total number</param>
        /// <returns>the percentage of the number to the given total.</returns>
        public static double PercentageOf(
            this int value,
            int total
        ) {
            return value / total * 100;
        }

        /// <summary>
        /// Calculates the percentage of the number to the given total.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total">The total number</param>
        /// <returns>the percentage of the number to the given total.</returns>
        public static double PercentageOf(
            this int value,
            double total
        ) {
            return value / total * 100;
        }

        /// <summary>
        /// Calculates the percentage of the number to the given total.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total">The total number</param>
        /// <returns>the percentage of the number to the given total.</returns>
        public static double PercentageOf(
            this double value,
            int total
        ) {
            return value / total * 100;
        }

        public static double ToSqlCulture(this double value) {
            var attemptedValue = value.ToString();
            var englishCultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            string wantedSeperator = englishCultureInfo.NumberFormat.NumberDecimalSeparator;
            string alternateSeperator = wantedSeperator == ","
                ? "."
                : ",";

            if (attemptedValue.IndexOf(wantedSeperator, StringComparison.Ordinal) == -1
                && attemptedValue.IndexOf(alternateSeperator, StringComparison.Ordinal) != -1) {
                attemptedValue = attemptedValue.Replace(alternateSeperator, wantedSeperator);
            }

            try {
                if (string.IsNullOrWhiteSpace(attemptedValue)) {
                    return 0;
                }

                value = double.Parse(attemptedValue, NumberStyles.Any);
            }
            catch (FormatException e) {
                throw new Exception(e.Message);
            }

            return value;
        }

        #endregion Numbers

        #region Exceptions

        /// <summary>
        /// Throws NullReferenceException if object is null.
        /// </summary>
        /// <param name="obj"></param>
        public static void ThrowIfNull(this object obj) {
            if (obj != null) {
                return;
            }

            throw new NullReferenceException("Object can't be null!");
        }

        #endregion Exceptions

        /// <summary>
        /// Indicates if the value is equal to at least one of the given args.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="args">An array of values that the value will be checked against.</param>
        /// <returns>true if the value is equal to at least one of the given args.</returns>
        public static bool IsIn<T>(
            this T value,
            params T[] args
        ) {
            return args.Any(arg => arg.Equals(value));
        }

        /// <summary>
        /// Indicates if the value is equal to at least one of the given args.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="args">An enumerable of values that the value will be checked against.</param>
        /// <returns>true if the value is equal to at least one of the given args.</returns>
        public static bool IsIn<T>(
            this T value,
            IEnumerable<T> args
        ) {
            return args.Any(arg => arg.Equals(value));
        }

        /// <summary>
        /// Indicates if the value is equal to at least one of the given args.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args">An array of values that the value will be checked against.</param>
        /// <returns>true if the value is equal to at least one of the given args.</returns>
        public static bool IsIn(
            this IUID value,
            params IUID[] args
        ) {
            return args.Any(arg => arg.Id.Equals(value.Id));
        }

        /// <summary>
        /// Indicates if the value is not equal to any of the given args.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="args">An array of values that the value will be checked against.</param>
        /// <returns>true if the value is not equal to any of the given args.</returns>
        public static bool IsNotIn<T>(
            this T value,
            params T[] args
        ) {
            return args.All(arg => !arg.Equals(value));
        }

        /// <summary>
        /// Indicates if the value is not equal to any of the given args.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="args">An enumerable of values that the value will be checked against.</param>
        /// <returns>true if the value is not equal to any of the given args.</returns>
        public static bool IsNotIn<T>(
            this T value,
            IEnumerable<T> args
        ) {
            return args.All(arg => !arg.Equals(value));
        }

        /// <summary>
        /// Indicates if the value is not equal to any of the given args.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="args">An array of values that the value will be checked against.</param>
        /// <returns>true if the value is not equal to any of the given args.</returns>
        public static bool IsNotIn(
            this IUID value,
            params IUID[] args
        ) {
            return args.All(arg => !arg.Id.Equals(value.Id));
        }

        /// <summary>
        /// Indicates if the value is between the min and max values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value that will be checked.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value/</param>
        /// <returns>true if the value is between the min and max values.</returns>
        public static bool IsBetween<T>(
            this T value,
            T min,
            T max
        ) {
            return Comparer<T>.Default.Compare(value, min) >= 0 &&
                   Comparer<T>.Default.Compare(value, max) <= 0;
        }

        /// <summary>
        /// Indicates if the value is not between the min and max values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value that will be checked.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value/</param>
        /// <returns>true if the value is not between the min and max values.</returns>
        public static bool IsNotBetween<T>(
            this T value,
            T min,
            T max
        ) {
            return Comparer<T>.Default.Compare(value, min) < 0 ||
                   Comparer<T>.Default.Compare(value, max) > 0;
        }
    }
}