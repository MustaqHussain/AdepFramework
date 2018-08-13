ALTER TABLE [dbo].[OrganisationAttribute]
    ADD CONSTRAINT [FK_OrganisationAttribute_ApplicationAttribute] FOREIGN KEY ([ApplicationAttributeCode]) REFERENCES [dbo].[ApplicationAttribute] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

