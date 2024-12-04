using System.Text;

namespace FuelAcc.Client.Services
{
    public interface IFileSaveService
    {
        Task SaveToFile(string filename, byte[] data);
        Task SaveToFile(string filename, MemoryStream ms);
        Task SaveToFile(string filename, Stream stm);
        Task SaveToFile(string filename, string text, Encoding encoding = null);
    }
}