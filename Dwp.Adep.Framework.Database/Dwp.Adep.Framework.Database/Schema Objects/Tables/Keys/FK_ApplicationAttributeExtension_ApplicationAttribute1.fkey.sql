ALTER TABLE [dbo].[ApplicationAttributeExtension]
    ADD CONSTRAINT [FK_ApplicationAttributeExtension_ApplicationAttribute1] FOREIGN KEY ([ApplicationAttributeCode]) REFERENCES [dbo].[ApplicationAttribute] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;

