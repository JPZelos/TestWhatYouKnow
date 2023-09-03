using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Questions
{
    public interface IQuestionService
    {
        IList<Question> GetAllQuestions();
        Question GetQuestionById(int questionId);
    }

    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public IList<Question> GetAllQuestions()
        {
            return _questionRepository.Table.ToList();
        }

        public Question GetQuestionById(int questionId)
        {
            if (questionId == 0)
            {
                return null;
            }

            return _questionRepository.GetById(questionId);
        }
    }
}