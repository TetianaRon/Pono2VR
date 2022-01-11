using UnityEditor;

namespace Assets.Complete360Tour.Runtime.AutoTour
{
    public interface INodeDataManager
    {
        ExtNodeData GetData(int Id);
        ExtNodeData GetDataByName(string Name);
        int NameIntoGuid(string Name);
        LanguageData SwitchLanguage(int langIndex);

    }

    public class LanguageData
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
    }

    //todo: Make singleton
    public class NodeDataManager : INodeDataManager
    {
        public ExtNodeData GetData(int Id)
        {
            throw new System.NotImplementedException();
        }

        public ExtNodeData GetDataByName(string Name)
        {
            throw new System.NotImplementedException();
        }

        public int NameIntoGuid(string Name)
        {
            throw new System.NotImplementedException();
        }

        public LanguageData SwitchLanguage(int langIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}