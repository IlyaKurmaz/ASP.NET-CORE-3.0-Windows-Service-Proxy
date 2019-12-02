using Microsoft.AspNetCore.Http;
using SFTP_Proxy_Service.Enums;
using System.Collections.Generic;

namespace SFTP_Proxy_Service.Services
{
    public interface ISFTPSenderService
    {
        UploadStatus Send(IEnumerable<IFormFile> files);
    }
}