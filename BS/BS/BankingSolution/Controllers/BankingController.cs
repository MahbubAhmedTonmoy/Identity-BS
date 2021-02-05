using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSolution.Application.Model;
using BankingSolution.Application.Service;
using BankingSolution.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankingSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankingController : ControllerBase
    {
       
        private readonly ILogger<BankingController> _logger;
        private readonly IAccountService service;

        public BankingController(ILogger<BankingController> logger, IAccountService service)
        {
            _logger = logger;
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            var result = service.GetAccounts();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Post(TransferAmmount transferAmmount)
        {
            service.Transfer(transferAmmount);
            return Ok();
        }
    }
}
