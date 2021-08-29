using JwtDemo.Helpers;
using JwtDemo.Models.Parameter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JwtDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly JwtHelpers _jwtHelper;
        private readonly ILogger<TestController> _logger;

        public TestController(
            JwtHelpers jwtHelpers,
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
        /// 登入後才能查詢呦！
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public string Get() => User.Identity.Name;
    }
}
