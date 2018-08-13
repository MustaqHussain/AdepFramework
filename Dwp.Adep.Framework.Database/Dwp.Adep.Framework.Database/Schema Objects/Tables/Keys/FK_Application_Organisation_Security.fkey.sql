ALTER TABLE [dbo].[Application]
    ADD CONSTRAINT [FK_Application_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Application] NOCHECK CONSTRAINT [FK_Application_Organisation_Security];

