using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using TWYK.Core;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        #region Fields
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<Chapter> _chapteRepository;
        private readonly IRepository<Topic> _topicRepository;

        public CustomerService(IRepository<Customer> customerRepository, IRepository<Quiz> quizRepository, IRepository<Chapter> chapteRepository, IRepository<Topic> topicRepository) {
            _customerRepository = customerRepository;
            _quizRepository = quizRepository;
            _chapteRepository = chapteRepository;
            _topicRepository = topicRepository; 
        }

        public bool UniqueUserName(string username) {
            if (username.IsNullOrEmpty())
                return true;

            var exists = !_customerRepository.Table.Any(c => c.UserName == username);
            return exists;
        }

        public Customer GetCustomerById(int id) {
            if(id == 0)
                return null;
            return _customerRepository.GetById(id);
        }

        /// <summary>
        /// Gets a customer by GUID
        /// </summary>
        /// <param name="customerGuid">Customer GUID</param>
        /// <returns>A customer</returns>
        public Customer GetCustomerByGuid(Guid customerGuid) {
            if (customerGuid == Guid.Empty) {
                return null;
            }

            var query = from c in _customerRepository.Table
                where c.CustomerGuid == customerGuid
                orderby c.Id
                select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        public virtual Customer GetCustomerByUsername(string username) {
            if (string.IsNullOrWhiteSpace(username)) {
                return null;
            }

            var query = from c in _customerRepository.Table
                orderby c.Id
                where c.UserName == username
                select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        public virtual IList<Customer> GetAll() {
            var query = _customerRepository.Table;
            return query.ToList();
        }

        public virtual IList<Customer> GetAllByTeacher(int teacherId)
        {
            /*
            select distinct c.* from Customer c
            inner join Quiz qz On qz.CustomerId = c.id
            inner join Chapter cp On cp.Id = qz.ChapterId
            inner join Topic tp On cp.TopicId = tp.Id
            where tp.CustomerId = 2

            var query = from package in context.Packages
            join container in context.Containers on package.ContainerID equals container.ID
            join userHasPackage in context.UserHasPackages on package.ID equals userHasPackage.PackageID
            where userHasPackage.UserID == "SomeUser"
            select new
            {
                package.ID,
                container.Name,
                package.Code,
                package.Code2
            };
             */
            var query = from cust in _customerRepository.Table
                join qz in _quizRepository.Table on cust.Id equals qz.CustomerId
                join cp in _chapteRepository.Table on qz.ChapterId equals cp.Id
                join tp in _topicRepository.Table on cp.TopicId equals tp.Id
                        where tp.CustomerId == teacherId
                        select cust;

            var teacherUsers = query.Distinct().ToList();

            return teacherUsers;
        }

        /// <summary>
        /// Insert a guest customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual Customer InsertGuestCustomer() {
            var customer = new Customer {
                CustomerGuid = Guid.NewGuid(),
            };

            //add to 'Guests' role
            customer.RoleNames = SystemCustomerRoleNames.Guests;

            _customerRepository.Insert(customer);

            return customer;
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        public string GetCustomerRoleBySystemName(string systemName) {
            if (string.IsNullOrWhiteSpace(systemName)) {
                return null;
            }

  
            var query = from c in _customerRepository.Table
                orderby c.Id
                where c.RoleNames.Contains(systemName)
                select c;
            var customer = query.FirstOrDefault();

            if (customer != null)
                return systemName;

            return null;
        }

        /// <summary>
        /// Insert a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void InsertCustomer(Customer customer) {
            if (customer == null) {
                throw new ArgumentNullException("customer");
            }

            _customerRepository.Insert(customer);
        }

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void UpdateCustomer(Customer customer) {
            if (customer == null) {
                throw new ArgumentNullException("customer");
            }

            _customerRepository.Update(customer);
        }

        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public CustomerLoginResults ValidateCustomer(string usernameOrEmail, string password) {
            var query = _customerRepository.Table;
            query = query.Where(c => c.UserName == usernameOrEmail);

            var customer = query.FirstOrDefault();

            if (customer == null) {
                return CustomerLoginResults.CustomerNotExist;
            }

            //only registered can login
            if (!customer.IsRegistered()) {
                return CustomerLoginResults.NotRegistered;
            }

            if (customer.Password != password) {
                return CustomerLoginResults.WrongPassword;
            }

            //update login details
            customer.LastLoginDateUtc = DateTime.UtcNow;
            _customerRepository.Update(customer);

            return CustomerLoginResults.Successful;
        }
        
        #endregion
    }
}