using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignIn.Models;

public partial class EmployeeTable
{
    public int EmpId { get; set; }

    public int CompId { get; set; }

    public string Username { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
