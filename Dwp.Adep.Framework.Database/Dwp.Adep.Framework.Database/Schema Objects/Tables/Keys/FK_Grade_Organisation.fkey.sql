ALTER TABLE [dbo].[Grade]
    ADD CONSTRAINT [FK_Grade_Organisation] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Grade] NOCHECK CONSTRAINT [FK_Grade_Organisation];

