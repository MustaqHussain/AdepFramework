ALTER TABLE [dbo].[Holiday]
    ADD CONSTRAINT [FK_Holiday_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

