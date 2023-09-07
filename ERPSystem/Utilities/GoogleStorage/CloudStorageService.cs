using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.IO;

namespace ERPSystem.Utilities.GoogleStorage
{
    public class CloudStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public CloudStorageService()
        {
            // Load the credentials from the service account JSON key file
            var credential = GoogleCredential.FromFile("D:\\GitHub\\WPFLearning\\ERPSystem\\ERPSystem\\imagestockforwpfapp-14e48c55bd0b.json");

            // Create the storage client with the authenticated credentials
            _storageClient = StorageClient.Create(credential);
            _bucketName = "project_management_image_stock";
        }

        public void UploadFile(string localFilePath, string objectName)
        {
            using (var fileStream = File.OpenRead(localFilePath))
            {
                _storageClient.UploadObject(_bucketName, objectName, null, fileStream);
            }
        }

        public void DownloadFile(string objectName, string localFilePath)
        {
            string destinationFolder = Path.GetDirectoryName(localFilePath);

            Directory.CreateDirectory(destinationFolder);

            using (var outputFile = File.OpenWrite(localFilePath))
            {
                _storageClient.DownloadObject(_bucketName, objectName, outputFile);
            }
        }


        public void UpdateFile(string localFilePath, string objectName)
        {
            UploadFile(localFilePath, objectName);
        }

        public void DeleteFile(string objectName)
        {
            _storageClient.DeleteObject(_bucketName, objectName);
        }
    }
}
