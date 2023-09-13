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
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Topic> _topicRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<TestResult> _testResultRepository;

        public InstallationService(
            IRepository<Customer> customerRepository,
            IRepository<Topic> topicRepository,
            IRepository<Chapter> chapterRepository,
            IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository,
            IRepository<TestResult> testResultRepository
        ) {
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

        }

        public bool CanConnectToDb() {
            return _customerRepository.CanConnectToDb(new Customer());
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
                    Password = "123",
                    FirstName = "Teacher",
                    LastName = "B",
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
                    PasScore = 50,
                    SuccessMsg = "Συγχαρητήρια για την επιδοσή σας !!!",
                    PassMsg = "Περάσατε, αλλά θα μπορούσατε να πάτε καλύτερα με λίγη προσπάθεια ακόμα.",
                    FaultMsg = "Δυστυχώς πρέπει να μελετήσετε περισσότερο το υλικό. Προσπαθήστε ξανά!!",
                },
                new Chapter {
                    TopicId = 1,
                    Name = "Λίπανση",
                    Description = "Περιγραφή για την λίπανση των φυτών",
                    PasScore = 50,
                    SuccessMsg = "Συγχαρητήρια για την επιδοσή σας !!!",
                    PassMsg = "Περάσατε, αλλά θα μπορούσατε να πάτε καλύτερα με λίγη προσπάθεια ακόμα.",
                    FaultMsg = "Δυστυχώς πρέπει να μελετήσετε περισσότερο το υλικό. Προσπαθήστε ξανά!!",
                },
                new Chapter {
                    TopicId = 1,
                    Name = "Κλάδεμα",
                    Description = "Περιγραφή για το κλάδεμα των φυτών",
                    PasScore = 50,
                    SuccessMsg = "Συγχαρητήρια για την επιδοσή σας !!!",
                    PassMsg = "Περάσατε, αλλά θα μπορούσατε να πάτε καλύτερα με λίγη προσπάθεια ακόμα.",
                    FaultMsg = "Δυστυχώς πρέπει να μελετήσετε περισσότερο το υλικό. Προσπαθήστε ξανά!!",
                },

                new Chapter {
                    TopicId = 2,
                    Name = "Ωκεανογραφία",
                    Description = "Περιγραφή για την Ωκεανογραφία",
                    PasScore = 50,
                    SuccessMsg = "Συγχαρητήρια για την επιδοσή σας !!!",
                    PassMsg = "Περάσατε, αλλά θα μπορούσατε να πάτε καλύτερα με λίγη προσπάθεια ακόμα.",
                    FaultMsg = "Δυστυχώς πρέπει να μελετήσετε περισσότερο το υλικό. Προσπαθήστε ξανά!!",
                },
                new Chapter {
                    TopicId = 2,
                    Name = "Αλιεία",
                    Description = "Περιγραφή για την Αλιεία των ψαριών",
                    PasScore = 50,
                    SuccessMsg = "Συγχαρητήρια για την επιδοσή σας !!!",
                    PassMsg = "Περάσατε, αλλά θα μπορούσατε να πάτε καλύτερα με λίγη προσπάθεια ακόμα.",
                    FaultMsg = "Δυστυχώς πρέπει να μελετήσετε περισσότερο το υλικό. Προσπαθήστε ξανά!!",
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
                   
                    SuccessValue = 3
                },
                new Question {
                    ChapterId = 1,
                    Description = "Πόση ποσότητα νερού απαιτείται κατά το πότισμα των φυτών",
                    Score = 40,
                    SuccessValue = 2
                },

                new Question {
                    ChapterId = 2,
                    Description = "Τι λίπασμα χρησιμοποιούμε",
                    Score = 50,
                    SuccessValue = 1
                },

                //4
                new Question {
                    ChapterId = 2,
                    Description = "Κάθε πότε λιπαίνουμε",
                    Score = 50,
                    SuccessValue = 4
                },

                //5
                new Question {
                    ChapterId = 3,
                    Description = "Πότε κλαδεύουμε",
                    Score = 70,
                    SuccessValue = 2
                },

                //6
                new Question {
                    ChapterId = 3,
                    Description = "Είναι το ψαροκόκαλο το καλύτερο σχήμα κλάδευσης",
                    Score = 30,
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