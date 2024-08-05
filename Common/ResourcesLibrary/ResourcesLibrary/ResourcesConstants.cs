using System;
using System.Collections.Generic;
using System.Text;

namespace ResourcesLibrary
{
    public class ResourcesConstants
    {
        #region ResourcesName
        public const string MemberRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Member";
        public const string ClaimRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Claim";
        public const string VendorRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Vendor";
        public const string VendorContractRsx = "Alivi.Arizona.ResourcesLibrary.Resources.VendorContract";
        public const string VendorContractFeeScheduleRsx = "Alivi.Arizona.ResourcesLibrary.Resources.VendorContractFeeSchedule";
        public const string LocationRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Location";
        public const string CommonRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Common";
        public const string AttachmentRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Attachment";
        public const string UserRsx = "Alivi.Arizona.ResourcesLibrary.Resources.User";
        public const string TradingPartnerRsx = "Alivi.Arizona.ResourcesLibrary.Resources.TradingPartner";
        public const string PayerContractRsx = "Alivi.Arizona.ResourcesLibrary.Resources.PayerContract";
        public const string PayerRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Payer";
        public const string PayerCrossReferenceRsx = "Alivi.Arizona.ResourcesLibrary.Resources.PayerCrossReference";
        public const string PlanBenefitRsx = "Alivi.Arizona.ResourcesLibrary.Resources.PlanBenefit";
        public const string PlanGroupContractRsx = "Alivi.Arizona.ResourcesLibrary.Resources.PlanGroupContract";
        public const string FeeScheduleDefinitionRsx = "Alivi.Arizona.ResourcesLibrary.Resources.FeeScheduleDefinition";
        public const string FeeScheduleRegionRsx = "Alivi.Arizona.ResourcesLibrary.Resources.FeeScheduleRegion";
        public const string FeeSchedulePriceRsx = "Alivi.Arizona.ResourcesLibrary.Resources.FeeSchedulePrice";
        public const string ServiceCategoryRsx = "Alivi.Arizona.ResourcesLibrary.Resources.ServiceCategory";
        public const string ProprietaryFileRsx = "Alivi.Arizona.ResourcesLibrary.Resources.ProprietaryFile";
        public const string ProviderRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Provider";
        public const string ProviderHospitalRsx = "Alivi.Arizona.ResourcesLibrary.Resources.ProviderHospital";
        public const string ProviderPanelRsx = "Alivi.Arizona.ResourcesLibrary.Resources.ProviderPanel";
        public const string ProviderVendorRsx = "Alivi.Arizona.ResourcesLibrary.Resources.ProviderVendor";
        public const string EligibilityImportProfileRsx = "Alivi.Arizona.ResourcesLibrary.Resources.EligibilityImportProfile";
        public const string PaymentProcessingProfileRsx = "Alivi.Arizona.ResourcesLibrary.Resources.PaymentProcessingProfile";
        public const string PaymentProcessingRunRsx = "Alivi.Arizona.ResourcesLibrary.Resources.PaymentProcessingRun";
        public const string Hipaa837ExportRsx = "Alivi.Arizona.ResourcesLibrary.Resources.Hipaa837Export";

        #endregion

        #region ResourcesKeysName

        public const string RegisterAlreadyExists = "RegisterAlreadyExists";

        public const string SuccessDeletedMember = "SuccessDeleteMember";
        public const string SuccessSaveMember = "SuccessSaveMember";
        public const string RequiredMemberIdentifiers = "RequiredMemberIdentifiers";
        public const string RequiredMemberContact = "RequiredMemberContact";
        public const string RequiredMemberAddress = "RequiredMemberAddress";
        public const string MemberContactsInvalidDates = "MemberContactsInvalidDates";
        public const string MemberContactsInvalidEmail = "MemberContactsInvalidEmail";
        public const string MemberContactsInvalidPhone = "MemberContactsInvalidPhone";
        public const string MemberAddressInvalidDates = "MemberAddressInvalidDates";
        public const string MemberIdentifiersInvalidDates = "MemberIdentifiersInvalidDates";
        public const string MemberIdentifierExist = "MemberIdentifierExist";
        public const string SuccessDeleteMemberProvider = "SuccessDeleteMemberProvider";
        public const string SuccessSaveMemberProvider = "SuccessSaveMemberProvider";
        public const string SuccessDeleteMemberPlanGroup = "SuccessDeleteMemberPlanGroup";
        public const string SuccessSaveMemberPlanGroup = "SuccessSaveMemberPlanGroup";
        public const string MemberProviderAlreadyExists = "MemberProviderAlreadyExists";
        public const string MemberPlanGroupInvalidDates = "MemberPlanGroupInvalidDates";
        public const string MemberPlanGroupAlreadyExists = "MemberPlanGroupAlreadyExists";
        public const string MemberDOBInvalidDate = "MemberDOBinvalidDate";
        public const string MemberInvalidZip = "MemberInvalidZip";   
        public const string ErrorDuplicatedMemberIdentity = "ErrorDuplicatedMemberIdentity";   

        public const string InvalidVendorNumber = "InvalidVendorNumber";
        public const string SuccessDeletedVendor = "SuccessDeletedVendor";
        public const string SuccessDeletedLocation = "SuccessDeletedLocation";
        public const string SuccessSavedVendor = "SuccessSavedVendor";

        public const string ErrorDatesOverlapping = "ErrorDatesOverlapping";
        public const string ErrorInvalidDatesToAssign = "ErrorInvalidDatesToAssign";
        public const string ErrorOnDbUpdateSaving = "ErrorOnDbUpdateSaving";
        public const string ErrorSavingAttachment = "ErrorSavingAttachment";
        public const string ErrorEntityTypeNotFound = "ErrorEntityTypeNotFound";
        public const string MsjErrorSendMail = "msjErrorSendMail";
        public const string ErrorInvalidEffective = "ErrorInvalidEffective";
        public const string InvalidPhoneExtension = "InvalidPhoneExtension";
        public const string GeneralErrorMsg = "GeneralError";
        public const string ErrorBackingUpFile = "ErrorBackingUpFile";

        public const string ErrorContractFeeSheduleSamePriority = "ErrorContractFeeSheduleSamePriority";
        public const string ErrorContractFeeScheduleExist = "ErrorContractFeeScheduleExist";

        public const string ErrorPlanNameAlreadyExists = "ErrorPlanNameAlreadyExists";
        public const string ErrorPlanGroupNameAlreadyExists = "ErrorPlanGroupNameAlreadyExists";
        public const string ErrorPlanOrGroupIsNotActive = "PlanOrGroupIsNotActive";
        public const string ErrorPlanIsNotActive = "PlanIsNotActive";
        public const string ErrorGroupIsNotActive = "GroupIsNotActive";

        public const string ErrorServiceCategoryNameAlreadyExists = "ErrorServiceCategoryNameAlreadyExists";
        public const string ErrorServiceCategoryGroupNameAlreadyExists = "ErrorServiceCategoryGroupNameAlreadyExists";
        public const string SuccessDeletedFeeScheduleDefinition = "SuccessDeletedFeeScheduleDefinition";
        public const string SuccessDeletedFeeSchedulePrice = "SuccessDeletedFeeSchedulePrice";
        public const string SuccessDeletedVendorContract = "SuccessDeletedVendorContract";
        public const string SuccessDeletedVendorContractFeeSchedule = "SuccessDeletedVendorContractFeeSchedule";
        public const string DeletedSuccessfully = "DeletedSuccessfully";
        public const string ServiceCategoryWasDeletedSuccessfully = "ServiceCategoryWasDeletedSuccessfully";
        public const string ServiceCategoryGroupWasDeletedSuccessfully = "ServiceCategoryGroupWasDeletedSuccessfully";
        public const string SuccessDeletePFProfile = "SuccessDeletePFProfile";
        public const string SuccessDeletePFFormat = "SuccessDeletePFFormat";
        public const string ValidateMessageAssignPlan = "ValidateMessageAssignPlan";
        public const string ValidateMessageAssignGroup = "ValidateMessageAssignGroup";

        public const string DeletedSuccessfullyGroup = "DeletedSuccessfullyGroup";
        public const string SuccessAssignPlan = "SuccessAssignPlan";
        public const string SuccessSaveGroup = "SuccessSaveGroup";
        public const string SuccessAssignGroup = "SuccessAssignGroup";
        public const string DeletedSuccessfullyPlan = "DeletedSuccessfullyPlan";
        public const string DeletedSuccessfullyPlanGroup = "DeletedSuccessfullyPlanGroup";
        public const string SuccessSavePlanGroup = "SuccessSavePlanGroup";
        public const string ValidateMessageAssignPlanGroup = "ValidateMessageAssignPlanGroup";
        public const string ValidateMessageGroup = "ValidateMessageGroup";
        public const string PlanGroupBenefitInvalidDates = "PlanGroupBenefitInvalidDates";
        public const string SuccessDeletePlanGroupBenefit = "SuccessDeletePlanGroupBenefit";
        public const string ValidateRequiredLimitFields = "ValidateRequiredLimitFields";

        public const string SuccessIdentifier = "SuccessIdentifier";
        public const string ErrorIdentifierAlreadyExists = "ErrorIdentifierAlreadyExists";
        public const string DeletedSuccessfullyIdentifier = "DeletedSuccessfullyIdentifier";
        public const string ErrorGroupNumberAlreadyExists = "ErrorGroupNumberAlreadyExists";

        public const string ErrorSavePayer = "ErrorSavePayer";
        public const string SuccessSavePayer = "SuccessSavePayer";
        public const string SuccessDeletePayer = "SuccessDeletePayer";

        public const string SuccessSavePayerContract = "SuccessSavePayerContract";
        public const string SuccessDeletePayerContract = "SuccessDeletePayerContract";
        public const string ErrorSavePayerContractContacts = "ErrorSavePayerContractContacts";
        public const string ErrorSavePayerContractDelegations = "ErrorSavePayerContractDelegations";
        public const string PayerContractInvalidDates = "PayerContractInvalidDates";

        public const string SuccessDeletePlan = "SuccessDeletePlan";
        public const string SuccessSavePlan = "SuccessSavePlan";
        public const string ErrorSavePlan = "ErrorSavePlan";

        public const string SuccessSaveTradingPartner = "SuccessSaveTradingPartner";
        public const string SuccessDeleteTradingPartner = "SuccessDeleteTradingPartner";
        public const string SuccesDeleteTPProfileGroup = "SuccesDeleteTPProfileGroup";
        public const string SuccesSaveTPProfileGroup = "SuccesSaveTPProfileGroup";
        public const string CommunicationTypeRequired = "CommunicationTypeRequired";
        public const string TradingPartnerNotFound = "TradingPartnerNotFound";
        public const string TPPFunctionalGroupNotFound = "TPPFunctionalGroupNotFound";
        public const string PrevImportedFile = "PrevImportedFile";
        public const string TPGroupCommunicationInvalidDates = "TPGroupCommunicationInvalidDates";
        public const string EligibilityRunning = "EligibilityRunning";

        public const string SuccessDeleteProvider = "SuccessDeleteProvider";
        public const string SuccessDeleteProviderHospital = "SuccessDeleteProviderHospital";
        public const string SuccessDeleteProviderPanel = "SuccessDeleteProviderPanel";
        public const string SuccessDeleteProviderSpecialtyAssignment = "SuccessDeleteProviderSpecialtyAssignment";
        public const string SuccessSaveProviderSpecialtyAssignment = "SuccessSaveProviderSpecialtyAssignment";
        public const string ErrorProviderSpecialtyAssignmentOverlapping = "ErrorProviderSpecialtyAssignmentOverlapping";
        public const string ErrorProviderSpecialtyAssignmentIsPrimaryOverlapping = "ErrorProviderSpecialtyAssignmentIsPrimaryOverlapping";

        public const string SuccessDeleteEligibilityImportProfile = "SuccessDeleteEligibilityImportProfile";
        public const string SuccessSaveEligibilityImportProfile = "SuccessSaveEligibilityImportProfile";
        public const string ErrorSaveEligibilityImportProfile = "ErrorSaveEligibilityImportProfile";
        public const string ErrorEligibilityDescriptionAlreadyExists = "ErrorEligibilityDescriptionAlreadyExists";
        public const string ErrorEligibilityImportProfileOverlapping = "ErrorEligibilityImportProfileOverlapping";
        public const string RequiredDependentSequenceRulesValidation = "RequiredDependentSequenceRulesValidation";

        public const string SuccessDeletedPaymentProcessingProfile = "SuccessDeletedPaymentProcessingProfile";
        public const string SuccessSavedPaymentProcessingProfile = "SuccessSavedPaymentProcessingProfile";

        public const string SuccessDeletedPaymentProcessingRun = "SuccessDeletedPaymentProcessingRun";
        public const string SuccessSavedPaymentProcessingRun = "SuccessSavedPaymentProcessingRun";

        public const string SuccessSavedPPRunClaimOrigin = "SuccessSavedPPRunClaimOrigin";
        public const string SuccessSavedPPRunClaimStatus = "SuccessSavedPPRunClaimStatus";
        public const string SuccessSavedPPRunClaimStyle = "SuccessSavedPPRunClaimStyle";
        public const string SuccessSavedPPRunPayer = "SuccessSavedPPRunPayer";
        public const string SuccessSavedPPRunSpecialty = "SuccessSavedPPRunSpecialty";
        public const string SuccessSavedPPRunVendor = "SuccessSavedPPRunVendor";
        public const string SuccessDeletedPPRunClaimOrigin = "SuccessDeletedPPRunClaimOrigin";
        public const string SuccessDeletedPPRunClaimStatus = "SuccessDeletedPPRunClaimStatus";
        public const string SuccessDeletedPPRunClaimStyle = "SuccessDeletedPaymentProcessingRun";
        public const string SuccessDeletedPPRunPayer = "SuccessDeletedPPRunPayer";
        public const string SuccessDeletedPPRunSpecialty = "SuccessDeletedPPRunSpecialty";
        public const string SuccessDeletedPPRunVendor = "SuccessDeletedPPRunVendor";

        public const string PPRunErrorSaveClaimOrigin = "ErrorSaveClaimOrigin";
        public const string PPRunErrorSavingClaimStatus = "ErrorSavingClaimStatus";
        public const string PPRunErrorSavingClaimStyles = "ErrorSavingClaimStyles";
        public const string PPRunErrorSavingPayers = "ErrorSavingPayers";
        public const string PPRunErrorSavingSpecialties = "ErrorSavingSpecialties";
        public const string PPRunErrorSavingVendors = "ErrorSavingVendors";
        public const string PPRunErrorSaveClaim = "ErrorSaveClaim";
        public const string PPRunValidateClaimOriginToExclude = "ValidateClaimOriginToExclude";
        public const string PPRunValidateClaimStatusToExclude = "ValidateClaimStatusToExclude";
        public const string PPRunValidateClaimStyleToExclude = "ValidateClaimStyleToExclude";
        public const string PPRunValidatePayerToExclude = "ValidatePayerToExclude";
        public const string PPRunValidateSpecialtyToExclude = "ValidateSpecialtyToExclude";
        public const string PPRunValidateVendorAndPayerExists = "ValidateVendorAndPayerExists";
        public const string PPRunValidateVendorToExclude = "ValidateVendorToExclude";
        public const string ValidationBackup = "ValidationBackup";
        public const string ValidationUncheckSetupCompleted = "ValidationUncheckSetupCompleted";
        public const string BackOutCompleted = "BackOutCompleted";
        public const string NotFoundClaimToExtract = "NotFoundClaimToExtract";
        public const string ErrorLoadingRunInformation = "ErrorLoadingRunInformation";
        public const string ErrorLoadingProfileInformation = "ErrorLoadingProfileInformation";
        public const string ErrorProcessingStep = "ErrorProcessingStep";
        public const string PaymentProcessingRunExecute = "PaymentProcessingRunExecute";
        public const string FileImportRequired = "FileImportRequired";

        public const string PaymentProcessingProfileClaimStyleExist = "ClaimStyleExist";
        public const string PaymentProcessingProfileClaimOriginExist = "ClaimOriginExist";
        public const string PaymentProcessingProfileClaimStatusExist = "ClaimStatusExist";
        public const string PaymentProcessingProfilePayerExist = "PayerExist";
        public const string PaymentProcessingProfileVendorExist = "VendorExist";
        public const string PaymentProcessingProfileSpecialtyExist = "SpecialtyExist";

        public const string SuccessDeletedPaymentProcessingProfileClaimStyle = "SuccessDeletedClaimStyle";
        public const string SuccessDeletedPaymentProcessingProfileClaimOrigin = "SuccessDeletedClaimOrigin";
        public const string SuccessDeletedPaymentProcessingProfileClaimStatus = "SuccessDeletedClaimStatus";
        public const string SuccessDeletedPaymentProcessingProfilePayer = "SuccessDeletedPayer";
        public const string SuccessDeletedPaymentProcessingProfileVendor = "SuccessDeletedVendor";
        public const string SuccessDeletedPaymentProcessingProfileSpecialty = "SuccessDeletedSpecialty";

        public const string PaymentProcessingProfileErrorSaveClaimOrigin = "ErrorSaveClaimOrigin";
        public const string PaymentProcessingProfileErrorSavingClaimStatus = "ErrorSavingClaimStatus";
        public const string PaymentProcessingProfileErrorSavingClaimStyles = "ErrorSavingClaimStyles";
        public const string PaymentProcessingProfileErrorSavingPayers = "ErrorSavingPayers";
        public const string PaymentProcessingProfileErrorSavingSpecialties = "ErrorSavingSpecialties";
        public const string PaymentProcessingProfileErrorSavingVendors = "ErrorSavingVendors";
        public const string PaymentProcessingProfileValidateClaimOriginToExclude = "ValidateClaimOriginToExclude";
        public const string PaymentProcessingProfileValidateClaimStatusToExclude = "ValidateClaimStatusToExclude";
        public const string PaymentProcessingProfileValidateClaimStyleToExclude = "ValidateClaimStyleToExclude";
        public const string PaymentProcessingProfileValidatePayerToExclude = "ValidatePayerToExclude";
        public const string PaymentProcessingProfileValidateSpecialtyToExclude = "ValidateSpecialtyToExclude";
        public const string PaymentProcessingProfileValidateVendorAndPayerExists = "ValidateVendorAndPayerExists";
        public const string PaymentProcessingProfileValidateVendorToExclude = "ValidateVendorToExclude";


        public const string PPPIncludeExcludeErrorNoRecords = "PPPIncludeExcludeErrorNoRecords";
        public const string PPPDescriptionExist = "PPPDescriptionExist";
        public const string ErrorMissingFile = "ErrorMissingFile";
        public const string ISANotMatches = "ISANotMatches";
        public const string ISAInvalid = "ISAInvalid";
        public const string GSInvalid = "GSInvalid";
        public const string ImportServiceNotFound = "ImportServiceNotFound";
        public const string FileImported = "FileImported";
        public const string TransactionNotMatches = "TransactionNotMatches";

        public const string ErrorProviderVendorOverlapping = "ErrorProviderVendorOverlapping";
        public const string SuccessDeletedProviderVendor = "SuccessDeletedProviderVendor";
        public const string SuccessSaveProviderVendor = "SuccessSaveProviderVendor";
        public const string ErrorProviderVendorLocationWithProviderVendorOverlapping = "ErrorProviderVendorLocationWithProviderVendorOverlapping";

        public const string SuccessDeletedProviderVendorLocation = "SuccessDeletedProviderVendorLocation";
        public const string SuccessSaveProviderVendorLocation = "SuccessSaveProviderVendorLocation";
        public const string ErrorProviderVendorLocationOverlapping = "ErrorProviderVendorLocationOverlapping";
        public const string SpecialtyNetworkEpic = "SpecialtyNetworkEpic";

        public const string ErrorDiferentDOS = "ErrorDiferentDOS";
        public const string ErrorFutureDays = "ErrorFutureDays";

        public const string ErrorPayerEffective = "ErrorPayerEffective";
        public const string ErrorMemberEffective = "ErrorMemberEffective";
        public const string ErrorProviderEffective = "ErrorProviderEffective";
        public const string ErrorVendorEffective = "ErrorVendorEffective";
        public const string ErrorMainEntitiesAssign = "ErrorMainEntitiesAssign";
        public const string ErrorDiagnosisCodesInvalid = "ErrorDiagnosisCodesInvalid";

        public const string ClaimSavedAndAdjudicated = "ClaimSavedAndAdjudicated";
        public const string ClaimSavedNoAdjudicated = "ClaimSavedNoAdjudicated";
        public const string ClaimSaved = "ClaimSaved";
        public const string ClaimInquiryErrorFile = "ClaimInquiryErrorFile";
        
        public const string ErrorSavePayerCrossReference = "ErrorSavePayerCrossReference";
        public const string SuccessSavePayerCrossReference = "SuccessSavePayerCrossReference";
        public const string SuccessDeletePayerCrossReference = "SuccessDeletePayerCrossReference";
        public const string ErrorPayerCrossReferenceWithPayerOverlapping = "ErrorPayerCrossReferenceWithPayerOverlapping";
        public const string ErrorPayerCrossReferenceOverlapping = "ErrorPayerCrossReferenceOverlapping";
        public const string ErrorPayerCrossReferenceExists = "ErrorPayerCrossReferenceExists";

        public const string SuccessSaveFeeScheduleRegion = "SuccessSaveFeeScheduleRegion";
        public const string SuccessDeletedFeeScheduleRegion = "SuccessDeletedFeeScheduleRegion";
        public const string HeaderToastRegionSave = "HeaderToastRegionSave";
        public const string HeaderToastRegionUpdate = "HeaderToastRegionUpdate";
        public const string SuccessUpdateFeeScheduleRegion = "SuccessUpdateFeeScheduleRegion";

        public const string SuccessDeletePlanGroupContract = "SuccessDeletePlanGroupContract";
        public const string SuccessDeletePlanGroupContractFeeSchedule = "SuccessDeletePlanGroupContractFeeSchedule";
        #region Hipaa837Export
        public const string SuccessSavedExportFilter = "SuccessSavedExportFilter";
        public const string SuccessDeletedExportFilter = "SuccessDeletedExportFilter";
        public const string SuccessExportedClaims = "SuccessExportedClaims";
        public const string ErrorNoClaimsFound = "ErrorNoClaimsFound";
        public const string ErrorOnSaveNoDates = "ErrorOnSaveNoDates";
        public const string ErrorFilterAlreadyExported = "ErrorFilterAlreadyExported";
        public const string ErrorFilterNotExported = "ErrorFilterNotExported";
        public const string SuccessBackoutExportFilter = "SuccessBackoutExportFilter";
        public const string SuccessExportedFile = "SuccessExportedFile";
        public const string ErrorBackoutClaimsIn277Or835 = "ErrorBackoutClaimsIn277Or835";
        #endregion Hipaa837Export
        #endregion
    }
}
