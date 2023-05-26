using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crd.DataAccess.Migrations.DbModels
{
    public class CallRecord
    {
        public long CallerId { get; set; }
        public long RecipientId { get; set; }
        public DateOnly CallDate { get; set; }
        public TimeOnly EndTime { get; set; }
        public uint DurationSeconds { get; set; }

        [Precision(11, 3)]
        public decimal CostPence { get; set; }

        [Key]
        [MaxLength(33)]
        public string Reference { get; set; }

        [MaxLength(3)]
        public string CurrencyIsoCode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
    }
}
