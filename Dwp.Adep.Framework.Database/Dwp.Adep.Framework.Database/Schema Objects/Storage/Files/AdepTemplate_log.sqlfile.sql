ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [AdepTemplate_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_1.ldf', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

