using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.TestResults
{
    public interface ITestResultService
    {
        IList<TestResult> GetAllTestResults();
        TestResult GetTestResultById(int testResultId);
    }

    public class TestResultService : ITestResultService
    {
        private readonly IRepository<TestResult> _testResultRepository;

        public TestResultService(IRepository<TestResult> testResultRepository)
        {
            _testResultRepository = testResultRepository;
        }

        public IList<TestResult> GetAllTestResults()
        {
            return _testResultRepository.Table.ToList();
        }

        public TestResult GetTestResultById(int testResultId)
        {
            if (testResultId == 0)
            {
                return null;
            }

            return _testResultRepository.GetById(testResultId);
        }
    }
}