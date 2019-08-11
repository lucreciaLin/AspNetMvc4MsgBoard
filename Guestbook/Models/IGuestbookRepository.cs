using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guestbook.Models
{
    public interface IGuestbookRepository
    {
        IList<GuestbookEntry> GetMostRecentEntries();
        GuestbookEntry FindById(int id);
        IList<CommentSummary> GetCommentSummary();
        void AddEntry(GuestbookEntry entry);
    }
}
