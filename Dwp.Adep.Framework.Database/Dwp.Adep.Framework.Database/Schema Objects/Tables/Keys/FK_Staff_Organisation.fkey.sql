ALTER TABLE [dbo].[Staff]
    ADD CONSTRAINT [FK_Staff_Organisation] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Staff] NOCHECK CONSTRAINT [FK_Staff_Organisation];

