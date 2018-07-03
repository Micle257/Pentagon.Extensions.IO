// -----------------------------------------------------------------------
//  <copyright file="ExcelReaderProvider.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Excel
{
    public class ExcelReaderProvider : IExcelReaderProvider
    {
        public IExcelFileReader Create(string fileName) => new ExcelFileReader(fileName);
    }
}