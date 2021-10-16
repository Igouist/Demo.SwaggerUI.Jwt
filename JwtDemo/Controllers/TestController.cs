using JwtDemo.Helpers;
using JwtDemo.Models.Parameter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JwtDemo.Controllers
{
    /// <summary>
    /// 測試站台
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly Helpers.JwtHelper _jwtHelper;
        private readonly ILogger<TestController> _logger;

        /// <summary>
        /// Init
        /// </summary>
        public TestController(
            Helpers.JwtHelper jwtHelpers,
            ILogger<TestController> logger)
        {
            this._jwtHelper = jwtHelpers;
            this._logger = logger;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="parameter">The login.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("sign-in")]
        public string SignIn(LoginParameter parameter)
            => this.IsLoginSuccess(parameter)
                ? _jwtHelper.GenerateToken(parameter.Account)
                : string.Empty;

        // DEMO 都給你登入成功啦
        private bool IsLoginSuccess(LoginParameter parameter)
            => true;

        /// <summary>
        /// 查詢（登入後才能查詢呦！）
        /// </summary>
        /// <returns></returns>
        /// <response code="200">回傳使用者資訊</response>
        /// <response code="401">尚未登入</response>  
        [HttpGet]
        [Authorize]
        public string Get() => User.Identity.Name;
    }
}
