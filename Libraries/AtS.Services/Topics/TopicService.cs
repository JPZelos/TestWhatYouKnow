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
    }
}