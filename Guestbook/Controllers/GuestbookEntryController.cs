using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Guestbook.Models;

namespace Guestbook.Controllers
{
    public class GuestbookEntryController : ApiController
    {
        private IGuestbookRepository _repository;
        public GuestbookEntryController()
        {
            _repository = new GuestbookRepository();
        }
        public GuestbookEntryController(
            IGuestbookRepository repository)
        {
            _repository = repository;
        }
        // GET api/guestbookentry
        public IEnumerable<GuestbookEntry> Get()
        {
            var mostRecentEntries =
                _repository.GetMostRecentEntries();
            return mostRecentEntries;
        }
        // GET api/guestbookentry/5
        public GuestbookEntry Get(int id)
        {
            var entry = _repository.FindById(id);
            if (entry == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return entry;
        }
        // POST api/guestbookentry
        public HttpResponseMessage Post(GuestbookEntry value)
        {
            if (!ModelState.IsValid)
            {
                var errors =
                    (from state in ModelState
                     where state.Value.Errors.Any()
                     select new
                     {
                         state.Key,
                         Errors = state.Value.Errors.Select(
                             error => error.ErrorMessage)
                     })
                    .ToDictionary(error => error.Key, error => error.Errors);
                return Request.CreateResponse(
                    HttpStatusCode.BadRequest, errors);
            }
            _repository.AddEntry(value);
            var response = Request.CreateResponse(
                HttpStatusCode.Created,
                value, Configuration);
            response.Headers.Location = new Uri(Request.RequestUri,
                "/api/guestbookentry/"
                + value.Id);
            return response;
        }
    }
}