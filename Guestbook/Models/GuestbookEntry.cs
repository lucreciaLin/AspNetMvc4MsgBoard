using System;
using System.ComponentModel.DataAnnotations;
public class GuestbookEntry
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Message { get; set; }
    public DateTime DateAdded { get; set; }
}