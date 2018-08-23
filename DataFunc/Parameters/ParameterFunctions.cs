using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DataFunc.Parameters
{
    public static class ParameterFunctions
    {
        public static T ToDbParameter<T>(this string name, DbType type, object value) where T : DbParameter, new()
            => new T
            {
                ParameterName = name,
                DbType = type,
                Value = value
            };

        public static T ToDbParameter<T>(this string name, DbType type, object value, byte precision) where T : DbParameter, new()
            => new T
            {
                ParameterName = name,
                DbType = type,
                Value = value,
                Precision = precision
            };

        public static T ToDbParameter<T>(this string name, DbType type, object value, byte precision, byte scale) where T : DbParameter, new()
            => new T
            {
                ParameterName = name,
                DbType = type,
                Value = value,
                Precision = precision,
                Scale = scale
            };

        public static T ToDbParameter<T>(this string name, DbType type, object value, int size) where T : DbParameter, new()
            => new T
            {
                ParameterName = name,
                DbType = type,
                Value = value,
                Size = size
            };

        public static T ToDbParameter<T>(this DataRow row, string name, DbType type, object value) where T : DbParameter, new()
            => new T
            {
                ParameterName = $"@{name}",
                DbType = type,
                Value = row[name]
            };

        public static T ToDbParameter<T>(this DataRow row, string name, DbType type, object value, int size) where T : DbParameter, new()
            => new T
            {
                ParameterName = $"@{name}",
                DbType = type,
                Value = row[name],
                Size = size
            };

        #region String

        public static T ToAnsiStringDbParameter<T>(this string name, string value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.AnsiString, value);

        public static T ToAnsiFixedLengthStringDbParameter<T>(this string name, string value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.AnsiStringFixedLength, value);

        public static T ToAnsiFixedLengthStringDbParameter<T>(this string name, char value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.AnsiStringFixedLength, value);

        public static T ToStringDbParameter<T>(this string name, string value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.String, value);

        public static T ToStringDbParameter<T>(this string name, char[] value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.String, value);

        public static T ToFixedLengthStringDbParameter<T>(this string name, string value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.StringFixedLength, value);

        #endregion

        #region Int

        public static T ToInt16DbParameter<T>(this string name, short value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Int16, value);

        public static T ToInt32Parameter<T>(this string name, int value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Int32, value);

        public static T ToInt64DbParameter<T>(this string name, long value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Int64, value);

        public static T ToUInt16DbParameter<T>(this string name, ushort value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.UInt16, value);

        public static T ToUInt32DbParameter<T>(this string name, uint value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.UInt32, value);

        public static T ToUInt64DbParameter<T>(this string name, ulong value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.UInt64, value);

        #endregion

        #region Double

        public static T ToDoubleDbParameter<T>(this string name, double value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Double, value);

        public static T ToDoubleDbParameter<T>(this string name, double value, byte precision) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Double, value, precision);

        #endregion

        #region Decimal

        public static T ToDecimalDbParameter<T>(this string name, object value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Decimal, value);

        public static T ToDecimalDbParameter<T>(this string name, object value, byte precision, byte scale = 0) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Decimal, value, precision, scale);

        #endregion

        #region Single

        public static T ToSingleDbParameter<T>(this string name, float value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Single, value);

        public static T ToSingleDbParameter<T>(this string name, float value, byte precision) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Single, value, precision);

        #endregion

        #region Date

        public static T ToDateDbParameter<T>(this string name, DateTime value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Date, value);

        public static T ToDateTimeDbParameter<T>(this string name, DateTime value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.DateTime, value);

        public static T ToDateTime2DbParameter<T>(this string name, DateTime value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.DateTime2, value);

        public static T ToDateTimeOffsetDbParameter<T>(this string name, DateTimeOffset value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.DateTimeOffset, value);

        public static T ToTimeDbParameter<T>(this string name, TimeSpan value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Time, value);

        #endregion

        #region Boolean

        public static T ToBooleanDbParameter<T>(this string name, bool value) where T : DbParameter, new()
            => name.ToDbParameter<T>(DbType.Boolean, value);

        #endregion
    }
}
