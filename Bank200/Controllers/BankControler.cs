using Bank200.Exceptions;
using Bank200.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
        public HttpResponseMessage PostSavingsAccount([FromBody] IdAndAmount idAndDeposit)
        {
            try
            {
                _accountService.openSavingsAccount(idAndDeposit.Id, idAndDeposit.Deposit);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (MinimumDepositException ex)
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };

            }

        }
        [HttpPost("CurrentAccount")]
        public void PostCurrentAccount([FromBody] PostCurrentAccount postCurrent)
        {
            _accountService.openCurrentAccount(postCurrent.Id);
        }

        [HttpPost("Deposit")]
        public HttpResponseMessage PostDeposit([FromBody] IdAndAmount idAndDeposit)
        {
            try
            {
                _accountService.deposit(idAndDeposit.Id, idAndDeposit.Deposit);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (AccountNotFoundException ex)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound) {Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };

            }
        }

        // POST api/<BankControler>
        [HttpPost("SavingsAccountWithdraw")]
        public HttpResponseMessage PostSavingsAccountWithdraw([FromBody] IdAndAmount idAndDeposit)
        {
            try
            {
                _accountService.withdraw(idAndDeposit.Id, idAndDeposit.Deposit);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (MinimumDepositException ex)
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };

            }

        }

        // POST api/<BankControler>
        [HttpPost("SavingsAccountWithdraw")]
        public HttpResponseMessage PostCurrentAccountWithdraw([FromBody] IdAndAmount idAndDeposit)
        {
            try
            {
                _accountService.withdraw(idAndDeposit.Id, idAndDeposit.Deposit);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (MinimumDepositException ex)
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable) { Content = new StringContent(ex.Message) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };

            }

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
   
    public class PostCurrentAccount 
    {
        [Required]
        public long Id { get; set; }
    }

    public class IdAndAmount
    {
        [Required]
        public long Id { get; set; }
        public long Deposit { get; set; }
    }
}
