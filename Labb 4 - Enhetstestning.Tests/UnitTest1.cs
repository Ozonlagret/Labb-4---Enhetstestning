using Labb_4___Enhetstestning;
using System.Security.Cryptography;
namespace Labb_4___Enhetstestning.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private LibrarySystem _library;

        [TestInitialize]
        public void Setup()
        {
            _library = new LibrarySystem();
        }

        [TestMethod]
        public void GetAllBooks_BooksValidIsbnNumbers_ReturnTrue()
        {
            List<Book> books = _library.GetAllBooks();

            for (int i = 0; i < books.Count; i++)
            {
                bool hasThirteenDigits = books[i].ISBN.All(char.IsDigit) && books[i].ISBN.Length == 13;

                Assert.IsTrue(hasThirteenDigits, $"Book '{books[i].Title}' has an invalid ISBN: {books[i].ISBN}");
            }

        }

        [TestMethod]
        public void AddBook_FaultyIsbnNumber_ReturnFalse()
        {
            var book = new Book("Title", "Author", "dsfvsd", 1995);

            bool result = _library.AddBook(book);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddBook_DuplicateISBN_ReturnFalse()
        {
            var bookA = new Book("Title A", "Author A", "1234567890123", 2025);
            _library.AddBook(bookA);

            var bookB = new Book("Title B", "Author B", "1234567890123", 2024);
            _library.AddBook(bookB);

            bool result = _library.AddBook(bookB);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveBook_CheckBookAfterRemoval_ReturnFalse()
        {
            var book = new Book("Test", "Test", "9780451524925", 1949);
            _library.AddBook(book);
            _library.BorrowBook(book.ISBN);

            bool result = _library.RemoveBook(book.ISBN);

            Assert.IsFalse(result);

        }

        [TestMethod]
        [DataRow("19")]
        [DataRow("1984")]
        [DataRow("To")]
        [DataRow("To Kill a Mockingbird")]
        [DataRow("to kill a mockingbird")]
        public void SearchByTitle_SearchWithIncompleteAndLowerCaseWords_CountOver0ReturnTrue(string title)
        {
            var result = _library.SearchByTitle(title);

            Assert.IsTrue(result.Count > 0);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [DataRow("harper lee")]
        [DataRow("Harper Lee")]
        [DataRow("ja")]
        [DataRow("Jane")]
        public void SearchByAuthor_SearchWithIncompleteAndLowerCaseWords_CountOver0ReturnTrue(string author)
        {
            var result = _library.SearchByAuthor(author);

            Assert.IsTrue(result.Count > 0);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [DataRow(true, false)]
        [DataRow(false, true)]
        public void BorrowBook_CheckIfBorrowed_FailsIfItIs(bool isBorrowed, bool expected)
        {
            var book = _library.SearchByISBN("9780451524935");
            book.IsBorrowed = isBorrowed;
            
            bool result = _library.BorrowBook(book.ISBN);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(false, false)]
        [DataRow(true, true)]
        public void ReturnBook_CheckIfBorrowed_SucceedsIfItIs(bool isBorrowed, bool expected)
        {
            var book = _library.SearchByISBN("9780451524935");
            book.IsBorrowed = isBorrowed;

            bool result = _library.ReturnBook(book.ISBN);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnBook_CheckIfDateIsNullified_ReturnTrue()
        {
            _library.BorrowBook("9780451524935");
            var book = _library.SearchByISBN("9780451524935");

            Assert.IsTrue(book.BorrowDate.HasValue);

            _library.ReturnBook("9780451524935");
            Assert.IsTrue(book.BorrowDate == null); 
        }

        [TestMethod]
        [DataRow(8)]
        [DataRow(6)]
        [DataRow(0)]
        public void IsBookOverdue_CheckIfBorrowTimeExceedsLoanPeriod_ReturnTrueIfExceeded(int borrowDays)
        {
            _library.BorrowBook("9780451524935");
            var book = _library.SearchByISBN("9780451524935");

            book.BorrowDate = DateTime.Now.AddDays(-borrowDays);

            var result = _library.IsBookOverdue("9780451524935", 7);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CalculateLateFee()
        {
            int daysLate = 5;
            decimal expected = daysLate * 0.5m;

            var result = _library.CalculateLateFee("9780451524935", 5);

            Assert.AreEqual(expected, result);
        }
    }
}