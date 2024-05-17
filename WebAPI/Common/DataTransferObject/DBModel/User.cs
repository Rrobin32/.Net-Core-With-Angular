using System;
using System.Collections.Generic;

namespace DataTransferObject.DBModel;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public long ModifiedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
