// -----------------------------------------------------------------------
//  <copyright file="ExcelFileReader.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Excel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ClosedXML.Excel;
    using JetBrains.Annotations;

    public class ExcelFileReader : IExcelFileReader, IDisposable
    {
        string _fileName;

        [NotNull]
        readonly XLWorkbook _workBook;

        public ExcelFileReader(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name must be valid path.", nameof(fileName));
            }

            _fileName = Path.GetFullPath(fileName);

            _workBook = new XLWorkbook(_fileName);
        }

        /// <inheritdoc />
        public void CopyRange(string @from, string to, uint sheetNumber = 1)
        {
            var ws = _workBook.Worksheet((int)sheetNumber);

            var fromRange = ws.Range(from);
            var toRange = ws.Range(to);

            ws.Cell(to).Value = fromRange;
        }

        /// <inheritdoc />
        public void CutRange(string from, string to, uint sheetNumber = 1)
        {
            var ws = _workBook.Worksheet((int)sheetNumber);

            CopyRange(from, to, sheetNumber);

            ws.Cell(from).Clear();
        }

        /// <inheritdoc />
        public void AdjustColumnsWidth(uint sheetNumber = 1)
        {
            var ws = _workBook.Worksheet((int)sheetNumber);

            ws.Columns().AdjustToContents();
        }

        /// <inheritdoc />
        public void SetColumnWidth(string column, double width, uint sheetNumber = 1)
        {
            var ws = _workBook.Worksheet((int)sheetNumber);

            ws.Column(column).Width = width;
        }

        /// <inheritdoc />
        public IList<string> GetColumnData(uint rowNumber, string column, uint sheetNumber = 1)
        {
            if (rowNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(rowNumber));

            if (!column.All(c => char.IsLetter(c)) && column.Length > 0)
                throw new ArgumentException(message: "The column format is invalid. Use format [a-zA-Z]+");

            var result = new List<string>();

            var ws = _workBook.Worksheet((int)sheetNumber);
            var range = ws.RangeUsed();

            var sheetRowCount = range.RowCount();

            var dataRowCount = sheetRowCount - (rowNumber - 1);

            var cellRange = ws.Range($"{column}{rowNumber}", $"{column}{rowNumber + dataRowCount}");

            foreach (var o in cellRange.RowsUsed())
            {
                if (string.IsNullOrEmpty(o.ToString()))
                    result.Add(o.ToString());
            }

            return result;
        }

        /// <inheritdoc />
        public IList<IList<string>> GetRangeValues(ExcelCellLocation cell, uint columnSpan = 0, uint sheetNumber = 1)
        {
            var result = new List<IList<string>>();

            var ws = _workBook.Worksheet((int)sheetNumber);
            var range = ws.RangeUsed();
            var rowCount = range.RowCount();
            var dataRowCount = rowCount - (cell.Row - 1);

            var cellRange = ws.Range($"{cell}", $"{cell.Column + columnSpan}{cell.Row + dataRowCount}");

            foreach (var o in cellRange.RowsUsed())
            {
                var innerList = new List<string>();
                foreach (var c in o.Cells())
                {
                    innerList.Add(c.ToString());
                }
                result.Add(innerList);
            }

            return result;
        }

        /// <inheritdoc />
        public IList<IDictionary<string, string>> GetRangeValuesMap(ExcelCellLocation cell, uint columnSpan = 0, uint sheetNumber = 1)
        {
            var result = new List<IDictionary<string, string>>();

            var ws = _workBook.Worksheet((int)sheetNumber);
            var range = ws.RangeUsed();
            var rowCount = range.RowCount();
            var dataRowCount = rowCount - (cell.Row - 1);

            var lastColumn = new ExcelCellLocation("A", 1).IncrementColumn(range.ColumnCount()).IncrementRow(range.RowCount());
            lastColumn = ExcelCellLocation.Parse(ws.LastCellUsed().ToString());
            var to = columnSpan == 0 ? lastColumn.ToString() : $"{cell.Column + columnSpan}{cell.Row + dataRowCount}";
            var cellRange = ws.Range($"{cell}", to);

            foreach (var o in cellRange.RowsUsed())
            {
                var innerList = new Dictionary<string, string>();
                foreach (var col in o.Cells())
                {
                    innerList.Add(col.Address.ColumnLetter, col.ToString());
                }
                result.Add(innerList);
            }

            return result;
        }

        /// <inheritdoc />
        public void Save()
        {
            _workBook.Save();
        }

        /// <inheritdoc />
        public void SetCell(string cell, string value, uint sheetNumber = 1)
        {
            var ws = _workBook.Worksheet((int)sheetNumber);
            var c = ws.Cell(cell);
            c.Value = value;
        }

        /// <inheritdoc />
        public void MergeCells(ExcelCellRange range, uint sheetNumber = 1)
        {
            var ws = _workBook.Worksheet((int)sheetNumber);
            ws.Range(range.FirstCell.ToString(), range.SecondCell.ToString()).Merge();
        }

        /// <inheritdoc />
        public void CenterCells(ExcelCellRange range, uint sheetNumber = 1)
        {
            var ws = _workBook.Worksheet((int)sheetNumber);
            var r = ws.Range(range.FirstCell.ToString(), range.SecondCell.ToString());

            r.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            r.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _workBook.Dispose();
        }
    }
}