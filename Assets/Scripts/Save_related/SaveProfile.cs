using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveLoad.Runtime
{
    [System.Serializable]
    public sealed class SaveProfile<T> where T : SaveProfileData
    {
        public string name;
        public T saveData;

        private SaveProfile(){ }

        public SaveProfile(string name, T saveData)
        {
            this.name = name;
            this.saveData = saveData;

        }
    }

    public abstract record SaveProfileData { }
    public record DefaultSave : SaveProfileData
    {
        public string[] dialogs;
        public string sceneName;
        public Vector3 position;
        public bool[] items;
    }
    public record TestSave : SaveProfileData
    {
        public string sceneName;
        public Vector3 position;
    }
    public record ThingsSave : SaveProfileData
    {
        public bool[] inventory;
    }

}