using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagementSystem.Models;

namespace FileManagementSystem.Service
{
    public class MetadataExtractManager : IMetadataExtractManager
    {
        private const string FileFetchingError = "Error while fetching file details";
        private const string DirectoryNotFoundError = "Directory was not found";


        List<FileMetadataModel> fileMetadataList = new();
        public List<FileDetailModel> FetchFileCountByType()
        {
            try
            {
                List<FileDetailModel>? fileDetails = null;

                // Group by files with an extension
                var result = fileMetadataList.Select(f => f.Extension?.TrimStart('.').ToLower())
                                 .GroupBy(y => y, (ex, excnt) => new
                                 {
                                     Extension = ex,
                                     Count = excnt.Count()
                                 });

                // Prepare the final result 
                fileDetails = new(result.Count());
                foreach (var i in result)
                {
                    var fileByType = new FileDetailModel
                    {
                        Extension = i.Extension,
                        Count = i.Count
                    };

                    fileDetails.Add(fileByType);
                }
                return fileDetails;
            }
            catch (Exception)
            {
                throw new Exception(FileFetchingError);
            }
        }

        public List<FileMetadataModel> FetchFilesByType(string fileType)
        {
            // Filter files by extension
            return fileMetadataList.Where(f => f.Extension?.TrimStart('.').ToLower() == fileType.ToLower()).ToList();
        }

        public List<FileDetailModel> Process(string folderPath)
        {
            try
            {
                // Read Files from Folder 
                if (!Directory.Exists(folderPath))
                {
                    throw new ArgumentException(DirectoryNotFoundError);
                }

                // Save Files in Collection
                List<FileMetadataModel> fileByTypes = new();
                DirectoryInfo dir = new(folderPath);

                // Filter files by extension
                var files = dir.GetFiles();

                // Prepare the final result 
                fileMetadataList = new List<FileMetadataModel>();
                foreach (var i in files)
                {
                    var fileByType = new FileMetadataModel
                    {
                        Name = Path.GetFileName(i.FullName),
                        Size = $"{Math.Round(((i.Length / 1024f) / 1024f), 2)} MB",
                        CreatedOn = i.CreationTimeUtc.ToShortDateString(),
                        ModifiedOn = i.LastWriteTimeUtc.ToShortDateString(),
                        Extension = Path.GetExtension(i.FullName)
                    };

                    fileMetadataList.Add(fileByType);
                }

                // Return Collection of FileTypeByCount
                return FetchFileCountByType();


            }
            catch (Exception)
            {
                throw new Exception(FileFetchingError);
            }
        }
    }
}
