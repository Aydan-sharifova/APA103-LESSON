using System;
using System.Collections.Generic;
using _10.Generic_Types_Collections.Exceptions;


namespace _10.Generic_Types_Collections.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Book> BorrowedBooks { get; set; }

        public Member(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            BorrowedBooks = new List<Book>();
        }

        public void BorrowBook(Book book)
        {
            if (BorrowedBooks.Count >= 3)
            {
                throw new MaxBorrowLimitException($"{Name} artıq maksimum sayda kitab götürüb.");
            }

            BorrowedBooks.Add(book);
            Console.WriteLine($"Kitab götürüldü: {book.Title}");
        }

        public void ReturnBook(int bookId)
        {
            Book foundBook = null;

            foreach (var book in BorrowedBooks)
            {
                if (book.Id == bookId)
                {
                    foundBook = book;
                    break;
                }
            }

            if (foundBook == null)
            {
                throw new ItemNotFoundException($"ID-si {bookId} olan kitab {Name} adlı üzvdə tapılmadı.");
            }

            BorrowedBooks.Remove(foundBook);
            Console.WriteLine($"Kitab qaytarıldı: {foundBook.Title}");
        }

        public void DisplayBorrowedBooks()
        {
            if (BorrowedBooks.Count == 0)
            {
                Console.WriteLine("Borc kitab yoxdur");
                return;
            }

            Console.WriteLine($"{Name} tərəfindən götürülən kitablar:");
            foreach (var book in BorrowedBooks)
            {
                book.DisplayInfo();
            }
        }
    }
}

