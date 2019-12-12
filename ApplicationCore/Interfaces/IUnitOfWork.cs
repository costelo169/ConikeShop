namespace ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {
         IProductRepository Products {get;}
         int Complete();
    }
}