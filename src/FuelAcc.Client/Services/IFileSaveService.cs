using System.Text;

namespace FuelAcc.Client.Services
{
    public interface IFileSaveService
    {
        Task SaveToFile(string fileName, byte[] data);
        Task SaveToFile(string fileName, Stream stm);
        Task SaveToFile(string fileName, string text, Encoding encoding = null);
        Task SaveToFile(string fileName, Uri uri);
    }
}