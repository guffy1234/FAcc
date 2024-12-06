using iText.Commons.Utils;
using Microsoft.JSInterop;
using System.IO;
using System.Text;

namespace FuelAcc.Client.Services
{
    public class FileSaveService : IFileSaveService
    {
        private readonly IJSRuntime _jSRuntime;

        public FileSaveService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task SaveToFile(string fileName, byte[] data)
        {
            using var memstm = new MemoryStream(data);
            await SaveToFile(fileName, memstm);
        }

        public async Task SaveToFile(string fileName, Stream stm)
        {
            using var streamRef = new DotNetStreamReference(stm);

            await _jSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }

        public async Task SaveToFile(string fileName, string text, Encoding encoding = default)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(text);

            await SaveToFile(fileName, bytes);
        }

        public async Task SaveToFile(string fileName, Uri uri)
        {
            await _jSRuntime.InvokeVoidAsync("triggerFileDownload", fileName, uri.ToString());
        }
    }
}