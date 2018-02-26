using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChatAPI.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatAPI.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // GET api/chat
        [HttpGet]
        public string Get()
        {
            _hubContext.Clients.All.InvokeAsync("updateStuff", "some random text");
            return "I have been called!!";
        }


    }
    
}
