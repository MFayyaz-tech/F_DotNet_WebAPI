use Figgers_Enterprise_dev
GO
IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_users')
BEGIN
CREATE TABLE [dbo].[fe_users](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar] (50) NULL,
	[email_address] [nvarchar](100) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[user_type] [nvarchar] (50) NOT NULL,
	[role_id] [bigint] NULL,
	[approval_status] [nvarchar] (50) NULL,
	[rejected_reason] [nvarchar] (50) NULL,
	[status] [tinyint] NULL,
	[reset_password_token] [nvarchar](255) NULL,
	[token_expiry_date] [datetime] NULL,
	[Reset_password_OTP] [nvarchar](10) NULL,
	[OTP_expiry_date] [datetime] NULL,
	[last_login_date] [datetime] NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_user_id] PRIMARY KEY NONCLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_customer')
BEGIN
CREATE TABLE [dbo].[fe_customer](
	[customer_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL UNIQUE,
	[first_name] [nvarchar](100) NOT NULL,
	[last_name] [nvarchar](100) NOT NULL,
	[phone] [nvarchar] (255) NULL,
	[address1] [nvarchar] (100) NULL,
	[city] [nvarchar] (50) NULL,
	[state] [nvarchar] (50) NULL,
	[zip_code] [nvarchar] (50) NULL,
	[country] [nvarchar] (50) NULL,
	[lat] [decimal] (6,2) NULL,
	[lng] [decimal] (6,2) NULL,
	[signature] [nvarchar] (50) NULL,
	[photo_path] [nvarchar](255) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_customer_id] PRIMARY KEY NONCLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[fe_customer] 
ADD CONSTRAINT [FK_fe_customer_user_id] 
FOREIGN KEY ([user_id]) 
REFERENCES [dbo].[fe_users]([user_id]);
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_agency')
BEGIN
CREATE TABLE [dbo].[fe_agency](
	[agency_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL UNIQUE,
	[company_name] [nvarchar](100) NOT NULL,
	[phone] [nvarchar] (255) NULL,
	[agency_site] [nvarchar](100) NOT NULL,
	[agency_support_email] [nvarchar](100) NULL,
	[agency_fax] [nvarchar] (100) NULL,
	[agency_profile] [nvarchar] (400) NULL,
	[agency_contact_person] [nvarchar] (100) NULL,
	[address1] [nvarchar] (100) NULL,
	[city] [nvarchar] (50) NULL,
	[state] [nvarchar] (50) NULL,
	[zip_code] [nvarchar] (50) NULL,
	[country] [nvarchar] (50) NULL,
	[lat] [decimal] (6,2) NULL,
	[lng] [decimal] (6,2) NULL,
	[signature] [nvarchar] (50) NULL,
	[photo_path] [nvarchar](255) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_agency_id] PRIMARY KEY NONCLUSTERED 
(
	[agency_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_agency] 
ADD CONSTRAINT [FK_fe_agency_user_id] 
FOREIGN KEY ([user_id]) 
REFERENCES [dbo].[fe_users] ([user_id]);
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_jobs')
BEGIN
CREATE TABLE [dbo].[fe_jobs](
	[job_id] [bigint] IDENTITY(1,1) NOT NULL,
	[customer_id] [bigint] NOT NULL,
	[job_title] [nvarchar](100) NOT NULL,
	[price_type] [nvarchar](100) NOT NULL,
	[price_min] [decimal](6,2) NULL,
	[price_max] [decimal](6,2) NULL,
	[duration_type] [nvarchar] (100) NULL,
	[from_date] [datetime] NULL,
	[to_date] [datetime] NULL,
	[bidder_type] [nvarchar] (50) NULL,
	[lat] [decimal](9, 6) NULL,
	[lng] [decimal](9, 6) NULL,
	[job_description] [nvarchar] (400) NULL,
	[job_status] [nvarchar] (50) NULL,
	[job_category] [varchar] (50) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_job_id] PRIMARY KEY NONCLUSTERED 
(
	[job_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_jobs] 
ADD CONSTRAINT [FK_fe_job_customer_id] 
FOREIGN KEY ([customer_id]) 
REFERENCES [dbo].[fe_customer] ([customer_id]);
END
GO



IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_job_bid')
BEGIN
CREATE TABLE [dbo].[fe_job_bid](
	[bid_id] [bigint] IDENTITY(1,1) NOT NULL,
	[agency_id] [bigint] NOT NULL,
	[job_id] [bigint] NOT NULL,
	[bid_amount] DECIMAL(10, 2),
	[bid_date] [datetime] NOT NULL,
	[bid_type] [nvarchar] (20) NULL,
	[bid_notes] [nvarchar] (400) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_bid_id] PRIMARY KEY NONCLUSTERED 
(
	[bid_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_job_bid] 
ADD CONSTRAINT [FK_fe_job_bid_agency_id] 
FOREIGN KEY ([agency_id]) 
REFERENCES [dbo].[fe_agency] ([agency_id]);

ALTER TABLE [dbo].[fe_job_bid] 
ADD CONSTRAINT [FK_fe_job_bid_job_id] 
FOREIGN KEY ([job_id]) 
REFERENCES [dbo].[fe_jobs] ([job_id]);
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_job_contract')
BEGIN
CREATE TABLE [dbo].[fe_job_contract](
	[contract_id] [bigint] IDENTITY(1,1) NOT NULL,
	[job_id] [bigint] NOT NULL,
	[agency_id] [bigint] NOT NULL,
	[bid_id] [bigint] NOT NULL,
	[contract_status] [nvarchar](50) NULL,
	[contract_progress] [int] NULL,
	[agency_feedback] [nvarchar](400) NULL,
	[agency_rating] [int] NULL,
	[customer_feedback] [nvarchar](400) NULL,
	[customer_rating] [int] NULL,
	[attachment_media] [varchar] (200) NULL,
	[cancelation_reason] [varchar](200) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_contract_id] PRIMARY KEY NONCLUSTERED 
(
	[contract_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_job_contract_activity')
BEGIN
CREATE TABLE [dbo].[fe_job_contract_activity](
	[contract_activity_id] [bigint] IDENTITY(1,1) NOT NULL,
	[contract_id] [bigint] NOT NULL,
	[contract_status] [nvarchar](50) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_contract_activity_id] PRIMARY KEY NONCLUSTERED 
(
	[contract_activity_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_job_contract_progress')
BEGIN
CREATE TABLE [dbo].[fe_job_contract_progress](
	[contract_progress_id] [bigint] IDENTITY(1,1) NOT NULL,
	[contract_id] [bigint] NOT NULL,
	[contract_progress] [int] NULL,
	[contract_status] [nvarchar](20) NULL,
	[contract_notes] [nvarchar](400) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_contract_progress_id] PRIMARY KEY NONCLUSTERED 
(
	[contract_progress_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_customer_cards')
BEGIN
CREATE TABLE [dbo].[fe_customer_cards](
	[customer_card_id] [bigint] IDENTITY(1,1) NOT NULL,
	[customer_id] [bigint] NOT NULL,
	[card_id] [varchar] (100) NULL,
	[brand] [nvarchar](100) NOT NULL,
	[country] [nvarchar](100) NOT NULL,
	[exp_year] [nvarchar](5) NULL,
	[exp_month] [nvarchar](3) NULL,
	[last4] [nvarchar](4) NULL,
	[is_default] [bit] NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_customer_card_id] PRIMARY KEY NONCLUSTERED 
(
	[customer_card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[customer_card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[fe_customer_cards]  WITH CHECK ADD  CONSTRAINT [FK_fe_card_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[fe_customer] ([customer_id])


ALTER TABLE [dbo].[fe_customer_cards] CHECK CONSTRAINT [FK_fe_card_customer_id]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_agency_bank_details')
BEGIN
CREATE TABLE [dbo].[fe_agency_bank_details](
	[bank_id] [bigint] IDENTITY(1,1) NOT NULL,
	[agency_id] [bigint] NOT NULL,
	[bank_name] [varchar](100) NULL,
	[account_title] [nvarchar](100) NOT NULL,
	[account_number] [nvarchar](50) NOT NULL,
	[description] [nvarchar](500) NULL,
	[is_default] [bit] NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_agency_bank_id] PRIMARY KEY NONCLUSTERED 
(
	[bank_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[bank_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_agency_bank_details]  WITH CHECK ADD  CONSTRAINT [FK_agency_bank_details_agency_id] FOREIGN KEY([agency_id])
REFERENCES [dbo].[fe_agency] ([agency_id])


ALTER TABLE [dbo].[fe_agency_bank_details] CHECK CONSTRAINT [FK_agency_bank_details_agency_id]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_agency_license')
BEGIN
CREATE TABLE [dbo].[fe_agency_license](
	[license_id] [bigint] IDENTITY(1,1) NOT NULL,
	[agency_id] [bigint] NOT NULL,
	[license_name] [nvarchar](80) NOT NULL,
	[issuing_authority] [nvarchar](50) NOT NULL,
	[expiry_date] [varchar] (10) NOT NULL,
	[license_state] [nvarchar](50) NULL,
	[license_identity] [nvarchar](200) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
	[license_type] [varchar](50) NULL,
	[License_front_image_path] [nvarchar](200) NULL,
	[License_back_image_path] [varchar](200) NULL,
	[is_default] [bit] NULL,
 CONSTRAINT [pk_agency_license_id] PRIMARY KEY NONCLUSTERED 
(
	[license_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[license_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_agency_license]  WITH CHECK ADD  CONSTRAINT [FK_pk_agency_license_agency_id] FOREIGN KEY([agency_id])
REFERENCES [dbo].[fe_agency] ([agency_id])


ALTER TABLE [dbo].[fe_agency_license] CHECK CONSTRAINT [FK_pk_agency_license_agency_id]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_trainings')
BEGIN
CREATE TABLE [dbo].[fe_trainings](
	[training_id] [bigint] IDENTITY(1,1) NOT NULL,
	[agency_id] [bigint] NOT NULL,
	[training_title] [nvarchar](100) NOT NULL,
	[trainer_id] [bigint] NOT NULL,
	[from_date] [datetime] NULL,
	[to_date] [datetime] NULL,
	[duration] [varchar] (100) NULL,
	[fee] [decimal] (6,2) NOT NULL,
	[location_lat] [decimal] (9,2) NULL,
	[location_lng] [decimal] (9,2) NULL,
	[details] [varchar] (8000) NULL,
	[training_status] [varchar] (80) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_training_id] PRIMARY KEY NONCLUSTERED 
(
	[training_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


--ALTER TABLE [dbo].[fe_trainings]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_agency_id] FOREIGN KEY([agency_id])
--REFERENCES [dbo].[fe_agency] ([agency_id])


--ALTER TABLE [dbo].[fe_trainings]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_trainer_id] FOREIGN KEY([trainer_id])
--REFERENCES [dbo].[fe_trainers] ([trainer_id])
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_training_media')
BEGIN
CREATE TABLE [dbo].[fe_training_media](
	[media_id] [bigint] IDENTITY(1,1) NOT NULL,
	[training_id] [bigint] NOT NULL,
	[media_name] [nvarchar](100) NULL,
	[media_path] [varchar] (200) NULL,
	[media_type] [varchar] (800) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_media_id] PRIMARY KEY NONCLUSTERED 
(
	[media_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_training_media]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_media_training_id] FOREIGN KEY([training_id])
REFERENCES [dbo].[fe_trainings] ([training_id])
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_trainers')
BEGIN
CREATE TABLE [dbo].[fe_trainers](
	[trainer_id] [bigint] IDENTITY(1,1) NOT NULL,
	[agency_id] [bigint] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[first_name] [varchar] (80) NOT NULL,
	[last_name] [varchar] (80) NOT NULL,
	[phone] [varchar] (20) NULL,
	[license_number] [varchar] (50) NULL,
	[experience] [varchar] (50) NULL,
	[intoduction] [varchar] (400) NULL,
	[address1] [nvarchar](100) NULL,
	[city] [nvarchar](50) NULL,
	[state] [nvarchar](50) NULL,
	[zip_code] [nvarchar](50) NULL,
	[country] [nvarchar](50) NULL,
	[lat] [decimal](6, 2) NULL,
	[lng] [decimal](6, 2) NULL,
	[photo_path] [nvarchar](255) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_trainer_id] PRIMARY KEY NONCLUSTERED 
(
	[trainer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_trainers]  WITH CHECK ADD  CONSTRAINT [FK_fe_trainer_agency_id] FOREIGN KEY([agency_id])
REFERENCES [dbo].[fe_agency] ([agency_id])

ALTER TABLE [dbo].[fe_trainers]  WITH CHECK ADD  CONSTRAINT [FK_fe_trainer_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[fe_users] ([user_id])
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_training_enrollment')
BEGIN
CREATE TABLE [dbo].[fe_training_enrollment](
	[enrollment_id] [bigint] IDENTITY(1,1) NOT NULL,
	[training_id] [bigint] NOT NULL,
	[customer_id] [bigint] NOT NULL,
	[enrollment_status] [varchar] (50) NULL,
	[enrollment_date] [datetime] NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [pk_enrollment_id] PRIMARY KEY NONCLUSTERED 
(
	[enrollment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_training_enrollment]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_enrollment_training_id] FOREIGN KEY([training_id])
REFERENCES [dbo].[fe_trainings] ([training_id])


ALTER TABLE [dbo].[fe_training_enrollment] CHECK CONSTRAINT [FK_fe_training_enrollment_training_id]


ALTER TABLE [dbo].[fe_training_enrollment]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_enrollment_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[fe_customer] ([customer_id])


ALTER TABLE [dbo].[fe_training_enrollment] CHECK CONSTRAINT [FK_fe_training_enrollment_customer_id]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_training_feedback')
BEGIN
CREATE TABLE [dbo].[fe_training_feedback](
	[training_feedback_id] [bigint] IDENTITY(1,1) NOT NULL,
	[training_id] [bigint] NOT NULL,
	[customer_id] [bigint] NOT NULL,
	[feedback] [varchar] (50) NULL,
	[rating] [int] NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
 CONSTRAINT [training_feedback_id] PRIMARY KEY NONCLUSTERED 
(
	[training_feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[fe_training_feedback]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_feedback_training_id] FOREIGN KEY([training_id])
REFERENCES [dbo].[fe_trainings] ([training_id])


ALTER TABLE [dbo].[fe_training_feedback] CHECK CONSTRAINT [FK_fe_training_feedback_training_id]


ALTER TABLE [dbo].[fe_training_feedback]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_feedback_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[fe_customer] ([customer_id])


ALTER TABLE [dbo].[fe_training_feedback] CHECK CONSTRAINT [FK_fe_training_feedback_customer_id]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_training_enrollment_media')
BEGIN
CREATE TABLE [dbo].[fe_training_enrollment_media](
	[media_id] [bigint] IDENTITY(1,1) NOT NULL,
	[enrollment_id] [bigint] NOT NULL,
	[media_name] [nvarchar](100) NULL,
	[media_path] [varchar](200) NULL,
	[media_type] [varchar](800) NULL,
	[media_category] [varchar] (100) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL
 CONSTRAINT [pk_enrollment_media_id] PRIMARY KEY NONCLUSTERED 
(
	[media_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[fe_training_enrollment_media]  WITH CHECK ADD  CONSTRAINT [FK_fe_training_enrollment_media_training_id] FOREIGN KEY([enrollment_id])
REFERENCES [dbo].[fe_training_enrollment] ([enrollment_id])

ALTER TABLE [dbo].[fe_training_enrollment_media] CHECK CONSTRAINT [FK_fe_training_enrollment_media_training_id]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_notification_tokens')
BEGIN
CREATE TABLE [dbo].[fe_notification_tokens](
    [token_id] [bigint] IDENTITY(1,1) NOT NULL,
    [user_id] [bigint] NOT NULL,
    [token] [nvarchar](4000)   NULL,
    [is_deleted] [bit] NULL,
    [is_active] [bit] NULL,
    [create_date] [datetime] NOT NULL,
    [created_by] [bigint] NOT NULL,
    [update_date] [datetime] NULL,
    [updated_by] [bigint] NULL
    
 CONSTRAINT [token_id] PRIMARY KEY NONCLUSTERED 
(
    [token_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_services')
BEGIN
CREATE TABLE [dbo].[fe_services](
	[services_id] [bigint] IDENTITY(1,1) NOT NULL,
	[agency_id] [bigint] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[service_title] [varchar](100) NULL,
	[service_description] [varchar](4000) NULL,
	[service_banner] [nvarchar](max) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL,
	[price_type] [varchar](50) NULL,
	[is_obsulate] [bigint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[fe_services] ADD  CONSTRAINT [PK_fe_services] PRIMARY KEY CLUSTERED 
(
	[services_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_release_management')
BEGIN
CREATE TABLE [dbo].[fe_release_management](
	[release_id] [int] IDENTITY(1,1) NOT NULL,
	[build] [varchar](50) NOT NULL,
	[sprint] [varchar](50) NOT NULL,
	[branch] [varchar](50) NOT NULL,
	[comments] [varchar](500) NULL,
	[target_app] [varchar](80) NULL,
	[create_date] [datetime] NULL
PRIMARY KEY CLUSTERED 
(
	[release_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_list_item')
BEGIN
CREATE TABLE [dbo].[fe_list_item](
	[list_item_id] [bigint] IDENTITY(1,1) NOT NULL,
	[list_type] [varchar](50) NULL,
	[code] [varchar](50) NULL,
	[name] [varchar](200) NULL,
	[display_order] [int] NULL,
	[is_deleted] [bit] NULL,
	[create_date] [datetime] NULL,
	[created_by] [bigint] NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL
PRIMARY KEY CLUSTERED 
(
	[list_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_feedback_reply')
BEGIN
CREATE TABLE [dbo].[fe_feedback_reply](
    [reply_id] [bigint] IDENTITY(1,1) NOT NULL,
    [training_feedback_id] [bigint] NOT NULL,
    [message_reply] VARCHAR(4000), -- Increased the title length for more descriptive titles

    [is_deleted] [bit] NULL,
    [is_active] [bit] NULL,
    [create_date] [datetime] NOT NULL,
    [created_by] [bigint] NOT NULL,
    [update_date] [datetime] NULL,
    [updated_by] [bigint] NULL,
    CONSTRAINT [PK_fe_feedback_reply] PRIMARY KEY CLUSTERED 
    (
        [reply_id] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
            ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) 
    ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_agent')
BEGIN
CREATE TABLE [dbo].[fe_agent](
    [agent_id] [bigint] IDENTITY(1,1) NOT NULL,
    [agency_id] [bigint] NOT NULL,
    [user_id] [bigint] NOT NULL,
    [first_name] VARCHAR(50),
    [last_name] VARCHAR(50),
    [phone] VARCHAR(50),
     [email_address] VARCHAR(100),
    [license_number] VARCHAR(50),
    [experince] VARCHAR(50),
    [introduction] VARCHAR(500),
    [address1] VARCHAR(50),
    [city] VARCHAR(50),
    [state] VARCHAR(50),
    [zip_code] VARCHAR(50),
    [country] VARCHAR(50),
	[lat] DECIMAL(9, 6),  
    [lng] DECIMAL(9, 6),  
    [photo_path] VARCHAR(500),
    [is_deleted] [bit] NULL,
    [is_active] [bit] NULL,
    [create_date] [datetime] NOT NULL,
    [created_by] [bigint] NOT NULL,
    [update_date] [datetime] NULL,
    [updated_by] [bigint] NULL,
    CONSTRAINT [PK_fe_agent] PRIMARY KEY CLUSTERED 
    (
  [agent_id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
 ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [fk_agency] FOREIGN KEY (agency_id) REFERENCES [dbo].[fe_agency](agency_id),
 CONSTRAINT [fk_users] FOREIGN KEY (user_id) REFERENCES [dbo].[fe_users](user_id)           
) ON [PRIMARY]
END
GO


IF NOT EXISTS(SELECT 1 FROM SYS.TABLES WHERE Name = 'fe_payment')
BEGIN
CREATE TABLE [dbo].[fe_payment](
	[payment_id] [bigint] IDENTITY(1,1) NOT NULL,
	[transaction_id] [varchar](255) NULL,
	[amount] [decimal](18, 2) NOT NULL,
	[card_id] [bigint] NULL,
	[job_id] [bigint] NULL,
	[bid_id] [bigint] NULL,
	[training_id] [bigint] NULL,
	[payment_type] [varchar](50) NULL,
	[payment_status] [varchar](50) NULL,
	[is_deleted] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[created_by] [bigint] NOT NULL,
	[update_date] [datetime] NULL,
	[updated_by] [bigint] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[fe_payment] ADD  CONSTRAINT [payment_id] PRIMARY KEY NONCLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO




