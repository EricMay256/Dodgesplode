using UnityEngine;

namespace UBear.InputSystem
{
    [System.Serializable]
    public struct RespawnStatusPayload
    {
        public ulong playerId;
        public string message;           // "Respawning in" or "Waiting for Next Spawn Point"
        public string subtext;           // "05" or ""
        public bool showSubtext;         // true for countdown, false for waiting
    }

    [CreateAssetMenu(fileName = "RespawnStatusEvent", menuName = "UBear/Game Events/RespawnStatusEvent")]
    public class RespawnStatusEvent : GameEvent<RespawnStatusPayload> { }
}
