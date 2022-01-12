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
}