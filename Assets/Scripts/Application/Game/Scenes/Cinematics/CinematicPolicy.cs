using Game.Core.Cinematics.Abstractions;
using Game.Core.Cinematics.DataStructures;
using Game.Core.Cinematics.Enums;
using UnityEngine;

namespace Game.Application.Cinematics
{
    public class CinematicPolicy : ICinematicPolicy
    {
        public CinematicResult Evaluate(CinematicState current, ICinematicRequest request)
        {
            if (!CheckNotPlaying(current, request))
            {
                return Denied();
            }
            return Approved(request);
        }

        private bool CheckNotPlaying(CinematicState current, ICinematicRequest request)
        {
            if ((current != CinematicState.Playing) && (current != CinematicState.Paused))
            {
                return true;
            }
            return false;
        }
        private CinematicResult Approved(ICinematicRequest request)
        {
            Debug.Log("Cinematic request approved");
            return new CinematicResult(true, request.Id);
        }
        private CinematicResult Denied()
        {
            Debug.Log("Cinematic request denied");
            return new CinematicResult(false, CinematicId.None);
        }
    }
}