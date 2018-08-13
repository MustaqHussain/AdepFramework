ALTER TABLE [dbo].[OrganisationType]
    ADD CONSTRAINT [FK_OrganisationType_OrganisationTypeGroup] FOREIGN KEY ([OrganisationTypeGroupCode]) REFERENCES [dbo].[OrganisationTypeGroup] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[OrganisationType] NOCHECK CONSTRAINT [FK_OrganisationType_OrganisationTypeGroup];

