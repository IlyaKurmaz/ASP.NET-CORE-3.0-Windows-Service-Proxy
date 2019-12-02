using Microsoft.AspNetCore.Http;
using System;

namespace SFTP_Proxy_Service.Gateway
{
    public interface ISFTPClient : IDisposable
    {
        void Connect();
        void UploadFile(IFormFile file);
        void Disconnect();
    }
}