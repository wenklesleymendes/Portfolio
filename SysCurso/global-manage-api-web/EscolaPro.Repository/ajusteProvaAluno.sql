ALTER TABLE [ProvaAluno] DROP CONSTRAINT [FK_ProvaAluno_AgendaProva_AgendaProvaId];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProvaAluno]') AND [c].[name] = N'AgendaProvaId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProvaAluno] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ProvaAluno] ALTER COLUMN [AgendaProvaId] int NULL;

GO

ALTER TABLE [ProvaAluno] ADD CONSTRAINT [FK_ProvaAluno_AgendaProva_AgendaProvaId] FOREIGN KEY ([AgendaProvaId]) REFERENCES [AgendaProva] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210225190004_ajusteProvaAluno', N'3.1.3');

GO

