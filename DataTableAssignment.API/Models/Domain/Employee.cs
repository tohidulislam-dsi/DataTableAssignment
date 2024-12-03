﻿using System.Numerics;

namespace DataTableAssignment.API.Models.Domain
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
    }
}
