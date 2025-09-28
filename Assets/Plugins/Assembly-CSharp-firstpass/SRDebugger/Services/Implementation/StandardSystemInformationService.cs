namespace SRDebugger.Services.Implementation
{
	[global::SRF.Service.Service(typeof(global::SRDebugger.Services.ISystemInformationService))]
	public class StandardSystemInformationService : global::SRDebugger.Services.ISystemInformationService
	{
		private readonly global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.IList<global::SRDebugger.InfoEntry>> _info = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.IList<global::SRDebugger.InfoEntry>>();

		public StandardSystemInformationService()
		{
			CreateDefaultSet();
		}

		public global::System.Collections.Generic.IEnumerable<string> GetCategories()
		{
			return _info.Keys;
		}

		public global::System.Collections.Generic.IList<global::SRDebugger.InfoEntry> GetInfo(string category)
		{
			if (!_info.TryGetValue(category, out var value))
			{
				global::UnityEngine.Debug.LogError(global::SRF.SRFStringExtensions.Fmt("[SystemInformationService] Category not found: {0}", category));
				return new global::SRDebugger.InfoEntry[0];
			}
			return value;
		}

		public void Add(global::SRDebugger.InfoEntry info, string category = "Default")
		{
			if (!_info.TryGetValue(category, out var value))
			{
				value = new global::System.Collections.Generic.List<global::SRDebugger.InfoEntry>();
				_info.Add(category, value);
			}
			if (global::System.Linq.Enumerable.Any(value, (global::SRDebugger.InfoEntry p) => p.Title == info.Title))
			{
				throw new global::System.ArgumentException("An InfoEntry object with the same title already exists in that category.", "info");
			}
			value.Add(info);
		}

		public global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<string, object>> CreateReport(bool includePrivate = false)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<string, object>> dictionary = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<string, object>>();
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.IList<global::SRDebugger.InfoEntry>> item in _info)
			{
				global::System.Collections.Generic.Dictionary<string, object> dictionary2 = new global::System.Collections.Generic.Dictionary<string, object>();
				foreach (global::SRDebugger.InfoEntry item2 in item.Value)
				{
					if (!item2.IsPrivate || includePrivate)
					{
						dictionary2.Add(item2.Title, item2.Value);
					}
				}
				dictionary.Add(item.Key, dictionary2);
			}
			return dictionary;
		}

		private void CreateDefaultSet()
		{
			_info.Add("System", new global::SRDebugger.InfoEntry[7]
			{
				global::SRDebugger.InfoEntry.Create("Operating System", global::UnityEngine.SystemInfo.operatingSystem),
				global::SRDebugger.InfoEntry.Create("Device Name", global::UnityEngine.SystemInfo.deviceName, isPrivate: true),
				global::SRDebugger.InfoEntry.Create("Device Type", global::UnityEngine.SystemInfo.deviceType),
				global::SRDebugger.InfoEntry.Create("Device Model", global::UnityEngine.SystemInfo.deviceModel),
				global::SRDebugger.InfoEntry.Create("CPU Type", global::UnityEngine.SystemInfo.processorType),
				global::SRDebugger.InfoEntry.Create("CPU Count", global::UnityEngine.SystemInfo.processorCount),
				global::SRDebugger.InfoEntry.Create("System Memory", SRFileUtil.GetBytesReadable((long)global::UnityEngine.SystemInfo.systemMemorySize * 1024L * 1024))
			});
			_info.Add("Unity", new global::SRDebugger.InfoEntry[9]
			{
				global::SRDebugger.InfoEntry.Create("Version", global::UnityEngine.Application.unityVersion),
				global::SRDebugger.InfoEntry.Create("Debug", global::UnityEngine.Debug.isDebugBuild),
				global::SRDebugger.InfoEntry.Create("Unity Pro", global::UnityEngine.Application.HasProLicense()),
				global::SRDebugger.InfoEntry.Create("Genuine", global::SRF.SRFStringExtensions.Fmt("{0} ({1})", global::UnityEngine.Application.genuine ? "Yes" : "No", global::UnityEngine.Application.genuineCheckAvailable ? "Trusted" : "Untrusted")),
				global::SRDebugger.InfoEntry.Create("System Language", global::UnityEngine.Application.systemLanguage),
				global::SRDebugger.InfoEntry.Create("Platform", global::UnityEngine.Application.platform),
				global::SRDebugger.InfoEntry.Create("IL2CPP", "No"),
				global::SRDebugger.InfoEntry.Create("Application Version", global::UnityEngine.Application.version),
				global::SRDebugger.InfoEntry.Create("SRDebugger Version", "1.7.1")
			});
			_info.Add("Display", new global::SRDebugger.InfoEntry[4]
			{
				global::SRDebugger.InfoEntry.Create("Resolution", () => global::UnityEngine.Screen.width + "x" + global::UnityEngine.Screen.height),
				global::SRDebugger.InfoEntry.Create("DPI", () => global::UnityEngine.Screen.dpi),
				global::SRDebugger.InfoEntry.Create("Fullscreen", () => global::UnityEngine.Screen.fullScreen),
				global::SRDebugger.InfoEntry.Create("Orientation", () => global::UnityEngine.Screen.orientation)
			});
			_info.Add("Runtime", new global::SRDebugger.InfoEntry[4]
			{
				global::SRDebugger.InfoEntry.Create("Play Time", () => global::UnityEngine.Time.unscaledTime),
				global::SRDebugger.InfoEntry.Create("Level Play Time", () => global::UnityEngine.Time.timeSinceLevelLoad),
				global::SRDebugger.InfoEntry.Create("Current Level", delegate
				{
					global::UnityEngine.SceneManagement.Scene activeScene = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
					return global::SRF.SRFStringExtensions.Fmt("{0} (Index: {1})", activeScene.name, activeScene.buildIndex);
				}),
				global::SRDebugger.InfoEntry.Create("Quality Level", () => global::UnityEngine.QualitySettings.names[global::UnityEngine.QualitySettings.GetQualityLevel()] + " (" + global::UnityEngine.QualitySettings.GetQualityLevel() + ")")
			});
			global::UnityEngine.TextAsset textAsset = (global::UnityEngine.TextAsset)global::UnityEngine.Resources.Load("UnityCloudBuildManifest.json");
			global::System.Collections.Generic.Dictionary<string, object> dictionary = ((textAsset != null) ? (global::SRF.Json.Deserialize(textAsset.text) as global::System.Collections.Generic.Dictionary<string, object>) : null);
			if (dictionary != null)
			{
				global::System.Collections.Generic.List<global::SRDebugger.InfoEntry> list = new global::System.Collections.Generic.List<global::SRDebugger.InfoEntry>(dictionary.Count);
				foreach (global::System.Collections.Generic.KeyValuePair<string, object> item in dictionary)
				{
					if (item.Value != null)
					{
						string value = item.Value.ToString();
						list.Add(global::SRDebugger.InfoEntry.Create(GetCloudManifestPrettyName(item.Key), value));
					}
				}
				_info.Add("Build", list);
			}
			_info.Add("Features", new global::SRDebugger.InfoEntry[4]
			{
				global::SRDebugger.InfoEntry.Create("Location", global::UnityEngine.SystemInfo.supportsLocationService),
				global::SRDebugger.InfoEntry.Create("Accelerometer", global::UnityEngine.SystemInfo.supportsAccelerometer),
				global::SRDebugger.InfoEntry.Create("Gyroscope", global::UnityEngine.SystemInfo.supportsGyroscope),
				global::SRDebugger.InfoEntry.Create("Vibration", global::UnityEngine.SystemInfo.supportsVibration)
			});
			_info.Add("Graphics", new global::SRDebugger.InfoEntry[13]
			{
				global::SRDebugger.InfoEntry.Create("Device Name", global::UnityEngine.SystemInfo.graphicsDeviceName),
				global::SRDebugger.InfoEntry.Create("Device Vendor", global::UnityEngine.SystemInfo.graphicsDeviceVendor),
				global::SRDebugger.InfoEntry.Create("Device Version", global::UnityEngine.SystemInfo.graphicsDeviceVersion),
				global::SRDebugger.InfoEntry.Create("Max Tex Size", global::UnityEngine.SystemInfo.maxTextureSize),
				global::SRDebugger.InfoEntry.Create("NPOT Support", global::UnityEngine.SystemInfo.npotSupport),
				global::SRDebugger.InfoEntry.Create("Render Textures", global::SRF.SRFStringExtensions.Fmt("{0} ({1})", global::UnityEngine.SystemInfo.supportsRenderTextures ? "Yes" : "No", global::UnityEngine.SystemInfo.supportedRenderTargetCount)),
				global::SRDebugger.InfoEntry.Create("3D Textures", global::UnityEngine.SystemInfo.supports3DTextures),
				global::SRDebugger.InfoEntry.Create("Compute Shaders", global::UnityEngine.SystemInfo.supportsComputeShaders),
				global::SRDebugger.InfoEntry.Create("Image Effects", global::UnityEngine.SystemInfo.supportsImageEffects),
				global::SRDebugger.InfoEntry.Create("Cubemaps", global::UnityEngine.SystemInfo.supportsRenderToCubemap),
				global::SRDebugger.InfoEntry.Create("Shadows", global::UnityEngine.SystemInfo.supportsShadows),
				global::SRDebugger.InfoEntry.Create("Stencil", global::UnityEngine.SystemInfo.supportsStencil),
				global::SRDebugger.InfoEntry.Create("Sparse Textures", global::UnityEngine.SystemInfo.supportsSparseTextures)
			});
		}

		private static string GetCloudManifestPrettyName(string name)
		{
			return name switch
			{
				"scmCommitId" => "Commit", 
				"scmBranch" => "Branch", 
				"cloudBuildTargetName" => "Build Target", 
				"buildStartTime" => "Build Date", 
				_ => name.Substring(0, 1).ToUpper() + name.Substring(1), 
			};
		}
	}
}
