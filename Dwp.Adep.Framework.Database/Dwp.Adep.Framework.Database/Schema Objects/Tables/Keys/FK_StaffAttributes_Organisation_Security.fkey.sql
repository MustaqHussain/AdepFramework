ALTER TABLE [dbo].[StaffAttributes]
    ADD CONSTRAINT [FK_StaffAttributes_Organisation_Security] FOREIGN KEY ([SecurityLabel]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[StaffAttributes] NOCHECK CONSTRAINT [FK_StaffAttributes_Organisation_Security];

