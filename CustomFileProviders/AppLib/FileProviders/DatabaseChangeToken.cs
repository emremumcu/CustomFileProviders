using CustomFileProviders.AppData;
using CustomFileProviders.AppData.Entities;
using Microsoft.Extensions.Primitives;

namespace CustomFileProviders.AppLib.FileProviders
{
    public class DatabaseChangeToken : IChangeToken
    {
        private AppDbContext _context;
        private string _location;

        public DatabaseChangeToken(string location, AppDbContext context)
        {
            _context = context;
            _location = location;
        }
        public bool ActiveChangeCallbacks => false;

        public bool HasChanged
        {
            get
            {
                // If only some columns are required from the entity:
                // var posts = context.Posts.Where(p => p.Tags == "<sql-server>").Select(p => new { p.Id, p.Title });

                ViewEntity? view = _context.DbViews
                    .Where(e => e.Location == _location)
                    .Select(i => new ViewEntity() { LastModified = i.LastModified, LastRequested = i.LastRequested })
                    .FirstOrDefault();

                if (view != null && view.LastRequested != null && view.LastRequested < view.LastModified)
                    return true;
                else
                    return false;
            }
        }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;
    }

    internal class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Instance { get; } = new EmptyDisposable();
        private EmptyDisposable() { }
        public void Dispose() { }
    }
}
