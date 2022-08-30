using System.ComponentModel.DataAnnotations;

namespace POC.DataAccess.Models
{
    public class ExampleTable
    {
        [Key]
        public Guid Id { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string BusinessId { get; set; }
        [Required]
        public decimal Amount1 { get; set; }
        [Required]
        public decimal Amount2 { get; set; }

        [Required]
        public int Amount3 { get; set; }

        [Required]
        public bool Processed { get; set; } = false;

        [Required]
        public bool HasError { get; set; } = false;

    }
}
