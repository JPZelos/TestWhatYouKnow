using System.Collections.Generic;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Services.Security
{
    public class PermissionService : IPermissionService
    {
        private readonly IWorkContext _workContext;

        private Dictionary<string, List<string>> PermitionRecords {
            get {
                var permitionRecords = new Dictionary<string, List<string>>();
                permitionRecords.Add(SystemCustomerRoleNames.Registered, new List<string> {
                    "Catalog.List"
                });

                permitionRecords.Add(SystemCustomerRoleNames.Guests, new List<string> {
                    "Catalog.List"
                });

                permitionRecords.Add(SystemCustomerRoleNames.Administrators, new List<string> {
                    "AdminPanel",
                    "Catalog.List",
                    "Catalog.AddProduct"
                });

                permitionRecords.Add(SystemCustomerRoleNames.Teachers, new List<string> {
                    "AdminPanel"
                });

                permitionRecords.Add(SystemCustomerRoleNames.Students, new List<string> {
                    "Catalog.List",
                    "Catalog.ChaptertDetails",
                    "Answer.DoTest",
                    "Answer.Results"
                });

                return permitionRecords;
            }
        } // = new Dictionary<string, List<string>>();


        public PermissionService(IWorkContext workContext) {
            _workContext = workContext;
 
        }


        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName) {
            return Authorize(permissionRecordSystemName, _workContext.CurrentCustomer);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, Customer customer) {
            if (string.IsNullOrEmpty(permissionRecordSystemName)) {
                return false;
            }
            

            foreach (var userRole in PermitionRecords.Keys) {
                if (customer.RoleNames.Contains(userRole)) {
                    var prs = PermitionRecords[userRole];
                    foreach (var pr in prs) {
                        if (permissionRecordSystemName == pr) {
                            return true;
                        }
                    }
                }
            }
            
            //no permission found
            return false;
        }

       

    }
}