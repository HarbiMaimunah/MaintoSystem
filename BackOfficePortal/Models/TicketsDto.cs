using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackOfficePortal.Models
{
    public class TicketsDto
    {

        public int Id { get; set; }

        public int BeneficiaryID { get; set; }

        public int StatusID { get; set; }
        public string StatusTypeAr { get; set; }
        public string StatusTypeEn { get; set; }

        public DateTime Date { get; set; }
        public string Picture { get; set; }

        //Maintenance type section
        public int MaintenanceTypeID { get; set; }
        public string MaintenanceTypeNameAr { get; set; }
        public string MaintenanceTypeNameEn { get; set; }

        public string Description { get; set; }

        public string BuildingManagerComment { get; set; }

        public int? FloorId { get; set; }

        public bool IsCancelled { get; set; } = false;
        public int? CancellationReasonID { get; set; }
        public string ReasonTypeAr { get; set; }
        public string ReasonTypeEn { get; set; }

        public int? RejectedBy { get; set; }

        public string RejectionReason { get; set; }

        //audit trail
        public int CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
