using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETrading.Models
{
    public class PaymentMode {
        public int PaymentModeId { get; set; }

        public string PaymentModeName { get; set; }
    }
    public class PaymentStatus
    {
        public int PaymentStatusId { get; set; }

        public string PaymentStatusName { get; set; }
    }
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public float Amount { get; set; }

        public DateTime? IssuedDate { get; set; }

        public DateTime? RealizationDate { get; set; }

        public string PaymentMode { get; set; }

        public int? PurchaseOrderId { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        public List<PaymentMode> PaymentModes { get {
            List<PaymentMode> modes = new List<PaymentMode> { };
            modes.Add(new PaymentMode { PaymentModeId = 1, PaymentModeName = "Cheque" });
            modes.Add(new PaymentMode { PaymentModeId = 2, PaymentModeName = "Cash" });
            modes.Add(new PaymentMode { PaymentModeId = 3, PaymentModeName = "Other" });

            return modes;
            }
        }

        public List<PaymentStatus> PaymentStatuses { get {
            List<PaymentStatus> statuses = new List<PaymentStatus> { };
            statuses.Add(new PaymentStatus { PaymentStatusId=1, PaymentStatusName="sent"});
            statuses.Add(new PaymentStatus { PaymentStatusId = 1, PaymentStatusName = "recieved" });
            statuses.Add(new PaymentStatus { PaymentStatusId = 1, PaymentStatusName = "realized" });

            return statuses;
        } }
    }
}