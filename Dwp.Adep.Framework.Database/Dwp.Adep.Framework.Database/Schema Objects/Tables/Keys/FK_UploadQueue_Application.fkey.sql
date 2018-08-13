ALTER TABLE [dbo].[UploadQueue]
    ADD CONSTRAINT [FK_UploadQueue_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[UploadQueue] NOCHECK CONSTRAINT [FK_UploadQueue_Application];

