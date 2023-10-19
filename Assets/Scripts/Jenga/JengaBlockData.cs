namespace JengaGame
{
    [System.Serializable]
    public class JengaBlockData
    {
        public JengaBlockData() {}

        public int id;
        public string subject;
        public string grade; // identifies stack
        public int mastery; // identifies block prefab
        public string domainid;
        public string domain;
        public string cluster;
        public string standardid;
        public string standarddescription;
    }
}