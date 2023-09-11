using System;
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

        /// <summary>
        /// Insert a question
        /// </summary>
        /// <param name="question">Question</param>
        void InsertQuestion(Question question);

        /// <summary>
        /// Updates the question
        /// </summary>
        /// <param name="question">Question</param>
        void UpdateQuestion(Question question);

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="question">Question</param>
        void DeleteQuestion(Question question);

        List<Question> GetQuestionsByChapterId(int chapterId);
    }

    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(IRepository<Question> questionRepository) {
            _questionRepository = questionRepository;
        }

        public IList<Question> GetAllQuestions() {
            return _questionRepository.Table.ToList();
        }

        public Question GetQuestionById(int questionId) {
            if (questionId == 0) {
                return null;
            }

            return _questionRepository.GetById(questionId);
        }

        public List<Question> GetQuestionsByChapterId(int chapterId) {
            if (chapterId == 0)
            {
                return null;
            }

            var questions = _questionRepository.Table.Where(q=>q.ChapterId==chapterId).ToList();

            return questions;
        }

        #region GRUD

        /// <summary>
        /// Insert a question
        /// </summary>
        /// <param name="question">Question</param>
        public virtual void InsertQuestion(Question question) {
            if (question == null) {
                throw new ArgumentNullException(nameof(question));
            }

            _questionRepository.Insert(question);
        }

        /// <summary>
        /// Updates the question
        /// </summary>
        /// <param name="question">Question</param>
        public virtual void UpdateQuestion(Question question) {
            if (question == null) {
                throw new ArgumentNullException(nameof(question));
            }

            _questionRepository.Update(question);
        }

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="question">Question</param>
        public virtual void DeleteQuestion(Question question) {
            if (question == null) {
                throw new ArgumentNullException(nameof(question));
            }

            _questionRepository.Delete(question);
        }

        #endregion
    }
}