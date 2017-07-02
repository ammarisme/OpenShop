using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WholesaleTradingPortal.Models
{
    [Table("Settings")]
    public class Setting
    {
    [Key]
    public int Id { get; set; }

    public string SystemIdNumber { get; set; }

    public string Name { get; set; }

    public string ReportHead { get; set; }

    public string Logo { get; set; }

   }
}

