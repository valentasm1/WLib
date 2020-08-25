using System;

namespace Wlib.Core.Admin.Data.Domain.Entities
{
    public class EventLogEntity : ApplicationEntityBase, IAuditEntity
    {
        public string SessionID { get; set; }

        public string ObjectId { get; set; }

        public string ObjectName { get; set; }

        public string Event { get; set; }
        public string User { get; set; }

        public string Message { get; set; }
        public string MessageAdditional { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
