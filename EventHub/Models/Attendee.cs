using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models;

public class Attendee
{
    public int AttendeeId { get; set; }

    [Required]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Street { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Column("ZipCode")]
    public string PostalCode { get; set; } = string.Empty;

    // At most one badge can belong to an attendee.
    public Badge? Badge { get; set; }

    // A calculated property, not a database column.
    [NotMapped]
    public string FullAddress => $"{Street}, {City}, {Country}, {PostalCode}";
}
