using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace lab10
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            context.Database.EnsureCreated();
            Console.WriteLine(context.Books.Find(1));

            // po kazdej zmianie musimy zapisać zmiany
            //context.Books.Add(new Book() {  });
            //context.SaveChanges();

            /*            context.Books.Remove(context.Books.Find(1));
                        context.Books.Add(new Book() { Title = "PHP", EditionYear = 2000, AuthorId = 1 });
                        var book = context.Books.Find(2);
                        book.EditionYear = 2010;
                        context.Books.Update(book);
                        context.SaveChanges();*/

            IQueryable<Book> books = from b in context.Books
                                     select b;
            Console.WriteLine("Lista książek:");
            Console.WriteLine(String.Join("\n", books));

            var booksWithAuthors = from book in context.Books
                                   join author in context.Authors
                                   on book.AuthorId equals author.Id
                                   select new { Title = book.Title, Author = author.Name };
            Console.WriteLine("Join:");
            Console.WriteLine(String.Join("\n", booksWithAuthors));

            Console.WriteLine("Foreach:");
            foreach (var item in booksWithAuthors)
            {
                Console.WriteLine(item.Author);
            }

            // zapisz LINQ, które zwróci listę rekordów z polami Id książki i nazwisko autora
            // dla książek wydanych po 2019r

            var ex1 = from b in context.Books
                      join a in context.Authors
                      on b.AuthorId equals a.Id
                      where b.EditionYear > 2019
                      select new { BookId = b.Id, Author = a.Name };

            Console.WriteLine("Zadanie 1:");
            Console.WriteLine(String.Join("\n", ex1));

            booksWithAuthors = context.Books.Join(
                context.Authors,
                book => book.AuthorId,
                author => author.Id,
                (book, author) => new { Title = book.Title, Author = author.Name }
                );


            string xml =
                "<books>" +
                    "<book>" +
                        "<id>1</id>" +
                        "<title>C#</title>" +
                        "<editionYear>2000</editionYear>" +
                    "</book>" +
                    "<book>" +
                        "<id>2</id>" +
                        "<title>Java</title>" +
                        "<editionYear>2002</editionYear>" +
                    "</book>" +
                "</books>";

            XDocument doc = XDocument.Parse(xml);
            var xmlBooks = doc
                .Elements("books")
                .Elements("book")
                .Select(x => new { Id = x.Element("id").Value,
                    Title = x.Element("title").Value,
                    EditionYear = x.Element("editionYear").Value
                });

            Console.WriteLine("XML:");
            foreach(var item in xmlBooks)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("BankApi");

            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xmlRates = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");

            XDocument bankApi = XDocument.Parse(xmlRates);
            var bank = bankApi
                .Element("ArrayOfExchangeRatesTable")
                .Element("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(x => new {
                    Code = x.Element("Code").Value,
                    Bid = x.Element("Bid").Value,
                    Ask = x.Element("Ask").Value
                });

            foreach (var item in bank)
            {
                Console.WriteLine(item);
            }
        }

        public record Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int AuthorId { get; set; }
            public int EditionYear { get; set; }
        }

        public record Author
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class AppContext : DbContext
        {
            public DbSet<Book> Books { get; set; }
            public DbSet<Author> Authors { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("DATASOURCE=d:\\database\\data.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder
                    .Entity<Book>()
                    .ToTable("books")
                    .HasData(
                        new Book() { Id = 1, AuthorId = 1, EditionYear = 2020, Title = "C#"},
                        new Book() { Id = 2, AuthorId = 1, EditionYear = 2018, Title = "C#"},
                        new Book() { Id = 3, AuthorId = 2, EditionYear = 2021, Title = "C#"},
                        new Book() { Id = 4, AuthorId = 2, EditionYear = 2019, Title = "C#"}
                    );
                modelBuilder
                    .Entity<Author>()
                    .ToTable("authors")
                    .HasData(
                        new Author() { Id = 1, Name = "Freeman" },
                        new Author() { Id = 2, Name = "Bloch" }
                    );
            }
        }
    }
}
