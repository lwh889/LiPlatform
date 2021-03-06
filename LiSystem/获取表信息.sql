alter PROCEDURE [dbo].[GetTableInfo](
@dataBaseName NVARCHAR(MAX),
 @tableName NVARCHAR(MAX)
 )
AS
BEGIN
EXEC('

SELECT
--                 CASE
--            WHEN col.colorder = 1 THEN
--                obj.name
--            ELSE
--                ''''
--        END AS 表名,
             obj.name AS tableName,
       col.colorder AS columnOrder,
       col.name AS columnName,
       --ISNULL(ep.[value], '''') AS columnDesc,
       t.name AS columnType,
       col.length AS columnLength,
       ISNULL(COLUMNPROPERTY(col.id, col.name, ''Scale''), 0) AS columnScale,
       --CASE
       --    WHEN COLUMNPROPERTY(col.id, col.name, ''IsIdentity'') = 1 THEN
       --        1
       --    ELSE
       --        0
       --END AS 标识,
       CASE
           WHEN EXISTS
                (
                    SELECT 1
                    FROM ' + @dataBaseName + '.dbo.sysindexes si
                        INNER JOIN ' + @dataBaseName + '.dbo.sysindexkeys sik
                            ON si.id = sik.id
                               AND si.indid = sik.indid
                        INNER JOIN ' + @dataBaseName + '.dbo.syscolumns sc
                            ON sc.id = sik.id
                               AND sc.colid = sik.colid
                        INNER JOIN ' + @dataBaseName + '.dbo.sysobjects so
                            ON so.name = si.name
                               AND so.xtype = ''PK''
                    WHERE sc.id = col.id
                          AND sc.colid = col.colid
                ) THEN
               1
           ELSE
               0
       END AS bPrimaryKey,
       CASE
           WHEN col.isnullable = 1 THEN
               1
           ELSE
               0
       END AS bIsNull,
       ISNULL(comm.text, '''') AS columnDefault
FROM ' + @dataBaseName + '.dbo.syscolumns col
    LEFT JOIN ' + @dataBaseName + '.dbo.systypes t
        ON col.xtype = t.xusertype
    INNER JOIN ' + @dataBaseName + '.dbo.sysobjects obj
        ON col.id = obj.id
           AND obj.xtype = ''U''
           AND obj.status >= 0
    LEFT JOIN ' + @dataBaseName + '.dbo.syscomments comm
        ON col.cdefault = comm.id
    LEFT JOIN sys.extended_properties ep
        ON col.id = ep.major_id
           AND col.colid = ep.minor_id
           AND ep.name = ''MS_Description''
    LEFT JOIN sys.extended_properties epTwo
        ON obj.id = epTwo.major_id
           AND epTwo.minor_id = 0
           AND epTwo.name = ''MS_Description''
WHERE obj.name = ''' + @tableName + ''' --表名
ORDER BY col.colorder;

')
END