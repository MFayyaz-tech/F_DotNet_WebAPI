IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'job_category' AND Object_ID = Object_ID(N'fe_jobs'))
BEGIN
	ALTER TABLE fe_jobs
	ADD [job_category] [varchar] (50) NULL
END


IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'attachment_media' AND Object_ID = Object_ID(N'fe_job_contract'))
BEGIN
	ALTER TABLE fe_job_contract
	add [attachment_media] [varchar] (200) NULL,
	[cancelation_reason] [varchar](200) NULL
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS WHERE Name = N'job_category' AND Object_ID = Object_ID(N'fe_jobs'))
BEGIN
ALter table fe_jobs 
add [job_category] [varchar] (50) NULL
END


IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS WHERE Name = N'social_id' AND Object_ID = Object_ID(N'fe_users'))
BEGIN
ALTER TABLE fe_users
ADD login_type VARCHAR(50),
    social_id VARCHAR(100)
END


IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS WHERE Name = N'agent_id' AND Object_ID = Object_ID(N'fe_job_contract'))
BEGIN
ALTER TABLE fe_job_contract
ADD agent_id [bigint] null
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS WHERE Name = N'job_category' AND Object_ID = Object_ID(N'fe_jobs'))
BEGIN
ALter table fe_jobs 
add [job_category] [varchar] (50) NULL
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'training_category' AND Object_ID = Object_ID(N'fe_trainings'))
BEGIN
	ALTER TABLE fe_trainings
	add [training_category] [varchar] (200) NULL
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'rejection_reason' AND Object_ID = Object_ID(N'fe_training_enrollment'))
BEGIN
	ALter table fe_training_enrollment
	add rejection_reason [varchar] (800) NULL
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'attachment_media' AND Object_ID = Object_ID(N'fe_training_feedback'))
BEGIN
	ALTER TABLE fe_training_feedback
	add [attachment_media] [varchar] (400) NULL
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'is_approval_required' AND Object_ID = Object_ID(N'Fe_trainings'))
BEGIN
	ALTER TABLE Fe_trainings
	add [is_approval_required] [bit] NULL
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'category' AND Object_ID = Object_ID(N'Fe_training_media'))
BEGIN
	ALTER TABLE Fe_training_media
	add [category] [varchar] (100) NULL
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'training_progress' AND Object_ID = Object_ID(N'fe_trainings'))
BEGIN
	alter table fe_trainings 
add training_progress [int]null
END

ALTER TABLE [dbo].[fe_agency_license]
ALTER COLUMN [expiry_date] [datetime] NULL;


IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'document_path' AND Object_ID = Object_ID(N'fe_list_item'))
BEGIN
alter table fe_list_item
add [document_path] [varchar] (400)  NULL 
END

IF NOT EXISTS(SELECT 1 FROM SYS.COLUMNS 
          WHERE Name = N'discount' AND Object_ID = Object_ID(N'fe_services'))
BEGIN
ALTER TABLE fe_services
ADD [discount] [decimal](18, 2) NULL,
    [category_id] [bigint] NULL
END



