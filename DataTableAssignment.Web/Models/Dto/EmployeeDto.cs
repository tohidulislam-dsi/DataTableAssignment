﻿namespace DataTableAssignment.Web.Models.Dto
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public EmployeeDetailsDto EmployeeDetailsDto { get; set; }

    }
}
