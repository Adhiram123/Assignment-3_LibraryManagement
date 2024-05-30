using Assignment_3_LIbraryManagementSystem.Entites;
using Assignment_3_LIbraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Assignment_3_LIbraryManagementSystem.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class MemberController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        public string ContainerName = "Member";

        public Container Container;

        [HttpPost]
        public async Task<MemberModel> AddMemberEntity(MemberModel memberModel)
        {
            //1.Create Obj of entity and map  all the fields from model to entity
            MemberEntity member = new MemberEntity();
            /* book.UId = bookModel.UId;
             book.Id = bookModel.Id;*/
            member.Name = memberModel.Name;
            member.DateOfBirth = memberModel.DateOfBirth;
            member.Email = memberModel.Email;

            //2.Assign values to mandatory fields
            member.Id = Guid.NewGuid().ToString();
            member.UId = member.Id;
            member.DocumentType = "member";
            member.CreatedBy = "";
            member.CreatedOn = DateTime.Now;
            member.UpdatedBy = "";
            member.UpdatedOn = DateTime.Now;
            member.Version = 1;
            member.Active = true;
            member.Archive = false;

            //3.Add data to the database
            MemberEntity response = await Container.CreateItemAsync(member);

            //return the Model
            MemberModel responseModel = new MemberModel();
            responseModel.Name = response.Name;
            responseModel.DateOfBirth = response.DateOfBirth;
            responseModel.Email = responseModel.Email;

            return responseModel;
        }
        [HttpGet]
        public async Task<MemberModel> GetMemberByUid(string UId)
        {
            var member = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archive == false).FirstOrDefault();
            MemberModel memberModel = new MemberModel();
            memberModel.UId = member.UId;
            memberModel.Name = member.Name;
            memberModel.DateOfBirth = member.DateOfBirth;
            memberModel.Email = member.Email;
            
            return memberModel;

        }

        [HttpGet]
        public async Task<List<MemberModel>> GetAllMembers()
        {
            //fetch the records
            var members = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.Active == true && q.Archive == false && q.DocumentType == "member");

            //maps the fields to the Model
            List<MemberModel> memberModel = new List<MemberModel>();
            foreach (var member in members)
            {
                MemberModel model = new MemberModel();
                model.UId = member.UId; 
                model.Name = member.Name;
                model.DateOfBirth = member.DateOfBirth;
                model.Email = member.Email;
                

                memberModel.Add(model);
            }
            return memberModel;
        }

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);

            return container;

        }
        [HttpPut]
        public async Task<MemberModel> UpdateMember(MemberModel member)
        {
            //1.get existing Record By Uid
            var existingMember = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == member.UId && q.Active == true && q.Archive == false).FirstOrDefault();
            //2.Replace Records
            existingMember.Archive = true;
            await Container.ReplaceItemAsync(existingMember, existingMember.Id);

            //3.Assign values for the mandatory fields
            existingMember.Id = Guid.NewGuid().ToString();
            existingMember.UpdatedBy = "";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version + 1;
            existingMember.Active = true;
            existingMember.Archive = false;

            //4.Assign values to the field which we will get from RequestObject
            existingMember.Name = member.Name;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Email = member.Email;

            //5.Add data to the Database
            existingMember = await Container.CreateItemAsync(existingMember);

            //6.Return
            MemberModel response = new MemberModel();
            response.UId = existingMember.UId;
            response.Name = existingMember.Name;
            response.DateOfBirth = existingMember.DateOfBirth;
            response.Email = existingMember.Email;

            return response;


        }
        public MemberController()
        {
            Container = GetContainer();
        }
    }
}
