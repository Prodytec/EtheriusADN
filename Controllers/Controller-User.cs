using EtheriusWebAPI.Models;
using EtheriusWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable

namespace EtheriusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region CLASS LEVEL DECLARATION

        private readonly IUserRepository userRepository;
        private readonly Response response;
        
        #endregion

        #region CONSTRUCTOR DECLARATION
        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
            response = new Response();
        }
        #endregion

        #region ACTION METHODS

        /// <summary>
        /// Add User.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userResponse = await userRepository.AddUser(model);
                    if (userResponse.IsSuccess == true)
                    {
                        return Ok(userResponse);
                    }
                    else
                    {
                        return BadRequest(userResponse);
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
            response.IsSuccess = false;
            response.Message = "Invalid State";
            response.Data = "ModelState object is invalid";
            return BadRequest(response);
        }

        /// <summary>
        /// Update User.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userResponse = await userRepository.UpdateUser(model);
                    if (userResponse.IsSuccess == true)
                    {
                        return Ok(userResponse);
                    }
                    return BadRequest(userResponse);
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed";
                    response.Data = "Error Encountered: " + ex;
                    return BadRequest(response);
                }
            }
            response.IsSuccess = false;
            response.Message = "Invalid State";
            response.Data = "ModelState object is invalid";
            return BadRequest(response);
        }

        /// <summary>
        /// Get User by ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(string email, string password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userRepository.GetUserWithRolesAndPermissions(email,password);
                    if (user != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Retrieved User Successfully";
                        response.Data = user;
                        return Ok(response);
                    }
                    else 
                    {
                        response.IsSuccess = false;
                        response.Message = "User Not Found";
                        response.Data = "";
                        return BadRequest(response);
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
            response.IsSuccess = false;
            response.Message = "Invalid State";
            response.Data = "ModelState object is invalid";
            return BadRequest(response);
        }

        #endregion
    }
}
