using System;
using System.Collections.Generic;
using TWYK.Core;
using TWYK.Core.Domain;

namespace TWYK.Web.Models
{
    public class CustomerModel : BaseEntity
    {
        private ICollection<TopicModel> _topics;
        private ICollection<Quiz> _quizzes;
        /// <summary>
        /// Ctor
        /// </summary>
        public CustomerModel() {
            this.CustomerGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }

        public string SystemName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        public virtual ICollection<Quiz> Quizzes {
            get => _quizzes ??= new List<Quiz>();
            set => _quizzes = value;
        }

        public virtual ICollection<TopicModel> Topics {
            get => _topics ??= new List<TopicModel>();
            set => _topics = value;
        }
    }
}