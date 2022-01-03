using CustomFileProviders.AppData;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.Extensions.Primitives;

namespace CustomFileProviders.AppLib.FileProviders
{
    public class DatabaseFileProvider : IFileProvider
    {
        private readonly AppDbContext _context;
        public DatabaseFileProvider(IServiceProvider serviceProvider)
        {
            _context = new AppDbContext();
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return NotFoundDirectoryContents.Singleton;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var result = new DatabaseFileInfo(subpath, _context);

            // NotFoundFileInfo tells the view engine to try another provider, or another view location
            return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return new DatabaseChangeToken(filter, _context);
        }
    }
}
