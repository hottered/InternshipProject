using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IBlobService
    {
        public Task<Stream> GetBlobAsync(string name);

        public Task<IEnumerable<string>> ListBlobsAsync();

        public Task<string> UploadFileBlobAsync(IFormFile file);

        public Task DeleteBlobAsync(string blobName);
    }
}
