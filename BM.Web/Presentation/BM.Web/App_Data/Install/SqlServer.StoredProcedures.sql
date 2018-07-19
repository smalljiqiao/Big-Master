/**
 * Type    ：Procedure
 * Name    ：CodeMessageResourcesImport
 * Function：Read Data From Xml File To Initialize CodeMessage Table
 * Author  ：MatueXL
 * CreTime ：2018/7/19
**/
CREATE PROCEDURE [dbo].[CodeMessageResourcesImport]
(
	@XmlPackage xml
)
AS
BEGIN
	CREATE TABLE #CodeMessageResourceTemp
	(
		[ResourceCode] [nvarchar](5) NOT NULL,
		[ResourceMessage] [nvarchar](200) NOT NULL
	)

INSERT INTO #CodeMessageResourceTemp(ResourceCode,ResourceMessage)
SELECT nref.value('Code[1]','nvarchar(5)'),nref.value('Message[1]','nvarchar(200)')
FROM @XmlPackage.nodes('//Language//LocaleResource') AS R(nref)

DECLARE @ResourceCode nvarchar(5)
DECLARE @ResourceMessage nvarchar(200)
DECLARE cur_localeresource CURSOR FOR
SELECT ResourceCode,ResourceMessage FROM #CodeMessageResourceTemp
OPEN cur_localeresource
FETCH NEXT FROM cur_localeresource INTO @ResourceCode,@ResourceMessage
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO [CodeMessage]
	(
		[Code],
		[Message]
	)
	VALUES
	(
		@ResourceCode,
		@ResourceMessage
	)

FETCH NEXT FROM cur_localeresource INTO @ResourceCode, @ResourceMessage
END

CLOSE cur_localeresource
DEALLOCATE cur_localeresource

DROP TABLE #CodeMessageResourceTemp

END
GO