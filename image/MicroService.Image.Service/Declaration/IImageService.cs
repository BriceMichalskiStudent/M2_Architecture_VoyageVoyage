using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Image.Service.Declaration
{
    using MicroService.Image.Service.Model;

    /// <summary>
    /// Service de gestion des images.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Sauvegarde le fichier.
        /// </summary>
        /// <param name="bytes">Bytes du fichiers.</param>
        /// <param name="fileName">Nom du fichier.</param>
        /// <returns>Entite fichier.</returns>
        FileModel SaveFile(byte[] bytes, string fileName);

        /// <summary>
        /// Supprime le fichier.
        /// </summary>
        /// <param name="id">Identifiant du fichier.</param>
        void DeleteFile(string id);

        /// <summary>
        /// Met à jour le fichier.
        /// </summary>
        /// <param name="id">Identifiant du fichier.</param>
        /// <param name="bytes">Bytes du fichiers.</param>
        /// <param name="fileName">Nom du fichier.</param>
        /// <returns>Entite fichier.</returns>
        FileModel UpdateFile(string id, byte[] bytes, string fileName);

        /// <summary>
        /// Récupère le fichier.
        /// </summary>
        /// <param name="id">Identifiant du fichier.</param>
        /// <returns>Entite fichier.</returns>
        FileModel GetFile(string id);

        /// <summary>
        /// Liste les fichiers.
        /// </summary>
        /// <returns>Entites fichiers.</returns>
        List<FileModel> ListFile();
    }
}
