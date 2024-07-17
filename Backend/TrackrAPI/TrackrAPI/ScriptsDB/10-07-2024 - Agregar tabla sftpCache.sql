CREATE TABLE Trackr.SftpCache
(
    Id INT PRIMARY KEY IDENTITY(1,1), -- Assuming there's an Id column as a primary key
    FilePath NVARCHAR(500) NOT NULL,
    LastWriteTime DATETIME NOT NULL
)