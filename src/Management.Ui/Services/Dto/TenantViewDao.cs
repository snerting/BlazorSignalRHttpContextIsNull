using Management.Ui.Services.Dto;
using System;
using System.Collections.Generic;

namespace Management.Ui.Services.Dto
{
    public class TenantViewDao
    {
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public DateTimeOffset? Deleted { get; set; }
    }
}