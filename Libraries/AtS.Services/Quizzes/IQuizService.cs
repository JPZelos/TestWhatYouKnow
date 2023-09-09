using System;
using System.Linq;
using System.Runtime.CompilerServices;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Quizzes
{
    public interface IQuizService
    {
        void DeleteQuiz(Quiz quiz);
        Quiz GetQuizById(int quizId);
        void InsertQuiz(Quiz quiz);
        void UpdateQuiz(Quiz quiz);
        int GetMaxQuizTries(int userId, int chapterId);
    }

    public class QuizService : IQuizService
    {
        private readonly IRepository<Quiz> _quizRepository;

        public QuizService(IRepository<Quiz> quizRepository) {
            _quizRepository = quizRepository;
        }

        public Quiz GetQuizById(int quizId) {
            if (quizId == 0) {
                return null;
            }

            return _quizRepository.GetById(quizId);
        }

        public int GetMaxQuizTries(int userId, int chapterId) {
            if (userId == 0 || chapterId == 0) {
                return 0;
            }
            var userChapterQuizzes = _quizRepository.Table
                .Where(q => q.CustomerId == userId && q.ChapterId == chapterId).ToList();

            return userChapterQuizzes.Any() ? userChapterQuizzes.Max(q => q.Tries) + 1 : 1;
        }

        /// <summary>
        /// Insert a quiz
        /// </summary>
        /// <param name="quiz">Quiz</param>
        public virtual void InsertQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException(nameof(quiz));
            }

            _quizRepository.Insert(quiz);
        }

        /// <summary>
        /// Updates the quiz
        /// </summary>
        /// <param name="quiz">Quiz</param>
        public virtual void UpdateQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException(nameof(quiz));
            }

            _quizRepository.Update(quiz);
        }

        /// <summary>
        /// Deletes a quiz
        /// </summary>
        /// <param name="quiz">Quiz</param>
        public virtual void DeleteQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException(nameof(quiz));
            }

            _quizRepository.Delete(quiz);
        }
    }
}