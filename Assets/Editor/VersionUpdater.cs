#if UNITY_EDITOR
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


namespace InControl
{
	[InitializeOnLoad]
	public class VersionUpdater
	{
		static VersionUpdater()
		{
#pragma warning disable CS0618 // Type or member is obsolete
			EditorApplication.playmodeStateChanged += Update;
#pragma warning restore CS0618 // Type or member is obsolete
		}


		static void Update()
		{
			if (!EditorApplication.isPlaying)
			{
#pragma warning disable CS0618 // Type or member is obsolete
				EditorApplication.playmodeStateChanged -= Update;
#pragma warning restore CS0618 // Type or member is obsolete
				UpdateVersion();
			}
		}


		static void UpdateVersion()
		{
			string versionPath = "Assets/InControl/Source/VersionInfo.cs";
			string versionText = GetFileContents(versionPath);
			if (versionText != null)
			{
				versionText = Regex.Replace(versionText, @"Build = (?<value>\d+)", ReplaceFunc);
				PutFileContents(versionPath, versionText);
				AssetDatabase.Refresh();
			}
		}


		static string ReplaceFunc(Match match)
		{
			var value = int.Parse(match.Groups["value"].Value) + 1;
			return "Build = " + value.ToString();
		}


		static string GetFileContents(string fileName)
		{
			StreamReader streamReader = new StreamReader(fileName);
			var fileContents = streamReader.ReadToEnd();
			streamReader.Close();

			return fileContents;
		}


		static void PutFileContents(string filePath, string content)
		{
			StreamWriter streamWriter = new StreamWriter(filePath);
			streamWriter.Write(content.Trim());
			streamWriter.Flush();
			streamWriter.Close();
		}
	}
}
#endif

