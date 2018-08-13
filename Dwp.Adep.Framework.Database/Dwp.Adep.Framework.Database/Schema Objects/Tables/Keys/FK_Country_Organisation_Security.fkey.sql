ALTER TABLE [dbo].[Country]
    ADD CONSTRAINT [FK_Country_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

