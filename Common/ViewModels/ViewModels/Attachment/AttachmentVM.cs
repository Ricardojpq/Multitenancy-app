using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ViewModels.Attachment
{
    public class AttachmentVM: BaseViewModel
    {
        public int EntityId { get; set; }
        public string? EntityTypeId { get; set; }
        public string? AttachmentCategoryId { get; set; }
        [MaxLength(80)]
        public string? Identifier { get; set; }
        [MaxLength(80)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile File { get; set; }
        public string? Base64 { get; set; }
        public string? Path { get; set; }
        public string? PathForViewFile { get; set; }
        public string? FileExtension { get; set; }
        public long? Size { get; set; }
        public string? MimeType { get; set; }
        public AttachmentVM()
        {
        }

        public void RandomizeIdentifier()
        {
            Identifier = File != null ? Guid.NewGuid() + "." + File.FileName.Split(".").Last() : "";
        }
    }
}
