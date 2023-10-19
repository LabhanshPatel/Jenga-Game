namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.Events;

    public class JengaBlock : MonoBehaviour
    {
        [SerializeField] MasteryType masteryType = MasteryType.Glass;
        private JengaBlockData data;
        private BoxCollider boxCollider;
        private MeshRenderer meshRenderer;
        private Rigidbody rigidbody;
        private Material sharedMaterial;

        private const float PADDING = 0.1f;
        private static readonly Color SELECTED_COLOR = new Color(1, 0.5f, 0);

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            sharedMaterial = meshRenderer.sharedMaterial;
            boxCollider = GetComponent<BoxCollider>();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void SetData(JengaBlockData newData)
        {
            data = newData;
        }

        public void SetHighlighted(bool isHighlighted)
        {
            if (isHighlighted)
                meshRenderer.material.color = SELECTED_COLOR;
            else
                meshRenderer.material = sharedMaterial;
        }

        public void SetEnabled(bool shouldEnable)
        {
            boxCollider.enabled = shouldEnable;
            meshRenderer.enabled = shouldEnable;
            rigidbody.isKinematic = !shouldEnable;
        }

        public float GetHeight()
        {
            if (!meshRenderer)
                meshRenderer = GetComponent<MeshRenderer>();
            return meshRenderer.bounds.extents.y * 2;
        }

        public float GetWidth()
        {
            if (!meshRenderer)
                meshRenderer = GetComponent<MeshRenderer>();
            return (meshRenderer.bounds.extents.z * 2) + PADDING;
        }

        public JengaBlockData GetData()
        {
            return data;
        }

        public MasteryType GetMasteryType()
        {
            return masteryType;
        }
    }

}