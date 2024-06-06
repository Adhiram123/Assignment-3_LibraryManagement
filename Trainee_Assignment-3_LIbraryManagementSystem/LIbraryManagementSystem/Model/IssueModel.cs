

namespace Assignment_3_LIbraryManagementSystem.Model
{
    public class IssueModel
    {
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UID { get; set; }

        [JsonProperty(PropertyName = "bookID", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "memberId", NullValueHandling = NullValueHandling.Ignore)]
        public string MemberId { get; set; }

        [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IssueDate { get; set; }

        [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ReturnDate { get; set; }

        [JsonProperty(PropertyName = "isreturned", NullValueHandling = NullValueHandling.Ignore)]

        public bool IsReturned { get; set; }


    }
}
