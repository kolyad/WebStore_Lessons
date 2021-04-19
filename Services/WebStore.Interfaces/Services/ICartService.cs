using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
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
