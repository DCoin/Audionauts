using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public static class AudioUtility {

	public static void PlayClip(AudioClip clip) {

		Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

		Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");

		string name = "PlayClip";
		BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;
		Binder binder = null;
		Type[] types = new System.Type[] { typeof(AudioClip) };
		ParameterModifier[] modifiers = null;

		MethodInfo method = audioUtilClass.GetMethod(name, bindingAttr, binder, types, modifiers);

		object obj = null;
		object[] parameters = new object[] { clip };

		method.Invoke(obj, parameters);

	}
}
