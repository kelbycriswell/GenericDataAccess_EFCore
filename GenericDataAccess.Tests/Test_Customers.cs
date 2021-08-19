using GenericDataAccess.Context;
using GenericDataAccess.Context.Base;
using GenericDataAccess.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericDataAccess.Tests
{
    [TestClass]
    public class Test_Customers
    {
        
        [TestMethod]
        public void a_Get()
        {
            CustomerRepo repo = new CustomerRepo(false);
            Customer cust = repo.Find(1);
            Assert.IsNotNull(cust);
        }

        [TestMethod]
        public void b_GetWithOrder()
        {
            CustomerRepo repo = new CustomerRepo(false, false, "Orders.LineItems.Item");
            Customer cust = repo.FindWhere(f => f.ID == 1);
            Assert.IsNotNull(cust.Orders);
        }

        [TestMethod]
        public void c_GetAll()
        {
            CustomerRepo repo = new CustomerRepo(false, false, "Orders.LineItems.Item");
            List<Customer> custs = repo.GetAll();
            Assert.IsTrue(custs.Any());
        }

        [TestMethod]
        public void d_GetAllWithOrders()
        {
            CustomerRepo repo = new CustomerRepo(false, false, "Orders.LineItems.Item");
            List<Customer> custs = repo.GetWhere(g => g.Orders.Any());
            Assert.IsTrue(custs.All(a => a.Orders.Any()));
        }

        [TestMethod]
        public void e_Add()
        {
            Customer cust2Add = new Customer
            {
                Deleted = false,
                FName = "Test",
                MI = "A",
                LName = "Person Jr",
                DateOfBirth = new DateTimeOffset(new DateTime(2018, 05, 30)),
                Gender = Gender.Male,
                StreetAddress = "437 North Wasatch Drive",
                Unit = null,
                City = "Layton",
                StateAbbr = "UT",
                ZipCode = "84041",
                PrimaryPhone = "801-336-3839",
                SecondaryPhone = null,
                Email = "123@test.com"
            };
            CustomerRepo repo = new CustomerRepo();
            repo.AddOrUpdate(ref cust2Add);
            Assert.AreEqual(1, repo.Save());
        }

        [TestMethod]
        public void f_UpdateOneSameRepo()
        {
            string testLName = "Person Sr.";
            CustomerRepo repo = new CustomerRepo();
            Customer cust = repo.Find(1);
            cust.LName = testLName;
            repo.AddOrUpdate(ref cust);
            repo.Save();
            Customer custTest = repo.Find(1);
            Assert.AreEqual(testLName , custTest.LName);
        }

        [TestMethod]
        public void UpdateOneSeparateRepos()
        {            
           Customer testUpdateCustomer = UpdateOneSeparateRepos_Get();
            UpdateOneSeparateRepos_Set(ref testUpdateCustomer);
            CustomerRepo repo3 = new CustomerRepo();
            repo3.AddOrUpdate(ref testUpdateCustomer);
            repo3.Save();
            Customer custTest = repo3.Find(1);
            Assert.AreEqual("Person III", custTest.LName);
        }

        Customer UpdateOneSeparateRepos_Get()
        {
            CustomerRepo repo1 = new CustomerRepo();
            var cust = repo1.Find(1);
            repo1.Dispose();
            return cust;
        }

        void UpdateOneSeparateRepos_Set(ref Customer cust)
        {
            string testLName = "Person III";
            cust.LName = testLName;
            CustomerRepo repo2 = new CustomerRepo();
            repo2.AddOrUpdate(ref cust);
            repo2.Save();
            Customer custTest = repo2.Find(1);
            Assert.AreEqual(testLName, custTest.LName);
        }


        [TestMethod]
        public void h_UpdateAll()
        {
            string testAddress = "123 Fake Street";
            CustomerRepo repo1 = new CustomerRepo();
            List<Customer> custs1 = repo1.GetAll();
            custs1.ForEach(fe => fe.StreetAddress = testAddress);
            repo1.Dispose(false);
            CustomerRepo repo2 = new CustomerRepo();
            repo2.AddOrUpdate(ref custs1);
            repo2.Save();
            List<Customer> custs2 = repo2.GetAll();
            Assert.IsTrue(custs2.All(a => a.StreetAddress == testAddress));
        }

        [TestMethod]
        public void i_Delete()
        {
            int startCount, testCount;
            CustomerRepo repo1 = new CustomerRepo(false);
            List<Customer> custs1 = repo1.GetWhere(w => !w.Deleted);
            startCount = custs1.Count();
            testCount = startCount - 1;
            Customer cust2Delete = custs1.Last();
            cust2Delete.Deleted = true;
            repo1.AddOrUpdate(ref cust2Delete);
            repo1.Save();
            repo1.Dispose();
            CustomerRepo repo2 = new CustomerRepo(false);            
            List<Customer> custs2 = repo2.GetWhere(w => !w.Deleted);
            Assert.AreEqual(testCount, custs2.Count());
        }
    }
}
