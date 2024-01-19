using Bank200.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank200.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BankControler : ControllerBase
    {

        private readonly IAccountService _accountService;
        public BankControler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: api/<BankControler>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BankControler>/5
        [HttpGet("{id}")]
        public string Get(long id)
        {
            return "value";
        }

        // POST api/<BankControler>
        [HttpPost("SavingsAccount")]
        public void PostSavingsAccount([FromBody] PostSavingsAccount postSavings)
        {
            Ok(postSavings);

            try
            {
                _accountService.openSavingsAccount(postSavings.Id, postSavings.Deposit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           // _accountService.openSavingsAccount(postSavings.Id,postSavings.Deposit);

        }
        [HttpPost("CurrentAccount")]
        public void PostCurrentAccount([FromBody] PostCurrentAccount postCurrent)
        {
            _accountService.openCurrentAccount(postCurrent.Id);
        }

        // PUT api/<BankControler>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BankControler>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public class PostSavingsAccount
    {
        [Required]
        public long Id { get; set; }
        [Required]
        
        public long Deposit { get; set; }
    }

    public class PostCurrentAccount 
    {
        [Required]
        public long Id { get; set; }
    }
}
