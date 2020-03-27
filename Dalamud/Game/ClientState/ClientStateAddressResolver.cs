using System;
using Dalamud.Game.Internal;

namespace Dalamud.Game.ClientState
{
    public sealed class ClientStateAddressResolver : BaseAddressResolver {
        // Static offsets
        public IntPtr ActorTable { get; private set; }
        public IntPtr LocalContentId { get; private set; }
        public IntPtr JobGaugeData { get; private set; }

        // Functions
        public IntPtr SetupTerritoryType { get; private set; }
        
        protected override void Setup64Bit(SigScanner sig) {
            ActorTable = sig.GetStaticAddressFromSig("48 8D 0D ?? ?? ?? ?? 85 ED", 0) + 0x148;
            LocalContentId = sig.GetStaticAddressFromSig("48 8B 05 ?? ?? ?? ?? 48 89 86 ?? ?? ?? ??", 0);
            JobGaugeData = sig.GetStaticAddressFromSig("E8 ?? ?? ?? ?? FF C6 48 8D 5B 0C", 0xB9) + 0x10;

            SetupTerritoryType = sig.ScanText("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC 20 48 8B F9 66 89 91 ?? ?? ?? ??");
        }
    }
}
