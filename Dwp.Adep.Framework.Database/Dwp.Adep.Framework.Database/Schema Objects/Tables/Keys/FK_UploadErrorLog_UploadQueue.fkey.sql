ALTER TABLE [dbo].[UploadErrorLog]
    ADD CONSTRAINT [FK_UploadErrorLog_UploadQueue] FOREIGN KEY ([UploadCode]) REFERENCES [dbo].[UploadQueue] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[UploadErrorLog] NOCHECK CONSTRAINT [FK_UploadErrorLog_UploadQueue];

