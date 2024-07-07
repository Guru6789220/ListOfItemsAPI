using ListOfItems.Models.DTO;

namespace ListOfItems.Services.IServices
{
    public interface IAuthentiation
    {
        LoginDTO Login(LoginDTO login);

        (string token,DateTime expires) GenertateToken(LoginDTO login);
    }
}
