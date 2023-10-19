namespace JengaGame
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Jenga Block Prefabs", order = 1)]
    public class JengaBlockPrefabs : ScriptableObject
    {
        [SerializeField] JengaBlock[] blockPrefabs;

        public float GetBlockHeight()
        {
            return blockPrefabs[0].GetHeight();
        }

        public float GetBlockWidth()
        {
            return blockPrefabs[0].GetWidth();
        }

        public JengaBlock GetBlockPrefab(int mastery)
        {
            return blockPrefabs[mastery];
        }
    }

}