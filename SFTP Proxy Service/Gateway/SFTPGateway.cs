using Microsoft.Extensions.Options;
using SFTP_Proxy_Service.Options;

namespace SFTP_Proxy_Service.Gateway
{
    public class SFTPGateway : ISFTPGateway
    {
        public ISFTPClient GetSFTPClient(IOptions<SFTPOptions> options)
        {
            return new SFTPClient(options);
        }
    }
}
