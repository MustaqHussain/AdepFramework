ALTER TABLE [dbo].[StaffAttributes]
    ADD CONSTRAINT [FK_StaffAttributes_Staff] FOREIGN KEY ([StaffCode]) REFERENCES [dbo].[Staff] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffAttributes] NOCHECK CONSTRAINT [FK_StaffAttributes_Staff];

