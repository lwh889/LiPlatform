--select * from LiForm 
--select * from LiPanel 
--select * from LiControlGroup 
--select * from LiButtonGroup 
--select * from LiButton 
--select * from LiControl 

--select *
--from LiForm A1 
--left join LiPanel A2 on A1.id = A2.formModelId 
--where A1.id = 2


--delete from LiControl where controlGroupId in (select controlGroupId from LiControlGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where id in (2,3))))

--delete from LiControlGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where id in (2,3)))

--delete from LiPanel where formModelId in (select id from LiForm where id in (2,3))

--delete from LiForm where id in (2,3)
-----------------------------------------------

alter procedure sp_CreateTable(
@formId int
)
as
begin
	--变量
	declare @SqlAll nvarchar(max)
	set @SqlAll = ''

	declare @ModelSql nvarchar(max)
	set @ModelSql = ' declare @tableId int '
	
	declare @formCode nvarchar(30)
	declare @formText nvarchar(30)
	declare @formType nvarchar(20)

	declare @entityType nvarchar(20)
	declare @panelId int
	declare @systemCode nvarchar(10)
	declare @dataBaseName nvarchar(30)
	declare @tableType nvarchar(30)
	declare @tableName nvarchar(30)
	declare @parentTableName nvarchar(30)
	declare @parentDatabaseGeneratedType nvarchar(10)
	declare @primaryKeyName nvarchar(30)
	declare @parentPrimaryKeyName nvarchar(30)
	declare @foreigntKeyName nvarchar(30)
	declare @keyType nvarchar(30)
	declare @columnName nvarchar(30)
	declare @columnAbbName nvarchar(100)
	declare @columnDesc nvarchar(30)
	declare @columnType nvarchar(100)
	declare @columnLength int
	declare @entityColumnName nvarchar(50)
	declare @childEntityColumnNames nvarchar(500)
	declare @bIsNull bit
	declare @defaultVaule nvarchar(4000)
	declare @columnScale int
	
	declare @columnWidth int
	declare @basicInfoType nvarchar(50) 
	declare @dictInfoType nvarchar(50) 
	declare @basicInfoShowFieldName nvarchar(50) 
	declare @basicInfoRelationFieldName nvarchar(50) 
	declare @basicInfoKeyFieldName nvarchar(50)
	declare @gridlookUpEditShowModelJson nvarchar(4000)

	--

	--表信息
	select A1.name formCode,A1.text formText,formType, A2.parentTableName,A2.parentPrimaryKeyName,A2.id panelId, A2.type tableType, tableName,primaryKeyName,foreigntKeyName,keyType,entityColumnName,childEntityColumnNames,A3.systemDataBaseName,A3.systemCode
	into #TempTable
	from LiForm A1 
	left join LiPanel A2 on A1.id = A2.formModelId 
	left join LiSystemInfo A3 on A3.systemCode = A1.systemCode
	where A1.id = @formId


	declare licursor cursor for select formCode,formText,formType,parentTableName,parentPrimaryKeyName,panelId,tableType,tableName,primaryKeyName,foreigntKeyName,keyType,entityColumnName,childEntityColumnNames,systemDataBaseName,systemCode  from #TempTable 
	open licursor
	fetch next from licursor into @formCode,@formText,@formType,@parentTableName,@parentPrimaryKeyName,@panelId,@tableType,@tableName,@primaryKeyName,@foreigntKeyName,@keyType,@entityColumnName,@childEntityColumnNames,@dataBaseName,@systemCode
	while(@@FETCH_STATUS = 0)
	begin

		--
		declare @InsertSql nvarchar(max)
		declare @UpdateSql nvarchar(max)
		declare @entityOrder nvarchar(30)
		declare @entityColumnNameTemp nvarchar(50)
		
		set @entityOrder = ''

		if(ISNULL(@entityColumnName, '') = '')
		begin
			set @entityColumnNameTemp = 'NULL'
		end
		else
		begin
			set @entityColumnNameTemp = '''' + @entityColumnName + ''''
		end

		if(@tableType = 'Basic')
		begin
			set @entityOrder = 'master'
		end
		else
		begin
			set @entityOrder = 'slave'
		end

		
		if(@formType = '树形基础档案' or @formType = '基础档案')
		begin
			set @entityType = 'Basic'
		end
		else
		begin
			set @entityType = 'Voucher'
		end


		set @InsertSql = 'if not exists(select 1 from ' + @dataBaseName + '.dbo.sysobjects where id=object_id(''' + @dataBaseName + '.dbo.' + @tableName + ''') and type = ''U'') BEGIN CREATE TABLE ' + @dataBaseName + '.dbo.' + @tableName + ' ( ' 
		set @UpdateSql = ''
		set @ModelSql = @ModelSql+  ' if not exists (select 1 from TableInfo where entityKey = ''' + @formCode + ''' and tableName = ''' + @tableName + ''') 
							BEGIN 
							insert into TableInfo (systemCode,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
							select ''' + @systemCode + ''',''' + @dataBaseName + ''',''' + @entityType + ''',''' + @formCode + ''',''' + @entityOrder + ''',' + @entityColumnNameTemp + ',''' + @tableName + ''',''' + @formCode + ''',''' + @formText + ''', null,''JsonModel'',''' + @primaryKeyName + ''',null,getdate()
							set @tableId = @@identity
							END
							ELSE
							BEGIN
							UPDATE TableInfo SET systemCode = ''' + @systemCode + ''',dataBaseName = ''' + @dataBaseName + ''',entityOrder = ''' + @entityOrder + ''',tableAliasName = ''' + @formCode + ''',tableAbbName = ''' + @formText + ''',keyName=''' + @primaryKeyName + ''' where entityKey = ''' + @formCode + ''' and tableName = ''' + @tableName + '''
							
							select top 1  @tableId = id from TableInfo where entityKey = ''' + @formCode + ''' and tableName = ''' + @tableName + '''
							END '

		declare @primaryKeyType nvarchar(20)
		declare @databaseGeneratedType nvarchar(10)
		if(@keyType = 'Identity')
		begin
			set @primaryKeyType = 'int'
			set @databaseGeneratedType = '1'

			if(@tableType = 'Basic')
			begin 
				set @parentDatabaseGeneratedType = '1'
			end
		end
		else
		begin
			set @primaryKeyType = 'uniqueidentifier'
			set @databaseGeneratedType = '3'
			
			if(@tableType = 'Basic')
			begin 
				set @parentDatabaseGeneratedType = '3'
			end
		end

		set @ModelSql = @ModelSql + ' if not exists (select 1 from ColumnInfo where fid = @tableId and columnName = ''' + @primaryKeyName + ''' )
							BEGIN
							--relationshipType要为0
							insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
							,basicInfoType,dictInfoType,basicInfoShowFieldName,basicInfoRelationFieldName,basicInfoKeyFieldName,columnWidth,controlType,gridlookUpEditShowModelJson) 
							select @tableId, ''' + @primaryKeyName + ''',''主键'',''' + @primaryKeyType + ''',9,1,0,0,' + @databaseGeneratedType + ',null,0,null
							,null,null,null,null,null,null,''Key'',null
							END
							ELSE
							BEGIN
							update ColumnInfo set controlType = ''Key''  where fid =  @tableId  and columnName = ''' + @primaryKeyName + '''
							END
							'

		if(@keyType = 'Identity')
		begin
		set @InsertSql = @InsertSql + @primaryKeyName + ' int identity(1,1) primary key not null,'
		end
		else
		begin
		set @InsertSql = @InsertSql + @primaryKeyName + ' Uniqueidentifier primary key not null,'
		end

		if(@formType = '树形基础档案')
		BEGIN
			set @ModelSql = @ModelSql + ' if not exists (select 1 from ColumnInfo where fid = @tableId and columnName = ''ParentID'' )
								BEGIN
								--relationshipType要为0
								insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
								,basicInfoType,dictInfoType,basicInfoShowFieldName,basicInfoRelationFieldName,basicInfoKeyFieldName,columnWidth,controlType,gridlookUpEditShowModelJson)  
								select @tableId, ''ParentID'',''树形父键'',''' + @primaryKeyType + ''',9,0,0,0,0,null,0,null
								,null,null,null,null,null,null,''Key'',null

								END
								ELSE
								BEGIN
								update ColumnInfo set controlType = ''Key''  where fid =  @tableId  and columnName = ''ParentID''
								END'

			if(@keyType = 'Identity')
			begin
			set @InsertSql = @InsertSql + 'ParentID int null,'
			end
			else
			begin
			set @InsertSql = @InsertSql + 'ParentID Uniqueidentifier null,'
			end
		END

		if(@tableType <> 'Basic')
		begin
			declare @foreigntKeyType nvarchar(20)
			declare @foreigntDatabaseGeneratedType nvarchar(10)
			if(@keyType = 'Identity')
			begin
				set @foreigntKeyType = 'int'
				set @foreigntDatabaseGeneratedType = '1'
			end
			else
			begin
				set @foreigntKeyType = 'uniqueidentifier'
				set @foreigntDatabaseGeneratedType = '3'
			end

			set @ModelSql = @ModelSql + ' if not exists (select 1 from ColumnInfo where fid = @tableId and columnName = ''' + @foreigntKeyName + ''' )
							BEGIN
							--relationshipType要为0
							insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
							,basicInfoType,dictInfoType,basicInfoShowFieldName,basicInfoRelationFieldName,basicInfoKeyFieldName,columnWidth,controlType,gridlookUpEditShowModelJson)  
							select @tableId,''' + @foreigntKeyName + ''',''外键'',''' + @foreigntKeyType + ''',9,0,1,0,'+ @foreigntDatabaseGeneratedType + ',''' + @parentPrimaryKeyName + ''',' + @parentDatabaseGeneratedType + ',''' + @parentTableName + '''
							,null,null,null,null,null,null,''Key'',null
							
							END
							ELSE
							BEGIN
							update ColumnInfo set controlType = ''Key'' where fid =  @tableId  and columnName = ''' + @foreigntKeyName + '''
							END'

			set @InsertSql = @InsertSql + @foreigntKeyName 
			if(@keyType = 'Identity')
			begin
				set @InsertSql = @InsertSql + ' int '
			end
			else
			begin
				set @InsertSql = @InsertSql + ' Uniqueidentifier '
			end
			set @InsertSql = @InsertSql + 'REFERENCES ' + @parentTableName + '('+ @parentPrimaryKeyName+') NOT NULL,'
		end
		
			
		if(ISNULL(@childEntityColumnNames, '') <> '')
		begin
			if(charindex(',',@childEntityColumnNames)=0)
			begin
				set @ModelSql = @ModelSql + ' if not exists (select 1 from ColumnInfo where fid = @tableId and columnName = ''' + @childEntityColumnNames + ''' )
				BEGIN
				insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
				,basicInfoType,dictInfoType,basicInfoShowFieldName,basicInfoRelationFieldName,basicInfoKeyFieldName,columnWidth,controlType,gridlookUpEditShowModelJson)  
				select @tableId,''' + @childEntityColumnNames + ''',''数据集'',''collection'', -1,0,0,2,0,null,0,null
				,null,null,null,null,null,null,''Collection'',null
				
				END
				ELSE
				BEGIN
				update ColumnInfo set controlType = ''Collection'' where fid =  @tableId  and columnName = ''' + @childEntityColumnNames + '''
				END'
			end
			else
			begin
				while charindex(',',@childEntityColumnNames)>0
				begin
					declare @TempChildEntityColumnNames nvarchar(30)
					set @TempChildEntityColumnNames =left(@childEntityColumnNames,charindex(',',@childEntityColumnNames)-1)
					set @ModelSql = @ModelSql + ' if not exists (select 1 from ColumnInfo where fid = @tableId and columnName = ''' + @TempChildEntityColumnNames + ''' )
					BEGIN
					insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
					,basicInfoType,dictInfoType,basicInfoShowFieldName,basicInfoRelationFieldName,basicInfoKeyFieldName,columnWidth,controlType,gridlookUpEditShowModelJson)  
					select @tableId,''' + @TempChildEntityColumnNames + ''',''数据集'',''collection'', -1,0,0,2,0,null,0,null
					,null,null,null,null,null,null,''Collection'',null
				
					END
					ELSE
					BEGIN
					update ColumnInfo set controlType = ''Collection'' where fid =  @tableId  and columnName = ''' + @TempChildEntityColumnNames + '''
					END'

					set @childEntityColumnNames=right(@childEntityColumnNames,len(@childEntityColumnNames)-charindex(',',@childEntityColumnNames))
					if(charindex(',',@childEntityColumnNames)<=0)
					begin
						set @ModelSql = @ModelSql + ' if not exists (select 1 from ColumnInfo where fid = @tableId and columnName = ''' + @childEntityColumnNames + ''' )
						BEGIN
						insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
						,basicInfoType,dictInfoType,basicInfoShowFieldName,basicInfoRelationFieldName,basicInfoKeyFieldName,columnWidth,controlType,gridlookUpEditShowModelJson)  
						select @tableId,''' + @childEntityColumnNames + ''',''数据集'',''collection'', -1,0,0,2,0,null,0,null
						,null,null,null,null,null,null,''Collection'',null
				
						END
						ELSE
						BEGIN
						update ColumnInfo set controlType = ''Collection'' where fid =  @tableId  and columnName = ''' + @childEntityColumnNames + '''
						END'
					end

				end 

			end
		end
		-----------列信息
		IF Object_id('Tempdb..#TempColumn') IS NOT NULL
		BEGIN
		DROP TABLE #TempColumn
		END

		select A4.name columnName,A4.text columnDesc,A4.controltype columnType,A4.length columnLength,A4.bIsNull,A4.defaultVaule,A4.scale columnScale
		,A4.width columnWidth,A4.basicInfoKey basicInfoType,A4.dictInfoType
		,case when A4.controltype = 'GridLookUpEditRefAssist' then A4.basicInfoAssistFieldName else A4.basicInfoShowFieldName end basicInfoShowFieldName
		,case when A4.controltype = 'GridLookUpEditRefAssist' then A4.basicInfoAssistType else A4.basicInfoTableKey end basicInfoRelationFieldName
		,case when A4.controltype = 'GridLookUpEditRefAssist' then A4.basicInfoAssistType else A4.basicInfoTableKey end basicInfoKeyFieldName
		,A4.gridlookUpEditShowModelJson
		into #TempColumn
		from LiForm A1 
		left join LiPanel A2 on A1.id = A2.formModelId 
		left join LiControlGroup A3 on A2.id = A3.panelModelId
		left join LiControl A4 on A3.id = A4.controlGroupId
		where A2.id = @panelId

		declare licursorColumn cursor for select columnName,columnDesc,columnType,columnLength,bIsNull,defaultVaule,columnScale
													,columnWidth,basicInfoType,dictInfoType,basicInfoShowFieldName
													,basicInfoRelationFieldName,basicInfoKeyFieldName,gridlookUpEditShowModelJson from #TempColumn 
		open licursorColumn
		fetch next from licursorColumn into @columnName,@columnDesc,@columnType,@columnLength,@bIsNull,@defaultVaule,@columnScale
												,@columnWidth,@basicInfoType,@dictInfoType,@basicInfoShowFieldName
													,@basicInfoRelationFieldName,@basicInfoKeyFieldName,@gridlookUpEditShowModelJson
		while(@@FETCH_STATUS = 0)
		begin
		if(@columnType is not null  )
		begin
		
			declare @columnType_ModelSql nvarchar(50)
			declare @length_ModelSql nvarchar(20)
			set @columnType_ModelSql = ''
			
			set @InsertSql = @InsertSql + @columnName
			set @UpdateSql = @UpdateSql + ' if not exists( select 1 from ' + @dataBaseName + '.dbo.syscolumns where id= object_id(''' + @dataBaseName + '.dbo.' + @tableName + ''') and name='''+ @columnName + ''') BEGIN  ALTER TABLE ' + @dataBaseName + '.dbo.' + @tableName + ' ADD ' + @columnName


			if(@columnType = 'IntEdit')
			begin
				set @InsertSql = @InsertSql + ' int'
				set @UpdateSql = @UpdateSql + ' int'
				set @columnType_ModelSql = 'int'
				set @length_ModelSql = '9'
			end
			
			if(@columnType = 'DecimalEdit')
			begin
				set @InsertSql = @InsertSql + ' decimal('+ cast(@columnLength as nvarchar(20)) + ',' + cast(@columnScale as nvarchar(20)) + ')'
				set @UpdateSql = @UpdateSql + ' decimal('+ cast(@columnLength as nvarchar(20)) + ',' + cast(@columnScale as nvarchar(20)) + ')'
				set @columnType_ModelSql = 'decimal'
				set @length_ModelSql = cast(@columnLength as nvarchar(20))
			end
			
			if(@columnType = 'TextEdit' or @columnType = 'VoucherCodeEdit' or @columnType = 'UserEdit')
			begin
				set @InsertSql = @InsertSql + ' nvarchar('+ cast(@columnLength as nvarchar(20)) + ')'
				set @UpdateSql = @UpdateSql + ' nvarchar('+ cast(@columnLength as nvarchar(20)) + ')'
				set @columnType_ModelSql = 'nvarchar'
				set @length_ModelSql = cast(@columnLength as nvarchar(20))
			end

			
			if(@columnType = 'CheckEdit')
			begin
				set @InsertSql = @InsertSql + ' bit'
				set @UpdateSql = @UpdateSql + ' bit'
				set @columnType_ModelSql = 'bit'
				set @length_ModelSql = '1'
			end
			
			if(@columnType = 'MemoEdit')
			begin
				set @InsertSql = @InsertSql + ' nvarchar(max)'
				set @UpdateSql = @UpdateSql + ' nvarchar(max)'
				set @columnType_ModelSql = 'nvarchar'
				set @length_ModelSql = '9999'
			end
			
			if(@columnType = 'DateTimeEdit' or @columnType = 'DateEdit')
			begin
				set @InsertSql = @InsertSql + ' datetime2'
				set @UpdateSql = @UpdateSql + ' datetime2'
				set @columnType_ModelSql = 'datetime2'
				set @length_ModelSql = '0'
			end

			if(@columnType = 'TimeEdit')
			begin
				set @InsertSql = @InsertSql + ' datetime2'
				set @UpdateSql = @UpdateSql +' datetime2'
				set @columnType_ModelSql = 'datetime2'
				set @length_ModelSql = '0'
			end
			
			if(@columnType = 'GridLookUpEditComboBox'or @columnType = 'StatusEdit')
			begin
				set @InsertSql = @InsertSql + ' nvarchar(255)'
				set @UpdateSql = @UpdateSql + ' nvarchar(255)'
				set @columnType_ModelSql = 'nvarchar'
				set @length_ModelSql = '255'
			end
			
			if( @columnType = 'GridLookUpEditRef' or @columnType = 'TreeListLookUpEdit' or @columnType = 'GridLookUpEditRefAssist' )
			begin
				set @InsertSql = @InsertSql + ' nvarchar(255)'
				set @UpdateSql = @UpdateSql + ' nvarchar(255)'
				set @columnType_ModelSql = 'nvarchar'
				set @length_ModelSql = '255'
			end
			
			if(@defaultVaule is not null)
			begin
				set @InsertSql = @InsertSql + ' default ' + @defaultVaule
				set @UpdateSql = @UpdateSql + ' default ' + @defaultVaule
			end

			--ALTER TABLE 只允许添加满足下述条件的列: 列可以包含 Null 值；或者列具有指定的 DEFAULT 定义；或者要添加的列是标识列或时间戳列；或者，如果前几个条件均未满足，则表必须为空以允许添加此列
			--if(@bIsNull = 0)
			--begin
			--	set @InsertSql = @InsertSql + ' not null '
			--	set @UpdateSql = @UpdateSql + ' not null '
			--end

			set @InsertSql = @InsertSql + ','
			set @UpdateSql = @UpdateSql + ' END'

			set @ModelSql = @ModelSql + ' if not exists (select 1 from ColumnInfo where fid = @tableId and columnName = ''' + @columnName + ''' ) 
										begin
										insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
										,basicInfoType,dictInfoType,basicInfoShowFieldName,basicInfoRelationFieldName,basicInfoKeyFieldName,columnWidth,controlType,gridlookUpEditShowModelJson
										) 
										select @tableId, ''' + @columnName + ''',''' + @columnDesc + ''', ''' + @columnType_ModelSql + ''',' + @length_ModelSql + ',0,0,0,0,null,0,null
										, ''' + ISNULL(@basicInfoType,'') + ''', ''' + ISNULL(@dictInfoType,'') + ''', ''' + ISNULL(@basicInfoShowFieldName,'') + ''', ''' + ISNULL(@basicInfoRelationFieldName,'') + ''', ''' + ISNULL(@basicInfoKeyFieldName,'') + ''', ' + CAST(@columnWidth as nvarchar(20)) + ', ''' + ISNULL(@columnType,'') + ''', ''' + ISNULL(@gridlookUpEditShowModelJson,'') + '''
										end 
										
										else
										
										begin
											update ColumnInfo set columnAbbName = ''' + @columnDesc + ''',columnType = ''' + @columnType_ModelSql + ''',length = ' + @length_ModelSql + ',
											basicInfoType = ''' + ISNULL(@basicInfoType,'') + ''',dictInfoType=''' + ISNULL(@dictInfoType,'') + ''',basicInfoShowFieldName=''' + ISNULL(@basicInfoShowFieldName,'') + ''',basicInfoRelationFieldName= ''' + ISNULL(@basicInfoRelationFieldName,'') + ''',
											basicInfoKeyFieldName=''' + ISNULL(@basicInfoKeyFieldName,'') + ''',columnWidth=''' +  CAST(@columnWidth as nvarchar(20)) + ''',controlType=''' + ISNULL(@columnType,'') + ''',gridlookUpEditShowModelJson=''' + ISNULL(@gridlookUpEditShowModelJson,'') + '''
											where fid =  @tableId  and columnName = ''' + @columnName + '''
										end'

			
		end
			fetch next from licursorColumn into @columnName,@columnDesc,@columnType,@columnLength,@bIsNull,@defaultVaule,@columnScale
												,@columnWidth,@basicInfoType,@dictInfoType,@basicInfoShowFieldName
													,@basicInfoRelationFieldName,@basicInfoKeyFieldName,@gridlookUpEditShowModelJson
		end
		close licursorColumn
		Deallocate licursorColumn

		set @InsertSql = @InsertSql + ' dModifyDate datetime default getdate() , dCreateDate datetime default getdate() )  END '
		----------------------

		fetch next from licursor into @formCode,@formText,@formType,@parentTableName,@parentPrimaryKeyName,@panelId,@tableType,@tableName,@primaryKeyName,@foreigntKeyName,@keyType,@entityColumnName,@childEntityColumnNames,@dataBaseName,@systemCode

		set @SqlAll = @SqlAll + @InsertSql + ' ELSE BEGIN ' + @UpdateSql + ' END '
	end
	close licursor
	Deallocate licursor

	set @SqlAll = @SqlAll + @ModelSql
	insert ShowResult (resultText,dDate) values (@SqlAll,getdate())
	print @SqlAll
	exec( @SqlAll)


end