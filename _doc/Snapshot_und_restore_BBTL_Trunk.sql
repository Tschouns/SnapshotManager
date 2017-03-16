-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------
-- @Description: Setup for the snapshots. Only one snapshot config can be active at execution time, but those not
--				 needed can be uncommented.
--				 Only two config entries (snapshot name and DB name) must be adjusted per snapshot / DB.
--				 All bit variables are to define what has to be done: create / restore or delete a snapshot.
-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	
USE master;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Setup]','P')) BEGIN DROP PROCEDURE [dbo].Setup END
GO
CREATE PROCEDURE dbo.Setup @SnapshotName AS nvarchar(MAX) OUTPUT, @DBName AS nvarchar(MAX) OUTPUT, @path AS nvarchar(MAX) OUTPUT, @CreateSnapshot AS BIT OUTPUT, @RestoreSnapshot AS BIT OUTPUT, @DeleteSnapshot AS BIT OUTPUT AS
BEGIN
	
	-----------------------------------------------------------------------------
	-- [BBTL_ELIPS_INT]
	-----------------------------------------------------------------------------
	SET @SnapshotName		= 'BBTL_ELIPS_INT_SNAPSHOT_20170301'
	SET @DBName				= 'BBTL_Trunk'

	-----------------------------------------------------------------------------
	-- [BBTL_ELIPS_PROD]
	-----------------------------------------------------------------------------
	--SET @SnapshotName		= 'BBTL_ELIPS_PROD_SNAPSHOT_20161107'
	--SET @DBName				= 'BBTL_ELIPS_PROD'

	-----------------------------------------------------------------------------
	-- General config, all steps marked with an "1" will be executed.
	-- 0 = no / 1 = yes
	SET @CreateSnapshot		= 1
	SET @RestoreSnapshot	= 0
	SET @DeleteSnapshot		= 0

	SET @path				= '\\wr-sql14\G SSD\MSSQL12.SQL2014_02\MSSQL\DATA\'
	-----------------------------------------------------------------------------

END
GO

-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	
-----------------------------------------------------------------------------------------------------------------------	

-----------------------------------------------------------------------------------------------------------------------
-- @Description: Creates a new snapshot
-----------------------------------------------------------------------------------------------------------------------	
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CreateSnapshot]','P'))
    DROP PROCEDURE [dbo].CreateSnapshot
GO

CREATE PROCEDURE dbo.CreateSnapshot
	@SnapshotName AS nvarchar(MAX),
	@DBName AS nvarchar(MAX),
	@path AS nvarchar(MAX)
AS
BEGIN

	DECLARE @cursor CURSOR
	DECLARE @query NVARCHAR(MAX)
	DECLARE @SnapshotNumber INT
	SET @query = 'CREATE DATABASE ' + @SnapshotName + ' ON '
	SET @SnapshotNumber = 1

	DECLARE @LogicalFileName AS NVARCHAR(MAX)
	SET @cursor = CURSOR FOR
		SELECT
			mf.name AS LogicalFileName
		FROM
			sys.master_files mf
		INNER JOIN 
			sys.databases db ON db.database_id = mf.database_id
		WHERE
			db.name = @DBName AND
			type_desc <> 'LOG'

	OPEN @cursor
		FETCH NEXT
			FROM @cursor INTO @LogicalFileName
	
		WHILE @@FETCH_STATUS = 0
			BEGIN
			
				SET @query +=
					'(NAME = ' + @LogicalFileName + ', FILENAME = ''' + @path + @SnapshotName + '_' + CAST(@SnapshotNumber AS NVARCHAR(30)) + '.ss''),'
				SET @SnapshotNumber += 1

				FETCH NEXT
					FROM @cursor INTO @LogicalFileName
			END
	CLOSE @cursor
	DEALLOCATE @cursor

	-- This removes the last character, in our case the redundant comma
	SET @query = LEFT(@query, LEN(@query) - 1)
	SET @query +=
		' AS SNAPSHOT OF [' + @DBName + '];'
	
	EXEC sp_sqlexec @query

END

GO

-----------------------------------------------------------------------------------------------------------------------
-- @Description: Restores a snapshot
-----------------------------------------------------------------------------------------------------------------------	
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[RestoreSnapshot]','P'))
    DROP PROCEDURE [dbo].RestoreSnapshot
GO

CREATE PROCEDURE dbo.RestoreSnapshot
	@SnapshotName AS nvarchar(MAX),
	@DBName AS nvarchar(MAX),
	@path AS nvarchar(MAX)
AS
BEGIN
	
	DECLARE @query NVARCHAR(MAX)
	SET @query =
		'RESTORE DATABASE ' + @DBName + ' FROM DATABASE_SNAPSHOT = ''' + @SnapshotName + ''';'
	EXEC sp_sqlexec @query

END

GO

---------------------------------------------------------------------------------------------------
-- @Description:	Main
---------------------------------------------------------------------------------------------------
DECLARE @path NVARCHAR(MAX)
DECLARE @DBName NVARCHAR(MAX)
DECLARE @SnapshotName NVARCHAR(MAX)
DECLARE @CreateSnapshot BIT
DECLARE @Restore BIT
DECLARE @Delete BIT

EXEC dbo.Setup  @SnapshotName OUTPUT, @DBName OUTPUT, @path OUTPUT, @CreateSnapshot OUTPUT, @Restore OUTPUT, @Delete OUTPUT

IF (@Delete = 1)
	BEGIN 
		DECLARE @query NVARCHAR(MAX)
		SET @query =
			'DROP DATABASE ' + @SnapshotName + ' ;'
		EXEC sp_sqlexec @query
	END

IF (@CreateSnapshot = 1)
	BEGIN 
		EXEC dbo.CreateSnapshot @SnapshotName, @DBName, @path
	END

IF (@Restore = 1)
	BEGIN 
		EXEC dbo.RestoreSnapshot @SnapshotName, @DBName, @path
	END

---------------------------------------------------------------------------------------------------
-- Description:	   Drop procedures
---------------------------------------------------------------------------------------------------
DROP PROCEDURE [dbo].Setup
DROP PROCEDURE [dbo].CreateSnapshot
DROP PROCEDURE [dbo].RestoreSnapshot

GO
