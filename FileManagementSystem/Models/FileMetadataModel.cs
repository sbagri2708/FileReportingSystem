namespace FileManagementSystem.Models
{
    public class FileMetadataModel
    { 
        public string? Name { get; set; }
        public string? Extension { get; set; }
        public string? CreatedOn { get; set; }

        public string? ModifiedOn { get; set; }
        public string? Size { get; set; }
    }
}
