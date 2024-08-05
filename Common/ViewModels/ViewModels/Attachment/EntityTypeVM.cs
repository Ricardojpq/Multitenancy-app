using System.ComponentModel.DataAnnotations;

namespace ViewModels.Attachment
{
    public class EntityTypeVM
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        public int ApplicationId { get; set; }
        [MaxLength(80)]
        public string ApplicationName { get; set; }
    }
}
