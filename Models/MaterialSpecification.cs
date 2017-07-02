
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class MaterialSpecification
    {
        [Key]
        public int MaterialSpecificationId { get; set; }

        // FK = Material
        public int MaterialId { get; set; }

        public string Specification { get; set; }

        public string Value { get; set; }
    }
}