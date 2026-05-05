#nullable enable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library
{
    
    public record Book(int Id, string Title, string Author);

    class Program
    {
        private static List<Book> books = new();

        private static readonly string filePath =
            Path.Combine(Directory.GetCurrentDirectory(), "books.json");

        static async Task Main(string[] args)
        {
            Console.WriteLine($"JSON Path: {filePath}\n");

            await LoadBooksAsync();

            while (true)
            {
                Console.WriteLine("""
                1. Add Book
                2. Remove Book
                3. Search Book
                4. Sort Books
                5. View All Books
                6. Exit
                """);

                Console.Write("Choose option: ");

                try
                {
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input.\n");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            await AddBookAsync();
                            break;
                        case 2:
                            await RemoveBookAsync();
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
                            Console.WriteLine("Invalid choice.\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Main Error: {ex.Message}");
                }
            }
        }

        //LOAD JSON
        static async Task LoadBooksAsync()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No JSON file found. It will be created on first save.\n");
                    books = new List<Book>();
                    return;
                }

                string json = await File.ReadAllTextAsync(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    books = new List<Book>();
                    return;
                }

                books = JsonSerializer.Deserialize<List<Book>>(json)
                        ?? new List<Book>();

                Console.WriteLine("Books loaded successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load Error: {ex.Message}");
                books = new List<Book>();
            }
        }

        // SAVE JSON (FILE CREATED HERE)
        static async Task SaveBooksAsync()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(books, options);

                //This line CREATES the file if it doesn't exist
                await File.WriteAllTextAsync(filePath, json);

                Console.WriteLine("Books saved.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save Error: {ex.Message}");
            }
        }

        static async Task AddBookAsync()
        {
            try
            {
                Console.Write("Enter Id: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID.\n");
                    return;
                }

                if (books.Any(b => b.Id == id))
                {
                    Console.WriteLine("Book with this ID already exists.\n");
                    return;
                }

                Console.Write("Enter Title: ");
                string title = Console.ReadLine() ?? "";

                Console.Write("Enter Author: ");
                string author = Console.ReadLine() ?? "";

                var newBook = new Book(id, title, author);

                books.Add(newBook);

                await SaveBooksAsync();

                Console.WriteLine("Book added.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add Error: {ex.Message}");
            }
        }

        static async Task RemoveBookAsync()
        {
            try
            {
                Console.Write("Enter Id to remove: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID.\n");
                    return;
                }

                var book = books.FirstOrDefault(b => b.Id == id);

                if (book is not null)
                {
                    books.Remove(book);
                    await SaveBooksAsync();
                    Console.WriteLine($"Removed: {book.Title}\n");
                }
                else
                {
                    Console.WriteLine("Book not found.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove Error: {ex.Message}");
            }
        }

        static void SearchBook()
        {
            try
            {
                Console.Write("Enter title to search: ");
                string search = Console.ReadLine() ?? "";

                var results = books
                    .Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (results.Count == 0)
                {
                    Console.WriteLine("No books found.\n");
                    return;
                }

                foreach (var book in results)
                {
                    Console.WriteLine(book);
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Search Error: {ex.Message}");
            }
        }

        static void SortBooks()
        {
            try
            {
                Console.WriteLine("1. Title Asc");
                Console.WriteLine("2. Author Asc");
                Console.WriteLine("3. Title Desc");
                Console.WriteLine("4. Author Desc");

                Console.Write("Choose: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.\n");
                    return;
                }

                List<Book>? sorted = choice switch
                {
                    1 => books.OrderBy(b => b.Title).ToList(),
                    2 => books.OrderBy(b => b.Author).ToList(),
                    3 => books.OrderByDescending(b => b.Title).ToList(),
                    4 => books.OrderByDescending(b => b.Author).ToList(),
                    _ => null
                };

                if (sorted is null)
                {
                    Console.WriteLine("Invalid choice.\n");
                    return;
                }

                foreach (var book in sorted)
                {
                    Console.WriteLine(book);
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sort Error: {ex.Message}");
            }
        }

        static void ViewBooks()
        {
            try
            {
                if (!books.Any())
                {
                    Console.WriteLine("No books available.\n");
                    return;
                }

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"View Error: {ex.Message}");
            }
        }
    }
}
