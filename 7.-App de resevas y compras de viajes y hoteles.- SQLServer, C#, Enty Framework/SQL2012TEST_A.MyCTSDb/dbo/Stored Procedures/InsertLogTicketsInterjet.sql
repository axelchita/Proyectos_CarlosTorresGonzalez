﻿-- =============================================
-- Author:		Jessica Gutierrez Becerril
-- Create date: 06/01/14
-- Description:	Insertar los tickets de Interjet
-- =============================================
CREATE PROCEDURE [dbo].[InsertLogTicketsInterjet]
@TicketNumber as varchar(50),
@PNR_Airline as varchar(10),
@PNR_Sabre as varchar(10)
AS
BEGIN
	insert into [dbo].[Log_Tickets_Interjet]
	values(GETDATE(),@PNR_Airline,@PNR_Sabre,@TicketNumber)

END