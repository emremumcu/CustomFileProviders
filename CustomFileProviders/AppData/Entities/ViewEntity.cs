using Microsoft.EntityFrameworkCore;

namespace CustomFileProviders.AppData.Entities
{
    [Index(nameof(Location), IsUnique = true)]
    public class ViewEntity: BaseEntity
    {
        public string Location { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime? LastRequested { get; set; }

    }
}
