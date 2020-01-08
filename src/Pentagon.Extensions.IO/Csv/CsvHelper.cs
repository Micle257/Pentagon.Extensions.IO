// -----------------------------------------------------------------------
//  <copyright file="JsonHelpers.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Csv
{
    using System.Collections.Generic;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary> Provides helper methods for CSV data manipulation. </summary>
    public static class CsvHelper
    {
        [NotNull]
        public static string FormatCsv([NotNull] params string[] values)
        {
            var sb = new StringBuilder();

            var first = false;

            foreach (var value in values)
            {
                if (!first)
                {
                    sb.Append("\"" + value.Replace("\"", "\"\"") + "\"");
                    first = true;
                    continue;
                }

                sb.Append(",\"" + value.Replace("\"", "\"\"") + "\"");
            }

            return sb.ToString();
        }

        [NotNull]
        [ItemNotNull]
        public static IEnumerable<string> ReadCsvLine([NotNull] string line)
        {
            var inValue = false;

            var currentValue = new StringBuilder();

            for (var index = 0; index < line.Length; index++)
            {
                var c = line[index];

                if (c == ',')
                {
                    if (inValue)
                    {
                        currentValue.Append(c);
                    }
                    else
                    {
                        yield return currentValue.ToString();

                        currentValue.Clear();
                    }
                }
                else if (inValue && c == '"' && index < line.Length - 1 && line[index + 1] == '"')
                {
                    currentValue.Append(c);

                    index++;
                }
                else if (c == '"')
                {
                    inValue = !inValue;
                }
                else
                {
                    currentValue.Append(c);
                }
            }

            yield return currentValue.ToString();
        }
    }
}