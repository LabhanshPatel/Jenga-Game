namespace JengaGame
{
    using UnityEngine;
    using TMPro;
    using System.Collections.Generic;

    public class JengaStack : MonoBehaviour
    {
        [SerializeField] private string stackId = "6th Grade";
        [SerializeField] private JengaBlockPrefabs prefabs;
  
        [SerializeField] private TMP_Text gradeLabel;
        private List<JengaBlock> blocks = new List<JengaBlock>();
        JengaBlockData data;

        private const int BLOCKS_PER_ROW = 3;       
        public Vector3 Position => GetMidPoint();
     

        void Start()
        {
            UpdateGradeLabelDisplay(stackId);
        }

        public void Clear()
        {
            for (int i=blocks.Count-1; i>=0; i--)
                RemoveBlock(blocks[i]);
        }

        public void AddBlock(JengaBlockData newData)
        {
            JengaBlock prefab = prefabs.GetBlockPrefab(newData.mastery);
            JengaBlock instance = Instantiate(prefab, GetSpawnPosition(), GetSpawnRotation());
            instance.SetData(newData);
            instance.transform.SetParent(transform, true);
            blocks.Add(instance);
        }

        public void RemoveBlock(JengaBlock block)
        {
            blocks.Remove(block);
            Destroy(block.gameObject);
        }

        public void TestWithout(MasteryType type)
        {
            for (int i=0; i<blocks.Count; i++)
            {
                blocks[i].SetEnabled(blocks[i].GetMasteryType() != type);
            }
        }

        public void UpdateGradeLabelDisplay(string gradeDisplay)
        {
            gradeLabel.text = gradeDisplay;
        }

        public Vector3 GetMidPoint()
        {
            return transform.position + ((Vector3.up * GetHeight()) / 2.0f);
        }

        public int GetRows()
        {
            return blocks.Count % BLOCKS_PER_ROW;
        }

        public float GetBlockHeight()
        {
            return prefabs.GetBlockHeight();
        }
        public float GetBlockWidth()
        {
            return prefabs.GetBlockWidth();
        }

        public float GetHeight()
        {
            return GetBlockHeight() * GetRows();
        }

        public string GetStackId()
        {
            return stackId;
        }

        Vector3 GetSpawnPosition()
        {
            return transform.position - 
                ((blocks.Count / BLOCKS_PER_ROW) % 2 == 0 ? Vector3.forward : Vector3.right) * (GetBlockWidth()) +
                (Vector3.up * (GetBlockHeight() / 2.0f)) +

            (((blocks.Count / BLOCKS_PER_ROW) % 2 == 0 ? Vector3.forward : Vector3.right) *
                GetBlockWidth() * (blocks.Count % BLOCKS_PER_ROW)) +

            (Vector3.up * GetBlockHeight() * (blocks.Count / BLOCKS_PER_ROW));
        }

        Quaternion GetSpawnRotation()
        {
            if ((blocks.Count / BLOCKS_PER_ROW) % 2 == 0)
                return Quaternion.identity;
            else
                return Quaternion.Euler(0, 90, 0);
        } 
    }

}