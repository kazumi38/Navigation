// using System.Diagnostics;
// using System.Collections;
// using System.Collections.Generic;

// using UnityEngine;

// #if ENABLE_WINMD_SUPPORT
// using System;
// using Windows.Media.Capture.Frames;
// using Windows.Media.Capture;
// using Windows.Media.MediaProperties;
// using System.Threading.Tasks;
// using Windows.Graphics.Imaging;
// using System.Runtime.InteropServices.WindowsRuntime;

// #endif

// public class AsyncTest : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {

//     }
// #if ENABLE_WINMD_SUPPORT
//     public async void awaitasync()
//     {
//         var sensorGroups = await MediaFrameSourceGroup.FindAllAsync();
//         var foundGroup = sensorGroups.Select(g => new
//         {
//             group = g,
//             color1 = g.SourceInfos.Where(info => info.SourceKind == MediaFrameSourceKind.Color && info.DeviceInformation.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front).FirstOrDefault(),
//             color2 = g.SourceInfos.Where(info => info.SourceKind == MediaFrameSourceKind.Color && info.DeviceInformation.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back).FirstOrDefault()
//         }).Where(g => g.color1 != null && g.color2 != null).FirstOrDefault();

//         if (foundGroup == null)
//         {
//             System.Diagnostics.Debug.WriteLine("No groups found.");
//             return;
//         }
//     }
// #endif
// }
