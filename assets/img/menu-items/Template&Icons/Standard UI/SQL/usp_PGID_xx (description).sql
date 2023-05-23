Use CarServiceDev
Go

If Exists (Select Name From SYSOBJECTS Where Type = 'P' and Name = 'usp_PGID_00' ) 
Drop Procedure usp_PGID_00
Go

Create Procedure usp_PGID_00
		@Parameter1 nvarchar(5),
		@Parameter2 nvarchar(6),
		@Parameter3 nvarchar(6),
		@Parameter4 nvarchar(20)                
                    	
As
Begin
 
/* Description: Stored Procedure for 

 ================================================
 [History ]
 Revision No.	1.0
 Create By:		Passakorn 
 Create Date:	2016-05-16
 Modify By:		Passakorn
 Modify Date:	2016-05-16
 Comment:		usp_PGID_00 (Description)

 ================================================
*/

SET NOCOUNT ON

-- usp_PGID_00 '01','255906','255907','passakorn'


BEGIN TRY
	BEGIN TRANSACTION    -- Start the transaction

	-- Transaction 1
	DELETE FROM [TableName]
	 WHERE Field1 = @Parameter1
	   AND Field2 = @Parameter2
	   AND Field3 = @Parameter3
	   AND Field4 = @Parameter4

	-- Transaction 2
	INSERT INTO [TableName]
	  (Field1, Field2, Field3, Field4)	
	  VALUES(@Parameter1, @Parameter2, @Parameter3, @Parameter4)

	-- If we reach here, success!
	COMMIT
END TRY
BEGIN CATCH
	-- Whoops, there was an error
	IF @@TRANCOUNT > 0
		ROLLBACK

	-- Raise an error with the details of the exception
	DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
	SELECT @ErrMsg = ERROR_MESSAGE(),
		 @ErrSeverity = ERROR_SEVERITY()

	RAISERROR(@ErrMsg, @ErrSeverity, 1)
END CATCH

/* ---------- End of Query Script ---------- */
RETURN
END
GO
