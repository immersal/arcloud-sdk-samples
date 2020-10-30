/*===============================================================================
Copyright (C) 2020 Immersal Ltd. All Rights Reserved.

This file is part of the Immersal SDK.

The Immersal SDK cannot be copied, distributed, or made available to
third-parties for commercial purposes without written permission of Immersal Ltd.

Contact sdk@immersal.com for licensing requests.
===============================================================================*/

using System;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Immersal.REST
{
    public class CoroutineJob
    {
        public IJobHost host;
        public bool isNetworkError = false;
        public Action OnStart;
        public Action<UnityWebRequest> OnError;
        public Action<float> OnProgress;

        public virtual IEnumerator RunJob()
        {
            yield return null;
        }
    }

    public class CoroutineJobClear : CoroutineJob
    {
        public bool anchor;
        public Action<SDKClearResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobClear ***************************");
            this.OnStart?.Invoke();

            SDKClearRequest r = new SDKClearRequest();
            r.token = host.token;
            r.anchor = this.anchor;
            string jsonString = JsonUtility.ToJson(r);
            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.CLEAR_JOB), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                Debug.Log("Response code: " + request.responseCode);

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);

                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKClearResult result = JsonUtility.FromJson<SDKClearResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobConstruct : CoroutineJob
    {
        public string name;
        public int featureCount = 600;
        public Action<SDKConstructResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobConstruct ***************************");
            this.OnStart?.Invoke();

            SDKConstructRequest r = new SDKConstructRequest();
            r.token = host.token;
            r.name = this.name;
            r.featureCount = this.featureCount;

            string jsonString = JsonUtility.ToJson(r);
            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.CONSTRUCT_MAP), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                Debug.Log("Response code: " + request.responseCode);

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKConstructResult result = JsonUtility.FromJson<SDKConstructResult>(request.downloadHandler.text);
                    OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobRestoreMapImages : CoroutineJob
    {
        public int id;
        public Action<SDKRestoreMapImagesResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobRestoreMapImages ***************************");
            this.OnStart?.Invoke();

            SDKRestoreMapImagesRequest r = new SDKRestoreMapImagesRequest();
            r.token = host.token;
            r.id = this.id;
            string jsonString = JsonUtility.ToJson(r);
            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.RESTORE_MAP_IMAGES), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKRestoreMapImagesResult result = JsonUtility.FromJson<SDKRestoreMapImagesResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobDeleteMap : CoroutineJob
    {
        public int id;
        public Action<SDKDeleteMapResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobDeleteMap ***************************");
            this.OnStart?.Invoke();

            SDKDeleteMapRequest r = new SDKDeleteMapRequest();
            r.token = host.token;
            r.id = this.id;
            string jsonString = JsonUtility.ToJson(r);
            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.DELETE_MAP), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKDeleteMapResult result = JsonUtility.FromJson<SDKDeleteMapResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobStatus : CoroutineJob
    {
        public Action<SDKStatusResult> OnSuccess;

        public override IEnumerator RunJob()
        {
//            Debug.Log("*************************** CoroutineJobStatus ***************************");
            this.OnStart?.Invoke();

            SDKStatusRequest r = new SDKStatusRequest();
            r.token = host.token;
            string jsonString = JsonUtility.ToJson(r);
            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.STATUS), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKStatusResult result = JsonUtility.FromJson<SDKStatusResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobCapture : CoroutineJob
    {
        public int run;
        public int index;
        public bool anchor;
        public Vector4 intrinsics;
        public Matrix4x4 rotation;
        public Vector3 position;
        public double latitude;
        public double longitude;
        public double altitude;
        public string encodedImage;
        public string imagePath;
        public Action<SDKImageResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobCapture ***************************");
            this.OnStart?.Invoke();

            SDKImageRequest imageRequest = new SDKImageRequest();
            imageRequest.token = host.token;
            imageRequest.run = this.run;
            imageRequest.index = this.index;
            imageRequest.anchor = this.anchor;
            imageRequest.px = position.x;
            imageRequest.py = position.y;
            imageRequest.pz = position.z;
            imageRequest.r00 = rotation.m00;
            imageRequest.r01 = rotation.m01;
            imageRequest.r02 = rotation.m02;
            imageRequest.r10 = rotation.m10;
            imageRequest.r11 = rotation.m11;
            imageRequest.r12 = rotation.m12;
            imageRequest.r20 = rotation.m20;
            imageRequest.r21 = rotation.m21;
            imageRequest.r22 = rotation.m22;
            imageRequest.fx = intrinsics.x;
            imageRequest.fy = intrinsics.y;
            imageRequest.ox = intrinsics.z;
            imageRequest.oy = intrinsics.w;
            imageRequest.latitude = latitude;
            imageRequest.longitude = longitude;
            imageRequest.altitude = altitude;

            byte[] image = File.ReadAllBytes(imagePath);
            imageRequest.b64 = Convert.ToBase64String(image);

            string jsonString = JsonUtility.ToJson(imageRequest);

            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.CAPTURE_IMAGE), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                Debug.Log("Response code: " + request.responseCode);

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKImageResult result = JsonUtility.FromJson<SDKImageResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                    File.Delete(imagePath);
                }
            }
        }
    }

    public class CoroutineJobLocalize : CoroutineJob
    {
        public Vector4 intrinsics;
        public Quaternion rotation;
        public Vector3 position;
        public byte[] pixels;
        public int width;
        public int height;
        public int param1;
        public int param2;
        public float param3;
        public float param4;
        public Action<int, Vector3, Quaternion> OnSuccess;
        public Action OnFail;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobLocalize ***************************");
            this.OnStart?.Invoke();

            Vector3 pos = new Vector3();
            Quaternion rot = new Quaternion();

            Task<int> t = Task.Run(() =>
            {
                GCHandle bufferHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                int r = Immersal.Core.LocalizeImage(out pos, out rot, width, height, ref intrinsics, bufferHandle.AddrOfPinnedObject());
                bufferHandle.Free();
                return r;
            });

            while (!t.IsCompleted)
            {
                yield return null;
            }

            int mapHandle = t.Result;

            if (mapHandle >= 0)
            {
                this.OnSuccess?.Invoke(mapHandle, pos, rot);
            }
            else
            {
                this.OnFail?.Invoke();
            }
        }
    }

    public class CoroutineJobLocalizeServer : CoroutineJob
    {
        public Vector4 intrinsics;
        public Quaternion rotation;
        public Vector3 position;
        public byte[] pixels;
        public int width;
        public int height;
        public int channels;
        public int param1;
        public int param2;
        public float param3;
        public float param4;
        public double latitude = 0.0;
        public double longitude = 0.0;
        public double radius = 0.0;
        public bool useGPS = false;
        public SDKMapId[] mapIds;
        public Action<SDKLocalizeResult> OnResult;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobLocalize On-Server ***************************");
            this.OnStart?.Invoke();

            byte[] capture = new byte[channels * width * height + 1024];
            Task<(string, icvCaptureInfo)> t = Task.Run(() =>
            {
                icvCaptureInfo info = Immersal.Core.CaptureImage(capture, capture.Length, pixels, width, height, channels);
                return (Convert.ToBase64String(capture, 0, info.captureSize), info);
            });

            while (!t.IsCompleted)
            {
                yield return null;
            }

            string encodedImage = t.Result.Item1;
            icvCaptureInfo captureInfo = t.Result.Item2;

            SDKLocalizeRequest imageRequest = this.useGPS ? new SDKGeoLocalizeRequest() : new SDKLocalizeRequest();
            imageRequest.token = host.token;
            imageRequest.fx = intrinsics.x;
            imageRequest.fy = intrinsics.y;
            imageRequest.ox = intrinsics.z;
            imageRequest.oy = intrinsics.w;
            imageRequest.param1 = 0;
            imageRequest.param2 = 12;
            imageRequest.param3 = 0.0;
            imageRequest.param4 = 2.0;

            imageRequest.b64 = encodedImage;

            if (this.useGPS)
            {
                SDKGeoLocalizeRequest gr = imageRequest as SDKGeoLocalizeRequest;
                gr.latitude = this.latitude;
                gr.longitude = this.longitude;
                gr.radius = this.radius;
            }
            else
            {
                imageRequest.mapIds = this.mapIds;
            }

            string jsonString = JsonUtility.ToJson(imageRequest);
            string endpoint = this.useGPS ? Endpoint.SERVER_GEOLOCALIZE : Endpoint.SERVER_LOCALIZE;

            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, endpoint), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    Debug.Log(request.downloadHandler.text);
                    SDKLocalizeResult result = JsonUtility.FromJson<SDKLocalizeResult>(request.downloadHandler.text);
                    this.OnResult?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobEcef : CoroutineJob
    {
        public int id;
        public Action<SDKEcefResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobEcef ***************************");
            this.OnStart?.Invoke();

            SDKEcefRequest ecefRequest = new SDKEcefRequest();
            ecefRequest.token = host.token;
            ecefRequest.id = this.id;

            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.SERVER_ECEF), JsonUtility.ToJson(ecefRequest)))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKEcefResult result = JsonUtility.FromJson<SDKEcefResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobListJobs : CoroutineJob
    {
        public double latitude = 0.0;
        public double longitude = 0.0;
        public double radius = 0.0;
        public bool useGPS = false;
        public List<int> activeMaps;
        public Action<SDKJobsResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobListJobs ***************************");
            this.OnStart?.Invoke();

            SDKJobsRequest r = this.useGPS ? new SDKGeoJobsRequest() : new SDKJobsRequest();
            r.token = host.token;

            if (this.useGPS)
            {
                SDKGeoJobsRequest gr = r as SDKGeoJobsRequest;
                gr.latitude = this.latitude;
                gr.longitude = this.longitude;
                gr.radius = this.radius;
            }

            string jsonString = JsonUtility.ToJson(r);
            string endpoint = this.useGPS ? Endpoint.LIST_GEOJOBS : Endpoint.LIST_JOBS;

            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, endpoint), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKJobsResult result = JsonUtility.FromJson<SDKJobsResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobLoadMap : CoroutineJob
    {
        public int id;
        public Action<SDKMapResult> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobLoadMap ***************************");
            this.OnStart?.Invoke();

            SDKMapRequest r = new SDKMapRequest();
            r.token = host.token;
            r.id = this.id;

            string jsonString = JsonUtility.ToJson(r);
            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.LOAD_MAP), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
                request.useHttpContinue = false;
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    SDKMapResult result = JsonUtility.FromJson<SDKMapResult>(request.downloadHandler.text);
                    this.OnSuccess?.Invoke(result);
                }
            }
        }
    }

    public class CoroutineJobFreeMap : CoroutineJob
    {
        public int id;
        public Action<int> OnSuccess;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobFreeMap ***************************");
            this.OnStart?.Invoke();

            Task<int> t0 = Task.Run(() =>
            {
                return Immersal.Core.FreeMap(id);
            });

            while (!t0.IsCompleted)
            {
                yield return null;
            }
            
            this.OnSuccess?.Invoke(t0.Result);
        }
    }

    public class CoroutineJobLogin : CoroutineJob
    {
        public string username;
        public string password;
        public Action<SDKLoginResult> OnResult;

        public override IEnumerator RunJob()
        {
            Debug.Log("*************************** CoroutineJobLogin ***************************");
            this.OnStart?.Invoke();

            SDKLoginRequest loginRequest = new SDKLoginRequest();
            loginRequest.login = this.username;
            loginRequest.password = this.password;

            string jsonString = JsonUtility.ToJson(loginRequest);
            using (UnityWebRequest request = UnityWebRequest.Put(string.Format(Endpoint.URL_FORMAT, host.server, Endpoint.LOGIN), jsonString))
            {
                request.method = UnityWebRequest.kHttpVerbPOST;
				request.useHttpContinue = false;
				request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    this.OnProgress?.Invoke(request.uploadHandler.progress);
                    yield return null;
                }

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(request.error);
                    this.isNetworkError = true;
                    this.OnError?.Invoke(request);
                }
                else
                {
                    SDKLoginResult loginResult = JsonUtility.FromJson<SDKLoginResult>(request.downloadHandler.text);
                    OnResult?.Invoke(loginResult);
                }
            }
        }
    }
}
