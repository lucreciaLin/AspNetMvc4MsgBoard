using System.Data.Entity;
namespace Guestbook.Models
{
    public class GuestbookContext : DbContext
    { 
        public GuestbookContext() : base("Guestbook")
        {
        }
        public DbSet<GuestbookEntry> Entries { get; set; }
    }
}