CREATE PROCEDURE [dbo].[RecordingSelectCommand]
(
	@CallReference varchar(20)
)
AS
	SET NOCOUNT ON;
Select


convert(varchar,c.act_date + CAST(STUFF(STUFF(RIGHT('00000000' + CAST(c.act_time AS VARCHAR),6),3,0,':'),6,0,':')AS TIME),103) + ' - ' +
coalesce(t.sname,c.tsr) COLLATE DATABASE_DEFAULT + ' - ' +
convert(varchar,coalesce(time_connect,0)) + ' seconds' Description,
r.file_name FileName,
c.act_date CallDate

from Noble.dbo.call_history c
left join Noble.dbo.rec_playint r
on nullif(c.d_record_id,0) = r.d_record_id
left join Noble.dbo.appl_status as1
on c.status = as1.status and c.appl = as1.appl
left join Noble.dbo.appl_sys_status as2
on c.status = as2.status
left join Noble.dbo.addistats ad
on c.appl = ad.pappl and c.status = ad.pstatus and c.addi_status = ad.addistatus
left join Noble.dbo.tsrmaster t
on c.tsr = t.tsr
where c.lm_filler2 = @CallReference
and r.file_name is not null and c.tsr is not null
and c.time_connect > 0

order by c.act_date desc