using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Image.Service.Service
{
    using System.IO;

    using Amazon;
    using Amazon.Runtime;
    using Amazon.S3;
    using Amazon.S3.Model;

    using MicroService.Core;
    using MicroService.Image.Service.Declaration;
    using MicroService.Image.Service.Model;
    using MicroService.Image.Service.Repository;

    /// <summary>
    /// Implementation amazon du <see cref="IImageService"/>
    /// </summary>
    public class ImageServiceAmazon : IImageService
    {
        private FileRepository _fileRepository;

        private IAmazonS3 _amazonS3Client;

        private string _bucketName;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ImageServiceAmazon"/>
        /// </summary>
        public ImageServiceAmazon()
        {
            _fileRepository = (FileRepository)IocManager.GetSingleton<FileRepository>();

            string key = Environment.GetEnvironmentVariable("AMAZON_KEY");
            string secret = Environment.GetEnvironmentVariable("AMAZON_SECRET");

            AWSCredentials awsCredentials = new BasicAWSCredentials(key, secret);
            _amazonS3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.EUWest3);
            _bucketName = Environment.GetEnvironmentVariable("AMAZON_BUCKET");
        }

        /// <summary>
        /// Sauvegarde le fichier.
        /// </summary>
        /// <param name="bytes">Bytes du fichiers.</param>
        /// <param name="fileName">Nom du fichier.</param>
        /// <returns>Entite fichier.</returns>
        public FileModel SaveFile(byte[] bytes, string fileName)
        {
            FileModel fileModel = new FileModel { FileName = fileName, Key = $@"prod/--ID--{Path.GetExtension(fileName)}" };

            _fileRepository.Create(fileModel);

            fileModel.Key = $@"prod/{fileModel.Id}{Path.GetExtension(fileName)}";

            PutObjectRequest request = new PutObjectRequest() { InputStream = new MemoryStream(bytes), BucketName = _bucketName, Key = fileModel.Key };

            PutObjectResponse response = _amazonS3Client.PutObjectAsync(request).Result;

            fileModel.Url = $@"https://{_bucketName}.s3.eu-west-3.amazonaws.com/{fileModel.Key}";

            _fileRepository.Update(fileModel);

            return fileModel;
        }

        /// <summary>
        /// Supprime le fichier.
        /// </summary>
        /// <param name="id">Identifiant du fichier.</param>
        public void DeleteFile(string id)
        {
            FileModel fileModel = GetFile(id);
            var deleteFileRequest = new DeleteObjectRequest { BucketName = _bucketName, Key = fileModel.Key };
            DeleteObjectResponse fileDeleteResponse = _amazonS3Client.DeleteObjectAsync(deleteFileRequest).Result;

            _fileRepository.Delete(id);
        }

        /// <summary>
        /// Met à jour le fichier.
        /// </summary>
        /// <param name="id">Identifiant du fichier.</param>
        /// <param name="bytes">Bytes du fichiers.</param>
        /// <param name="fileName">Nom du fichier.</param>
        /// <returns>Entite fichier.</returns>
        public FileModel UpdateFile(string id, byte[] bytes, string fileName)
        {
            FileModel fileModel = GetFile(id);
            fileModel.FileName = fileName;
            File.WriteAllBytes(fileModel.Url, bytes);

            PutObjectRequest request = new PutObjectRequest() { InputStream = new MemoryStream(bytes), BucketName = _bucketName, Key = fileModel.Key };

            PutObjectResponse response = _amazonS3Client.PutObjectAsync(request).Result;
            return _fileRepository.Update(fileModel);
        }

        /// <summary>
        /// Récupère le fichier.
        /// </summary>
        /// <param name="id">Identifiant du fichier.</param>
        /// <returns>Entite fichier.</returns>
        public FileModel GetFile(string id)
        {
            return _fileRepository.Get(id);
        }

        /// <summary>
        /// Liste les fichiers.
        /// </summary>
        /// <returns>Entites fichiers.</returns>
        public List<FileModel> ListFile()
        {
            return _fileRepository.List();
        }
    }
}
