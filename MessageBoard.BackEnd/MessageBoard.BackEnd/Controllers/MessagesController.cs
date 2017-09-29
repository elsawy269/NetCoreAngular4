using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MessageBoard.BackEnd.Models;

namespace MessageBoard.BackEnd.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private ApiContext _contex;

        public MessagesController(ApiContext contex)
        {
            this._contex = contex;
        }
        public IEnumerable<Message> Get()
        {
            return _contex.messages;
        }
        [Route("{owner}")]
        public IEnumerable<Message> Get(string owner)
        {
            return _contex.messages.Where(c=>c.Owner==owner);
        }
        [HttpPost]
        public Message Post([FromBody] Message message)
        {
           var dbMessage= _contex.messages.Add(message).Entity;
            _contex.SaveChanges();
            return dbMessage;
        }
    }
}