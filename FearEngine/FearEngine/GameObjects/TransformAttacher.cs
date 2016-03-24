namespace FearEngine.GameObjects
{
    public abstract class TransformAttacher
    {
        Transform transform;

        public void AttactToTransform(Transform trans)
        {
            RemoveOldListener();
            transform = trans;
            transform.Changed += OnTransformChanged;
            OnTransformChanged(transform);
        }

        abstract protected void OnTransformChanged(Transform newTransform);

        private void RemoveOldListener()
        {
            if (transform != null)
            {
                transform.Changed -= OnTransformChanged;
            }
        }
    }
}
