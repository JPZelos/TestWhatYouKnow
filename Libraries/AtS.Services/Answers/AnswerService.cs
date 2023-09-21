using System;
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

        /// <summary>
        /// Insert a answer
        /// </summary>
        /// <param name="answer">Answer</param>
        void InsertAnswer(Answer answer);

        /// <summary>
        /// Updates the answer
        /// </summary>
        /// <param name="answer">Answer</param>
        void UpdateAnswer(Answer answer);

        /// <summary>
        /// Deletes a answer
        /// </summary>
        /// <param name="answer">Answer</param>
        void DeleteAnswer(Answer answer);

        List<Answer> GetByQuestion(int questionId);
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

        public List<Answer> GetByQuestion(int questionId) {
            if (questionId == 0)
            {
                return null;
            }

            return _answerRepository.Table.Where(a=>a.QuestionId==questionId).ToList();
        }

        #region GRUD

        /// <summary>
        /// Insert a answer
        /// </summary>
        /// <param name="answer">Answer</param>
        public virtual void InsertAnswer(Answer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }

            _answerRepository.Insert(answer);
        }

        /// <summary>
        /// Updates the answer
        /// </summary>
        /// <param name="answer">Answer</param>
        public virtual void UpdateAnswer(Answer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }

            _answerRepository.Update(answer);
        }

        /// <summary>
        /// Deletes a answer
        /// </summary>
        /// <param name="answer">Answer</param>
        public virtual void DeleteAnswer(Answer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }

            _answerRepository.Delete(answer);
        }

        #endregion
    }
}