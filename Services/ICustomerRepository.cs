using EtheriusWebAPI.Models;
using EtheriusWebAPI.Models.Entity;
# pragma warning disable
namespace EtheriusWebAPI.Services
{
    public interface ICustomerRepository
    {
        #region INTERFACES METHODS
        Task<Response> AddCustomer(CustomerModel customer);

        Task<Response> UpdateCustomer(CustomerModel customer);

        Task<Response> GetCustomers();

        Task<Response> GetStates();

        Task<Response> GetCities();

        Task<Response> GetCategories();

        Task<Response> GetTaxes();

        Task<Response> GetCustomerById(int customerId);

        Task<Response> ChangeCustomerStatus(int customerId, int status);

        Task<Response> AddCity(List<string> name);

        Task<Response> AddState(List<string> name);

        Task<Response> AddTax(List<string> name);

        Task<Response> AddCategory(List<string> name);

        #endregion
    }
}
