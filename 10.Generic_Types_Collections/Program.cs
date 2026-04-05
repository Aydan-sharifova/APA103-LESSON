using System;
using System.Collections.Generic;
using _10.Generic_Types_Collections.Delegates;
using _10.Generic_Types_Collections.Exceptions;
using _10.Generic_Types_Collections.Generics;
using _10.Generic_Types_Collections.Helpers;
using _10.Generic_Types_Collections.Models;
using _10.Generic_Types_Collections.Services;

namespace _10.Generic_Types_Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageDelegate printMessage = message => Console.WriteLine(message);
            BookDelegate printBook = book => Console.WriteLine(book);
            Predicate<Book> isBigBook = book => book.PageCount >= 300;
            Func<List<Book>, int> totalPages = books =>
            {
                int sum = 0;
                foreach (var book in books)
                {
                    sum += book.PageCount;
                }
                return sum;
            };

            List<Book> books = new List<Book>
            {
                new Book(1, "Martin Eden", "Jack London", 1909, 400),
                new Book(2, "1984", "George Orwell", 1949, 328),
                new Book(3, "Animal Farm", "George Orwell", 1945, 112),
                new Book(4, "Ağ Gəmi", "Cingiz Aytmatov", 1970, 200),
                new Book(5, "Qırıq Budaq", "Elçin", 1998, 350),
                new Book(6, "The Great Gatsby", "F. Scott Fitzgerald", 1925, 180),
                new Book(7, "To Kill a Mockingbird", "Harper Lee", 1960, 281),
                new Book(8, "The Hobbit", "J.R.R. Tolkien", 1937, 310),
                new Book(9, "Crime and Punishment", "Fyodor Dostoyevski", 1866, 430),
                new Book(10, "The Little Prince", "Antoine de Saint-Exupery", 1943, 96),
                new Book(11, "The Alchemist", "Paulo Coelho", 1988, 208),
                new Book(12, "Sefiller", "Victor Hugo", 1862, 500),
                new Book(13, "Romeo and Juliet", "William Shakespeare", 1597, 220),
                new Book(14, "Dracula", "Bram Stoker", 1897, 418),
                new Book(15, "Inferno", "Dan Brown", 2013, 480)
            };

            List<Member> members = new List<Member>
            {
                new Member(1, "Ali Məmmədov", "ali@mail.com"),
                new Member(2, "Leyla Həsənova", "leyla@mail.com"),
                new Member(3, "Vüqar Əliyev", "vuqar@mail.com")
            };

            Library<Book> nationalLibrary = new Library<Book>("Milli Kitabxana");
            BookManager manager = new BookManager();

            foreach (var book in books)
            {
                nationalLibrary.Add(book);
                manager.AddBook(book);
            }

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                ConsoleHelper.PrintHeader("KİTABXANA İDARƏETMƏ SİSTEMİ");
                Console.WriteLine("1. Bütün kitabları göstər");
                Console.WriteLine("2. Bütün member-ləri göstər");
                Console.WriteLine("3. Kitabı ada görə axtar");
                Console.WriteLine("4. Müəllifə görə kitabları göstər");
                Console.WriteLine("5. Member kitab götürsün");
                Console.WriteLine("6. Member kitab qaytarsın");
                Console.WriteLine("7. Member-in borc götürdüyü kitabları göstər");
                Console.WriteLine("8. Gözləmə növbəsinə adam əlavə et");
                Console.WriteLine("9. Növbədən növbəti şəxsi çağır");
                Console.WriteLine("10. Son qaytarılan kitabı göstər");
                Console.WriteLine("11. Statistikaya bax");
                Console.WriteLine("12. 300+ səhifəli kitabları göstər");
                Console.WriteLine("0. Çıxış");
                Console.Write("\nSeçim et: ");

                string choice = Console.ReadLine();

                try
                {
                    Console.Clear();

                    switch (choice)
                    {
                        case "1":
                            ConsoleHelper.PrintHeader("BÜTÜN KİTABLAR");
                            foreach (var book in nationalLibrary.GetAll())
                            {
                                book.DisplayInfo();
                            }
                            break;

                        case "2":
                            ConsoleHelper.PrintHeader("BÜTÜN MEMBER-LƏR");
                            foreach (var member in members)
                            {
                                Console.WriteLine($"ID: {member.Id} | Ad: {member.Name} | Email: {member.Email}");
                            }
                            break;

                        case "3":
                            ConsoleHelper.PrintHeader("KİTABI AXTAR");
                            Console.Write("Kitab adını daxil et: ");
                            string title = Console.ReadLine();

                            Book foundBook = manager.SearchByTitle(title);

                            if (foundBook == null)
                                throw new ItemNotFoundException("Bu adda kitab tapılmadı.");

                            Console.WriteLine("Tapılan kitab:");
                            foundBook.DisplayInfo();
                            break;

                        case "4":
                            ConsoleHelper.PrintHeader("MÜƏLLİFƏ GÖRƏ AXTARIŞ");
                            Console.Write("Müəllif adını daxil et: ");
                            string author = Console.ReadLine();

                            List<Book> authorBooks = manager.GetBooksByAuthor(author);

                            if (authorBooks.Count == 0)
                                throw new ItemNotFoundException("Bu müəllifə aid kitab tapılmadı.");

                            foreach (var book in authorBooks)
                            {
                                book.DisplayInfo();
                            }
                            break;

                        case "5":
                            ConsoleHelper.PrintHeader("KİTAB GÖTÜRMƏK");
                            ShowMembers(members);
                            Console.Write("Member ID daxil et: ");
                            int memberBorrowId = Convert.ToInt32(Console.ReadLine());

                            Member borrowMember = FindMemberById(members, memberBorrowId);

                            ShowBooks(books);
                            Console.Write("Kitab ID daxil et: ");
                            int borrowBookId = Convert.ToInt32(Console.ReadLine());

                            Book borrowBook = FindBookById(books, borrowBookId);

                            borrowMember.BorrowBook(borrowBook);
                            break;

                        case "6":
                            ConsoleHelper.PrintHeader("KİTAB QAYTARMAQ");
                            ShowMembers(members);
                            Console.Write("Member ID daxil et: ");
                            int memberReturnId = Convert.ToInt32(Console.ReadLine());

                            Member returnMember = FindMemberById(members, memberReturnId);

                            returnMember.DisplayBorrowedBooks();
                            Console.Write("Qaytarılacaq kitabın ID-ni daxil et: ");
                            int returnBookId = Convert.ToInt32(Console.ReadLine());

                            Book returnedBook = FindBorrowedBookById(returnMember, returnBookId);

                            returnMember.ReturnBook(returnBookId);
                            manager.ReturnBook(returnedBook);
                            break;

                        case "7":
                            ConsoleHelper.PrintHeader("MEMBER-İN BORC KİTABLARI");
                            ShowMembers(members);
                            Console.Write("Member ID daxil et: ");
                            int memberBooksId = Convert.ToInt32(Console.ReadLine());

                            Member memberBooks = FindMemberById(members, memberBooksId);
                            memberBooks.DisplayBorrowedBooks();
                            break;

                        case "8":
                            ConsoleHelper.PrintHeader("GÖZLƏMƏ NÖVBƏSİ");
                            Console.Write("Ad daxil et: ");
                            string queueName = Console.ReadLine();
                            manager.AddToWaitingQueue(queueName);
                            Console.WriteLine($"Hazırda növbədə: {manager.WaitingQueue.Count} nəfər var");
                            break;

                        case "9":
                            ConsoleHelper.PrintHeader("NÖVBƏDƏN ÇAĞIR");
                            string nextPerson = manager.ServeNextInQueue();
                            Console.WriteLine($"Xidmət edilir: {nextPerson}");
                            Console.WriteLine($"Qalan növbə sayı: {manager.WaitingQueue.Count}");
                            break;

                        case "10":
                            ConsoleHelper.PrintHeader("SON QAYTARILAN KİTAB");
                            Book lastReturned = manager.GetLastReturnedBook();
                            lastReturned.DisplayInfo();
                            break;

                        case "11":
                            ConsoleHelper.PrintHeader("STATİSTİKA");

                            int minYear = books[0].Year;
                            int maxYear = books[0].Year;

                            foreach (var book in books)
                            {
                                if (book.Year < minYear)
                                    minYear = book.Year;

                                if (book.Year > maxYear)
                                    maxYear = book.Year;
                            }

                            Console.WriteLine($"Ümumi kitab sayı: {manager.Books.Count}");
                            Console.WriteLine($"Ümumi member sayı: {members.Count}");
                            Console.WriteLine($"Növbədə nəfər sayı: {manager.WaitingQueue.Count}");
                            Console.WriteLine($"Stack-də kitab sayı: {manager.RecentlyReturned.Count}");
                            Console.WriteLine($"Ən köhnə kitab ili: {minYear}");
                            Console.WriteLine($"Ən yeni kitab ili: {maxYear}");
                            Console.WriteLine($"Bütün kitabların səhifə cəmi: {totalPages(books)}");
                            break;

                        case "12":
                            ConsoleHelper.PrintHeader("300+ SƏHİFƏLİ KİTABLAR");
                            foreach (var book in books)
                            {
                                if (isBigBook(book))
                                {
                                    printBook(book);
                                }
                            }
                            break;

                        case "0":
                            exit = true;
                            printMessage("Proqramdan çıxılır...");
                            break;

                        default:
                            Console.WriteLine("Yanlış seçim etdiniz.");
                            break;
                    }
                }
                catch (MaxBorrowLimitException ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }
                catch (ItemNotFoundException ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }
                catch (InvalidIndexException ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }
                catch (EmptyQueueException ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }
                catch (EmptyStackException ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Xəta: Düzgün formatda dəyər daxil edin.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Gözlənilməz xəta: {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine("\nDavam etmək üçün bir düymə basın...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowBooks(List<Book> books)
        {
            Console.WriteLine("Kitablar:");
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id} | {book.Title} | {book.Author}");
            }
        }

        static void ShowMembers(List<Member> members)
        {
            Console.WriteLine("Member-lər:");
            foreach (var member in members)
            {
                Console.WriteLine($"ID: {member.Id} | {member.Name} | {member.Email}");
            }
        }

        static Member FindMemberById(List<Member> members, int id)
        {
            foreach (var member in members)
            {
                if (member.Id == id)
                    return member;
            }

            throw new ItemNotFoundException("Bu ID-yə uyğun member tapılmadı.");
        }

        static Book FindBookById(List<Book> books, int id)
        {
            foreach (var book in books)
            {
                if (book.Id == id)
                    return book;
            }

            throw new ItemNotFoundException("Bu ID-yə uyğun kitab tapılmadı.");
        }

        static Book FindBorrowedBookById(Member member, int id)
        {
            foreach (var book in member.BorrowedBooks)
            {
                if (book.Id == id)
                    return book;
            }

            throw new ItemNotFoundException("Bu kitab member-də mövcud deyil.");
        }
    }
}