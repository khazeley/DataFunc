using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFunc
{
    public static class UtilityExtensions
    {
        public const string CloseSelect = "</select>";

        public static string ToStringFormatOrNull(this DateTime? date, string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentException("format".ToNullOrWhitespaceMessage(), "format");

            return date.HasValue ? date.Value.ToString(format) : null;
        }

        public static string ToStringFormatOrEmpty(this DateTime? date, string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentException("format".ToNullOrWhitespaceMessage(), "format");

            return date.HasValue ? date.Value.ToString(format) : "";
        }

        public static DateTime? ToNullableDateTime(this string dateText) => string.IsNullOrWhiteSpace(dateText) ? null : (DateTime?)DateTime.Parse(dateText);

        public static string ToNullMessage(this string parameterName) => $"'{parameterName}' cannot be null";

        public static string ToNullOrEmptyMessage(this string parameterName) => $"'{parameterName}' cannot be null or empty";

        public static string ToNullOrWhitespaceMessage(this string parameterName) => $"'{parameterName}' cannot be null or whitespace";

        public static string ToStringOrEmpty(this string text) => string.IsNullOrWhiteSpace(text) ? "" : text;

        public static string ToStringOrNa(this string text) => string.IsNullOrWhiteSpace(text) ? "NA" : text;

        public static IEnumerable<IEnumerable<T>> ToPaged<T>(this IEnumerable<T> values, int pageSize)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values), "values cannot be null");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize must be greater than 0");

            var valuesArray = values.ToArray();
            var valueCount = valuesArray.Length;
            if (valueCount <= pageSize)
                return new List<IEnumerable<T>> { valuesArray };

            var numPages = valueCount / pageSize;
            if (numPages == 0 || valueCount % pageSize > 0)
                numPages++;

            var pagedValues = new List<IEnumerable<T>>(numPages);
            for (var i = 1; i <= numPages; i++)
                pagedValues.Add(valuesArray.Skip((i - 1) * pageSize).Take(pageSize));

            return pagedValues;
        }

        public static bool HasData<T>(this IEnumerable<T> values) => values != null && values.Any();

        public static int? ToPositiveIntOrNull(this int? property) => property.HasValue && property.Value > 0 ? (int?)property.Value : null;

        public static int? ToPositiveIntOrNull(this int property) => property > 0 ? (int?)property : null;

        public static string ToNullIfWhitespace(this string text) => string.IsNullOrWhiteSpace(text) ? null : text;

        public static string ToOption(this string text) => $"<option value=\"{text}\">{text}</option>";

        public static string ToOption(this string text, string value) => $"<option value=\"{value}\">{text}</option>";

        public static string ToSelect(this string id) => $"<select id=\"{id}\"></select>";

        public static string ToOpenSelect(this string id) => $"<select id=\"{id}\">";

        public static string ToSelect(this string id, IEnumerable<string> options)
        {
            var sb = new StringBuilder();
            sb.AppendLine(id.ToOpenSelect());

            options.ToList().ForEach(option => sb.Append($"{option.ToOption()}\n"));

            sb.Append(CloseSelect);

            return sb.ToString();
        }


        public static string ToSelectOptions(this IEnumerable<string> options)
        {
            var sb = new StringBuilder();

            options.ToList().ForEach(option => sb.AppendLine(option.ToOption()));

            return sb.ToString();
        }
    }
}