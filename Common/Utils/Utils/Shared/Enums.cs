using System.ComponentModel.DataAnnotations;

namespace Utils.Shared
{
    #region Eligibility
    public enum TradingPartnerProfileImportExportStatus
    {
        [Display(Name = "Importing")]
        Importing = 1,
        [Display(Name = "Imported")]
        Imported = 2,
        [Display(Name = "Processing")]
        Processing = 3,
        [Display(Name = "Processed")]
        Processed = 4,
        [Display(Name = "Error Importing")]
        ErrorImporting = 5,
        [Display(Name = "Error Processing")]
        ErrorProcessing
    }

    public enum EligibilityImportProcessingStatus
    {
        ReadyToProcess = 1,
        ProcessingComplete,
        NoFurtherProcessingAllowed,
        Error,
        MemberIssue
    }

    public enum EligibilityImportRecordStatus
    {
        New = 1,
        Update,
        Terminate,
        Reinstatement,
        AuditOrCompare,
        Error
    }

    public enum EligibilityImportErrorType
    {
        [Display(Name = "Null")]
        Null,
        [Display(Name = "Member Not Found")]
        MemberNotFound,
        [Display(Name = "PCP Not Found")]
        PCPNotFound,
        [Display(Name = "Benefit Not Found")]
        BenefitNotFound,
        [Display(Name = "Plan Not Found")]
        PlanNotFound,
        [Display(Name = "Group Not Found")]
        GroupNotFound,
        [Display(Name = "Payer Not Found")]
        PayerNotFound,
        [Display(Name = "Member Effective Date Greater Than Expiration Date")]
        MemberEffectiveDateGreaterThanExpirationDate,
        [Display(Name = "Benefit Effective Date Greater Than Expiration Date")]
        BenefitEffectiveDateGreaterExpirationDate,
        [Display(Name = "PCP Effective Date Greater Than Expiration Date")]
        PCPEffectiveDateGreaterThanExpirationDate,
        [Display(Name = "Address Type Not Found")]
        AddressTypeNotFound,
        [Display(Name = "Relation To Insured Not Found")]
        RelationToInsuredNotFound,
        [Display(Name = "Gender Not Found")]
        GenderNotFound,
        [Display(Name = "Prefix Not Found")]
        PrefixNotFound,
        [Display(Name = "Suffix Not Found")]
        SuffixNotFound,
        [Display(Name = "Language Not Found")]
        LanguageNotFound,
        [Display(Name = "Married Status Not Found")]
        MarriedStatusNotFound,
        [Display(Name = "Nationality Not Found")]
        NationalityNotFound,
        [Display(Name = "Race Not Found")]
        RaceNotFound,
        [Display(Name = "Employment Status Not Found")]
        EmploymentStatusNotFound,
        [Display(Name = "Citizenship Not Found")]
        CitizenshipNotFound,
        [Display(Name = "Contact Type Not Found")]
        ContactTypeNotFound,
        [Display(Name = "Server Error")]
        ServerError,
        [Display(Name = "Member must have at least 3 identifiers: subscriber id, dependent sequence, and payer")]
        RequiredMemberIdentifiers,
        [Display(Name = "Invalid email")]
        MemberContactsInvalidEmail,
        [Display(Name = "Invalid phone")]
        MemberContactsInvalidPhone,
        [Display(Name = "Dates overlap with other records")]
        ErrorDatesOverlapping,
        [Display(Name = "Expiration date must be more than Effective date")]
        ErrorInvalidEffective,
        [Display(Name = "Dates out of the valid date range")]
        ErrorInvalidDatesToAssign,
        [Display(Name = "Member identifiers already exist")]
        MemberIdentifierExist,
        [Display(Name = "Plan dates overlaps")]
        PlanOverlaps,
        [Display(Name = "Provider dates overlaps")]
        ProviderOverlaps,
        [Display(Name = "Provider out of date range")]
        ProviderOutOfDateRange,
        [Display(Name = "Member identifiers overlaps")]
        IdentifierOverlaps,
        [Display(Name = "Member contacts overlaps")]
        ContactsOverlaps,
        [Display(Name = "Member address overlaps")]
        AddressOverlaps,
        [Display(Name = "Member issue")]
        MemberIssue,
        [Display(Name = "Member insurance already exist")]
        MemberInsuranceAlreadyExist
    }

    public enum EligibilityImportProcessingMode
    {
        [Display(Name = "Process unprocessed entries")]
        ProcessUnprocessedEntries = 1,
        [Display(Name = "Reprocess error entries")]
        ReprocessErrorEntries,
        [Display(Name = "Reprocess non error entries")]
        ReprocessNonErrorEntries,
        [Display(Name = "Reprocess all entries")]
        ReprocessingAllEntries
    }
    #endregion

    #region Claim
    public enum ClaimProcessingStatusTypeEnum
    {
        [Display(Name = "Received - Unassigned")]
        ReceivedUnassigned = 1,
        [Display(Name = "Received - Assigned")]
        ReceivedAssigned = 2,
        [Display(Name = "Suspended")]
        Suspended = 3,
        [Display(Name = "Ready to Pay")]
        ReadytoPay = 4,
        [Display(Name = "Paid")]
        Paid = 5,
        [Display(Name = "Rejected")]
        Rejected = 6,
        [Display(Name = "Voided")]
        Voided = 7,
        [Display(Name = "Submitted")]
        Submitted = 8,
        [Display(Name = "Acknowledged (999)")]
        Acknowledged = 9,
        [Display(Name = "Rejected (999)")]
        Rejected999 = 10,
        [Display(Name = "No Response / Missing")]
        NoResponseMissing = 11,
        [Display(Name = "Processed (835 match)")]
        Processed = 12,
        [Display(Name = "Sent To Accounting")]
        SentToAccounting = 13,
        [Display(Name = "Sent To Payer")]
        SentToPayer = 14,
        [Display(Name = "Review Voids")]
        ReviewVoids = 15,
        [Display(Name = "Review Replacements")]
        ReviewReplacements = 16,
        [Display(Name = "Voided Claims")]
        VoidedClaims = 17,
        [Display(Name = "Replacement Claims")]
        ReplacementClaims = 18,
        [Display(Name = "UM Suspended")]
        UMSuspended = 19


    };
    public enum ClaimPendGroupTypeEnum
    {
        [Display(Name = "Configuration - Benefit/Group")]
        BenefitGroup = 1,
        [Display(Name = "Configuration - Provider")]
        Provider = 2,
        [Display(Name = "Configuration - System Code")]
        SystemCode = 3,
        [Display(Name = "Member Issue")]
        MemberIssue = 4,
        [Display(Name = "Paper Claim")]
        PaperClaim = 5,
        [Display(Name = "Special Handing")]
        SpecialHanding = 6,
        [Display(Name = "837 Import Issue")]
        ImportIssue837 = 7,
        [Display(Name = "837 Re-Issued Claim")]
        ReIssuedClaim837 = 8,
        [Display(Name = "837 Vendor/Billing Provider Match Issue")]
        VendorBillingProvMatchIssue837 = 9,
        [Display(Name = "837 Subscriber Match Issue")]
        SubscriberMatchIssue837 = 10,
        [Display(Name = "837 Patient Match Issue")]
        PatientMatchIssue837 = 11,
        [Display(Name = "837 Payer Match Issue")]
        PayerMatchIssue837 = 12,
        [Display(Name = "837 Provider Match Issue")]
        ProviderMatchIssue837 = 13,
        [Display(Name = "837 Location Match Issue")]
        LocationMatchIssue837 = 14,
        [Display(Name = "837 Diagnosis Match Issue")]
        DiagnosisMatchIssue837 = 15,
        [Display(Name = "837 Procedure Match Issue")]
        ProcedureMatchIssue837 = 16,
        [Display(Name = "837 Modifier Match Issue")]
        ModifierMatchIssue837 = 17,
        [Display(Name = "837 Place Of Service Match Issue")]
        PlaceOfServiceMatchIssue837 = 18,
        [Display(Name = "Configuration – UM")]
        ConfigurationUM = 19
    };
    public enum ClaimPaymentsStatusEnum
    {
        [Display(Name = "Vendor Payments Submitted")]
        VendorPaymentsSubmitted = 1,
        [Display(Name = "Vendor Payments Posted")]
        VendorPaymentsPosted = 2,
        [Display(Name = "Vendor Payments Not Posted")]
        VendorPaymentsNotPosted = 3,
        [Display(Name = "Payer Bills Submitted (837)")]
        PayerBillsSubmitted837 = 4,
        [Display(Name = "Payer Acknowledgements Received (277CA)")]
        PayerAcknowledgementsReceived277CA = 5,
        [Display(Name = "Payer Remittance Advices Received (835)")]
        PayerRemittanceAdvicesReceived835 = 6,
        [Display(Name = "Paid in Full")]
        PaidInFull = 7,
        [Display(Name = "Partial/Over Payments")]
        PartialOverPayments = 8,
        [Display(Name = "Denied")]
        Denied = 9,
        [Display(Name = "Claims Awaiting Acknowledgements")]
        ClaimsAwaitingAcknowledgements = 10,
        [Display(Name = "Claims Awaiting Remittance Advices")]
        ClaimsAwaitingRemittanceAdvices = 11,

    };
    public enum DmeRentalFrequencyTypeEnum
    {
        [Display(Name = "Weekly")]
        Weekly = 1,
        [Display(Name = "Monthly")]
        Monthly = 4,
        [Display(Name = "Daily")]
        Daily = 6,
    };
    #endregion

    #region Prior Authorization
    public enum PriorAuthorizationEntityTypeEnum
    {
        [Display(Name = "Provider")]
        Provider = 1,
        [Display(Name = "Vendor")]
        Vendor = 2,
        [Display(Name = "Location")]
        Location = 3,
    };

    public enum PriorAuthorizationStatusEnum
    {
        [Display(Name = "Rcvd Unassigned")]
        RcvdUnassigned = 1,
        [Display(Name = "Approved")]
        Approved = 2,
        [Display(Name = "Denied")]
        Denied = 3,
        [Display(Name = "Pending")]
        Pending = 4,
        [Display(Name = "Voided")]
        Voided = 5,
        [Display(Name = "Duplicate")]
        Duplicate = 6,
        [Display(Name = "Medical Review")]
        MedicalReview = 7,
        [Display(Name = "Incomplete")]
        Incomplete = 8,
        [Display(Name = "Additional Info")]
        AdditionalInfo = 9,
    };

    public enum PriorAuthorizationTypeEnum
    {
        [Display(Name = "Pre-cert")]
        Precert = 1,
        [Display(Name = "Referral")]
        Referral = 2,

    };

    #endregion

}
