ALTER TABLE [dbo].[ApplicationAttribute]
    ADD CONSTRAINT [DF_ApplicationAttribute_IsRole] DEFAULT ((0)) FOR [IsRole];

