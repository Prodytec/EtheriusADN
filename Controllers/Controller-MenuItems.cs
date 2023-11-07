using EtheriusWebAPI.Models;
using EtheriusWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
# pragma warning disable
namespace EtheriusWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        #region CLASS LEVEL DECLARATION

        private readonly IMenuItemRepository menuItemRepository;
        private readonly Models.Response response;

        #endregion

        #region CONSTRUCTOR DECLARATION
        public MenuItemController(IMenuItemRepository _menuItemRepository)
        {
            menuItemRepository = _menuItemRepository;
            response = new Models.Response();
        }

        #endregion

        #region ACTION METHODS

        /// <summary>
        /// Add Menu.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMenuItem")]
        public async Task<IActionResult> AddMenuItem([FromBody] List<MenuItemModel> model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var menuResponse = await menuItemRepository.AddMenu(model);
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
        /// Get All MenuItem
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GelAllMenuItems")]
        public async Task<IActionResult> GelAllMenuItems()
        {
            try
            {
                var menuResponse = await menuItemRepository.GetAllMenuItems();
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

        #endregion
    }
}