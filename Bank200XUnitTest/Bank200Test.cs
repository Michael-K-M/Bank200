using Xunit;
using Bank200;
using System;
using Bank200.Database;
using Bank200.Service;
using Moq;
using Bank200.Account;
using Bank200.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bank200XUnitTest
{
    public class Bank200Test
    {

        private readonly Mock<ISystemDB> _mockDB;
        private readonly AccountService _service;
        public Bank200Test()
        {
            _mockDB = new Mock<ISystemDB>();
            _service = new AccountService(_mockDB.Object);
        }

        [Fact]
        public void Post_Deposit_Success()
        {
            //Arrange
            var controller = new BankControler(_service);
            var startingAmount = 4000;
            var IdAndDeposit = new IdAndAmount();
            var account = new SavingsAccount(1, startingAmount, "TestCustomer");
            IdAndDeposit.Id = account.Id;
            IdAndDeposit.Deposit = 2000;

            _mockDB
                .Setup(mock => mock.GetAccount(It.IsAny<long>())).Returns(account);

            //Act
            var result = controller.PostDeposit(IdAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
            _mockDB.Verify(mock => mock.GetAccount(It.IsAny<long>()), Times.Once);
            Assert.Equal(startingAmount + IdAndDeposit.Deposit, account.Balance);
            _mockDB.Verify(mock => mock.UpdateAccount(It.Is<IAccount>(x => x.Balance == startingAmount + IdAndDeposit.Deposit)), Times.Once);  
        }

        [Fact]
        public void Post_Deposit_NotFound()
        {
            //Arrange
            var controller = new BankControler(_service);
            var idAndDeposit = new IdAndAmount();
            idAndDeposit.Id = 6;
            idAndDeposit.Deposit = 2000;

            _mockDB
                .Setup(mock => mock.GetAccount(It.IsAny<long>())).Returns((IAccount)null);

            //Act
            var result = controller.PostDeposit(idAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            _mockDB.Verify(mock => mock.GetAccount(It.IsAny<long>()), Times.Once);            
            _mockDB.Verify(mock => mock.UpdateAccount(It.IsAny<IAccount>()), Times.Never);

        }

        [Fact]
        public void Post_openSavingsAccount_Success()
        {
            //Arrange
            var controller = new BankControler(_service);
            var idAndDeposit = new IdAndAmount();
            idAndDeposit.Id = 1;
            idAndDeposit.Deposit = 2000;

            _mockDB
                .Setup(mock => mock.openSavingsAccount(It.IsAny<long>(), It.IsAny<long>()));

            //Act
            var result = controller.PostSavingsAccount(idAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
            _mockDB.Verify(mock => mock.openSavingsAccount(It.IsAny<long>(),It.IsAny<long>()), Times.Once);
           
        }
        [Fact]
        public void Post_openSavingsAccount_InsufficentAmount()
        {

            //Arrange
            var controller = new BankControler(_service);
            var idAndDeposit = new IdAndAmount();
            idAndDeposit.Id = 1;
            idAndDeposit.Deposit = 500;

            //Act
            var result = controller.PostSavingsAccount(idAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.NotAcceptable, result.StatusCode);
            _mockDB.Verify(mock => mock.openSavingsAccount(It.IsAny<long>(), It.IsAny<long>()), Times.Never);

        }
        [Fact]
        public void Post_withdraw_SavingsSuccess()
        {
            //Arrange
            var controller = new BankControler(_service);
            var idAndDeposit = new IdAndAmount();
            var startingAmount = 4000;
            var account = new SavingsAccount(1, startingAmount, "TestCustomer");
            idAndDeposit.Id = 1;
            idAndDeposit.Deposit = 2000;

            _mockDB
                .Setup(mock => mock.GetAccount(It.IsAny<long>())).Returns(account);

            //Act
            var result = controller.PostSavingsAccountWithdraw(idAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
            _mockDB.Verify(mock => mock.GetAccount(It.IsAny<long>()), Times.Once);
            Assert.Equal(startingAmount - idAndDeposit.Deposit, account.Balance);
            _mockDB.Verify(mock => mock.UpdateAccount(It.Is<IAccount>(x => x.Balance == startingAmount - idAndDeposit.Deposit)), Times.Once);

        }
        [Fact]
        public void Post_withdraw_SavingsUnsuccessful()
        {
            //Arrange
            var controller = new BankControler(_service);
            var idAndDeposit = new IdAndAmount();
            var startingAmount = 1000;
            var account = new SavingsAccount(1, startingAmount, "TestCustomer");
            idAndDeposit.Id = 1;
            idAndDeposit.Deposit = 500;

            _mockDB
                .Setup(mock => mock.GetAccount(It.IsAny<long>())).Returns((IAccount)null);

            //Act
            var result = controller.PostSavingsAccountWithdraw(idAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            _mockDB.Verify(mock => mock.GetAccount(It.IsAny<long>()), Times.Once);
            
        }
        [Fact]
        public void Post_withdraw_CurrentSuccess()
        {
            //Arrange
            var controller = new BankControler(_service);
            var idAndDeposit = new IdAndAmount();
            var startingAmount = 4000;
            var Overdraft = 4000;
            var account = new CurrentAccount(3, "TestCustomer", startingAmount);
            idAndDeposit.Id = account.Id;
            idAndDeposit.Deposit = 2000;

            _mockDB
                .Setup(mock => mock.GetAccount(It.IsAny<long>())).Returns(account);

            //Act
            var result = controller.PostCurrentAccountWithdraw(idAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);
            _mockDB.Verify(mock => mock.GetAccount(It.IsAny<long>()), Times.Once);
            Assert.Equal(startingAmount - idAndDeposit.Deposit, account.Balance);
            _mockDB.Verify(mock => mock.UpdateAccount(It.Is<IAccount>(x => x.Balance == startingAmount - idAndDeposit.Deposit)), Times.Once);

        }
        [Fact]
        public void Post_withdraw_CurrentUnsuccessful()
        {
            //Arrange
            var controller = new BankControler(_service);
            var idAndDeposit = new IdAndAmount();
            idAndDeposit.Id = 1;
            idAndDeposit.Deposit = 500;

            _mockDB
                .Setup(mock => mock.GetAccount(It.IsAny<long>())).Returns((IAccount)null);

            //Act
            var result = controller.PostSavingsAccountWithdraw(idAndDeposit);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            _mockDB.Verify(mock => mock.GetAccount(It.IsAny<long>()), Times.Once);

        }
    }
}

