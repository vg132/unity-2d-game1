using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
	public class AddNameSpace : UnityEditor.AssetModificationProcessor
	{
		public static void OnWillCreateAsset(string path)
		{
			path = path.Replace(".meta", "");
			var index = path.LastIndexOf(".");
			if (index < 0)
			{
				return;
			}
			var file = path.Substring(index);
			if (file != ".cs" && file != ".js" && file != ".boo")
			{
				return;
			}
			index = Application.dataPath.LastIndexOf("Assets");
			path = Application.dataPath.Substring(0, index) + path;
			file = System.IO.File.ReadAllText(path);

			var lastPart = path.Substring(path.IndexOf("Assets"));

			var namespaceParts = lastPart.Substring(0, lastPart.LastIndexOf('/')).Split('/');
			if(namespaceParts.Length>=2)
			{
				var namespaceName = string.Join(".", namespaceParts.Skip(2));
				if (!string.IsNullOrEmpty(EditorSettings.projectGenerationRootNamespace))
				{
					namespaceName = $"{EditorSettings.projectGenerationRootNamespace}.{namespaceName}";
				}
				file = file.Replace("#NAMESPACE#", namespaceName);
				System.IO.File.WriteAllText(path, file);
				AssetDatabase.Refresh();
			}
		}
	}
}
