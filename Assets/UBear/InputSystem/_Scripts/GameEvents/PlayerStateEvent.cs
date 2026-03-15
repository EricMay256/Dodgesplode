using UnityEngine;

namespace UBear.InputSystem
{
    [CreateAssetMenu(menuName = "UBear/Game Events/Player State Event")]
    public class PlayerStateEvent : GameEvent<PlayerStatePayload> { }

    [System.Serializable]
    public struct PlayerStatePayload
    {
        public ulong playerId;
        //public PlayerLifeState newState;
        //public PlayerLifeState oldState;
    }
}
