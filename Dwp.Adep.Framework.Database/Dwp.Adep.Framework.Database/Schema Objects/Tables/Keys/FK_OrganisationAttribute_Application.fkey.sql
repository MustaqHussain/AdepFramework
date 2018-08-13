ALTER TABLE [dbo].[OrganisationAttribute]
    ADD CONSTRAINT [FK_OrganisationAttribute_Application] FOREIGN KEY ([ApplicationCode]) REFERENCES [dbo].[Application] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

