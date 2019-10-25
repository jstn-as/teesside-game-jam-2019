using UnityEngine;

namespace Player
{
    public class CollisionManager : MonoBehaviour
    {
        [SerializeField] private CollisionCheck _upCheck;
        [SerializeField] private CollisionCheck _downCheck;
        [SerializeField] private CollisionCheck _leftCheck;
        [SerializeField] private CollisionCheck _rightCheck;

        public bool IsUp()
        {
            return _upCheck.IsColliding();
        }

        public bool IsDown()
        {
            return _downCheck.IsColliding();
        }

        public bool IsLeft()
        {
            return _leftCheck.IsColliding();
        }

        public bool IsRight()
        {
            return _rightCheck.IsColliding();
        }
    }
}
