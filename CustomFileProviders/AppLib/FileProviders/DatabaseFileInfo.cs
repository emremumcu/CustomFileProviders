using CustomFileProviders.AppData;
using CustomFileProviders.AppData.Entities;
using Microsoft.Extensions.FileProviders;
using System.Text;

namespace CustomFileProviders.AppLib.FileProviders
{
    public class DatabaseFileInfo : IFileInfo
    {
        private ViewEntity? _view;

        public DatabaseFileInfo(string location, AppDbContext context)
        {
            _view = context.DbViews.Where(v => v.Location == location).FirstOrDefault();
        }

        public bool Exists => (_view != null);

        public bool IsDirectory => false;

        public DateTimeOffset LastModified => Convert.ToDateTime(_view?.LastModified);

        public long Length
        {
            get
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(_view?.Content ?? String.Empty)))
                {
                    return stream.Length;
                }
            }
        }

        public string Name => Path.GetFileName(_view?.Location ?? String.Empty);

        public string PhysicalPath => String.Empty;

        public Stream CreateReadStream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(_view?.Content ?? String.Empty));
        }
    }
}
