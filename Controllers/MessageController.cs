using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VersionedApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VersionedApi.Controllers
{
    [Route("api/{version}/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            if (messageService is null)
            {
                throw new ArgumentNullException(nameof(messageService));
            }

            this.messageService = messageService;
        }

        // GET: api/<MessageController>
        [HttpGet]
        public async Task<string> Get()
        {
            return await this.messageService.GetMessage();
        }

    }
}
