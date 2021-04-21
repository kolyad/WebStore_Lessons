using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface ICartService
    {
        void Add(int id);

        void Decrement(int id);

        void Delete(int id);

        void Clear();

        CartViewModel GetViewModel();
    }
}
