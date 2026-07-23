using UnityEngine;

public class CardSwipeController : MonoBehaviour
{
    [Header("Tuning")]
    [SerializeField] private float minSpeed = 200f;   // px/sec, too slow below this
    [SerializeField] private float maxSpeed = 900f;   // px/sec, too fast above this
    [SerializeField] private float graceDistance = 15f; // px of travel before gating kicks in
    [SerializeField] private float maxOutOfRangeTime = 0.15f; // forgiveness window, seconds

    private Vector2 _lastPos;
    private float _distanceTraveled;
    private float _outOfRangeTimer;
    private bool _swiping;

    public void BeginSwipe(Vector2 startPos)
    {
        _swiping = true;
        _lastPos = startPos;
        _distanceTraveled = 0f;
        _outOfRangeTimer = 0f;
    }

    public SwipeResult UpdateSwipe(Vector2 currentPos)
    {
        if (!_swiping) return SwipeResult.None;

        float dt = Time.deltaTime;
        float frameDist = Vector2.Distance(currentPos, _lastPos);
        float instSpeed = dt > 0f ? frameDist / dt : 0f;

        _distanceTraveled += frameDist;
        _lastPos = currentPos;

        // Grace period: don't gate speed until the player has actually
        // traveled a bit — real swipes ramp up from zero.
        if (_distanceTraveled < graceDistance)
        {
            return SwipeResult.InProgress;
        }

        bool tooSlow = instSpeed < minSpeed;
        bool tooFast = instSpeed > maxSpeed;

        if (tooSlow || tooFast)
        {
            _outOfRangeTimer += dt;
            if (_outOfRangeTimer > maxOutOfRangeTime)
            {
                return tooFast ? SwipeResult.FailedTooFast : SwipeResult.FailedTooSlow;
            }
        }
        else
        {
            _outOfRangeTimer = 0f; // reset forgiveness once back in range
        }

        return SwipeResult.InProgress;
    }

    public void EndSwipe()
    {
        _swiping = false;
    }
}

public enum SwipeResult { None, InProgress, FailedTooSlow, FailedTooFast, Success }