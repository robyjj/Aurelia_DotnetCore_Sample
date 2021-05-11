using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Hahn.ApplicatonProcess.February2021.Models;
using Hahn.ApplicatonProcess.February2021.Domain.Contracts;
using Hahn.ApplicatonProcess.February2021.API.Filters;

namespace Hahn.ApplicatonProcess.February2021.API.Controllers
{
    [ApiVersion("1.0")]    
    [Route("Assets")]
    [ApiController]
    [TypeFilter(typeof(AuditActionFilter))]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;            
        }

        /// <summary>
        /// Returns the complete list of assets
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var assets = await _assetService.GetAssets();
            return Ok(assets);
        }



        /// <summary>
        /// Filters the list of assets based on the parameters
        /// </summary>
        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> Get([FromQuery] AssetParameters assetParameters)
        {
            var assets = await _assetService.GetFilteredAssets(assetParameters);
            return Ok(assets);
        }
        
        /// <summary>
        /// Returns a single asset information based on the Asset ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var asset = await _assetService.GetAssetById(id);
            if (asset == null)
            {
                return NotFound();
            }
            return Ok(asset);
        }

        /// <summary>
        /// Creates an Asset
        /// </summary>
        /// <remarks>
        /// Sample request:        
        /// 
        ///     POST /Asset
        ///     {        
        ///       "assetName": "Asset - Test",
        ///       "department": 1,
        ///       "countryOfDepartment": "Germany",        
        ///       "eMailAdressOfDepartment": "germany@gmail.com",
        ///       "purchaseDate": "2021-05-01",
        ///       "broken": false,
        ///     }
        /// </remarks>
        /// <param name="asset"></param>        
        [HttpPost]
        public async Task<ActionResult<Asset>> Post([FromBody] Asset asset)
        {            
            await _assetService.CreateAsset(asset);
            return CreatedAtAction(
                "Get",
                new { id = asset.ID },
                asset
                );
        }

        /// <summary>
        /// Updates an Asset
        /// </summary>
        /// <remarks>
        /// Sample request:        
        /// 
        ///     PUT /Asset/2
        ///     {        
        ///       "assetName": "Asset - Test",
        ///       "department": 1,
        ///       "countryOfDepartment": "Germany",        
        ///       "eMailAdressOfDepartment": "germany@gmail.com",
        ///       "purchaseDate": "2021-05-01",
        ///       "broken": false,
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Asset asset)
        {
            if (id != asset.ID)
            {
                return BadRequest("Invalid Input");
            }
            try
            {
                await _assetService.UpdateAsset(id, asset);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }

        /// <summary>
        /// Deletes an asset based on ID
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Asset>> Delete(int id)
        {
            Asset asset = null;
            try
            {
                asset = await _assetService.DeleteAsset(id);
                if (asset == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return asset;
        }

        /// <summary>        
        /// Deletes multiple assets based on input ID's
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteMultiple")]
        public async Task<ActionResult<List<Asset>>> DeleteMuliple([FromQuery] int[] ids)
        {
            List<Asset> assets = null;
            try
            {
                assets = await _assetService.DeleteMultiple(ids);
                if (assets == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(assets);
        }
    }
}