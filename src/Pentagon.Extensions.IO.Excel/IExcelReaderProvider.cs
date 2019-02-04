// -----------------------------------------------------------------------
//  <copyright file="IExcelReaderProvider.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Excel
{
    public interface IExcelReaderProvider
    {
        IExcelFileReader Create(string fileName);
    }
}