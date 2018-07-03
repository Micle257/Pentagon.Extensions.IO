namespace Pentagon.Extensions.IO.Excel.Tests {
    using System;
    using Xunit;

    public class ExcelCellLocationTest
    {
        [Fact]
        public void Constructor_ColumnParameterIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new ExcelCellLocation(null, 5));
        }

        [Theory]
        [InlineData("15")]
        [InlineData("A1B5")]
        [InlineData("1B 5")]
        [InlineData("--f")]
        public void Constructor_ColumnParameterNotContainsOnlyLetters_Throws(string column)
        {
            Assert.Throws<ArgumentException>(() => new ExcelCellLocation(column, 5));
        }

        [Fact]
        public void Constructor_RowParameterIsEqualToZero_Throws()
        {
            Assert.Throws<ArgumentException>(() => new ExcelCellLocation("ABE", 0));
        }

        [Fact]
        public void ToString_ReturnsCorrectString()
        {
            var cell = new ExcelCellLocation("AB", 15);

            var text = cell.ToString();

            Assert.Equal("AB15", text);
        }

        [Fact]
        public void IncrementRow_RowParameterIsPositive_ReturnsCellWithCorrectRowNumber()
        {
            var cell = new ExcelCellLocation("AB", 15);

            var incCell = cell.IncrementRow(5);

            Assert.Equal((uint)20, incCell.Row);
        }

        [Fact]
        public void IncrementRow_RowParameterWouldMakeRowNegativeOrZero_Throws()
        {
            var cell = new ExcelCellLocation("AB", 15);

            Assert.Throws<ArgumentException>(() => cell.IncrementRow(-15));
            Assert.Throws<ArgumentException>(() => cell.IncrementRow(-30));
        }

        [Theory]
        [InlineData("A", 5, "F")]
        [InlineData("Z", 2, "AB")]
        [InlineData("F", -1, "E")]
        [InlineData("AC", -4, "Y")]
        [InlineData("A", 26, "AA")]
        [InlineData("BAA", 2, "BAC")]
        [InlineData("ERROR", -578, "ERQSL")]
        public void IncrementColumn_ReturnCellWithCorrectColumn(string oldColumn, int increment, string newColumn)
        {
            var cell = new ExcelCellLocation(oldColumn, 5);

            var newCell = cell.IncrementColumn(increment);

            Assert.Equal(newColumn, newCell.Column);
        }

        [Fact]
        public void TryParse_ValueParameterIsCorrect_ReturnsTrue()
        {
            var value = "AB2";

            var parseResult = ExcelCellLocation.TryParse(value, out var cell);

            Assert.True(parseResult);
        }

        [Fact]
        public void TryParse_ValueParameterIsCorrect_OutParameterIsCorrect()
        {
            var value = "AB2";

            ExcelCellLocation.TryParse(value, out var cell);

            Assert.Equal(value, cell.ToString());
        }

        [Fact]
        public void TryParse_ValueParameterIsMalformed_ReturnsFalse()
        {
            var value = "A-9";

            var parseResult = ExcelCellLocation.TryParse(value, out var cell);

            Assert.False(parseResult);
        }

        [Fact]
        public void TryParse_ValueParameterIsMalformed_OutParameterIsDefault()
        {
            var value = "  ";

            ExcelCellLocation.TryParse(value, out var cell);

            Assert.Equal(default(ExcelCellLocation), cell);
        }
    }
}