using System;
using System.Collections.Generic;

namespace Moduit.Interview.Dto
{
    public class OutputBase
    {
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
    }

    public class Output1Dto : OutputBase
    {
        public int id { get; set; }
        public int category { get; set; }
        public List<string> tags { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class Output2Dto : Output1Dto
    {
        public List<OutputBase> items { get; set; }
    }
}
