using FuelAcc.Application.Dto.Replication;

namespace FuelAcc.Application.Interface.Replication;

public interface IReplicationHelper
{
    string ConstructFileName(ReplictionPacketDto packet);

    string SerializeAndSign(ReplictionPacketDto packet);

    byte[] Compress(string serialized);

    string Decompress(byte[] data);

    ReplictionPacketDto Deserialize(string data);
}