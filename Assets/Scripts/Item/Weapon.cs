namespace Item
{
    public class Weapon : Item
    {
        private const float SwingDuration = 0.4f;
        private bool _facingRight;

        public void StartSwing(bool facingRight)
        {
            _facingRight = facingRight;
            gameObject.SetActive(true);
            // StartCoroutine(HandleWeaponCoroutine());
            const float swingAngle = 120f;
            const float swingOffset = 30f;
            const float radius = 1f;
            StartCoroutine(SwingableItem.SwingCoroutine(_facingRight, SwingDuration, transform, transform.parent,
                swingAngle, swingOffset, radius));
        }

        public void StopSwing()
        {
            gameObject.SetActive(false);
        }
    }
}