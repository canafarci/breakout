using UnityEngine;

namespace Breakout.Gameplay
{
    internal class Boundary : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D BoxCollider2D;

        private void Start()
        {
            RectTransform rectTransform = transform as RectTransform;
            
            if (rectTransform != null)
            {
                BoxCollider2D.size = rectTransform.rect.size;
                BoxCollider2D.offset = SetColliderOffset(rectTransform);
            }
        }
        
        //as we set the boundaries during runtime, because of different screen sizes
        //we need to calculate the offset based on the alignment of
        //the boundaries
        private Vector2 SetColliderOffset(RectTransform rectTransform)
        {
            Vector2 colliderPosition = rectTransform.localPosition;
            
            //switch statement looks even uglier, so I've choose this instead :)
            if (colliderPosition.x > 0)
                return new Vector2(rectTransform.rect.width * 0.5f, 0f);
            else if (colliderPosition.x < 0)
                return new Vector2(-rectTransform.rect.width * 0.5f, 0f);
            else if (colliderPosition.y > 0)
                return new Vector2(0f, rectTransform.rect.height * 0.5f);
            else
                return new Vector2(0f, -rectTransform.rect.height * 0.5f);
        }
    }
}
