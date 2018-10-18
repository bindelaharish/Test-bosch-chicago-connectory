#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RB.JobAssistant.Data;
using RB.JobAssistant.Models;
using RB.JobAssistant.Models.Mapper;
using RB.JobAssistant.Repo;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RB.JobAssistant.Controllers
{
    [Route("api/trades")]
    public class TradesController : Controller
    {
        private readonly ILogger<TradesController> _logger;
        private readonly IRepository _repo;

        public TradesController(IRepository repository, ILogger<TradesController> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        // GET api/trades
        [HttpGet(Order = 1)]
        [SwaggerResponse(200, Type = typeof(IEnumerable<TradeModel>))]
        [SwaggerResponse(404, Description = "The source was not found")]
        [SwaggerResponse(400, Description = "Your request was not understood")]
        [SwaggerResponse(500, Description = "Oops, something broke..")]
        [Produces(typeof(IEnumerable<TradeModel>))]
        public IActionResult GetAllTrades([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("Returning the list of trade objects.");
            int total;
            var tradeData = _repo.Filter(Trade.IsValid(), out total, pageNumber, pageSize);
            var models = JobAssistantMapper.MapObjects<TradeModel>(tradeData);
            _logger.LogDebug("Pagination trade models (count): " + total);
            return Ok(models.AsEnumerable());
        }

        // POST api/trades
        [HttpPost]
        //[ProducesResponseType(typeof(TradeModel), 201)]
        //[ProducesResponseType(typeof(TradeModel), 400)]                
        public async Task<IActionResult> CreateTrade([FromBody] TradeModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var trade = JobAssistantMapper.Map<Trade>(model);
                await _repo.Create(trade);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to create trade in DB repository");
                throw;
            }

            return Ok();
        }

        // PUT api/trades/5
        [HttpPut("{tradeId:int}")]
        public async Task<IActionResult> UpdateTrade(int tradeId, [FromBody] TradeModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            try
            {
                var trade = JobAssistantMapper.Map<Trade>(model);
                await _repo.Update(trade);
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, "Failed to update trade in DB repository");
                throw;
            }

            return Ok();
        }

        // DELETE api/trades/5
        [HttpDelete("{tradeId:int}")]
        public /* async */ void DeleteTrade(int tradeId)
        {
            throw new NotImplementedException("Delete tool not implemented!");
        }
    }
}