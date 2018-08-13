ALTER TABLE [dbo].[ADRoleLookup]
    ADD CONSTRAINT [FK_ADRoleLookup_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[ADRoleLookup] NOCHECK CONSTRAINT [FK_ADRoleLookup_Organisation_Security];

