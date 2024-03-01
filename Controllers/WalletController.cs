using AutoMapper;
using PayCorona.Dto;
using PayCorona.Interface;
using PayCorona.Middleware;
using PayCorona.Models;
using Microsoft.AspNetCore.Mvc;

namespace PayCorona.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : Controller
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletService _walletService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public WalletController(IWalletRepository walletRepository, IWalletService walletService, IMapper mapper, ITransactionService transactionService, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _walletService = walletService;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        [HttpGet("balance")]
        public IActionResult GetBalance([FromQuery] int clientID)
        {
            var balance = _walletService.GetBalance(clientID);
            return Ok(balance);
        }

        [HttpGet("history")]
        public IActionResult GetHistory([FromQuery] int clientID)
        {
            var transactions = _transactionService.GetTransactions(clientID);
            return Ok(transactions);
        }

        [HttpGet("walet")]
        [OurAuth]
        public IActionResult GetWallet(int clientID)
        {
            var walet = _mapper.Map<WalletDto>(_walletRepository.GetWalletByClientID(clientID));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(walet);
        }

        [HttpPut("deposit")]
        [OurAuth]
        public IActionResult AddDeposit([FromBody] decimal sum, int walletID)
        {
            _walletService.Deposit(sum, walletID);
            return Ok();
        }

        [HttpPut("send")]
        public IActionResult Send([FromBody] decimal sum, int reseverID, int senderID)
        {
            _walletService.Send(sum, reseverID, senderID);
            return Ok();
        }

    }
}
