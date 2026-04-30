using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + ", Title: " + Title + ", Author: " + Author;
        }
    }

    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\n1. Add Book");
                    Console.WriteLine("2. Remove Book");
                    Console.WriteLine("3. Search Book");
                    Console.WriteLine("4. Sort Books");
                    Console.WriteLine("5. View All Books");
                    Console.WriteLine("6. Exit");

                    Console.Write("Choose option: ");

                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Enter a number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            AddBook();
                            break;
                        case 2:
                            RemoveBook();
                            break;
                        case 3:
                            SearchBook();
                            break;
                        case 4:
                            SortBooks();
                            break;
                        case 5:
                            ViewBooks();
                            break;
                        case 6:
                            return;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
                finally
                {
                    Console.WriteLine("\n--- Operation completed ---\n");
                }
            }
        }

        static void AddBook()
        {
            try
            {
                Console.Write("Enter Id: ");
                int id;
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                if (books.Any(b => b.Id == id))
                {
                    Console.WriteLine("Book with this ID already exists.");
                    return;
                }

                Console.Write("Enter Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Author: ");
                string author = Console.ReadLine();

                books.Add(new Book
                {
                    Id = id,
                    Title = title,
                    Author = author
                });

                Console.WriteLine("Book added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AddBook: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("AddBook completed.");
            }
        }

        static void RemoveBook()
        {
            try
            {
                Console.Write("Enter Id to remove: ");
                int id;

                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                var book = books.FirstOrDefault(b => b.Id == id);

                if (book != null)
                {
                    books.Remove(book);
                    Console.WriteLine("Book removed.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RemoveBook: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("RemoveBook completed.");
            }
        }

        static void SearchBook()
        {
            try
            {
                Console.Write("Enter title to search: ");
                string title = Console.ReadLine();

                var results = books
                    .Where(b => b.Title != null && title != null &&
                                b.Title.ToLower().Contains(title.ToLower()))
                    .ToList();

                if (results.Count > 0)
                {
                    foreach (var book in results)
                    {
                        Console.WriteLine(book);
                    }
                }
                else
                {
                    Console.WriteLine("No books found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SearchBook: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("SearchBook completed.");
            }
        }

        static void SortBooks()
        {
            try
            {
                Console.WriteLine("1. Sort by Title (Ascending)");
                Console.WriteLine("2. Sort by Author (Ascending)");
                Console.WriteLine("3. Sort by Title (Descending)");
                Console.WriteLine("4. Sort by Author (Descending)");

                Console.Write("Choose: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }

                List<Book> sorted = null;

                switch (choice)
                {
                    case 1:
                        sorted = books.OrderBy(b => b.Title).ToList();
                        break;
                    case 2:
                        sorted = books.OrderBy(b => b.Author).ToList();
                        break;
                    case 3:
                        sorted = books.OrderByDescending(b => b.Title).ToList();
                        break;
                    case 4:
                        sorted = books.OrderByDescending(b => b.Author).ToList();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        return;
                }

                foreach (var book in sorted)
                {
                    Console.WriteLine(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SortBooks: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("SortBooks completed.");
            }
        }

        static void ViewBooks()
        {
            try
            {
                if (books.Count == 0)
                {
                    Console.WriteLine("No books available.");
                    return;
                }

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ViewBooks: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("ViewBooks completed.");
            }
        }
    }
}