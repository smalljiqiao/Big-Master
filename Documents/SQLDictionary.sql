USE BigMaster;
SET NOCOUNT ON; --不显示影响行数
SET STATISTICS TIME OFF; --不显示执行时间

DECLARE @TableName NVARCHAR(35)
DECLARE Tbls CURSOR FOR
  SELECT DISTINCT Table_name
  FROM   INFORMATION_SCHEMA.COLUMNS
  WHERE  Table_name NOT IN ( 'sysdiagrams' )
  ORDER  BY Table_name

OPEN Tbls

PRINT '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">'

PRINT '<html xmlns="http://www.w3.org/1999/xhtml">'

PRINT '<head>'

PRINT '<title>数据库字典</title>'

PRINT '<style type="text/css">'

PRINT 'body{margin:0; font:11pt "arial", "微软雅黑"; cursor:default;}'

PRINT '.tableBox{margin:10px auto; padding:0px; width:1000px; height:auto; background:#FBF5E3; border:1px solid #45360A}'

PRINT '.tableBox h3 {font-size:12pt; height:30px; line-height:30px; background:#45360A; padding:0px 0px 0px 15px; color:#FFF; margin:0px; text-align:left }'

PRINT '.tableBox p {font-size:10pt; line-height:30px; background:#45360A; padding:0px 0px 0px 15px; color:#FFF; margin:0px; text-align:left }'

PRINT '.tableBox table {width:1000px; padding:0px }'

PRINT '.tableBox th {height:25px; border-top:1px solid #FFF; border-left:1px solid #FFF; background:#F7EBC8; border-right:1px solid #E0C889; border-bottom:1px solid #E0C889 }'

PRINT '.tableBox td {height:25px; padding-left:10px; border-top:1px solid #FFF; border-left:1px solid #FFF; border-right:1px solid #E0C889; border-bottom:1px solid #E0C889 }'

PRINT '</style>'

PRINT '</head>'

PRINT '<body>'

FETCH NEXT FROM Tbls INTO @TableName

WHILE @@FETCH_STATUS = 0
  BEGIN
      PRINT '<div class="tableBox">'

      PRINT '<h3>' + @TableName + '</h3>'

      SELECT '<p>'
             + Isnull(Cast(ds.value AS VARCHAR(1000)), '')
             + '</p>'
      FROM   sys.extended_properties ds
             LEFT JOIN sysobjects tbs
                    ON ds.major_id = tbs.id
      WHERE  ds.minor_id = 0
             AND tbs.name = @TableName

      PRINT '<table cellspacint="0">'

      PRINT '<tr>'

      PRINT '<th>字段名称</th>'

      PRINT '<th>主键</th>'

      PRINT '<th>类型</th>'

      PRINT '<th>长度</th>'

      PRINT '<th>允许为空</th>'

      PRINT '<th>默认值</th>'

      PRINT '<th>描述</th>'

      PRINT '<th>数值精度</th>'

      PRINT '<th>标识列</th>'

      /* total count for the query result */
      DECLARE @totalcount INT
      DECLARE @rownum INT

      SELECT @totalcount = Count(1)
      FROM   information_schema.COLUMNS
      WHERE  TABLE_NAME = @TableName

      SET @rownum = 1

      WHILE @rownum <= @totalcount
        BEGIN
            --         SELECT '</tr><tr>',
            --'<td>' + COLUMN_NAME + '</td>',
            --'<td>' + DATA_TYPE + '</td>',
            --'<td>' + ISNULL(CAST(CHARACTER_MAXIMUM_LENGTH AS varchar(20)),'') + '</td>',
            --'<td>' + ISNULL(CAST(IS_NULLABLE AS varchar(20)),'') + '</td>',
            --'<td>' + ISNULL(COLUMN_DEFAULT,'') + '</td>',
            --'<td>' + ISNULL(CAST(b.value AS varchar(1000)),'') + '</td>'
            --         FROM   information_schema.COLUMNS AS a
            --                LEFT JOIN sys.extended_properties AS b
            --                       ON a.TABLE_NAME = Object_name(b.major_id)
            --                          AND a.ORDINAL_POSITION = b.minor_id
            --         WHERE  a.TABLE_NAME = @TableName
            --                AND ORDINAL_POSITION = @rownum
            --快速查看表结构
            SELECT '</tr><tr>',
                   '<td>' + col.name + '</td>',--列名
                   CASE
                     WHEN EXISTS (SELECT 1
                                  FROM   dbo.sysindexes si
                                         INNER JOIN dbo.sysindexkeys sik
                                                 ON si.id = sik.id
                                                    AND si.indid = sik.indid
                                         INNER JOIN dbo.syscolumns sc
                                                 ON sc.id = sik.id
                                                    AND sc.colid = sik.colid
                                         INNER JOIN dbo.sysobjects so
                                                 ON so.name = si.name
                                                    AND so.xtype = 'PK'
                                  WHERE  sc.id = col.id
                                         AND sc.colid = col.colid) THEN '<td>1</td>'
                     ELSE '<td></td>'
                   END,--主键
                   '<td>' + t.name + '</td>',--数据类型
                   '<td>' + Cast(col.length AS VARCHAR(20))
                   + '</td>',--长度
                   CASE
                     WHEN col.isnullable = 1 THEN '<td>1</td>'
                     ELSE '<td></td>'
                   END,--允许为空
                   '<td>' + Isnull(comm.text, '') + '</td>',--默认值
                   '<td>'
                   + Isnull(Cast(ep.[value] AS VARCHAR(1000)), '')
                   + '</td>',--说明
                   '<td>'
                   + Isnull(Cast(Columnproperty(col.id, col.name, 'Scale') AS VARCHAR(20)), 0)
                   + '</td>',--数值精度
                   CASE
                     WHEN Columnproperty(col.id, col.name, 'IsIdentity') = 1 THEN '<td>1</td>'
                     ELSE '<td></td>'
                   END --标识列
            FROM   dbo.syscolumns col
                   LEFT JOIN dbo.systypes t
                          ON col.xtype = t.xusertype
                   INNER JOIN dbo.sysobjects obj
                           ON col.id = obj.id
                              AND obj.xtype = 'U'
                              AND obj.status >= 0
                   LEFT JOIN dbo.syscomments comm
                          ON col.cdefault = comm.id
                   LEFT JOIN sys.extended_properties ep
                          ON col.id = ep.major_id
                             AND col.colid = ep.minor_id
                             AND ep.name = 'MS_Description'
                   LEFT JOIN sys.extended_properties epTwo
                          ON obj.id = epTwo.major_id
                             AND epTwo.minor_id = 0
                             AND epTwo.name = 'MS_Description'
            WHERE  obj.name = @TableName
                   AND colorder = @rownum
            ORDER  BY col.colorder;

            SET @rownum = @rownum + 1
        END

      PRINT '</tr></table>'

      PRINT '</div>'

      FETCH NEXT FROM Tbls INTO @TableName
  END

PRINT '</body></HTML>'

CLOSE Tbls
DEALLOCATE Tbls
USE Master; --退出占用

GO 
