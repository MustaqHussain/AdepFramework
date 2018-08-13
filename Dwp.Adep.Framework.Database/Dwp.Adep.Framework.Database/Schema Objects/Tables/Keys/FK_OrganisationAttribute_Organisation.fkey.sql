ALTER TABLE [dbo].[OrganisationAttribute]
    ADD CONSTRAINT [FK_OrganisationAttribute_Organisation] FOREIGN KEY ([OrganisationCode]) REFERENCES [dbo].[Organisation] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

