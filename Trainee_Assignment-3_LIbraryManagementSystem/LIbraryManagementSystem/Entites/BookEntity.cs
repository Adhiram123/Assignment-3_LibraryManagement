

namespace Assignment_3_LIbraryManagementSystem.Entites
{
    public class BookEntity
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string Id { get; set; }

        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "dtype", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdON", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]

        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archive", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archive { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "publishedDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime PublishedDate { get; set; }

        [JsonProperty(PropertyName = "iSBN", NullValueHandling = NullValueHandling.Ignore)]
        public string ISBN { get; set; }

        [JsonProperty(PropertyName = "isIssued", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsIssued { get; set; }



    }
}
