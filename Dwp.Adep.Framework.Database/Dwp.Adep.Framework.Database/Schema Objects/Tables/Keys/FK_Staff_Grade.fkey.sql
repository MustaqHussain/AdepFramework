﻿ALTER TABLE [dbo].[Staff]
    ADD CONSTRAINT [FK_Staff_Grade] FOREIGN KEY ([GradeCode]) REFERENCES [dbo].[Grade] ([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
ALTER TABLE [dbo].[Staff] NOCHECK CONSTRAINT [FK_Staff_Grade];

