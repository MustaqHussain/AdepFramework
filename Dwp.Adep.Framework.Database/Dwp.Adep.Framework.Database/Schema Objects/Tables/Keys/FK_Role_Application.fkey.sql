ALTER TABLE [dbo].[Role]
    ADD CONSTRAINT [FK_Role_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Role] NOCHECK CONSTRAINT [FK_Role_Application];

