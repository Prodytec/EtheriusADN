using EtheriusWebAPI.Models;
using EtheriusWebAPI.Models.Entity;
using EtheriusWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

#pragma warning disable

namespace EtheriusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region CLASS LEVEL DECLARATION

        private readonly ICustomerRepository customerRepository;
        private readonly Models.Response response;


        #endregion

        #region CONSTRUCTOR DECLARATION 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_customerRepository"></param>
        public CustomerController(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
            response = new Models.Response();
        }

        #endregion

        #region ACTION METHODS


        /// <summary>
        /// Add Tax.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> AddCity([FromBody] List<string> names)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cityResponse = await customerRepository.AddCity(names);
                    if (cityResponse.IsSuccess)
                    {
                        return Ok(cityResponse);
                    }
                    else
                    {
                        return BadRequest(cityResponse);
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                    return BadRequest(response);
                }
            }
            return BadRequest("Invalid Model State");
        }


        /// <summary>
        /// Add Tax.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddTax")]
        public async Task<IActionResult> AddTax([FromBody] List<string> names)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var taxResponse = await customerRepository.AddTax(names);
                    if (taxResponse.IsSuccess)
                    {
                        return Ok(taxResponse);
                    }
                    else
                    {
                        return BadRequest(taxResponse);
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                    return BadRequest(response);
                }
            }
            return BadRequest("Invalid Model State");
        }

        /// <summary>
        /// Add State.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddState")]
        public async Task<IActionResult> AddState([FromBody] List<string> names)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var stateResponse = await customerRepository.AddState(names);
                    if (stateResponse.IsSuccess)
                    {
                        return Ok(stateResponse);
                    }
                    else
                    {
                        return BadRequest(stateResponse);
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                    return BadRequest(response);
                }
            }
            return BadRequest("Invalid Model State");
        }

        /// <summary>
        /// Add Category.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCategories")]
        public async Task<IActionResult> AddCategories([FromBody] List<string> names)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryResponse = await customerRepository.AddCategory(names);
                    if (categoryResponse.IsSuccess)
                    {
                        return Ok(categoryResponse);
                    }
                    else
                    {
                        return BadRequest(categoryResponse);
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                    return BadRequest(response);
                }
            }
            return BadRequest("Invalid Model State");
        }

        /// <summary>
        /// Add Customer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customerResponse = await customerRepository.AddCustomer(model);
                    if (customerResponse.IsSuccess)
                    {
                        return Ok(customerResponse);
                    }
                    else
                    {
                        return BadRequest(customerResponse);
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                    return BadRequest(response);
                }
            }
            return BadRequest("Invalid Model State");
        }

        /// <summary>
        /// Update Customer.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model != null)
                    {
                        var customerResponse = await customerRepository.UpdateCustomer(model);
                        if (customerResponse.IsSuccess == true)
                        { 
                           return Ok(customerResponse);
                        }
                        return BadRequest(customerResponse);
                    }
                    else 
                    {
                        response.IsSuccess = false;
                        response.Message = "Null Value";
                        response.Data = "model can not be null";
                        return NotFound(response);
                    }
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                    return BadRequest(response);
                }
            }
            return BadRequest("Invalid Model State");
        }

        /// <summary>
        /// Get All Customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GelAllCustomers")]
        public async Task<IActionResult> GelAllCustomers()
        {
            try
            {
                var customerResponse = await customerRepository.GetCustomers();
                if (customerResponse.IsSuccess == true)
                {
                    return Ok(customerResponse);
                }
                return BadRequest(customerResponse);
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
                return BadRequest(response); 
            }
        }


        /// <summary>
        /// Get All Cities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCities")]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                var cityResponse = await customerRepository.GetCities();
                if (cityResponse.IsSuccess == true)
                {
                    return Ok(cityResponse);
                }
                return BadRequest(cityResponse);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
                return BadRequest(response);
            }
        }


        /// <summary>
        /// Get All States.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllStates")]
        public async Task<IActionResult> GetAllStates()
        {
            try
            {
                var stateResponse = await customerRepository.GetStates();
                if (stateResponse.IsSuccess == true)
                {
                    return Ok(stateResponse);
                }
                return BadRequest(stateResponse);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
                return BadRequest(response);
            }
        }


        /// <summary>
        /// Get All Categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categoryResponse = await customerRepository.GetCategories();
                if (categoryResponse.IsSuccess == true)
                {
                    return Ok(categoryResponse);
                }
                return BadRequest(categoryResponse);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
                return BadRequest(response);
            }
        }


        /// <summary>
        /// Get All Taxes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTaxes")]
        public async Task<IActionResult> GetAllTaxes()
        {
            try
            {
                var taxeResponse = await customerRepository.GetTaxes();
                if (taxeResponse.IsSuccess == true)
                {
                    return Ok(taxeResponse);
                }
                return BadRequest(taxeResponse);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Get Customer by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            if (id == null)
            {
                response.IsSuccess = false;
                response.Message = "Null Value";
                response.Data = "id can not be null";
                return NotFound(response);
            }

            try
            {
                var customerResponse = await customerRepository.GetCustomerById(id);

                if (customerResponse.IsSuccess == true)
                {
                    return Ok(customerResponse);
                }
                return BadRequest(customerResponse);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Change Customer Status.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ActivateOrDeactivate")]
        public async Task<IActionResult> ActivateOrDeactivate(int id, int status)
        {
            if (id == null)
            {
                response.IsSuccess = false;
                response.Message = "Null Value";
                response.Data = "id can not be null";
                return NotFound(response);
            }

            try
            {
                var customerStatusResponse = await customerRepository.ChangeCustomerStatus(id, status);

                if (customerStatusResponse.IsSuccess == true)
                {
                    return Ok(customerStatusResponse);
                }
                return BadRequest(customerStatusResponse);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed";
                response.Data = "Error Encountered: " + ex;
                return BadRequest(response);
            }
        }

        #endregion
    }
}

