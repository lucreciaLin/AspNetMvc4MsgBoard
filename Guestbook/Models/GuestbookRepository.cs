using System;
using System.Collections.Generic;
using System.Linq;
using Guestbook.Models;

public class GuestbookRepository : IGuestbookRepository
{
    private GuestbookContext _db = new GuestbookContext();
    public IList<GuestbookEntry> GetMostRecentEntries()
    {
        return (from entry in _db.Entries
                orderby entry.DateAdded descending
                select entry).Take(20).ToList();
    }
    public void AddEntry(GuestbookEntry entry)
    {
        entry.DateAdded = DateTime.Now;
        _db.Entries.Add(entry);
        _db.SaveChanges();
    }
    public GuestbookEntry FindById(int id)
    {
        var entry = _db.Entries.Find(id);
        return entry;
    }
    public IList<CommentSummary> GetCommentSummary()
    { 
        var entries = from entry in _db.Entries
            group entry by entry.Name into groupByName
            orderby groupByName.Count() descending
            select new CommentSummary
            {
                NumberOfComments = groupByName.Count(),
                UserName = groupByName.Key
            };
        return entries.ToList();
    }
}