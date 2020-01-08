// -----------------------------------------------------------------------
//  <copyright file="CsvHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Tests
{
    using System.Linq;
    using Csv;
    using Xunit;

    public class CsvHelperTests
    {
        [Fact]
        public void ReadCsvLine_LineWithAllDifficulties_Converts()
        {
            // ARRANGE
            var line = @","""",""5,3"",345,""te""""st,"","""",,";

            // ACT
            var result = CsvHelper.ReadCsvLine(line).ToList();

            // ASSERT
            Assert.Equal(8, result.Count);
            Assert.Equal(new[] {"", "", "5,3", "345", "te\"st,", "", "", ""}, result);
        }

        [Fact]
        public void FormatCsv_WithAllDifficulties_Converts()
        {
            // ARRANGE
            var line = new[] {"", "", "5,3", "345", "te\"st,", "", "", ""};

            // ACT
            var result = CsvHelper.FormatCsv(line).ToArray();

            // ASSERT
            Assert.Equal(@""""","""",""5,3"",""345"",""te""""st,"","""","""",""""", result);
        }
    }
}