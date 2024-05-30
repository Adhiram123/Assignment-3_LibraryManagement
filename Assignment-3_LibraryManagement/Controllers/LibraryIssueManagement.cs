using Assignment_3_LibraryManagement.Entity;
using Assignment_3_LIbraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Assignment_3_LIbraryManagementSystem.Controllers
{


    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class LibraryIssueManagement : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        public string ContainerName = "Book";

        public Container Container;
      



        [HttpPost]
        public async Task<IssueModel> AddIssueBookEntity(IssueModel issueModel)
        {
            BookEntity book = new BookEntity();


            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == book.UId && q.DocumentType == "book" && q.IsIssued == true && q.Active == true && q.Archive == false).FirstOrDefault();


            //1.Create Obj of entity and map  all the fields from model to entity
            IssueEntity issue = new IssueEntity();
            /* book.UId = bookModel.UId;
             book.Id = bookModel.Id;*/
            issue.BookId = issueModel.BookId;
            issue.MemberId = issueModel.MemberId;
            issue.IssueDate = issueModel.IssueDate;
            issue.Returndate = issueModel.ReturnDate;

            //2.Assign values to mandatory fields
            issue.Id = Guid.NewGuid().ToString();
            issue.UId = issue.Id;
            issue.DocumentType = "issue";
            issue.CreatedBy = "";
            issue.CreatedOn = DateTime.Now;
            issue.UpdatedBy = "";
            issue.UpdatedOn = DateTime.Now;
            issue.Version = 1;
            issue.Active = true;
            issue.Archive = false;



            //3.Add data to the database
            IssueEntity response = await Container.CreateItemAsync(issue);

            BookEntity bookEntity = new BookEntity();
            BookModel bookModel = new BookModel();
            MemberEntity memberEntity = new MemberEntity();
            //return the Model
            IssueModel responseModel = new IssueModel();

            // responseModel.BookId = bookEntity.UId;
            responseModel.BookId = response.BookId;
            responseModel.MemberId = response.MemberId;
            responseModel.IssueDate = response.IssueDate;
            responseModel.ReturnDate = response.Returndate;
            responseModel.IsReturned = response.IsReturned;

            return responseModel;




        }
        [HttpGet]
        public async Task<IssueModel> GetIssuedBooks(string UId)
        {
            var issue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archive == false && q.IsReturned == false).FirstOrDefault();
            IssueModel issueModel = new IssueModel();
            issueModel.UID = issue.UId;
            issueModel.BookId = issue.BookId;
            issueModel.MemberId = issue.MemberId;
            issueModel.IssueDate = issue.IssueDate;
            issueModel.ReturnDate = issue.Returndate;
            issueModel.IsReturned = issue.IsReturned;
            return issueModel;
        }

        [HttpPut]
        public async Task<IssueModel> UpdateIssuedBooks(IssueModel model)
        {
            //1.get existing Record By Uid
            var existingIssue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == model.UID && q.Active == true && q.Archive == false).FirstOrDefault();
            //2.Replace Records
            existingIssue.Archive = true;
            await Container.ReplaceItemAsync(existingIssue, existingIssue.Id);

            //3.Assign values for the mandatory fields
            existingIssue.Id = Guid.NewGuid().ToString();
            existingIssue.UpdatedBy = "";
            existingIssue.UpdatedOn = DateTime.Now;
            existingIssue.Version = existingIssue.Version + 1;
            existingIssue.Active = true;
            existingIssue.Archive = false;

            //4.Assign values to the field which we will get from RequestObject
            existingIssue.BookId = model.BookId;
            existingIssue.MemberId = model.MemberId;
            existingIssue.IssueDate = model.IssueDate;
            existingIssue.Returndate = model.ReturnDate;
            existingIssue.IsReturned = model.IsReturned;

            //5.Add data to the Database
            existingIssue = await Container.CreateItemAsync(existingIssue);

            //6.Return
            IssueModel response = new IssueModel();
            response.UID = existingIssue.UId;
            response.BookId = existingIssue.BookId;
            response.MemberId = existingIssue.MemberId;
            response.IssueDate = existingIssue.IssueDate;
            response.ReturnDate = existingIssue.Returndate;
            response.IsReturned = existingIssue.IsReturned;

            return response;

        }


        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);

            return container;

        }

        public LibraryIssueManagement()
        {
            Container = GetContainer();
            

        }


    }




}



