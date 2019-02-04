// -----------------------------------------------------------------------
//  <copyright file="IExcelFileReader.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Excel
{
    using System.Collections.Generic;

    public interface IExcelFileReader
    {
        void AdjustColumnsWidth(uint sheetNumber = 1);

        void SetColumnWidth(string column, double width, uint sheetNumber = 1);

        IList<string> GetColumnData(uint rowNumber, string column, uint sheetNumber = 1);

        IList<IList<string>> GetRangeValues(ExcelCellLocation cell, uint columnSpan, uint sheetNumber = 1);

        IList<IDictionary<string, string>> GetRangeValuesMap(ExcelCellLocation cell, uint columnSpan = 0, uint sheetNumber = 1);

        void CutRange(string from, string to, uint sheetNumber = 1);
        void CopyRange(string from, string to, uint sheetNumber = 1);
        void Save();
        void SetCell(string cell, string value, uint sheetNumber = 1);

        void MergeCells(ExcelCellRange range, uint sheetNumber = 1);

        void CenterCells(ExcelCellRange range, uint sheetNumber = 1);
    }
}