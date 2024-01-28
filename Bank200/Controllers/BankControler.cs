using Bank200.Exceptions;
using Bank200.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
    }

    public class IdAndAmount
    {
        [Required]
        public long Id { get; set; }
        public long Deposit { get; set; }
    }
}
