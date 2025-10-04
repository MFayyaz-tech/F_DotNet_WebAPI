using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum Database
    {
        MAIN,
        SECONDARY
    }
	public enum TransactionResponseCode
	{
		Approved = 1,
		Declined = 2,
		Error = 3,
		HeldForReview = 4
	}
}
