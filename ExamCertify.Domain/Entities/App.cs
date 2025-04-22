using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExamCertify.Domain.Entities;

[Table("App")]
public partial class App
{
    [Key]
    public int AppId { get; set; }

    [StringLength(50)]
    public string AppName { get; set; } = null!;

    [InverseProperty("App")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
