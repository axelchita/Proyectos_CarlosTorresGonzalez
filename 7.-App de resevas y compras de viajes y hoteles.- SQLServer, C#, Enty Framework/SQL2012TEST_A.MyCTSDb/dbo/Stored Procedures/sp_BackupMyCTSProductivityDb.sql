﻿-- =============================================
-- Autor:         Eduardo Vázquez Orozco
-- Fecha creacion: 18 de Abril
-- Modifico:            
-- Fecha modificacion:
-- Descripcion:   PA para busqueda e inserción de datos de la tabla 
--					DetailsPNRHotelTemp de Dev hacia DetailsPNRHotel en otro servidor
-- =============================================
CREATE Procedure sp_BackupMyCTSProductivityDb
as
begin    
	SET XACT_ABORT ON 
	Set NoCount On;
	Declare
    -- Parámetros del SP
		@Record varchar(50),
		@Confirmation_Number varchar(50),
		@Hotel varchar(50),
		@Provid_Direc_Pay bit,
		@DK varchar(50),
		@Phone varchar(50),
		@RFC varchar(50),
		@Mail varchar(50),
		@Request varchar(50),
		@Payment_form varchar(50),
		@Car_Number int,
		@Sales_Source varchar(50),
		@Operational_Unit varchar(50),
		@User varchar(50) ,
		@Service_Type varchar(50) ,
		@Provider varchar(50) ,
		@Site varchar(50) ,
		@In_Date datetime ,
		@Out_Date datetime ,
		@City varchar(50) ,
		@Room varchar(50) ,
		@Room_Type varchar(50) ,
		@Meal_Plan varchar(50) ,
		@Number_Nights int ,
		@Passenger_Name varchar(50) ,
		@Passenger_Number int ,
		@Surname varchar(50) ,
		@Title varchar(50) ,
		@Passenger_Type varchar(50) ,
		@Rate varchar(50) ,
		@Currency varchar(10) ,
		@Provider_Commission float ,
		@Cost float ,
		@Price float ,
		@Taxes float ,
		@Created_Date datetime ,
		@Time_Limit datetime ,
		@Status varchar(50) ,
		@Cancel_Number varchar(50) ,
		@ChainCode varchar(50) ,
		@ChangeType float ,
		@OrgId int ,
		@Pcc varchar(10) ,
		@RemarksString varchar(3000) ,
		@UserName varchar(50),
		@msg as nvarchar(100)

		begin try
			select 
				@Record=Record,
				@Confirmation_Number=Confirmation_Number, 
				@Hotel=Hotel, 
				@Provid_Direc_Pay=Provid_Direc_Pay,
				@DK=DK,
				@Phone=Phone,
				@RFC=RFC,
				@Mail=Mail,
				@Request=Request,
				@Payment_form=Payment_form,
				@Car_Number=Car_Number,
				@Sales_Source=Sales_Source,	
				@Operational_Unit=Operational_Unit,
				@User=[User],
				@Service_Type=Service_Type,
				@Provider=Provider,
				@Site=Site,
				@In_Date=In_Date,
				@Out_Date=Out_Date,
				@City=City,
				@Room=Room,
				@Room_Type=Room_Type,
				@Meal_Plan=Meal_Plan,
				@Number_Nights=Number_Nights,
				@Passenger_Name=Passenger_Name,
				@Passenger_Number=Passenger_Number,
				@Surname=Surname,
				@Title=Title,
				@Passenger_Type=Passenger_Type,
				@Rate=Rate,
				@Currency=Currency,
				@Provider_Commission=Provider_Commission,
				@Cost=Cost,
				@Price=Price,
				@Taxes=Taxes,
				@Created_Date=Created_Date,
				@Time_Limit=Time_Limit,
				@Status=Status,
				@Cancel_Number=Cancel_Number,
				@ChainCode=ChainCode,
				@ChangeType=ChangeType,
				@OrgId=OrgId,
				@Pcc=Pcc,
				@RemarksString=RemarksString,
				@UserName=UserName 
			from [200.52.81.200\SQL2005DEV].[MyCTSDb].dbo.DetailsPNRHotelTemp where ID=ID
 
			insert into [MyCTSDb].[dbo].[DetailsPNRHotel] --Agregar el nombre del servidor al cual se mandara los datos para hacer pruebas.
				(											  --Actualmente esta apuntando a desarrollo	"[servidor][base datos][dbo][tabla]"
					Record,
					Confirmation_Number,
					Hotel,
					Provid_Direc_Pay,
					DK,
					Phone,
					RFC,
					Mail,
					Request,
					Payment_form,
					Car_Number,
					Sales_Source,
					Operational_Unit,
					[User],
					Service_Type,
					Provider,
					Site,
					In_Date,
					Out_Date,
					City,
					Room,
					Room_Type,
					Meal_Plan,
					Number_Nights,
					Passenger_Name,
					Passenger_Number,
					Surname,
					Title,
					Passenger_Type,
					Rate,
					Currency,
					Provider_Commission,
					Cost,
					Price,
					Taxes,
					Created_Date,
					Time_Limit,
					Status,Cancel_Number,ChainCode,ChangeType,OrgId,Pcc,RemarksString,UserName
				)
			values
				(
					@Record,
					@Confirmation_Number,
					@Hotel,
					@Provid_Direc_Pay,
					@DK,
					@Phone,
					@RFC,
					@Mail,
					@Request,
					@Payment_form,
					@Car_Number,
					@Sales_Source,
					@Operational_Unit,
					@User,
					@Service_Type,
					@Provider,
					@Site,
					@In_Date,
					@Out_Date,
					@City,
					@Room,
					@Room_Type,
					@Meal_Plan,
					@Number_Nights,
					@Passenger_Name,
					@Passenger_Number,
					@Surname,
					@Title,
					@Passenger_Type,
					@Rate,	
					@Currency,
					@Provider_Commission,
					@Cost,
					@Price,
					@Taxes,
					@Created_Date,
					@Time_Limit,
					@Status,
					@Cancel_Number,
					@ChainCode,
					@ChangeType,
					@OrgId,
					@Pcc,
					@RemarksString,
					@UserName
				)
			delete [200.52.81.200\SQL2005DEV].[MyCTSDb].dbo.DetailsPNRHotelTemp
		end try
		begin catch
			PRINT 'Ocurrio un Error: ' + error_message() + ' en la línea ' + convert(nvarchar(255), error_line() ) + '.'			
		end catch
end