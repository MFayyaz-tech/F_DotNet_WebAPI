IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'job_types' and code ='lawyer' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('job_types','lawyer','Lawyer',1,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'job_types' and code ='legal_practice' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('job_types','legal_practice','Legal Practice',2,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'job_types' and code ='security_services' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('job_types','security_services','Security Services',3,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'job_types' and code ='accounting_finance_services' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('job_types','accounting_finance_services','Accounting & Finance Services',4,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'job_types' and code ='others' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('job_types','others','Others',5,GETDATE(),-99)
END
GO

--LicenceType

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'licence_type' and code ='lawyer' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('licence_type','lawyer','Lawyer',1,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'licence_type' and code ='legal_practice' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('licence_type','legal_practice','Legal Practice',2,GETDATE(),-99)
END
GO


IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'licence_type' and code ='security_services' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('licence_type','security_services','Security Services',3,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'licence_type' and code ='accounting_finance_services' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('licence_type','accounting_finance_services','Accounting & Finance Services',4,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'licence_type' and code ='others' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('licence_type','others','Others',5,GETDATE(),-99)
END
GO

--AgencyBidType

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'agency_bid_type' and code ='hour' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('agency_bid_type','hour','Hour',1,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'agency_bid_type' and code ='fixed' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('agency_bid_type','fixed','Fixed',2,GETDATE(),-99)
END
GO

--AddTrainingType

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='technology_and_it' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','technology_and_it','Technology and IT',1,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='business_and_management' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','business_and_management','Business and Management',2,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='healthcare_and_wellness' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','healthcare_and_wellness','Healthcare and Wellness',3,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='finance_and_accounting' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','finance_and_accounting','Finance and Accounting',4,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='marketing_and_sales' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','marketing_and_sales','Marketing and Sales',5,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='human_resources_and_recruitment' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','human_resources_and_recruitment','Human Resources and Recruitment',6,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='hospitality_and_tourism' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','hospitality_and_tourism','Hospitality and Tourism',7,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='education_and_teaching' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','education_and_teaching','Education and Teaching',8,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='engineering_and_construction' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','engineering_and_construction','Engineering and Construction',9,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='design_and_creative_arts' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','design_and_creative_arts','Design and Creative Arts',10,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='science_and_research' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','science_and_research','Science and Research',11,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='agriculture_and_farming' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','agriculture_and_farming','Agriculture and Farming',12,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='media_and_journalism' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','media_and_journalism','Media and Journalism',13,GETDATE(),-99)
END
GO


IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='transportation_and_logistics' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','transportation_and_logistics','Transportation and Logistics',14,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='legal_and_law' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','legal_and_law','Legal and Law',15,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_training_type' and code ='other' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_training_type','other','others',16,GETDATE(),-99)
END
GO

--Add Trainer Experince

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='0_to_6_month' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','0_to_6_month','0 to 6 month',1,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='6_to_12_month' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','6_to_12_month','6 to 12 month',2,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='1_to_2_years' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','1_to_2_years','1 to 2 years',3,GETDATE(),-99)
END
GO


IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='2_to_3_years' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','2_to_3_years','2 to 3 years',4,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='3_to_4_years' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','3_to_4_years','3 to 4 years',5,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='4_to_5_years' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','4_to_5_years','4 to 5 years',6,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='5_to_6_years' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','5_to_6_years','5 to 6 years',7,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_experince_type' and code ='10+_years' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_experince_type','10+_years','10+ years',8,GETDATE(),-99)
END
GO

--Add Trainer Skills

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_skills_type' and code ='beginner' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_skills_type','beginner','Beginner',1,GETDATE(),-99)
END
GO


IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_skills_type' and code ='intermediate' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_skills_type','intermediate','Intermediate',2,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'add_trainer_skills_type' and code ='expert' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('add_trainer_skills_type','expert','Expert',3,GETDATE(),-99)
END
GO


--CancelJobReason

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'cancel_job_reason_type' and code ='job_not_completed' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('cancel_job_reason_type','job_not_completed','Job not completed',1,GETDATE(),-99)
END
GO

IF NOT EXISTS(SELECT  top 1 1  FROM fe_list_item WHERE list_type = 'cancel_job_reason_type' and code ='job_not_required' )
BEGIN
	insert into fe_list_item(list_type,code,name,display_order,create_date,created_by)
	values('cancel_job_reason_type','job_not_required','Job not required',2,GETDATE(),-99)
END
GO



-- services type
IF NOT EXISTS (SELECT TOP 1 1 FROM fe_list_item WHERE list_type = 'service_type' AND code = 'cleaning')
BEGIN
    INSERT INTO fe_list_item (list_type, code, name, display_order, document_path, create_date, created_by)
    VALUES ('service_type', 'cleaning', 'Cleaning', 1, '\Documents\Services\cleaning.png', GETDATE(), -99)
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM fe_list_item WHERE list_type = 'service_type' AND code = 'repairing')
BEGIN
    INSERT INTO fe_list_item (list_type, code, name, display_order, document_path, create_date, created_by)
    VALUES ('service_type', 'repairing', 'Repairing', 2, '\Documents\Services\repairing.png', GETDATE(), -99)
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM fe_list_item WHERE list_type = 'service_type' AND code = 'painting')
BEGIN
    INSERT INTO fe_list_item (list_type, code, name, display_order, document_path, create_date, created_by)
    VALUES ('service_type', 'painting', 'Painting', 3, '\Documents\Services\painting.png', GETDATE(), -99)
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM fe_list_item WHERE list_type = 'service_type' AND code = 'electrician')
BEGIN
    INSERT INTO fe_list_item (list_type, code, name, display_order, document_path, create_date, created_by)
    VALUES ('service_type', 'electrician', 'Electrician', 4, '\Documents\Services\electrician.png', GETDATE(), -99)
END
GO


