/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



:r .\StaticData\dbo.OrganisationTypeGroup.PostDeployment.sql
:r .\StaticData\dbo.OrganisationType.PostDeployment.sql
:r .\StaticData\dbo.Organisation.PostDeployment.sql
:r .\StaticData\dbo.Application.PostDeployment.sql
:r .\StaticData\dbo.Role.PostDeployment.sql
:r .\StaticData\dbo.ADRoleLookup.PostDeployment.sql
:r .\StaticData\dbo.Grade.PostDeployment.sql
:r .\StaticData\dbo.Staff.PostDeployment.sql
:r .\StaticData\dbo.ApplicationAttribute.PostDeployment.sql
:r .\StaticData\dbo.StaffAttributes.PostDeployment.sql
:r .\StaticData\dbo.StaffOrganisation.PostDeployment.sql
:r .\StaticData\dbo.ApplicationOrganisationTypeGroup.PostDeployment.sql
:r .\StaticData\dbo.OrganisationHierarchy.PostDeployment.sql


:r .\StaticData\dbo.ApplicationAttribute.PostDeployment.Release2.sql
:r .\StaticData\dbo.StaffAttributes.PostDeployment.Release2.sql
:r .\StaticData\dbo.Category.PostDeployment.Release2.sql





