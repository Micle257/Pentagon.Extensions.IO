// -----------------------------------------------------------------------
//  <copyright file="ExcelFileReader.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using NetOffice.ExcelApi;
    using NetOffice.ExcelApi.Enums;

    public class ExcelFileReader : IExcelFileReader, IDisposable
    {
        readonly string _fileName;
        readonly Application _app;
        readonly Workbook _workBook;
        readonly Stack<object> _comObjects = new Stack<object>();

        public ExcelFileReader(string fileName)
        {
            _fileName = fileName;
            _app = new Application();
            _app.Visible = false;
            _workBook = _app.Workbooks.Open(_fileName);
        }

        ~ExcelFileReader()
        {
            ReleaseUnmanagedResources();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        public void AdjustColumnsWidth(uint sheetNumber = 1)
        {
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var range = sheet.UsedRange;
            range.Columns.AutoFit();

            _comObjects.Push(sheet);
            _comObjects.Push(range);
        }

        /// <inheritdoc />
        public void SetColumnWidth(string column, double width, uint sheetNumber = 1)
        {
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var range = sheet.Cells[1, column];
            range.ColumnWidth = width;

            _comObjects.Push(sheet);
            _comObjects.Push(range);
        }

        public IList<string> GetColumnData(uint rowNumber, string column, uint sheetNumber = 1)
        {
            if (rowNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(rowNumber));

            if (!column.All(c => char.IsLetter(c)) && column.Length > 0)
                throw new ArgumentException(message: "The column format is invalid. Use format [a-zA-Z]+");

            var result = new List<string>();
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var range = sheet.UsedRange;

            var sheetRowCount = range.Rows.Count;

            var dataRowCount = sheetRowCount - (rowNumber - 1);

            var cellRange = sheet.get_Range($"{column}{rowNumber}:{column}{rowNumber + dataRowCount}", Missing.Value);

            foreach (var o in cellRange)
            {
                if (o.Text != null)
                    result.Add(o.Text as string ?? o.Text.ToString());
            }

            _comObjects.Push(sheet);
            _comObjects.Push(range);
            _comObjects.Push(cellRange);

            return result;
        }

        /// <inheritdoc />
        public IList<IList<string>> GetRangeValues(ExcelCellLocation cell, uint columnSpan, uint sheetNumber = 1)
        {
            var result = new List<IList<string>>();

            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var range = sheet.UsedRange;
            var rowCount = range.Rows.Count;
            var dataRowCount = rowCount - (cell.Row - 1);
            var cellRange = sheet.get_Range($"{cell}", $"{cell.Column + columnSpan}{cell.Row + dataRowCount}");

            foreach (var o in cellRange.Rows)
            {
                var innerList = new List<string>();
                foreach (var col in o.Columns)
                    innerList.Add(col.Text as string ?? col.Text?.ToString());
                result.Add(innerList);
            }

            _comObjects.Push(sheet);
            _comObjects.Push(range);
            _comObjects.Push(cellRange);

            return result;
        }

        /// <inheritdoc />
        public IList<IDictionary<string, string>> GetRangeValuesMap(ExcelCellLocation cell, uint columnSpan = 0, uint sheetNumber = 1)
        {
            var result = new List<IDictionary<string, string>>();

            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var range = sheet.UsedRange;
            var rowCount = range.Rows.Count;
            var dataRowCount = rowCount - (cell.Row - 1);

            var lastColumn = new ExcelCellLocation(column: "A", row: 1).IncrementColumn(range.Columns.Count).IncrementRow(range.Rows.Count);
            object to = columnSpan == 0 ? lastColumn.ToString() : $"{cell.Column + columnSpan}{cell.Row + dataRowCount}";
            var cellRange = sheet.get_Range($"{cell}", to);

            foreach (var o in cellRange.Rows)
            {
                var count = 0;
                var innerList = new Dictionary<string, string>();
                foreach (var col in o.Columns)
                {
                    innerList.Add(cell.IncrementColumn(count).Column, col.Text as string ?? col.Text?.ToString());
                    count++;
                }

                result.Add(innerList);
            }

            _comObjects.Push(sheet);
            _comObjects.Push(range);
            _comObjects.Push(cellRange);

            return result;
        }

        public void CutRange(string from, string to, uint sheetNumber = 1)
        {
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var rangeFrom = sheet.get_Range(from);
            var rangeTo = sheet.get_Range(to);
            rangeFrom.Cut(rangeTo);

            _comObjects.Push(sheet);
            _comObjects.Push(rangeTo);
            _comObjects.Push(rangeFrom);
        }

        public void CopyRange(string from, string to, uint sheetNumber = 1)
        {
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var rangeFrom = sheet.get_Range(from);
            var rangeTo = sheet.get_Range(to);
            rangeFrom.Copy(rangeTo);

            _comObjects.Push(sheet);
            _comObjects.Push(rangeTo);
            _comObjects.Push(rangeFrom);
        }

        public void Save() => _workBook.Save();

        public void SetCell(string cell, string value, uint sheetNumber = 1)
        {
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var range = sheet.get_Range(cell);
            range.Value = value;

            _comObjects.Push(range);
            _comObjects.Push(sheet);
        }

        /// <inheritdoc />
        public void MergeCells(ExcelCellRange range, uint sheetNumber = 1)
        {
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var sheetRange = sheet.get_Range(range.FirstCell.ToString(), range.SecondCell.ToString());
            sheetRange.Merge();

            _comObjects.Push(sheet);
            _comObjects.Push(sheetRange);
        }

        /// <inheritdoc />
        public void CenterCells(ExcelCellRange range, uint sheetNumber = 1)
        {
            var sheet = _workBook.Sheets[sheetNumber] as Worksheet;
            var sheetRange = sheet.get_Range(range.FirstCell.ToString(), range.SecondCell.ToString());
            sheetRange.HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
            sheetRange.VerticalAlignment = XlVAlign.xlVAlignCenter;

            _comObjects.Push(sheet);
            _comObjects.Push(sheetRange);
        }

        void ReleaseUnmanagedResources()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            for (var i = 0; i < _comObjects.Count; i++)
            {
                var obj = _comObjects.Pop();
                Marshal.ReleaseComObject(obj);
            }

            _workBook.Close();
            Marshal.ReleaseComObject(_workBook);

            _app.Quit();
            Marshal.ReleaseComObject(_app);
        }
    }
}