using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VismaLibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            bool close = true;
            while (close)
            {
                Console.WriteLine("|---------------------------------------------|");
                Console.WriteLine("|Choose one:                                  |");
                Console.WriteLine("|1. Add a new book to library                 |");
                Console.WriteLine("|2. Take a book from the library(NOT FINISHED)|");
                Console.WriteLine("|3. Return a book(NOT FINISHED)               |");
                Console.WriteLine("|4. List all books                            |");
                Console.WriteLine("|5. Delete a book                             |");
                Console.WriteLine("|---------------------------------------------|");

                int option = int.Parse(Console.ReadLine());
                Program optionProgram = new();
                if (option == 1)
                {
                    optionProgram.AddBook();
                }
                else if (option == 2)
                {
                    //optionProgram.TakeBook();
                }
                else if (option == 3)
                {
                    // optionProgram.ReturnBook();
                }
                else if (option == 4)
                {
                    optionProgram.ListBooks();
                }
                else if (option == 5)
                {
                    optionProgram.BookDelete();
                }
                else if (option == 6)
                {
                    Console.WriteLine("Thank you for using program");
                    close = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option...");
                    Console.WriteLine("Please select the right option...");
                    Console.WriteLine("Write anything to see options...");
                }
                Console.ReadLine();
            }
        }
        private string jsonfile = @"C:\Users\Dominykas\source\repos\VismaLibrarySystem\VismaLibrarySystem\bookjsonfile.json";
        private void AddBook()
        {
            {
                Console.WriteLine("Book ISBN (only number):");
                var bookISBN = int.Parse(Console.ReadLine());
                Console.Write("Book name:");
                var bookName = (Console.ReadLine());
                Console.Write("Book author:");
                var bookAuthor = (Console.ReadLine());
                Console.Write("Book category:");
                var bookCategory = (Console.ReadLine());
                Console.Write("Book language:");
                var bookLanguage = (Console.ReadLine());
                Console.Write("Book publication date:");
                var bookPublicationDate = (Console.ReadLine());

                var addNewBook = "{ 'bookISBN': " + bookISBN + ", 'bookname': '" + bookName + "','bookauthor':'" + bookAuthor + "','bookcategory':'" + bookCategory + "','booklanguage':'" + bookLanguage + "','bookpublicationdate':'" + bookPublicationDate + "'}";
                //ADD BOOK TO LIBRARY ^
                try
                {
                    {
                        var json = File.ReadAllText(jsonfile);
                        var jsonObj = JObject.Parse(json);
                        var booksarray = jsonObj.GetValue("books") as JArray;
                        var newBook = JObject.Parse(addNewBook);
                        booksarray.Add(newBook);

                        jsonObj["books"] = booksarray;
                        string newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                        File.WriteAllText(jsonfile, newJsonResult);
                        Console.WriteLine("\nBook successfully added to library");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Add Error : " + ex.Message.ToString());
                }
            }
            Console.WriteLine("Write anything to see options or press ENTER");

        }
        //LIST ALL BOOKS ON "bookjsonfile.json" (Also lists all new added books)
        private void ListBooks()
        {
            var json = File.ReadAllText(jsonfile);
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {

                    JArray booksarray = (JArray)jObject["books"];
                    if (booksarray != null)
                    {
                        foreach (var item in booksarray)
                        {
                            Console.WriteLine("--------------------------------------------------------");
                            Console.WriteLine("Book ISBN :" + item["bookISBN"]);
                            Console.WriteLine("Book name :" + item["bookname"]);
                            Console.WriteLine("Book author :" + item["bookauthor"]);
                            Console.WriteLine("Book category :" + item["bookcategory"]);
                            Console.WriteLine("Book language :" + item["booklanguage"]);
                            Console.WriteLine("Book publication date :" + item["bookpublicationdate"]);
                            Console.WriteLine("--------------------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            Console.WriteLine("Write anything to see options or press ENTER");
        }
        //DELETES A BOOK FROM "bookjsonfile.json"
        private void BookDelete()
        {
            var json = File.ReadAllText(jsonfile);
            try
            {
                var jObject = JObject.Parse(json);
                JArray booksarray = (JArray)jObject["books"];
                Console.Write("Enter book ISBN to delete : ");
                var bookISBN = Convert.ToInt32(Console.ReadLine());

                if (bookISBN > 0)
                {
                    var bookToDelete = booksarray.FirstOrDefault(obj => obj["bookISBN"].Value<int>() == bookISBN);

                    booksarray.Remove(bookToDelete);

                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    File.WriteAllText(jsonfile, output);
                }
                else
                {
                    Console.Write("Invalid bookISBN, Try Again!");
                    ListBooks();
                }
            }
            catch (Exception)
            {
                throw;
            }
            Console.WriteLine("Write anything to see options or press ENTER");
        }
        //NOT FINISHED BORROW BOOK FROM LIBRARY ALSO THE RETURN BOOK

        /* static List<Book> bookList = new List<Book>();
         static List<BorrowBook> borrowList = new List<BorrowBook>();
         static Book book = new Book();
         static BorrowBook borrow = new BorrowBook();
         private void TakeBook()
         {

             var json = File.ReadAllText(jsonfile);
             try
             {
                 var jObject = JObject.Parse(json);
                 JArray booksarray = (JArray)jObject["books"];
                 Book book = new Book();
                 BorrowBook borrow = new BorrowBook();
                 Console.WriteLine("User id : {0}", (borrow.userId = borrowList.Count + 1));
                 Console.Write("Name :");
                 borrow.userName = Console.ReadLine();

                 Console.Write("Enter book ISBN : ");
                 var bookISBN = Convert.ToInt32(Console.ReadLine());
                 Console.Write("Number of Books : ");
                 borrow.borrowCount = int.Parse(Console.ReadLine());
                 int number;
                 number = 1;
                 if (number <= 3)
                 {

                 }
                 else
                 {
                     Console.Write("You can't take more than 3 books!");
                 }
                 borrow.borrowDate = DateTime.Now;
                 Console.WriteLine("Date - {0} and Time - {1}", borrow.borrowDate.ToShortDateString(), borrow.borrowDate.ToShortTimeString());

                 if (bookList.Exists(x => x.bookISBN == borrow.borrowbookISBN))
                 {
                     foreach (Book searchBookISBN in bookjsonfile.json)
                     {
                         if (searchBookISBN.bookCount >= searchBookISBN.bookCount - borrow.borrowCount && searchBookISBN.bookCount - borrow.borrowCount >= 0)
                         {
                             if (searchBookISBN.bookISBN == borrow.borrowbookISBN)
                             {
                                 searchBookISBN.bookCount = searchBookISBN.bookCount - borrow.borrowCount;
                                 break;
                             }
                         }
                         else
                         {
                             Console.WriteLine("Found 0 books", searchBookISBN.bookCount);
                             break;
                         }
                     }
                 }
                 else
                 {
                     Console.WriteLine("Book ISBN not found", borrow.borrowbookISBN);
                 }
                 borrowList.Add(borrow);
             }

             catch
             {

             }

          }   

     }
     class BorrowBook
     {
         public int userId;
         public string userName;
         public DateTime borrowDate;
         public int borrowCount;
     }
     class Book
     {
         public int bookCount;
     }
        */
    }
}
//END OF PROGRAM

