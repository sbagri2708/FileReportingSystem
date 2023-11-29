using FileManagementSystem.Models;
namespace FileManagementSystem.Service
{
    public interface IMetadataExtractManager
    {
        public List<FileDetailModel> Process(string folderPath);

        public List<FileDetailModel> FetchFileCountByType();

        public List<FileMetadataModel> FetchFilesByType(string fileType);
    }
}
