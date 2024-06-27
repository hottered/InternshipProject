using Azure.Storage.Blobs.Models;
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

        public Task UploadFileBlobAsync(string filePath,string fileName);

        public Task DeleteBlobAsync(string blobName);
    }
}
