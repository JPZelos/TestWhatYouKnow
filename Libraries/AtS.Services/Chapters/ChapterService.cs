using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Chapters
{
    public interface IChapterService
    {
        IList<Chapter> GetAllChapters();
        Chapter GetChapterById(int chapterId);
    }

    public class ChapterService : IChapterService
    {
        private readonly IRepository<Chapter> _chapterRepository;

        public ChapterService(IRepository<Chapter> chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        public IList<Chapter> GetAllChapters()
        {
            return _chapterRepository.Table.ToList();
        }

        public Chapter GetChapterById(int chapterId)
        {
            if (chapterId == 0)
            {
                return null;
            }

            return _chapterRepository.GetById(chapterId);
        }
    }
}