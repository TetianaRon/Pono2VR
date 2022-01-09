using UnityEditor;

namespace Assets.Complete360Tour.Runtime.AutoTour
{
    public interface INodeDataManager
    {
        ExtNodeData GetData(GUID Id);
        ExtNodeData GetDataByName(string Name);
        GUID NameIntoGuid(string Name);
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
        public ExtNodeData GetData(GUID Id)
        {
            throw new System.NotImplementedException();
        }

        public ExtNodeData GetDataByName(string Name)
        {
            throw new System.NotImplementedException();
        }

        public GUID NameIntoGuid(string Name)
        {
            throw new System.NotImplementedException();
        }

        public LanguageData SwitchLanguage(int langIndex)
        {
            throw new System.NotImplementedException();
        }
    }
}