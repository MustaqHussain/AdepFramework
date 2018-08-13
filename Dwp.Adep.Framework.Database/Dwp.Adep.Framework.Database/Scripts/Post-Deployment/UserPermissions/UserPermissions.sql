--Create Login
PRINT 'Creating Login'
USE [master]
GO
CREATE LOGIN [ADV\FrameworkWebServices] FROM WINDOWS WITH DEFAULT_DATABASE=[AdepFramework], DEFAULT_LANGUAGE=[British]
GO

--Create User
PRINT 'Creating User'
USE [AdepFramework]
GO
CREATE USER [adv\FrameworkWebServices] FOR LOGIN [adv\FrameworkWebServices]
GO

PRINT 'User Access to AdepFramework'
--Give user Read and Write access 
USE [AdepFramework]
GO
EXEC sp_addrolemember N'db_datareader', N'adv\FrameworkWebServices'
GO
USE [AdepFramework]
GO
EXEC sp_addrolemember N'db_datawriter', N'adv\FrameworkWebServices'
GO

Print 'User access to stored procedures'
--Grant access to stored procedures
USE [AdepFramework]
GO
GRANT EXECUTE ON dbo.AddCategory TO [adv\FrameworkWebServices]
GRANT EXECUTE ON dbo.ClearLogs TO [adv\FrameworkWebServices]
GRANT EXECUTE ON dbo.InsertCategoryLog TO [adv\FrameworkWebServices]
GRANT EXECUTE ON dbo.WriteLog TO [adv\FrameworkWebServices]
GO

Print 'Finished successfully'