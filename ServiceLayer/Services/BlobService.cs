﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public Task DeleteBlobAsync(string blobName)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("files");

            var blobClient = containerClient.GetBlobClient(name);

            var response = await blobClient.DownloadStreamingAsync();

            return response.Value.Content;

        }

        public Task<IEnumerable<string>> ListBlobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UploadFileBlobAsync(string filePath, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
