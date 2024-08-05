using Utils.Shared;
using System;

namespace FileManagement.DTOs
{
    public class PFImportFileHistoryDTO : BaseDTO
    {
        public int ProfileId { get; set; }

        public bool IsProcessed { get; set; }

        public string Description { get; set; }

        public int FormatFileId { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public DateTime DateReceived { get; set; }

        public string DateReceivedString => DateReceived == DateTime.MinValue ? "" : DateReceived.ToString("MM/dd/yyyy");

        public DateTime? DateProcessed { get; set; }

        public string DateProcessedString => DateProcessed == null ? "" : DateProcessed?.ToString("MM/dd/yyyy");

        public int MemberCount { get; set; }
        public int RecordsCount { get; set; }
        public string FormatDescription { get; set; }
        public string FormatFileTypeDescription { get; set; }
        public TradingPartnerProfileImportExportStatus Status { get; set; }
        public string StatusName { get; set; }

    }

}
