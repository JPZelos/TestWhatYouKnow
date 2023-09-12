using System;
using System.Collections.Generic;

namespace TWYK.Core.Domain
{
    public class Customer : BaseEntity
    {
        private ICollection<Topic> _topics;
        private ICollection<Quiz> _quizzes;
        /// <summary>
        /// Ctor
        /// </summary>
        public Customer()
        {
            this.CustomerGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }

        public string RoleNames { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        public virtual ICollection<Quiz> Quizzes {
            get => _quizzes ?? (_quizzes = new List<Quiz>());
             set => _quizzes = value;
        }

        public virtual ICollection<Topic> Topics {
            get => _topics ?? (_topics = new List<Topic>());
            set => _topics = value;
        }
    }
}