using System;
using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;
using TWYK.Services.Seo;

namespace TWYK.Services.Installation
{
    public interface IInstallationService
    {
        void InstallSampleData();
        bool CanConnectToDb();
    }

    public class InstallationService : IInstallationService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Topic> _topicRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<TestResult> _testResultRepository;

        public InstallationService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            IRepository<Customer> customerRepository,
            IRepository<Topic> topicRepository,
            IRepository<Chapter> chapterRepository,
            IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository,
            IRepository<TestResult> testResultRepository
        ) {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _customerRepository = customerRepository;
            _topicRepository = topicRepository;
            _chapterRepository = chapterRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _testResultRepository = testResultRepository;
        }

        public void InstallSampleData() {
            var customers = _customerRepository.Table.ToList();
            var topics = _topicRepository.Table.ToList();
            var chapters = _chapterRepository.Table.ToList();
            var questions = _questionRepository.Table.ToList();
            var answers = _answerRepository.Table.ToList();

            if (
                customers.Count == 0 &&
                topics.Count == 0 &&
                chapters.Count == 0 &&
                questions.Count == 0 &&
                answers.Count == 0
            ) {
                AddCustomers();
                AddTopics();
                AddChapter();
                AddQuestions();
                AddAnswers();
            }

            var categories = _categoryRepository.Table.ToList();
            var products = _productRepository.Table.ToList();

            // Init Categories
            if (categories.Count == 0) {
                categories = InitCategories();
            }

            if (products.Count == 0) {
                products = FakeProductFactory.GetSomeProducts(categories, 4, 8);

                _productRepository.Insert(products);

                foreach (var cat in categories) {
                    var catPrds = cat.Products.ToList();
                    for (var i = 0; i < catPrds.Count; i++) {
                        var prd = catPrds[i];
                        var pictureName = SeoExtensions.GetSeName($"{prd.Category.Name}-{i + 1}", true) + ".jpg";
                        prd.Picture = pictureName;
                    }

                    _productRepository.Update(catPrds);
                }
            }
        }

        public bool CanConnectToDb() {
            return _productRepository.CanConnectToDb(new Product());
        }

        private List<Category> InitCategories() {
            var categories = new List<Category> {
                new Category {
                    Name = "Soccer",
                    Description =
                        "Soccer  boots, called cleats or soccer shoes in North America,[1] are an item of footwear worn when playing association football. Those designed for grass pitches have studs on the outsole to aid grip."
                },
                new Category {
                    Name = "Running",
                    Description =
                        "Running shoes are footwear designed specifically to help you run in a way that will prevent injury and increase your athletic performance as a runner. They come in all sorts of styles and sizes—from minimalist shoes without many extra features to tricked-out types that help you as a runner."
                },
                new Category {
                    Name = "Tennis",
                    Description =
                        "Tennis shoes are athletic shoes made specifically for the sport of tennis. Tennis shoes provide stability for quick sprints and rapid turns. Tennis shoes are slightly heavy with thick soles that provide a quick bounce when needed. These shoes are also good for a number of other sports, which is one of the reasons many people refer to all generic athletic shoes as “tennis shoes.” It should be noted. However, that true tennis shoes are beneficial when playing a match."
                },
                new Category {
                    Name = "Trekking",
                    Description =
                        "Hiking (walking) boots are footwear specifically designed for protecting the feet and ankles during outdoor walking activities such as hiking. They are one of the most important items of hiking gear, since their quality and durability can determine a hiker's ability to walk long distances without injury. Hiking boots are constructed to provide comfort for walking considerable distance over rough terrain."
                },
                new Category {
                    Name = "Bowling ",
                    Description =
                        "Bowling shoes are specifically made for use at bowling alleys. They are usually made with a leather upper and a rubber sole that is very slick on the bottom. Casual bowlers rent shoes at a bowling alley while those bowlers who are more serious purchase their own. Those who buy their own bowling shoes will find an array of colors and styles. They will also find that their shoes have a rubber stopper for the non-sliding foot. Rental shoes are painted in garish colors to discourage theft and rarely offer a rubber stopper."
                },
            };
            _categoryRepository.Insert(categories);
            return categories;
        }

        public void AddCustomers() {
            var customers = new List<Customer> {
                new Customer {
                    FirstName = "Jason",
                    LastName = "Zelos Prapas",
                    UserName = "admin",
                    Email = "i.p.zelos@gmail.com",
                    Password = "123",
                    Address = "Agiou Spiridionos 45",
                    Address2 = null,
                    City = "Egaleo",
                    State = "Attiki",
                    Zip = "12243",
                    RoleNames = "Registered,Administrators",
                    IsAdmin = true,
                    HasShoppingCartItems = false,
                    LastLoginDateUtc = DateTime.UtcNow
                },
                new Customer {
                    Address = "Aigaleo",
                    Address2 = "UniWa",
                    City = "Aigaleo",
                    //CustomerGuid = new Guid(),
                    Email = "teacher_a@uniwa.gr",
                    UserName = "teacher_a",
                    Password = "123",
                    FirstName = "Teacher",
                    LastName = "A",
                    HasShoppingCartItems = false,
                    IsAdmin = false,
                    RoleNames = "Registered,Teachers"
                },
                new Customer {
                    Address = "Aigaleo",
                    Address2 = "UniWa",
                    City = "Aigaleo",
                    //CustomerGuid = new Guid(),
                    Email = "teacher_b@uniwa.gr",
                    UserName = "teacher_b",
                    FirstName = "Teacher",
                    LastName = "B",
                    HasShoppingCartItems = false,
                    IsAdmin = false,
                    RoleNames = "Registered,Teachers"
                },
                new Customer {
                    Address = "Aigaleo",
                    Address2 = "UniWa",
                    City = "Aigaleo",
                    //CustomerGuid = new Guid(),
                    Email = "std_a@uniwa.gr",
                    UserName = "std_a",
                    Password = "123",
                    FirstName = "Student",
                    LastName = "A",
                    HasShoppingCartItems = false,
                    IsAdmin = false,
                    RoleNames = "Registered,Students"
                },
                new Customer {
                    Address = "Aigaleo",
                    Address2 = "UniWa",
                    City = "Aigaleo",
                    //CustomerGuid = new Guid(),
                    Email = "std_b@uniwa.gr",
                    UserName = "std_b",
                    Password = "123",
                    FirstName = "Student",
                    LastName = "B",
                    HasShoppingCartItems = false,
                    IsAdmin = false,
                    RoleNames = "Registered,Students"
                }
            };
            _customerRepository.Insert(customers);
        }

        public void AddTopics() {
            var topics = new List<Topic> {
                new Topic {
                    CustomerId = 2,
                    Name = "Βοτανολογία",
                    Description = "Περιγραφή Βοτανολογίας"
                },
                new Topic {
                    CustomerId = 3,
                    Name = "Ιχθυολογία",
                    Description = "Περιγραφή Ιχθυολογίας"
                },
            };
            _topicRepository.Insert(topics);
        }

        public void AddChapter() {
            var chapters = new List<Chapter> {
                new Chapter {
                    TopicId = 1,
                    Name = "Πότισμα",
                    Description = "Περιγραφή για το πότισμα των φυτών",
                    PasScore = 50
                },
                new Chapter {
                    TopicId = 1,
                    Name = "Λίπανση",
                    Description = "Περιγραφή για την λίπανση των φυτών",
                    PasScore = 50
                },
                new Chapter {
                    TopicId = 1,
                    Name = "Κλάδεμα",
                    Description = "Περιγραφή για το κλάδεμα των φυτών",
                    PasScore = 50
                },

                new Chapter {
                    TopicId = 2,
                    Name = "Ωκεανογραφία",
                    Description = "Περιγραφή για την Ωκεανογραφία",
                    PasScore = 50
                },
                new Chapter {
                    TopicId = 2,
                    Name = "Αλιεία",
                    Description = "Περιγραφή για την Αλιεία των ψαριών",
                    PasScore = 50
                }
            };
            _chapterRepository.Insert(chapters);
        }

        public void AddQuestions() {
            var questions = new List<Question> {
                new Question {
                    ChapterId = 1,
                    Description = "Κάθε πότε ποτίζουμε τα φυτά",
                    Score = 60,
                    SuccessMsg = "Σωστό, κάθε φυτό έχει διαφορετική συχνότητα ποτίσματος",
                    FaultMsg = "Λάθος, κάθε φυτό έχει διαφορετική συχνότητα ποτίσματος",
                    SuccessValue = 3
                },
                new Question {
                    ChapterId = 1,
                    Description = "Πόση ποσότητα νερού απαιτείται κατά το πότισμα των φυτών",
                    Score = 40,
                    SuccessMsg = "Σωστό, κάθε φυτό έχει διαφορετικές απαιτήσεις σε νερό",
                    FaultMsg = "Λάθος, κάθε φυτό έχει διαφορετικές απαιτήσεις σε νερό",
                    SuccessValue = 2
                },

                new Question {
                    ChapterId = 2,
                    Description = "Τι λίπασμα χρησιμοποιούμε",
                    Score = 50,
                    SuccessMsg = "Σωστό, κάθε φυτό έχει άλλες ανάγκες λίπανσης",
                    FaultMsg = "Λάθος, κάθε φυτό έχει άλλες ανάγκες λίπανσης",
                    SuccessValue = 1
                },

                //4
                new Question {
                    ChapterId = 2,
                    Description = "Κάθε πότε λιπαίνουμε",
                    Score = 50,
                    SuccessMsg = "Σωστό, κάθε φυτό έχει διαφορετική συχνότητα λίπανσης",
                    FaultMsg = "Λάθος, κάθε φυτό έχει διαφορετική συχνότητα λίπανσης",
                    SuccessValue = 4
                },

                //5
                new Question {
                    ChapterId = 3,
                    Description = "Πότε κλαδεύουμε",
                    Score = 70,
                    SuccessMsg = "Σωστό, κάθε φυτό έχει άλλες ανάγκες κλαδέματος",
                    FaultMsg = "Λάθος, κάθε φυτό έχει άλλες ανάγκες κλαδέματος",
                    SuccessValue = 2
                },

                //6
                new Question {
                    ChapterId = 3,
                    Description = "Είναι το ψαροκόκαλο το καλύτερο σχήμα κλάδευσης",
                    Score = 30,
                    SuccessMsg = "Σωστό, κάθε φυτό επιδέχεται κάποια από τα σχήματα κλάδευσης",
                    FaultMsg = "Λάθος, κάθε φυτό επιδέχεται κάποια από τα σχήματα κλάδευσης",
                    SuccessValue = 3
                },
            };

            _questionRepository.Insert(questions);
        }

        public void AddAnswers() {
            var answers = new List<Answer> {
                new Answer {
                    QuestionId = 1,
                    Label = "Κάθε μέρα",
                    Value = 1
                },
                new Answer {
                    QuestionId = 1,
                    Label = "Κάθε εβδομάδα",
                    Value = 2
                },
                new Answer {
                    QuestionId = 1,
                    Label = "Κάθε φυτό έχει άλλη συχνόητα ποτίσματος",
                    Value = 3
                },

                new Answer {
                    QuestionId = 2,
                    Label = "Πέντε λίτρα",
                    Value = 1
                },
                new Answer {
                    QuestionId = 2,
                    Label = "Κάθε φυτό έχει άλλες ανάγκες ποτίσματος",
                    Value = 2
                },
                new Answer {
                    QuestionId = 2,
                    Label = "Δεκαπέντε λίτρα",
                    Value = 3
                },

                // id 3
                new Answer {
                    QuestionId = 3,
                    Label = "Κάθε φυτό χρειάζεται διαφορετικό λίπασμα",
                    Value = 1
                },
                new Answer {
                    QuestionId = 3,
                    Label = "Φώσφορο και Μαγνήσιο",
                    Value = 2
                },
                new Answer {
                    QuestionId = 3,
                    Label = "Φώσφορο, Μαγνήσιο και Κάλιο",
                    Value = 3
                },

                //4
                new Answer {
                    QuestionId = 4,
                    Label = "Κάθε δεκαπέντε μέρες",
                    Value = 1
                },
                new Answer {
                    QuestionId = 4,
                    Label = "Κάθε μήνα",
                    Value = 2
                },
                new Answer {
                    QuestionId = 4,
                    Label = "Μία φορά τον χρόνο",
                    Value = 3
                },
                new Answer {
                    QuestionId = 4,
                    Label = "Κάθε φυτό έχει διαφορετική συχνότητα λίπανσης",
                    Value = 4
                },

                //5
                new Answer {
                    QuestionId = 5,
                    Label = "Την Άνοιξη",
                    Value = 1
                },
                new Answer {
                    QuestionId = 5,
                    Label = "Ανάλογα με το φυτό",
                    Value = 2
                },
                new Answer {
                    QuestionId = 5,
                    Label = "Το Φθινόπωρο",
                    Value = 3
                },

                //6
                new Answer {
                    QuestionId = 6,
                    Label = "Το Ψαροκόκκαλο είναι το καταλληλότερο",
                    Value = 1
                },
                new Answer {
                    QuestionId = 6,
                    Label = "Το Κύπελο είναι το καταλληλότερο",
                    Value = 2
                },
                new Answer {
                    QuestionId = 6,
                    Label = "Κάθε φυτό επιδέχεται ένα ή περισσότερα σχήματα κλάδευσης",
                    Value = 3
                },
            };

            _answerRepository.Insert(answers);
        }
    }
}