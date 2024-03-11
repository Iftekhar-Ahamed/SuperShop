using System.ComponentModel.DataAnnotations;

namespace SuperShop.Model
{
    public class LogModel
    {
        public int Id { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Please enter Table value bigger than {1}")]
        public int TableId { get; set; }
        public long TablePk { get; set; }
        public long ActionBy { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionChanges { get; set; }
        public string JsonPayload { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [StringLength(50)]
        public string ActionType { get; set; }
    }
}
