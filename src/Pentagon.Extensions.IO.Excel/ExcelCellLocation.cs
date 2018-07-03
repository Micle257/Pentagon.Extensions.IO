// -----------------------------------------------------------------------
//  <copyright file="ExcelCellLocation.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Excel
{
    using System;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;

    public struct ExcelCellLocation
    {
        const string ColumnValues = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public ExcelCellLocation([NotNull] string column, uint row)
        {
            Column = column ?? throw new ArgumentNullException(nameof(column));
            Column = column.ToUpper();

            if (!Column.All(c => ColumnValues.ToCharArray().Any(cc => c == cc)))
                throw new ArgumentException(message: "The column must be constructed from letters.");

            if (row < 1)
                throw new ArgumentException(message: "The excel row must be greater than zero.");
            Row = row;
        }

        public ExcelCellLocation(int column, uint row) : this(ConvertToStringColumn(column), row) { }

        public string Column { get; }

        public uint Row { get; }

        public static bool TryParse(string value, out ExcelCellLocation cellLocation)
        {
            cellLocation = default(ExcelCellLocation);

            var column = new string(value.ToUpper().TakeWhile(c => ColumnValues.Any(cc => cc == c)).ToArray());

            if (string.IsNullOrWhiteSpace(column))
                return false;

            var rowText = new string(value.SkipWhile(c => ColumnValues.Any(cc => cc == c)).ToArray());

            if (!uint.TryParse(rowText, out var row))
                return false;

            cellLocation = new ExcelCellLocation(column, row);
            return true;
        }

        public static ExcelCellLocation Parse(string value)
        {
            if (!TryParse(value, out var cellLocation))
                throw new FormatException(message: "Excel buňka není ve správném formátu.");
            return cellLocation;
        }

        public override string ToString() => $"{Column}{Row}";

        public ExcelCellLocation IncrementRow(int increment)
        {
            if (Row + increment < 1)
                throw new ArgumentException(message: "The excel row must be greater than zero.");

            return new ExcelCellLocation(Column, (uint) (Row + increment));
        }

        public ExcelCellLocation IncrementColumn(int increment)
        {
            //var letterCollection = ColumnValues.Select(a => $"{a}").ToList();
            //var letterCount = letterCollection.Count;

            //var j = 0;
            //var sum = 0;
            //for (var i = Column.Length - 1; i >= 0; i--, j++)
            //{
            //    var add = j == 0 ? 0 : 1;
            //    var ch = Column[i];
            //    var chIndex = letterCollection.IndexOf(ch.ToString()) + add;
            //    sum += chIndex + (int)Math.Pow( letterCount, j);
            //}

            var sum = ConvertColumnStringToNumber(Column);

            var column = ConvertDecadeNumberToExcelLetterNumber(sum + increment);

            return new ExcelCellLocation(column, Row);
        }

        public ExcelCellLocation Increment(int column, int row) => IncrementRow(row).IncrementColumn(column);

        static string ConvertToStringColumn(int column) => throw new NotImplementedException();

        int ConvertColumnStringToNumber(string column)
        {
            var name = column.ToUpperInvariant();
            var result = 0;
            var digs = new int[column.Length];
            for (var i = 0; i < column.Length; i++)
            {
                result *= ColumnValues.Length;
                result += name[i] - ColumnValues[0] + 1;
            }

            return result;
        }

        string ConvertDecadeNumberToExcelLetterNumber(int number)
        {
            if (number < 0)
                throw new ArgumentException(message: "The number must be grater than or equal to zero.");

            var count = 'Z' - 'A' + 1;
            var dividend = number;
            var columnNameBuilder = new StringBuilder();
            var modulo = 0;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % count;
                columnNameBuilder.Insert(0, (char) ('A' + modulo));
                dividend = (dividend - modulo) / count;
            }

            return columnNameBuilder.ToString();
        }
    }
}