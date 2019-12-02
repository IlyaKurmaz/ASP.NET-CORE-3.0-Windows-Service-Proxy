using Microsoft.Extensions.Options;
using SFTP_Proxy_Service.Options;

namespace SFTP_Proxy_Service.Gateway
{
    public interface ISFTPGateway
    {
        ISFTPClient GetSFTPClient(IOptions<SFTPOptions> options);
    }
}