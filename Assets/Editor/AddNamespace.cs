using UnityEngine;
using UnityEditor;

namespace SurvivalGame.Editor
{
    public class AddNameSpace : UnityEditor.AssetModificationProcessor 
    {

        public static void OnWillCreateAsset(string path) 
        {
            path = path.Replace(".meta", "");
            int index = path.LastIndexOf(".");
            if(index < 0) return;
            string file = path.Substring(index);
            if(file != ".cs" && file != ".js" && file != ".boo") return;
            index = Application.dataPath.LastIndexOf("Assets");
            path = Application.dataPath.Substring(0, index) + path;
            file = System.IO.File.ReadAllText(path);

            string projectName = "SurvivalGame";
            string lastPart = path.Substring(path.IndexOf("Assets"));
            string _namespace = lastPart.Substring(lastPart.IndexOf('/')+1, lastPart.LastIndexOf('/') - lastPart.IndexOf('/')-1);
            if (_namespace.Substring(0, _namespace.IndexOf('/')).Contains("Scripts"))
                _namespace = $"{projectName}/{_namespace.Substring(_namespace.IndexOf('/')+1)}";

            _namespace = _namespace.Replace('/', '.');
            file = file.Replace("#NAMESPACE#", _namespace);

            System.IO.File.WriteAllText(path, file);
            AssetDatabase.Refresh();
        }
    }
}
