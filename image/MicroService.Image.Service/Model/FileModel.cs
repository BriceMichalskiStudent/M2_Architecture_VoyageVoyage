namespace MicroService.Image.Service.Model
{
    using MicroService.Core.Mongodb;

    /// <summary>
    /// Modele de fichier.
    /// </summary>
    public class FileModel : Modelbase
    {
        /// <summary>
        /// Obtient ou définit le nom du fichier.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Obtient ou définit l'url du fichier.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Obtient ou définit la clef du fichier.
        /// </summary>
        public string Key { get; set; }
    }
}
