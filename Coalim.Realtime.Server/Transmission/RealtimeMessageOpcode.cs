namespace Coalim.Realtime.Server.Transmission;

public enum RealtimeMessageOpcode : byte
{
    ClientLogin = 0,
    ServerLoginResponse = 1,
    ClientIdentifyPacketOpcode = 2,
    ServerPacketOpcode = 3,
    ServerJoinServer = 4,
    ServerInformation = 5,
}