ALTER TABLE [dbo].[StaffAttributes]
    ADD CONSTRAINT [FK_StaffAttributes_ApplicationAttribute] FOREIGN KEY ([ApplicationAttributeCode]) REFERENCES [dbo].[ApplicationAttribute] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffAttributes] NOCHECK CONSTRAINT [FK_StaffAttributes_ApplicationAttribute];

