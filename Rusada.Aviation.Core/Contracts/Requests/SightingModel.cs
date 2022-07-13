using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Rusada.Aviation.Core.Contracts.Requests
{
    public class SightingModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Make { get; set; }
        [Required]
        [MaxLength(128)]
        public string Model { get; set; }
        [Required]
        [MaxLength(8)]
        [RegularExpression(@"([a-zA-Z0-9]{1,2}-[a-zA-Z0-9]{1,5})")]
        public string Registration { get; set; }
        [Required]
        [MaxLength(255)]
        public string Location { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string FileData { get; set; }
    }
}
