using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EFTestExample.DAL;
using EFTestExample.Domain;
using System.Linq;
using EFTestExample.Commands;

namespace UnitTests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void EmailTests_HappyPath()
        {
            var repoMoq = new Mock<ISomeBoundedRepository>();
            var customers = new[] 
            {
                new Customer() { Id = 1, Email = "azaz@yandex.ru" },
                new Customer() { Id = 2, Email = "lalala@yandex.ru" },
            };

            repoMoq.Setup(e => e.Entity<Customer>()).Returns(customers.AsQueryable());


            var command = new ChangeEmailCommand(repoMoq.Object);

            const string newEmail = "NEW!!!!!!!!!@yandex.ru";
            command.Change(1, newEmail);

            Assert.AreEqual(newEmail, customers.Where(e => e.Id == 1).Single().Email);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmailTests_DuplicateEmail()
        {
            var repoMoq = new Mock<ISomeBoundedRepository>();
            var customers = new[]
            {
                new Customer() { Id = 1, Email = "azaz@yandex.ru" },
                new Customer() { Id = 2, Email = "lalala@yandex.ru" },
            };

            repoMoq.Setup(e => e.Entity<Customer>()).Returns(customers.AsQueryable());


            var command = new ChangeEmailCommand(repoMoq.Object);

            const string newEmail = "lalala@yandex.ru";
            command.Change(1, newEmail);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmailTests_NoCustomer()
        {
            var repoMoq = new Mock<ISomeBoundedRepository>();
            var customers = new[]
            {
                new Customer() { Id = 1, Email = "azaz@yandex.ru" },
                new Customer() { Id = 2, Email = "lalala@yandex.ru" },
            };

            repoMoq.Setup(e => e.Entity<Customer>()).Returns(customers.AsQueryable());


            var command = new ChangeEmailCommand(repoMoq.Object);

            const string newEmail = "asdasd2222";
            command.Change(67756756, newEmail);
        }
    }
}
