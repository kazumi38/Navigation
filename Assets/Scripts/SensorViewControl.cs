// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


#if ENABLE_WINMD_SUPPORT
using System;
using Windows.Media.Capture.Frames;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

#endif

public class SensorViewControl : MonoBehaviour {

    private Texture2D tex = null;
    private byte[] bytes = null;

    [SerializeField] private TextMeshProUGUI t;
    // Use this for initialization
    void Start() {
        Debug.Log("hello world\n");

#if ENABLE_WINMD_SUPPORT
        // Debug.Log("init unity uwp sensor\n");
        Task.Run(() => { InitSensor(); });
#endif
    }
	
	// Update is called once per frame
	void Update () {

	}


#if ENABLE_WINMD_SUPPORT
    
    private async void InitSensor()
    {
        Debug.Log("InitSensor\n");
        Debug.Log("\t=> mediaFrameSourceGroupList\n");

        // var mediaFrameSourceGroupList = MediaFrameSourceGroup.FindAllAsync();

        // if(await Task.WhenAny(mediaFrameSourceGroupList.AsTask(), Task.Delay(10000)) != mediaFrameSourceGroupList.AsTask())
        // {
        //     UnityEngine.Debug.Log("[DBG] : mediaFrameSourceGroupList timeout");
        //     Debug.Log("InitSensor => mediaFrameSourceGroupList timeout\n");
        //     return;
        // }

        // await Task.Delay(1000);
        Debug.Log("\t=> mediaFrameSourceGroupList\n");

        Debug.Log("[DBG] : mediaFrameSourceGroupList");

        // var mediaFrameSourceGroup = mediaFrameSourceGroupList[0];
        // Debug.Log("InitSensor => mediaFrameSourceGroup\n");
        
        // var mediaFrameSourceInfo = mediaFrameSourceGroup.SourceInfos[0];
        // Debug.Log("InitSensor => mediaFrameSourceInfo\n");

        // var mediaCapture = new MediaCapture();
        // Debug.Log("InitSensor => mediaCaption\n");

        // var settings = new MediaCaptureInitializationSettings()
        // {
        //     SourceGroup = mediaFrameSourceGroup,
        //     SharingMode = MediaCaptureSharingMode.SharedReadOnly,
        //     StreamingCaptureMode = StreamingCaptureMode.Video,
        //     MemoryPreference = MediaCaptureMemoryPreference.Cpu,
        // };
        // Debug.Log("InitSensor => settings fin\n");
        // try
        // {
        //     Debug.Log("InitSensor => try\n");
        //     await mediaCapture.InitializeAsync(settings);
        //     var mediaFrameSource = mediaCapture.FrameSources[mediaFrameSourceInfo.Id];
        //     var mediaframereader = await mediaCapture.CreateFrameReaderAsync(mediaFrameSource, mediaFrameSource.CurrentFormat.Subtype);
        //     mediaframereader.FrameArrived += FrameArrived;
        //     await mediaframereader.StartAsync();
        // }
        // catch (Exception e)
        // {
        //     Debug.Log("InitSensor => catch\n");
        //     UnityEngine.WSA.Application.InvokeOnAppThread(() => { UnityEngine.Debug.Log(e); }, true);
        // }
    }

    private void FrameArrived(MediaFrameReader sender, MediaFrameArrivedEventArgs args)
    {
        Debug.Log("FrameArrived\n");
        var mediaframereference = sender.TryAcquireLatestFrame();
        if (mediaframereference != null)
        {
            Debug.Log("FrameArrived => mediaframereference\n");
            var videomediaframe = mediaframereference?.VideoMediaFrame;
            var softwarebitmap = videomediaframe?.SoftwareBitmap;
            if (softwarebitmap != null)
            {
                Debug.Log("FrameArrived => softwarebitmap\n");
                softwarebitmap = SoftwareBitmap.Convert(softwarebitmap, BitmapPixelFormat.Rgba8, BitmapAlphaMode.Premultiplied);
                int w = softwarebitmap.PixelWidth;
                int h = softwarebitmap.PixelHeight;
                if (bytes==null)
                {
                    bytes = new byte[w * h * 4];
                }
                softwarebitmap.CopyToBuffer(bytes.AsBuffer());
                softwarebitmap.Dispose();
                UnityEngine.WSA.Application.InvokeOnAppThread(() => {
                    if (tex == null)
                    {
                        Debug.Log("FrameArrived => softwarebitmap => tex == null\n");

                        tex = new Texture2D(w, h, TextureFormat.RGBA32, false);
                        GetComponent<Renderer>().material.mainTexture = tex;
                    }
                    for (int i = 0; i < bytes.Length / 4; ++i)
                    {
                        byte b = bytes[i * 4];
                        bytes[i * 4 + 0] = 0;
                        bytes[i * 4 + 1] = 0;
                        bytes[i * 4 + 2] = 0;
                        bytes[i * 4 + 3] = 255;
                        if (b==0)
                        {
                            bytes[i * 4 + 0] = 128;
                        }
                        if (b==1)
                        {
                            bytes[i * 4 + 0] = 255;
                        }
                    }
                    tex.LoadRawTextureData(bytes);
                    tex.Apply();
                }, true);
            }
            mediaframereference.Dispose();
        }
    }

    
#endif
}