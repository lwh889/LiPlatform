--exec sp_getVoucherCode 'Sale', 'AABBCC', '20200331','2020-03-31'
alter procedure sp_getVoucherCode(
@entityKey nvarchar(50),
@fieldTextValue nvarchar(50),
@fieldDateValue nvarchar(50),
@dateValue datetime = null
)
as
BEGIN
declare @voucherCodeName nvarchar(30) --前缀
declare @prefixName nvarchar(10) --前缀
declare @iZero int 	--左边补0
declare @iStep int 	--流水号步位
declare @flowNoRange  nvarchar(10)
declare @iFlowNo int

declare @iYear int
declare @iMonth int
declare @iDay int

--获取单据编号方案
select @voucherCodeName = [name], @prefixName = prefixName,@iZero = iZero,@iStep = iStep,@flowNoRange = flowNoRange from LiVoucherCode where entityKey = @entityKey and bDefault = 1

--判断时间号
if(@dateValue is not null)
BEGIN
	if(@flowNoRange = 'DAY')
	BEGIN
		set @iDay = DATEPART(DAY, @dateValue)
		set @iMonth = DATEPART(MONTH, @dateValue)
		set @iYear = DATEPART(YEAR, @dateValue)
	END
	ELSE IF(@flowNoRange = 'MONTH')
	BEGIN
		set @iMonth = DATEPART(MONTH, @dateValue)
		set @iYear = DATEPART(YEAR, @dateValue)
	END
	ELSE IF(@flowNoRange = 'YEAR')
	BEGIN
		set @iYear = DATEPART(YEAR, @dateValue)
	END
END


if not exists (select 1 from LiFlowNo where entityKey = @entityKey and voucherCodeName =@voucherCodeName and iYear = @iYear and iMonth = @iMonth and iDay = @iDay)
BEGIN
	set @iFlowNo = 1
	INSERT INTO LiFlowNo (entityKey,voucherCodeName,iYear,iMonth,iDay,iFlow,modifyDate)
	VALUES (@entityKey,@voucherCodeName,@iYear,@iMonth,@iDay,@iFlowNo,GETDATE())
END
ELSE
BEGIN
	SELECT @iFlowNo = iFlow + @iStep FROM LiFlowNo WHERE entityKey = @entityKey and voucherCodeName =@voucherCodeName and iYear = @iYear and iMonth = @iMonth and iDay = @iDay
	UPDATE LiFlowNo SET iFlow = @iFlowNo,modifyDate = GETDATE()   WHERE entityKey = @entityKey and voucherCodeName =@voucherCodeName and iYear = @iYear and iMonth = @iMonth and iDay = @iDay
END

select ISNULL(@prefixName,'') + ISNULL(@fieldTextValue,'')+ ISNULL(@fieldDateValue,'') + RIGHT('000000000000000000' + CAST(@iFlowNo as nvarchar(9)),@iZero) VoucherNo

END