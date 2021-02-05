using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingTransfer.Application.Service;
using BankingTransfer.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankingTransfer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _service;

        public TransferController(ITransferService service)
        {
            _service = service;
        }


        // GET api/transfer
        [HttpGet]
        public ActionResult<IEnumerable<TransferLog>> Get()
        {
            return Ok(_service.GetTansferLogs());
        }

    }
}
