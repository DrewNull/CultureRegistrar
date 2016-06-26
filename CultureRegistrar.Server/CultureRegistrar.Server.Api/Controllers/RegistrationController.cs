namespace CultureRegistrar.Server.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Application;

    [EnableCors("*", "*", "*")]
    [RoutePrefix("registration")]
    public class RegistrationController : ApiController
    {
        private readonly ICultureService _cultureService;

        public RegistrationController(ICultureService cultureService)
        {
            if (cultureService == null) throw new ArgumentNullException(nameof(cultureService));

            this._cultureService = cultureService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Culture> Get()
        {
            return this._cultureService.Get();
        }

        [HttpPut]
        [Route("")]
        public void Put(IEnumerable<string> cultures)
        {
            this._cultureService.Register(cultures, true);
        }

        [HttpDelete]
        [Route("")]
        public void Delete(IEnumerable<string> cultures)
        {
            this._cultureService.Unregister(cultures);
        }
    }
}
