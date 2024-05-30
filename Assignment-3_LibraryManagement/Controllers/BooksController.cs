
using Assignment_3_LibraryManagement.Entity;
using Assignment_3_LIbraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;


namespace Assignment_3_LIbraryManagementSystem.Controllers
{

    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class BooksController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        public string ContainerName = "Book";

        public Container Container;

        [HttpPost]
        public async Task<BookModel> AddBookEntity(BookModel bookModel)
        {
            //1.Create Obj of entity and map  all the fields from model to entity
            BookEntity book = new BookEntity();
            /* book.UId = bookModel.UId;
             book.Id = bookModel.Id;*/
            book.Title = bookModel.Title;
            book.Author = bookModel.Author;
            book.PublishedDate = bookModel.PublishedDate;
            book.IsIssued = bookModel.IsIssued;

            //2.Assign values to mandatory fields
            book.Id = Guid.NewGuid().ToString();
            book.UId = book.Id;
            book.DocumentType = "book";
            book.CreatedBy = "";
            book.CreatedOn = DateTime.Now;
            book.UpdatedBy = "";
            book.UpdatedOn = DateTime.Now;
            book.Version = 1;
            book.Active = true;
            book.Archive = false;

            //3.Add data to the database
            BookEntity response = await Container.CreateItemAsync(book);

            //return the Model
            BookModel responseModel = new BookModel();
            responseModel.ISBN = response.ISBN;
            responseModel.Title = response.Title;
            responseModel.Author = responseModel.Author;
            responseModel.PublishedDate = responseModel.PublishedDate;
            responseModel.IsIssued = responseModel.IsIssued;

            return responseModel;
        }
        [HttpGet]
        public async Task<BookModel> GetBookByUid(string UId)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archive == false).FirstOrDefault();
            BookModel bookModel = new BookModel();
            bookModel.UID = book.UId;
            bookModel.ISBN = book.ISBN;
            bookModel.Title = book.Title;
            bookModel.Author = book.Author;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.IsIssued = book.IsIssued;
            return bookModel;

        }
        [HttpGet]
        public async Task<BookModel> GetBookByTitle(string Title)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Title == Title && q.Active == true && q.Archive == false).FirstOrDefault();
            BookModel bookModel = new BookModel();
            bookModel.UID = book.UId;
            bookModel.ISBN = book.ISBN;
            bookModel.Title = book.Title;
            bookModel.Author = book.Author;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.IsIssued = book.IsIssued;
            return bookModel;

        }
        [HttpGet]
        public async Task<List<BookModel>> GetAllBooks()
        {
            //fetch the records
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archive == false && q.DocumentType == "book");

            //maps the fields to the Model
            List<BookModel> bookModel = new List<BookModel>();
            foreach (var book in books)
            {
                BookModel model = new BookModel();
                model.UID = book.UId;
                model.ISBN = book.ISBN;
                model.Title = book.Title;
                model.Author = book.Author;
                model.PublishedDate = book.PublishedDate;
                model.IsIssued = book.IsIssued;

                bookModel.Add(model);
            }
            return bookModel;
        }
        [HttpGet]
        public async Task<List<BookModel>> GetAvailableBooks()
        {
            //fetch the records
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archive == false && q.DocumentType == "book" && q.IsIssued == false);

            //maps the fields to the Model
            List<BookModel> bookModel = new List<BookModel>();
            foreach (var book in books)
            {
                BookModel model = new BookModel();
                model.UID = book.UId;
                model.ISBN = book.ISBN;
                model.Title = book.Title;
                model.Author = book.Author;
                model.PublishedDate = book.PublishedDate;
                model.IsIssued = book.IsIssued;

                bookModel.Add(model);
            }
            return bookModel;
        }
        [HttpGet]
        public async Task<List<BookModel>> ShowIssuedBooks()
        {
            //fetch the records
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archive == false && q.DocumentType == "book" && q.IsIssued == true);

            //maps the fields to the Model
            List<BookModel> bookModel = new List<BookModel>();
            foreach (var book in books)
            {
                BookModel model = new BookModel();
                model.UID = book.UId;
                model.ISBN = book.ISBN;
                model.Title = book.Title;
                model.Author = book.Author;
                model.PublishedDate = book.PublishedDate;
                model.IsIssued = book.IsIssued;

                bookModel.Add(model);
            }
            return bookModel;
        }
        [HttpPut]
        public async Task<BookModel> UpdateBook(BookModel book)
        {
            //1.get existing Record By Uid
            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == book.UID && q.Active == true && q.Archive == false).FirstOrDefault();
            //2.Replace Records
            existingBook.Archive = true;
            await Container.ReplaceItemAsync(existingBook, existingBook.Id);

            //3.Assign values for the mandatory fields
            existingBook.Id = Guid.NewGuid().ToString();
            existingBook.UpdatedBy = "";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archive = false;

            //4.Assign values to the field which we will get from RequestObject
            existingBook.ISBN = book.ISBN;
            existingBook.Author = book.Author;
            existingBook.Title = book.Title;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.IsIssued = book.IsIssued;

            //5.Add data to the Database
            existingBook = await Container.CreateItemAsync(existingBook);

            //6.Return
            BookModel response = new BookModel();
            response.UID = existingBook.UId;
            response.ISBN = existingBook.ISBN;
            response.Title = existingBook.Title;
            response.Author = existingBook.Author;
            response.PublishedDate = existingBook.PublishedDate;
            response.IsIssued = existingBook.IsIssued;

            return response;


        }
        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);

            return container;

        }
        public BooksController()
        {
            Container = GetContainer();
        }

    }


}
