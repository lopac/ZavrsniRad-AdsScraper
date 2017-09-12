#region usings

using System.Collections.Generic;

#endregion

namespace AdsScraper.Web.ViewModels
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[][] Values { get; set; }
    }

    public class Value
    {
        public List<string> ColumnNames { get; set; }
        public List<string> ColumnTypes { get; set; }
        public List<List<string>> Values { get; set; }
    }

    public class Car
    {
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class Results
    {
        public Car car { get; set; }
    }

    public class ApiResponse
    {
        public Results Results { get; set; }
    }
}