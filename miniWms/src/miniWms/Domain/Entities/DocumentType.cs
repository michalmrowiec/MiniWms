using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace miniWms.Domain.Entities
{
    public class DocumentType
    {
        [MinLength(3)]
        [MaxLength(3)]
        public string DocumentTypeId { get; set; }
        [MaxLength(100)]
        public string DocumentTypeName { get; set; }
        public ActionType ActionType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public Employee? CreatedByEmployee { get; set; }
        public Employee? ModifiedByEmployee { get; set; }
        public IList<Document>? Documents { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ActionType
    {
        InternalTransfer,
        ExternalIssue,
        ExternalReceipt,
        InternalIssue,
        InternalReceipt
    }
}
