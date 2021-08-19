using GenericDataAccess.Context;
using GenericDataAccess.Repositories.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericDataAccess.Repository.Repositories
{
    public class CustomerRepo : DataAccessRepo<Customer, TestDb>
    {
        public CustomerRepo(bool lazyLoad = false, bool readOnly = false, params string[] include) : base(lazyLoad, readOnly, include)
        {

        }
    }
}
