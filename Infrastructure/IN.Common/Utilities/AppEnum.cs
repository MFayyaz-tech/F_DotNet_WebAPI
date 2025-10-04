using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IN.Common.Utilities
{
	public enum MessageStatus
	{
		[EnumMember]
		Success = 1,
		[EnumMember]
		Failed = 2
	}

	public enum EnRole
	{
		[EnumMember]
		Admin = 1,
		[EnumMember]
		Agent = 2,
		[EnumMember]
		Customer = 3,
		[EnumMember]
		Beneficiary = 4
	}

    public enum JobStatus
    {
        [EnumMember]
        Open,
        [EnumMember]
        Closed,
        [EnumMember]
        Rewarded,
        [EnumMember]
        InProgress,
        [EnumMember]
        Delivered,
        [EnumMember]
        Completed,
        [EnumMember]
        Failed,
        [EnumMember]
        Cancelled
    }

    public enum DurationType
    {
        OneTime,
        SpecificCondition,
        DateSpecific
    }
    public enum JobBidderType
    {
        AnyOne,
        Licensed,
        ProfessionalCompany
    }

    public enum TrainingStatus
    {
        UnPublished,
        Active,
        Completed
    }

    public enum EnrollmentStatus
    {
        Pending,
        Approved,
        Rejected,
        Enrolled,
        Completed
    }

    public static class EnRoleStr
	{
		public static string Admin = "Admin";
		public static string Agent = "Agent";
		public static string Customer = "Customer";
		public static string Beneficiary = "Beneficiary";
	}
}
