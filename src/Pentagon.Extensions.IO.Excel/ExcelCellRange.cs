// -----------------------------------------------------------------------
//  <copyright file="ExcelCellRange.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Excel
{
    using System;

    public struct ExcelCellRange
    {
        public ExcelCellRange(ExcelCellLocation firstCell, ExcelCellLocation secondCell)
        {
            FirstCell = firstCell;
            SecondCell = secondCell;
        }

        public ExcelCellLocation FirstCell { get; }

        public ExcelCellLocation SecondCell { get; }

        public static bool TryParse(string value, out ExcelCellRange cellRange)
        {
            cellRange = default(ExcelCellRange);

            var tokens = value.Split(':');

            if (tokens.Length != 2)
                return false;

            if (!ExcelCellLocation.TryParse(tokens[0], out var firstCell) || !ExcelCellLocation.TryParse(tokens[1], out var secondCell))
                return false;

            cellRange = new ExcelCellRange(firstCell, secondCell);

            return true;
        }

        public static ExcelCellRange Parse(string value)
        {
            if (!TryParse(value, out var cellRange))
                throw new FormatException(message: "Rozsah excel buněk není ve správném formátu.");
            return cellRange;
        }

        /// <inheritdoc />
        public override string ToString() => $"{FirstCell}:{SecondCell}";
    }
}