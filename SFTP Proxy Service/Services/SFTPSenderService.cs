using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Renci.SshNet;
using Serilog;
using SFTP_Proxy_Service.Enums;
using SFTP_Proxy_Service.Gateway;
using SFTP_Proxy_Service.Options;
using System;
using System.Collections.Generic;

namespace SFTP_Proxy_Service.Services
{
    public class SftpSenderService : ISFTPSenderService
    {
        private readonly ISFTPGateway _gateway;

        private readonly IOptions<SFTPOptions> _options;
        public SftpSenderService(ISFTPGateway gateway, IOptions<SFTPOptions> options)
        {
            _gateway = gateway;
            _options = options;
        }

        public UploadStatus Send(IEnumerable<IFormFile> files)
        {
            using (var client = _gateway.GetSFTPClient(_options))
            {
                try
                {
                    client.Connect();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Connection refused");

                    return UploadStatus.ConnectionRefused;
                }

                foreach(var file in files)
                {
                    try
                    {
                        client.UploadFile(file);
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, $"Upload failed for file {file.FileName}");
                        return UploadStatus.UploadFailed;
                    }

                    Log.Information($"File {file.Name} was uploaded successfully");
                }

                client.Disconnect();
            }

            return UploadStatus.UploadSuccessful;
        }

    }
}