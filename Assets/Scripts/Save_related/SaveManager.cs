using UnityEngine.Device;
using System.IO;
using Newtonsoft.Json;

namespace SaveLoad.Runtime
{
    public static class SaveManager
    {
        private static readonly string saveFolder = Application.persistentDataPath + "/GameData";
        public static void Delete(string profileName)
        {
            if (!File.Exists($"{saveFolder}/{profileName}")){
                throw new System.Exception("Save file not found!");
            }
            File.Delete($"{saveFolder}/{profileName}");
        }
        public static SaveProfile<T> Load<T>(string profileName) where T : SaveProfileData {
            try {
            var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
            //decrypt
            return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents);
            }
            catch {
            var sceneSave = new TestSave{position = new UnityEngine.Vector3(999,999,999), sceneName = "Empty"};
            var SaveProfile = new SaveProfile<TestSave>("Empty", sceneSave);
            SaveManager.Save(SaveProfile);
                return JsonConvert.DeserializeObject<SaveProfile<T>>(File.ReadAllText($"{saveFolder}/Empty"));
            }
        }

        public static void Save<T>(SaveProfile<T> save) where T : SaveProfileData
        {
            if (File.Exists($"{saveFolder}/{save.name}")){
                File.Delete($"{saveFolder}/{save.name}");
            }
            var jsonString = JsonConvert.SerializeObject(save, Formatting.Indented,
                new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            //encrypt
            if (!Directory.Exists(saveFolder)) {
                Directory.CreateDirectory(saveFolder);
            }
            File.WriteAllText($"{saveFolder}/{save.name}", jsonString);
        }
        public static void Drop(){
            var dir = new DirectoryInfo(saveFolder);
            try{
                dir.Delete(true);
            }
            catch{}
        }
    }
}