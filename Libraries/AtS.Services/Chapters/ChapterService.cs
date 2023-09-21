using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Chapters
{
    public interface IChapterService
    {
        void DeleteChapter(Chapter chapter);
        IList<Chapter> GetAllChapters();
        Chapter GetChapterById(int chapterId);

        List<Chapter> GetChaptersByTopic(int topicId);
        List<Chapter> GetChaptersByUserId(int topicId);
        void InsertChapter(Chapter chapter);
        void UpdateChapter(Chapter chapter);
    }

    public class ChapterService : IChapterService
    {
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<Topic> _topicRepository;

        public ChapterService(
            IRepository<Chapter> chapterRepository,
            IRepository<Topic> topicRepository
        ) {
            _chapterRepository = chapterRepository;
            _topicRepository = topicRepository;
        }

        public IList<Chapter> GetAllChapters() {
            return _chapterRepository.Table.ToList();
        }

        public Chapter GetChapterById(int chapterId) {
            if (chapterId == 0) {
                return null;
            }

            return _chapterRepository.GetById(chapterId);
        }

        public virtual List<Chapter> GetChaptersByTopic(int topicId) {
            if (topicId == 0) {
                return null;
            }

            return _chapterRepository.Table.Where(c=>c.TopicId == topicId).ToList();
        }

        public List<Chapter> GetChaptersByUserId(int topicId)
        {
            if (topicId == 0) {
                return null;
            }

            var query = from cp in _chapterRepository.Table
                join tp in _topicRepository.Table on cp.TopicId equals tp.Id
                where tp.CustomerId == topicId
                orderby cp.TopicId
                select cp;

            var chapters = query.ToList();
            return chapters;
        }

        #region GRUD

        /// <summary>
        /// Insert a chapter
        /// </summary>
        /// <param name="chapter">Chapter</param>
        public virtual void InsertChapter(Chapter chapter)
        {
            if (chapter == null)
            {
                throw new ArgumentNullException(nameof(chapter));
            }

            _chapterRepository.Insert(chapter);
        }

        /// <summary>
        /// Updates the chapter
        /// </summary>
        /// <param name="chapter">Chapter</param>
        public virtual void UpdateChapter(Chapter chapter)
        {
            if (chapter == null)
            {
                throw new ArgumentNullException(nameof(chapter));
            }

            _chapterRepository.Update(chapter);
        }

        /// <summary>
        /// Deletes a chapter
        /// </summary>
        /// <param name="chapter">Chapter</param>
        public virtual void DeleteChapter(Chapter chapter)
        {
            if (chapter == null)
            {
                throw new ArgumentNullException(nameof(chapter));
            }

            _chapterRepository.Delete(chapter);
        }

        #endregion
    }
}