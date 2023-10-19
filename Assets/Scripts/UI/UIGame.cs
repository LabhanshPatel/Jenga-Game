namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using System.Collections;
    using TMPro;

    public class UIGame : MonoBehaviour
    {
        [SerializeField] GameObject blockInfoPanel;
        [SerializeField] TMP_Text blockInfoText;

        const string BLOCK_INFO_FORMAT = "• {0}: {1}\n\n• {2}\n\n• {3}: {4}";

        void Awake()
        {
            blockInfoPanel.SetActive(false);
        }

        public void UpdateBlockInfoDisplay(JengaBlock block)
        {
            if (block)
            {
                JengaBlockData data = block.GetData();
                blockInfoText.text = string.Format(BLOCK_INFO_FORMAT, data.grade, data.domain, data.cluster, data.standardid, data.standarddescription);
            }
            else
                blockInfoText.text = string.Empty;
            blockInfoPanel.SetActive(block);
        }
    }
}