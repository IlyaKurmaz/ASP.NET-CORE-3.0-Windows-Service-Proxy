using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Renci.SshNet;
using SFTP_Proxy_Service.Options;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace SFTP_Proxy_Service.Gateway
{
    public class SFTPClient : ISFTPClient
    {
        private readonly SftpClient _client;

        public SFTPClient(IOptions<SFTPOptions> options)
        {
            _client = new SftpClient(BuildConnectionInfo(options.Value));
        }
        public void Connect()
        {
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public void UploadFile(IFormFile file)
        {
            _client.UploadFile(file.OpenReadStream(), file.FileName, true);

        }

        private ConnectionInfo BuildConnectionInfo(SFTPOptions options)
        {
            return new ConnectionInfo(host: options.Host, port: options.Port, username: options.UserName, new PasswordAuthenticationMethod(options.UserName, options.Password));
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
