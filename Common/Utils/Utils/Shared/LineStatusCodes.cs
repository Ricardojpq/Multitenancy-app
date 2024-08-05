using System.Collections.ObjectModel;

namespace Utils.Shared
{
    public static class LineStatusCodes
    {
        public const string Open = "OP";
        public const string Voided = "VD";
        public const string Informational = "IN";
        public const string Pended = "PD";
        public const string Denied = "DN";
        public const string Errored = "ER";
        public const string Closed = "CL";

        public static readonly ReadOnlyDictionary<string, string> StatusName = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>{
            { LineStatusCodes.Open,nameof(LineStatusCodes.Open) },
            { LineStatusCodes.Voided,nameof(LineStatusCodes.Voided) },
            { LineStatusCodes.Informational,nameof(LineStatusCodes.Informational) },
            { LineStatusCodes.Pended,nameof(LineStatusCodes.Pended) },
            { LineStatusCodes.Denied,nameof(LineStatusCodes.Denied) },
            { LineStatusCodes.Errored,nameof(LineStatusCodes.Errored) },
            { LineStatusCodes.Closed,nameof(LineStatusCodes.Closed) },
        });

        public static readonly ReadOnlyDictionary<string, string> StatusDescriptionLine = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>{
            { LineStatusCodes.Open, "Open Line"},
            { LineStatusCodes.Voided, "Void Line"},
            { LineStatusCodes.Informational, "Re-Open Line"},
            { LineStatusCodes.Pended, "Pend Line"},
            { LineStatusCodes.Denied, "Deny Line"},
            { LineStatusCodes.Errored, "Error Line"},
            { LineStatusCodes.Closed, "Close Line"}
        });

        public static readonly ReadOnlyDictionary<string, string> StatusColorsInfo = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string> {
            { LineStatusCodes.Open, "ok"},
            { LineStatusCodes.Voided, "void"},
            { LineStatusCodes.Informational, "info"},
            { LineStatusCodes.Pended, "warning"},
            { LineStatusCodes.Denied, "danger"},
            { LineStatusCodes.Errored, "danger"},
            { LineStatusCodes.Closed, "void"}
        });

    }

}
