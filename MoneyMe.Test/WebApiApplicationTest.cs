using Autofac.Extras.Moq;
using AutoFixture;
using Microsoft.EntityFrameworkCore.Storage;
using MoneyMe.Application.Command.Implementation;
using MoneyMe.Application.Command.Interface;
using MoneyMe.Application.Dto;
using MoneyMe.Domain.Entities;
using MoneyMe.Infrastructure.Data.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MoneyMe.Test
{
    public class WebApiApplicationTest : BaseTest
    {
        [Fact]
        public async Task AddCustomerCommand_Should_ThrowArgumentException()
        {
            var addCustomerCommand = Mock.Create<AddCustomerCommand>();
            await Assert.ThrowsAsync<Exception>(async () => await addCustomerCommand.AddCustomer(null, CancellationToken.None));
        }

        [Fact]
        public async Task AddCustomerCommand_Should_Success_CustomerAlreadyExist()
        {
            var addCustomerCommand = Mock.Create<AddCustomerCommand>();
            var customerDto = Fixture.Build<CustomerDTO>()
                .With(x => x.FirstName, "test")
                .With(x => x.LastName, "text")
                .Create();

            Mock.Mock<IGenericRepository>()
                .Setup(o => o.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(value: null);

            Mock.Mock<ICustomerRepository>()
                 .Setup(o => o.GetCustomerByDetails(It.IsAny<CustomerEntity>(), CancellationToken.None))
                 .ReturnsAsync(new CustomerEntity());

            var result =  await addCustomerCommand.AddCustomer(customerDto, CancellationToken.None);
            Assert.Equal("Success", result.Status.ToString());
            Assert.Equal("Customer already register", result.Message);
        }

        [Fact]
        public async Task AddCustomerCommand_Should_Success()
        {
            var addCustomerCommand = Mock.Create<AddCustomerCommand>();
            var customerDto = Fixture.Build<CustomerDTO>()
                .With(x => x.Id, 1)
                .With(x => x.DateOfBirth, "2023-01-01")
                .Create();


            var mockTransaction = new Mock<IDbContextTransaction>();
            Mock.Mock<IGenericRepository>()
                .Setup(o => o.BeginTransactionAsync(CancellationToken.None))
                .ReturnsAsync(mockTransaction.Object);

            Mock.Mock<ICustomerRepository>()
                 .Setup(o => o.GetCustomerByDetails(It.IsAny<CustomerEntity>(), CancellationToken.None))
                 .ReturnsAsync(value: null);

            var result = await addCustomerCommand.AddCustomer(customerDto, CancellationToken.None);
            Assert.Equal("Success", result.Status.ToString());
            Assert.Equal("Customer successfully inserted", result.Message);
        }
    }
}
