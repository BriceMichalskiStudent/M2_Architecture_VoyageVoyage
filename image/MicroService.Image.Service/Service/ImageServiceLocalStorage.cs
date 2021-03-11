using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Image.Service.Service
{
    using System.IO;

    using MicroService.Core;
    using MicroService.Image.Service.Declaration;
    using MicroService.Image.Service.Model;
    using MicroService.Image.Service.Repository;

    /// <summary>
    /// Implementation local du <see cref="IImageService"/>
    /// </summary>
    public class ImageServiceLocalStorage : IImageService
    {
        private FileRepository _fileRepository;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ImageServiceLocalStorage"/>
        /// </summary>
        public ImageServiceLocalStorage()
        {
            _fileRepository = (FileRepository)IocManager.GetSingleton<FileRepository>();
        }

        /// <summary>
        /// Sauvegarde le fichier.
        /// </summary>
        /// <param name="bytes">Bytes du fichiers.</param>
        /// <param name="fileName">Nom du fichier.</param>
        /// <returns>Entite fichier.</returns>
        public FileModel SaveFile(byte[] bytes, string fileName)
        {
            FileModel fileModel = new FileModel
                                      {
                                          FileName = fileName
                                      };

            _fileRepository.Create(fileModel);

            File.WriteAllBytes($@"/app/ged/{fileModel.Id}{Path.GetExtension(fileName)}", bytes);

            fileModel.Key = $@"{fileModel.Id}{Path.GetExtension(fileName)}";
            fileModel.Url = $@"/app/ged/{fileModel.Key}";
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
            File.Delete(fileModel.Url);
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
