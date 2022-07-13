using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rusada.Aviation.API.Extensions;
using Rusada.Aviation.Core.Contracts.Requests;
using Rusada.Aviation.Core.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rusada.Aviation.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SightingController : BaseController
    {
        private readonly ISightingService _sightingService;

        public SightingController(ISightingService sightingService)
        {
            _sightingService = sightingService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSighting(SightingModel sightingModel)
        {
            if (ModelState.IsValid)
            {
                var sighting = await _sightingService.SaveSightingAsync(sightingModel);
                return Ok(ApiResponse.GenerateResponse(true, sighting, null));
            }
            else { return BadRequest(ApiResponse.GenerateResponse(true, ModelState, new List<string> { "Invalid Data." })); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _sightingService.DeleteAsync(id);
            return Ok(ApiResponse.GenerateResponse(true, result, null));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SightingModel sightingModel)
        {
            var sighting = await _sightingService.UpdateAsync(sightingModel);
            return Ok(ApiResponse.GenerateResponse(true, sightingModel, null));
        }

        [HttpPost("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedData([FromBody] DataTableRequestModel dataTableRequest)
        {
            var sightingList = await _sightingService.GetPagedDataAsync(dataTableRequest);
            return Ok(sightingList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sighting = await _sightingService.GetByIdAsync(id);
            return Ok(ApiResponse.GenerateResponse(true, sighting, null));
        }
    }
}
