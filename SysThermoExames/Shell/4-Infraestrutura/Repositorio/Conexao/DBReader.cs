using Microsoft.Data.Sqlite;
using System;

namespace Repositorio.Conexao
{
    public static class ExtensoesDBReader
    {
        public static int GetInt32(this SqliteDataReader dr, string coluna)
        {
            return dr.GetInt32(dr.GetOrdinal(coluna));
        }

        public static object GetValue(this SqliteDataReader dr, string coluna)
        {
            return dr.GetValue(dr.GetOrdinal(coluna));
        }

        public static bool IsDBNull(this SqliteDataReader dr, string coluna)
        {
            return dr.GetBoolean(dr.GetOrdinal(coluna));
        }

        public static long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public static byte GetByte(this SqliteDataReader dr, string coluna)
        {
            return dr.GetByte(dr.GetOrdinal(coluna));
        }

        public static Type GetFieldType(this SqliteDataReader dr, string coluna)
        {
            return dr.GetFieldType(dr.GetOrdinal(coluna));
        }

        public static decimal GetDecimal(this SqliteDataReader dr, string coluna)
        {
            return dr.GetDecimal(dr.GetOrdinal(coluna));
        }

        public static int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public static string GetName(this SqliteDataReader dr, string coluna)
        {
            return dr.GetName(dr.GetOrdinal(coluna));
        }

        public static long GetInt64(this SqliteDataReader dr, string coluna)
        {
            return dr.GetInt64(dr.GetOrdinal(coluna));
        }

        public static double GetDouble(this SqliteDataReader dr, string coluna)
        {
            return dr.GetDouble(dr.GetOrdinal(coluna));
        }

        public static bool GetBoolean(this SqliteDataReader dr, string coluna)
        {
            return dr.GetBoolean(dr.GetOrdinal(coluna));
        }

        public static Guid GetGuid(this SqliteDataReader dr, string coluna)
        {
            return dr.GetGuid(dr.GetOrdinal(coluna));
        }

        public static DateTime GetDateTime(this SqliteDataReader dr, string coluna)
        {
            return dr.GetDateTime(dr.GetOrdinal(coluna));
        }

        public static string GetDataTypeName(this SqliteDataReader dr, string coluna)
        {
            return dr.GetDataTypeName(dr.GetOrdinal(coluna));
        }

        public static float GetFloat(this SqliteDataReader dr, string coluna)
        {
            return dr.GetFloat(dr.GetOrdinal(coluna));
        }

        public static long
          GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public static string GetString(this SqliteDataReader dr, string coluna)
        {
            return dr.GetString(dr.GetOrdinal(coluna));
        }

        public static char GetChar(this SqliteDataReader dr, string coluna)
        {
            return dr.GetChar(dr.GetOrdinal(coluna));
        }

        public static short GetInt16(this SqliteDataReader dr, string coluna)
        {
            return dr.GetInt16(dr.GetOrdinal(coluna));
        }

    }
}
