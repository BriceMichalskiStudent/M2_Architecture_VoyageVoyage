namespace MicroService.Image.Service.Repository
{
    using MicroService.Core.Mongodb;
    using MicroService.Image.Service.Model;

    /// <summary>
    /// Repository des fichiers.
    /// </summary>
    public class FileRepository : MongoRepository<FileModel>
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="FileRepository"/>
        /// </summary>
        public FileRepository() : base("files")
        {
        }
    }
}
