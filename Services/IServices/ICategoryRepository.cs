using ListOfItems.Models.DTO;

namespace ListOfItems.Services.IServices
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> Get();
        CategoryDTO get(int id);
        
        string Create(CategoryDTO categoryDTO);

        string update(CategoryDTO categoryDTO);

        string delete(int id);

    }
}
