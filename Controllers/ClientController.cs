
using PayCorona.Interface;
using PayCorona.Models;
using Microsoft.AspNetCore.Mvc;
using PayCorona.Dto;
using AutoMapper;

namespace PayCorona.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IClientService clientService, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet("getClients")]
        public IActionResult GetClients()
        {
            // dto - data transfer object
            var clients = _mapper.Map<List<ClientDto>>(_clientRepository.GetClients());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(clients);
        }

        [HttpPost("authorization")]
        public ActionResult<TokenDto> Authorization([FromBody] ClientLoginDto client)
        {
            var session =  _clientService.ClientAuth(client.Login, client.Password);
            if (session == null)
            {
                return BadRequest();
            }

            return Ok(new TokenDto {AuthToken = session.Token.ToString()});
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateClientRequest client) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var state = _clientService.ClientRegister(client.Login, client.Password, client.Name);
            if (state == false) 
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
