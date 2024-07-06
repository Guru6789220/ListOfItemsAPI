using ListOfItems.Models.DTO;

namespace ListOfItems.Services.IServices
{
    public interface IItemRepository
    {
        List<ItemListDTO> getall();
        ItemListDTO get(int id);
        string Create(ItemListDTO item);

        string Update(ItemListDTO item);

        string Delete(int id);
    }
}
