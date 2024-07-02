using ListOfItems.Models.DTO;

namespace ListOfItems.Services.IServices
{
    public interface IItemRepository
    {
        List<ItemListDTO> getall();
    }
}
