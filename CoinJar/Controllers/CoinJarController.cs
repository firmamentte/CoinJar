using System;
using System.Threading.Tasks;
using CoinJar.BLL;
using CoinJar.BLL.DataContract;
using Microsoft.AspNetCore.Mvc;

namespace CoinJar.Controllers
{
    [ApiController]
    [Route("api/CoinJar")]
    public class CoinJarController : ControllerBase
    {
        [Route("V1/AddCoin")]
        [HttpPost]
        public async Task<ActionResult> AddCoin([FromBody] AddCoinReq addCoinReq)
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                if (addCoinReq.Amount != 100 &&
                    addCoinReq.Amount != 50 &&
                    addCoinReq.Amount != 25 &&
                    addCoinReq.Amount != 10 &&
                    addCoinReq.Amount != 5 &&
                    addCoinReq.Amount != 1)
                {
                    ModelState.AddModelError("Amount", $"Amount must be one of the following: {100} or {50} or {25} or {10} or {5} or {1}");
                }

                if (addCoinReq.Volume <= 0)
                {
                    ModelState.AddModelError("Volume", "Volume must be greater zero");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiErrorResp(ModelState));
                }

                #endregion

                return Created(string.Empty, await CoinJarBLL.CoinHelper.AddCoin(addCoinReq));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("V1/GetTotalAmount")]
        [HttpGet]
        public async Task<ActionResult> GetTotalAmount()
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                #endregion

                return Ok(await CoinJarBLL.CoinHelper.GetTotalAmount());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("V1/Reset")]
        [HttpPatch]
        public async Task<ActionResult> Reset()
        {
            try
            {
                #region RequestValidation

                ModelState.Clear();

                #endregion

                await CoinJarBLL.CoinHelper.Reset();

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
