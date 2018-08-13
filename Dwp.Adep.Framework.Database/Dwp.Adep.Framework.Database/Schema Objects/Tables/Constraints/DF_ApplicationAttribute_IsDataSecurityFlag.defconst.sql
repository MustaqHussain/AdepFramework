ALTER TABLE [dbo].[ApplicationAttribute]
    ADD CONSTRAINT [DF_ApplicationAttribute_IsDataSecurityFlag] DEFAULT ((0)) FOR [IsDataSecurity];

