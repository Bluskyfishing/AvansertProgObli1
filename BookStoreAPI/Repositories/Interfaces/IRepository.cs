namespace BookStoreAPI.Repositories.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<T>
            where T : class
        {
            Task<ICollection<T>> GetAllAsync();
        }
    }
}
