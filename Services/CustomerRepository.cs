using EtheriusWebAPI.Models;
using EtheriusWebAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

#pragma warning disable

namespace EtheriusWebAPI.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Class Variable Declaration
        /// </summary>
        private readonly ApplicationDbContext db;
        private readonly Response response;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="_db"></param>
        public CustomerRepository(ApplicationDbContext _db)
        {
            db = _db;
            response = new Response();
        }

        /// <summary>
        /// add a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Response> AddCustomer(CustomerModel customer)
        {
            try
            {
                if (db != null)
                {
                    var existingCustomer = await db.Customer.FirstOrDefaultAsync(x => x.email == customer.email);
                    if (existingCustomer == null)
                    {

                        var newCustomer = new Customer
                        {
                            first_name = customer.first_name,
                            last_name = customer.last_name,
                            email = customer.email,
                            state = customer.state,
                            city = customer.city,
                            password = string.Empty,
                            tax_condition = customer.tax_condition,
                            is_active = 1,
                            created_date = DateTime.Now,
                            category = customer.category,
                        };
                        await db.Customer.AddAsync(newCustomer);
                        await db.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Message = "Customer Created Successfully.";
                        response.Data = newCustomer;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "User Already Exists.";
                        response.Data = existingCustomer;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// update an existing customer
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Response> UpdateCustomer(CustomerModel customer)
        {
            try
            {
                if (db != null)
                {
                    var existingCustomer = await db.Customer.FindAsync(customer.id);

                    if (existingCustomer != null)
                    {
                        // Check if the provided email already exists for a different customer
                        var emailExistsForOtherCustomer = await db.Customer
                            .AnyAsync(c => c.email == customer.email && c.id != customer.id);
                        if (!emailExistsForOtherCustomer)
                        {

                            // Update the properties of the existing customer
                            existingCustomer.first_name = customer.first_name;
                            existingCustomer.last_name = customer.last_name;
                            existingCustomer.email = customer.email;
                            existingCustomer.state = customer.state;
                            existingCustomer.tax_condition = customer.tax_condition;
                            existingCustomer.category = customer.category;
                            existingCustomer.password = string.Empty;
                            existingCustomer.city = customer.city;
                            db.Customer.Update(existingCustomer);

                            // Save the changes to the database
                            await db.SaveChangesAsync();
                            response.IsSuccess = true;
                            response.Message = "Customer Updated Successfully.";
                            response.Data = existingCustomer;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Email Already Exists.";
                            response.Data = existingCustomer;
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetCustomers()
        {
            try
            {
                if (db != null)
                {
                    var customers = await db.Customer.ToListAsync();
                    response.IsSuccess = true;
                    response.Message = "Get Customers Successfully";
                    response.Data = customers;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetCities()
        {
            try
            {
                if (db != null)
                {
                    var cities = await db.Cities.ToListAsync();
                    if (cities.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "Cities Fetched Successfully";
                        response.Data = cities;
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "No Cities Found";
                        response.Data = cities;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Get all state
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetStates()
        {
            try
            {
                if (db != null)
                {
                    var states = await db.States.ToListAsync();
                    if (states.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "States Fetched Successfully";
                        response.Data = states;
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "No States Found";
                        response.Data = states;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetCategories()
        {
            try
            {
                if (db != null)
                {
                    var categories = await db.Customer_Categories.ToListAsync();
                    if (categories.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "Categories Fetched Successfully";
                        response.Data = categories;
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "No Categories Found";
                        response.Data = categories;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Get All Taxes
        /// </summary>
        /// <returns></returns>
        public async Task<Response> GetTaxes()
        {
            try
            {
                if (db != null)
                {
                    var taxes = await db.Tax_Conditions.ToListAsync();
                    if (taxes.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "Taxes Fetched Successfully";
                        response.Data = taxes;
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "No Taxes Found";
                        response.Data = taxes;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Response> GetCustomerById(int customerId)
        {
            try
            {
                if (db != null)
                {
                    // Use FindAsync to retrieve a customer by ID asynchronously
                    var customer = await db.Customer.FindAsync(customerId);
                    response.IsSuccess = true;
                    response.Message = "Get Customer Successfully";
                    response.Data = customer;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Activate or deactivate a customer - 1 active , 0 deactivate
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<Response> ChangeCustomerStatus(int customerId, int status)
        {
            try
            {
                if (db != null)
                {
                    // Find the customer by ID
                    var customer = await db.Customer.FindAsync(customerId);

                    if (customer != null)
                    {
                        // Update the is_active property based on the 'activate' parameter
                        customer.is_active = status;
                        db.Customer.Update(customer);

                        // Save the changes to the database
                        await db.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Message = "Status Changed Successfully";
                        response.Data = customer;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Customer does not exist.";
                        response.Data = "";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;

        }


        /// <summary>
        /// Add City
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<Response> AddCity(List<string> name)
        {
            var createdIds = new List<int>();
            try
            {
                if (db != null)
                {
                    foreach (var citties in name)
                    {
                        var existingState = await db.Cities.FirstOrDefaultAsync(x => x.name == citties);
                        if (existingState == null)
                        {

                            var city = new Cities
                            {
                                name = citties
                            };
                            await db.Cities.AddAsync(city);
                            await db.SaveChangesAsync();
                            createdIds.Add(city.id);
                            response.IsSuccess = true;
                            response.Message = "State Added Successfully.";
                            response.Data = city;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "State Already Exists.";
                            response.Data = existingState;
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }

        /// <summary>
        /// Add State
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<Response> AddState(List<string> name)
        {
            var createdIds = new List<int>();
            try
            {
                if (db != null)
                {
                    foreach (var states in name)
                    {
                        var existingState = await db.States.FirstOrDefaultAsync(x => x.name == states);
                        if (existingState == null)
                        {

                            var newState = new States
                            {
                                name = states
                            };
                            await db.States.AddAsync(newState);
                            await db.SaveChangesAsync();
                            createdIds.Add(newState.id);
                            response.IsSuccess = true;
                            response.Message = "State Added Successfully.";
                            response.Data = newState;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "State Already Exists.";
                            response.Data = existingState;
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }


        /// <summary>
        /// Add tax 
        /// </summary>
        /// <param name="tax"></param>
        /// <returns></returns>
        public async Task<Response> AddTax(List<string> name)
        {
            var createdIds = new List<int>();
            try
            {
                if (db != null)
                {
                    foreach (var taxes in name)
                    {
                        var existingTax = await db.Tax_Conditions.FirstOrDefaultAsync(x => x.name == taxes);
                        if (existingTax == null)
                        {

                            var newTax = new Tax_Conditions
                            {
                                name = taxes
                            };
                            await db.Tax_Conditions.AddAsync(newTax);
                            await db.SaveChangesAsync();
                            createdIds.Add(newTax.id);
                            response.IsSuccess = true;
                            response.Message = "Tax Added Successfully.";
                            response.Data = newTax;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Tax Already Exists.";
                            response.Data = existingTax;
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }



        /// <summary>
        /// Add categories 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<Response> AddCategory(List<string> name)
        {
            var createdIds = new List<int>();
            try
            {
                if (db != null)
                {
                    foreach (var categories in name)
                    {
                        var existingCategory = await db.Customer_Categories.FirstOrDefaultAsync(x => x.name == categories);
                        if (existingCategory == null)
                        {

                            var newCategories = new Customer_Categories
                            {
                                name = categories
                            };
                            await db.Customer_Categories.AddAsync(newCategories);
                            await db.SaveChangesAsync();
                            createdIds.Add(newCategories.id);
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Category Already Exists.";
                            response.Data = existingCategory;
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Null Exception";
                    response.Data = "DbContext has null Value.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
            }
            return response;
        }
    }
}
