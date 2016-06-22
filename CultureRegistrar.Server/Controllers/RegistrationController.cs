namespace CultureRegistrar.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Application;

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
            foreach (string cultureName in cultures)
            {
                this._cultureService.Register(cultureName);
            }
        }

        [HttpDelete]
        [Route("")]
        public void Delete(IEnumerable<string> cultures)
        {
            foreach (string cultureName in cultures)
            {
                this._cultureService.Unregister(cultureName);
            }
        }
    }
}
