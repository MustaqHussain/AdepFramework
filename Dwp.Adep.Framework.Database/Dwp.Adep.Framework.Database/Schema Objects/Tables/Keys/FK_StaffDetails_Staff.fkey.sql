ALTER TABLE [dbo].[StaffDetails]
    ADD CONSTRAINT [FK_StaffDetails_Staff] FOREIGN KEY ([StaffCode]) REFERENCES [dbo].[Staff] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

