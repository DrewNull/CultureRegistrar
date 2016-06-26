namespace CultureRegistrar.Server.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Application;

    [EnableCors("*", "*", "*")]
    [RoutePrefix("admin")]
    public class AdminController : ApiController
    {
        private readonly IAppPoolService _appPoolService;

        public AdminController(IAppPoolService appPoolService)
        {
            if (appPoolService == null) throw new ArgumentNullException(nameof(appPoolService));

            this._appPoolService = appPoolService;
        }

        [HttpPost]
        [Route("")]
        public void Put(IEnumerable<string> appPoolNames)
        {
            this._appPoolService.RecycleAppPools(appPoolNames);
        }
    }
}