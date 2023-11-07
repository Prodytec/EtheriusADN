using EtheriusWebAPI.Models;
using EtheriusWebAPI.Models.Entity;
using EtheriusWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

# pragma warning disable

namespace EtheriusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        #region CLASS LEVEL DECLARATION

        private readonly IRoleRepository roleRepository;
        private readonly Response response;

        #endregion

        #region CONSTRUCTOR DECLARATION
        public RolesController(IRoleRepository _roleRepository)
        {
            roleRepository = _roleRepository;
            response = new Response();
        }
        #endregion

        #region ACTION METHODS

        /// <summary>
        /// Add Role.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var roleResponse = await roleRepository.AddRole(model);
                    if (roleResponse.IsSuccess == true)
                    {
                        return Ok(roleResponse);
                    }
                    else
                    {
                        return BadRequest(roleResponse);
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
        /// Update Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var roleResponse = await roleRepository.UpdateRole(model);
                    if (roleResponse.IsSuccess == true)
                    {
                        return Ok(roleResponse);
                    }
                    else
                    {
                        return BadRequest(roleResponse);
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
        /// Update Role by ID.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var menuId = await roleRepository.GetRoleById(roleId);
                    if (menuId != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Retrieved Role Successfully";
                        response.Data = menuId;
                        return Ok(response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Role Not Found";
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
            return BadRequest("Invalid Model State");
        }

        /// <summary>
        /// Change Role Status.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ActivateOrDeactivate")]
        public async Task<IActionResult> ActivateOrDeactivate(int roleId, int status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var menuResponse = await roleRepository.ChangeRoleStatus(roleId, status);
                    if (menuResponse.IsSuccess == true)
                    {
                        return Ok(menuResponse);
                    }
                    return BadRequest(menuResponse);
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
        /// Get All roles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GelAllRoles")]
        public async Task<IActionResult> GelAllRoles()
        {
            try
            {
                var roleResponse = await roleRepository.GetRoles();
                if (roleResponse.IsSuccess == true)
                {
                    return Ok(roleResponse);
                }
                return BadRequest(roleResponse);

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
