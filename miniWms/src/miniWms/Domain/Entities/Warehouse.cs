﻿using System.ComponentModel.DataAnnotations;

namespace miniWms.Domain.Entities
{
    public class Warehouse
    {
        public Guid WarehouseId { get; set; }
        [MaxLength(250)]
        public string WarehouseName { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string Region { get; set; }
        [MaxLength(20)]
        public string PostalCode { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public IList<WarehouseEntry> WarehouseEntries { get; set; } = new List<WarehouseEntry>();
        public IList<Document> DocumentsAsMainWarehouse { get; set; } = new List<Document>();
        public IList<Document> DocumentsAsTargetWarehouse { get; set; } = new List<Document>();
    }
}
