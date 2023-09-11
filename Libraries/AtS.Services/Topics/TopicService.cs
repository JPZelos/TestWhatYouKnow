using System;
using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Topics
{
    public interface ITopicService
    {
        IList<Topic> GetAllTopics();
        Topic GetTopicById(int topicId);
        List<Topic> GetTopicByUserId(int userId);

        /// <summary>
        /// Insert a topic
        /// </summary>
        /// <param name="topic">Topic</param>
        void InsertTopic(Topic topic);

        /// <summary>
        /// Updates the topic
        /// </summary>
        /// <param name="topic">Topic</param>
        void UpdateTopic(Topic topic);

        /// <summary>
        /// Deletes a topic
        /// </summary>
        /// <param name="topic">Topic</param>
        void DeleteTopic(Topic topic);
    }

    public class TopicService : ITopicService
    {
        private readonly IRepository<Topic> _topicRepository;

        public TopicService(IRepository<Topic> topicRepository) {
            _topicRepository = topicRepository;
        }

        public IList<Topic> GetAllTopics() {
            return _topicRepository.Table.ToList();
        }

        public Topic GetTopicById(int topicId) {
            if (topicId == 0) {
                return null;
            }

            return _topicRepository.GetById(topicId);
        }

        public List<Topic> GetTopicByUserId(int userId) {
            if (userId == 0) {
                return null;
            }

            var topics = _topicRepository.Table.Where(tp => tp.CustomerId == userId).ToList();
            return topics;
        }

        /// <summary>
        /// Insert a topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public virtual void InsertTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            _topicRepository.Insert(topic);
        }

        /// <summary>
        /// Updates the topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public virtual void UpdateTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            _topicRepository.Update(topic);
        }

        /// <summary>
        /// Deletes a topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public virtual void DeleteTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            _topicRepository.Delete(topic);
        }
    }
}