namespace Coalim.Realtime.Server.Transmission;

public enum RealtimeMessageOpcode : byte
{
    ClientLogin = 0,
    ServerUserInfo = 1,
    ClientIdentifyPacketOpcode = 2,
    ServerPacketOpcode = 3,
}