using System;
using _10.Generic_Types_Collections.Exceptions;
using _10.Generic_Types_Collections.Models;

namespace _10.Generic_Types_Collections.Services
{
    public class BookManager
    {
        public List<Book> Books { get; set; }
        public Dictionary<string, List<Book>> BooksByAuthor { get; set; }
        public Queue<string> WaitingQueue { get; set; }
        public Stack<Book> RecentlyReturned { get; set; }

        public BookManager()
        {
            Books = new List<Book>();
            BooksByAuthor = new Dictionary<string, List<Book>>();
            WaitingQueue = new Queue<string>();
            RecentlyReturned = new Stack<Book>();
        }

        public void AddBook(Book book)
        {
            foreach (var item in Books)
            {
                if (item.Id == book.Id)
                {
                    throw new DuplicateBookException($"ID-si {book.Id} olan kitab artıq mövcuddur.");
                }
            }

            Books.Add(book);

            if (!BooksByAuthor.ContainsKey(book.Author))
            {
                BooksByAuthor[book.Author] = new List<Book>();
            }

            BooksByAuthor[book.Author].Add(book);
        }

        public Book SearchByTitle(string title)
        {
            foreach (var book in Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }

            return null;
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            if (BooksByAuthor.ContainsKey(author))
            {
                return BooksByAuthor[author];
            }

            return new List<Book>();
        }

        public void AddToWaitingQueue(string memberName)
        {
            WaitingQueue.Enqueue(memberName);
            Console.WriteLine($"{memberName} növbəyə əlavə olundu");
        }

        public string ServeNextInQueue()
        {
            if (WaitingQueue.Count == 0)
            {
                throw new EmptyQueueException();
            }

            return WaitingQueue.Dequeue();
        }

        public void ReturnBook(Book book)
        {
            RecentlyReturned.Push(book);
            Console.WriteLine($"Kitab qəbul edildi: {book.Title}");
        }

        public Book GetLastReturnedBook()
        {
            if (RecentlyReturned.Count == 0)
            {
                throw new EmptyStackException();
            }

            return RecentlyReturned.Peek();
        }

        public Book PopLastReturnedBook()
        {
            if (RecentlyReturned.Count == 0)
            {
                throw new EmptyStackException();
            }

            return RecentlyReturned.Pop();
        }
    }

}

