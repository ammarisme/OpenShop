using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETrading.Models
{
    public class JobStatus
    {
        public int JobStatusId { get; set; }

        public string JobStatusName { get; set; }
    }
    public class Job
    {
        [Key]
        public int JobId { get; set; } // the primary key
        
        public DateTime? IssuedDate { get; set; } // issue date of the job 

        public string Status { get; set; }  // current status of the job  {initiated , completed, in-progress}
        
        public DateTime? DueDate { get; set; } // the due date of the job 
        
        public DateTime? CompletedDate { get; set; } // completed date
        
        public int? CustomerOrderId { get; set; }
        
        public int? EmployeeId { get; set; }

        public string Remark { get; set; }
            public List<JobStatus> JobStatuses { get {
                List<JobStatus> statuses = new List<JobStatus> { };
                statuses.Add(new JobStatus { JobStatusId = 1, JobStatusName = "Created" });
                statuses.Add(new JobStatus { JobStatusId = 2, JobStatusName = "Modified" });
                statuses.Add(new JobStatus { JobStatusId = 3, JobStatusName = "Cancelled" });
                statuses.Add(new JobStatus { JobStatusId = 4, JobStatusName = "Completed" });

                return statuses;
            } }
        // dependent tables
        public ICollection<MaterialInJob> MaterialsInJob { get; set; }

        public ICollection<ProductInJob> ProductInJob { get; set; }

    }
    public class MaterialInJob
    {
        [Key]
        public int MaterialInJobId { get; set; }

        // foreign key to Materials
        public int MaterialId { get; set; }

        // foreign key to Job
        public int JobId { get; set; }

        public float Quantity { get; set; }

        public string Remark { get; set; }

        public bool? Damaged { get; set; }
    }

    public class ProductInJob
    {
        [Key]
        public int ProductInJobId { get; set; }

        // foreign at Product
        public int ProductId { get; set; }

        // foreign at job
        public int JobId { get; set; }

        public int Quantity { get; set; }

        public string Remark { get; set; }

    }
}