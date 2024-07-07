alter procedure Usp_ItemList(
@Flag int,
@ItemId int=0,
@CategoryId int=0,
@SubCategoryId int=0,
@ItemName varchar(200)='',
@ItemDesc varchar(1000)='',
@Price Numeric(18,0)=0,
@discount numeric(18,0)=0,
@OutputMessage varchar(max)='' output)
as
begin
	if @Flag=1
	begin
		select IM.ItemId as ItemId,IM.CategoryId as CategoryId,CM.CategoryName as CategoryName,IM.SubCategoryId as SubCategoryId,SM.SubCategoryName as SubCategoryName,IM.ItemName as ItemName,IM.ItemDesc as ItemDesc,IM.Price as Price,IM.Discount as Discount,IM.CreatedDate as CreatedDate from ItemMaster IM
		inner join Category_Master CM on IM.CategoryId= CM.CategoryId
		inner join SubCategory_Master SM on SM.SubCategoryId=IM.SubCategoryId
	end
	if @flag=2
	begin
		select IM.ItemId,IM.CategoryId,CM.CategoryName,IM.SubCategoryId,SM.SubCategoryName,IM.ItemName,IM.ItemDesc,IM.Price,
		IM.Discount from ItemMaster IM inner join Category_Master CM on CM.CategoryId=IM.CategoryId
		inner join SubCategory_Master SM on SM.SubCategoryId=IM.SubCategoryId
		where ItemId=@ItemId
	end
	if @flag=3
	begin
		insert into ItemMaster(CategoryId,SubCategoryId,ItemName,ItemDesc,Price,Discount,CreatedDate)
		values (@CategoryId,@SubCategoryId,@ItemName,@ItemDesc,@Price,@discount,getdate())
		set @OutputMessage='Data Saved'
		select 'Data Saved' as Msg,0 as Status
	end
	if @flag=4
	begin
		update ItemMaster set CategoryId=@CategoryId,
							  SubCategoryId=@SubCategoryId,
							  ItemName=@ItemName,
							  ItemDesc=@ItemDesc,
							  Price=@Price,
							  Discount=@discount
				where ItemId=@ItemId
		set @OutputMessage='Data Updated '
		select 'Data Updated' as Msg,0 as Status
	end
	if @flag=5
	begin
	if exists (select * from ItemMaster where ItemId=@ItemId)
	begin
		delete from ItemMaster where ItemId=@ItemId
	end
	else
	begin
		set @OutputMessage='No Data To Delete'
		select 'No Data To delete' as Msg,0 as Status
	end
	end
end

--select * from Category_Master
--select * from SubCategory_Master

--insert into SubCategory_Master(SubCategoryName,Avaliable,CreatedDate,CategoryId)
--values('Ghee & Oils',1,getdate(),3),
--('Masalas & Spices',1,getdate(),3),
--('Soft Drinks',1,getdate(),3),
--('Health Drink Mix',1,getdate(),3),
--('Rice & Rice Products',1,getdate(),3)