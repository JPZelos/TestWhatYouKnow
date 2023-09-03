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
    }

    public class TopicService : ITopicService
    {
        private readonly IRepository<Topic> _topicRepository;

        public TopicService(IRepository<Topic> topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public IList<Topic> GetAllTopics()
        {
            return _topicRepository.Table.ToList();
        }

        public Topic GetTopicById(int topicId)
        {
            if (topicId == 0)
            {
                return null;
            }

            return _topicRepository.GetById(topicId);
        }
    }
}