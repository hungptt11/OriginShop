IF EXISTS(SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'selectProductCategoryByID' AND ROUTINE_SCHEMA = 'dbo')
		DROP PROCEDURE dbo.selectProductCategoryByID
go
CREATE PROCEDURE dbo.selectProductCategoryByID
	@Id int
AS
begin
	select * from ProductCategory where Id = @Id;
end