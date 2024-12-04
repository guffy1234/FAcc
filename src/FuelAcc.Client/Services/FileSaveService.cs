using Microsoft.JSInterop;
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

        public async Task SaveToFile(string filename, byte[] data)
        {
            var body64 = Convert.ToBase64String(data);
            await _jSRuntime.InvokeAsync<object>("saveAsFile", filename, body64);
        }

        public async Task SaveToFile(string filename, MemoryStream ms)
        {
            var data = ms.ToArray();
            await SaveToFile(filename, data);
        }

        public async Task SaveToFile(string filename, Stream stm)
        {
            using var ms = new MemoryStream();
            stm.CopyTo(ms);
            await SaveToFile(filename, ms);
        }

        public async Task SaveToFile(string filename, string text, Encoding encoding = default)
        {
            encoding = encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(text);

            await SaveToFile(filename, bytes);
        }
    }
}