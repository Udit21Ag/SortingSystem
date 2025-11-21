using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;

public class SensorCamera : MonoBehaviour
{
    public Camera cam;
    public float captureInterval = 0.2f;

    private RenderTexture rt;
    private Texture2D img;

    void Start()
    {
        if (cam == null) cam = GetComponent<Camera>();

        // CREATE RenderTexture ONCE ONLY
        rt = new RenderTexture(256, 256, 24);
        cam.targetTexture = rt;

        img = new Texture2D(256, 256, TextureFormat.RGB24, false);

        StartCoroutine(CaptureLoop());
    }

    IEnumerator CaptureLoop()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            byte[] imgBytes = CaptureImage();

            // Send async → DO NOT BLOCK UNITY
            StartCoroutine(SendToPythonAsync(imgBytes));

            yield return new WaitForSeconds(captureInterval);
        }
    }

    byte[] CaptureImage()
    {
        cam.Render();
        RenderTexture.active = rt;
        img.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        img.Apply();
        RenderTexture.active = null;

        return img.EncodeToJPG();
    }

    IEnumerator SendToPythonAsync(byte[] img)
    {
        yield return null;

        try
        {
            using (TcpClient client = new TcpClient())
            {
                client.ReceiveTimeout = 200;  // prevent freeze
                client.SendTimeout = 200;

                client.Connect("127.0.0.1", 5000);

                NetworkStream stream = client.GetStream();

                // Send size
                byte[] imgSize = System.BitConverter.GetBytes(img.Length);
                stream.Write(imgSize, 0, 4);

                // Send image
                stream.Write(img, 0, img.Length);

                // Read label with timeout
                if (stream.DataAvailable)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string label = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log("Python returned label: " + label);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Socket error: " + ex.Message);
        }
    }
}