namespace Wlib.Core.Admin.Data.Domain.Entities
{
    public enum FileType
    {
        UnKnown = 0
    }

    public class FileEntity : ApplicationEntityBase, IAuditEntity
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public FileType FileType { get; set; }
    }
}
