using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Answers
{
    public interface IAnswerService
    {
        IList<Answer> GetAllAnswers();
        Answer GetAnswerById(int answerId);
    }

    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> _answerRepository;

        public AnswerService(IRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public IList<Answer> GetAllAnswers()
        {
            return _answerRepository.Table.ToList();
        }

        public Answer GetAnswerById(int answerId)
        {
            if (answerId == 0)
            {
                return null;
            }

            return _answerRepository.GetById(answerId);
        }
    }
}