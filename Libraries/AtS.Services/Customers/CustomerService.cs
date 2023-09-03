using System;
using System.Collections.Generic;
using System.Linq;
using TWYK.Core;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        #region Fields
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(
            IRepository<Customer> customerRepository
        ) {
            _customerRepository = customerRepository;
        }


        public bool UniqueUserName(string username) {
            if (username.IsNullOrEmpty())
                return true;

            var exists = !_customerRepository.Table.Any(c => c.UserName == username);
            return exists;
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