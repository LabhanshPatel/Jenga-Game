namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    // Non-persistent singleton
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UnityEvent<JengaStack> OnCurrentStackChanged;
        [SerializeField] private UnityEvent<JengaBlock> OnCurrentBlockChanged;
        [SerializeField] private JengaStack currentStack;
        [SerializeField] private JengaStack[] stacks;

        private static GameManager instance;

        private RaycastHit raycastHit;
        private UIGame uiGame;
        private JengaBlock currentBlock;
        private int currentStackIndex = 7; // start with 7th grade

        private Dictionary<string, JengaStack> stackLookup = new Dictionary<string, JengaStack>();

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            foreach (JengaStack stack in stacks)
                stackLookup.Add(stack.GetStackId(), stack);        
        }

        IEnumerator Start()
        {
            while (!AppManager.IsInitialized())
                yield return null;
            Reset();
            OnCurrentStackChanged.Invoke(currentStack);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
                ShowBlockDetailsPanel();
        }

        public void ChangeStack(int gradeNo)
        {
            
            if (currentStackIndex != gradeNo)
                MoveToOtherStack(gradeNo);
        }

        void ShowBlockDetailsPanel()
        {
            if (currentBlock)
                currentBlock.SetHighlighted(false);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out raycastHit);
            if (hasHit)
            {
                JengaBlock block = raycastHit.collider.GetComponent<JengaBlock>();
                currentBlock = block;
                if (currentBlock)
                    currentBlock.SetHighlighted(true);
            }
            else
                currentBlock = null;
            OnCurrentBlockChanged.Invoke(currentBlock);
        }

        public void Reset()
        {
            foreach (JengaStack stack in stacks)
                stack.Clear();
            Initialize(AppManager.GetLoadedData());
        }

        public void Initialize(JengaBlockData[] dataArray)
        {
            // sorting data by requirements
            List<JengaBlockData> list = new List<JengaBlockData>(dataArray);
            List<JengaBlockData> sortedList = list
                .OrderBy(i => i.domain)
                .ThenBy(i => i.cluster)
                .ThenBy(i => i.standardid)
                .ToList();

            StartCoroutine(InitializeRoutine(sortedList.ToArray()));
        }

        IEnumerator InitializeRoutine(JengaBlockData[] dataArray)
        {
            foreach (JengaBlockData data in dataArray)
            {
                JengaStack stack;
                stackLookup.TryGetValue(data.grade, out stack);
                if (stack)
                {
                    stack.AddBlock(data);
                }
                else
                    Debug.LogWarning(data.grade + " stack could not be found?");
                yield return null;
            }
        }
      
        public void TestMyStack()
        {
            currentStack.TestWithout(MasteryType.Glass);
        }

        void OnDestroy()
        {
            instance = null;
        }

        public static JengaStack GetCurrentStack()
        {
            if (instance)
                return instance.currentStack;
            else
                return null;
        }

        public static bool IsInitialized()
        {
            return instance;
        }

        void MoveToOtherStack(int gradeNo)
        {
            currentStackIndex = gradeNo;

            currentStack = stacks[currentStackIndex - 6];
            OnCurrentStackChanged.Invoke(currentStack);
        }    
    }
}