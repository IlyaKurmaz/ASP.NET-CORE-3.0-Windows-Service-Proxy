using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFTP_Proxy_Service.Enums;
using SFTP_Proxy_Service.Services;
using System.Collections.Generic;

namespace SFTP_Proxy_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SFTPController : ControllerBase
    {
        private readonly ISFTPSenderService sftpSenderService;
        public SFTPController(ISFTPSenderService sftpSenderService)
        {
            this.sftpSenderService = sftpSenderService;
        }

        [HttpGet]
        public string Get()
        {
            return "Alive";
        }

        [HttpPost]
        public UploadStatus Post()
        {
            IEnumerable<IFormFile> files = Request.Form.Files;

            return sftpSenderService.Send(files);
        }
    }
}
