using UnityEngine;
using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

public class HandTrackingData
{
    public string State { get; set; }
}

public class SocketConnector : MonoBehaviour
{
    public static event Action<HandTrackingData> OnHandTrackingDataReceived;
    private ClientWebSocket webSocket;
    private CancellationTokenSource cancellationTokenSource;
    public string serverUrl = "ws://localhost:8765";
    public bool printToConsole = false;
    private string data;
    private bool isConnected = false;
    private bool isQuitting = false;

    async void Start()
    {
        await Connect();
    }

    private async Task Connect()
    {
        try
        {
            if (webSocket != null)
            {
                if (webSocket.State == WebSocketState.Open)
                    return;

                webSocket.Dispose();
            }

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }

            webSocket = new ClientWebSocket();
            cancellationTokenSource = new CancellationTokenSource();

            await webSocket.ConnectAsync(new Uri(serverUrl), cancellationTokenSource.Token);
            Debug.Log("Đã kết nối tới hand tracking server");
            isConnected = true;
            StartReceiving();
        }
        catch (Exception e)
        {
            Debug.LogError($"Lỗi kết nối: {e.Message}");
            await CleanupAsync();
            this.enabled = false;
        }
    }

    private async void StartReceiving()
    {
        var buffer = new byte[1024 * 4];

        while (isConnected && !isQuitting && webSocket != null && webSocket.State == WebSocketState.Open)
        {
            try
            {
                var result = new ArraySegment<byte>(buffer);
                WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
                    result,
                    cancellationTokenSource.Token
                );

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await CleanupAsync();
                    this.enabled = false;
                    break;
                }
                else
                {
                    data = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                    if (printToConsole)
                    {
                        Debug.Log($"Dữ liệu nhận được: {data}");
                    }

                    try
                    {
                        data = data.Trim();
                        if (data.StartsWith("{") && data.EndsWith("}"))
                        {
                            string[] keyValuePairs = data.Trim('{', '}').Split(',');
                            string state = "";

                            foreach (string pair in keyValuePairs)
                            {
                                string[] parts = pair.Split(':');
                                if (parts.Length == 2)
                                {
                                    string key = parts[0].Trim().Trim('"');
                                    string val = parts[1].Trim().Trim('"');

                                    if (key == "state")
                                    {
                                        state = val;
                                        break;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(state))
                            {
                                if (printToConsole)
                                {
                                    Debug.Log($"State: {state}");
                                    Debug.Log("-----------------------------------------");
                                }

                                if (!isQuitting && OnHandTrackingDataReceived != null)
                                {
                                    var handData = new HandTrackingData
                                    {
                                        State = state
                                    };
                                    OnHandTrackingDataReceived(handData);
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        if (!isQuitting)
                        {
                            Debug.LogError($"Lỗi xử lý dữ liệu: {err.Message}");
                            Debug.LogError($"Dữ liệu gốc: {data}");
                            await CleanupAsync();
                            this.enabled = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (!isQuitting)
                {
                    Debug.LogError($"Lỗi nhận dữ liệu: {e.Message}");
                    await CleanupAsync();
                    this.enabled = false;
                    break;
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        CleanupAsync().GetAwaiter().GetResult();
    }

    private void OnDisable()
    {
        CleanupAsync().GetAwaiter().GetResult();
    }

    private async Task CleanupAsync()
    {
        try
        {
            isConnected = false;
            isQuitting = true;

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }

            if (webSocket != null)
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    try
                    {
                        var timeoutToken = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            string.Empty,
                            timeoutToken.Token
                        );
                        timeoutToken.Dispose();
                    }
                    catch (Exception) { }
                }
                webSocket.Dispose();
                webSocket = null;
            }

            if (OnHandTrackingDataReceived != null)
            {
                foreach (Delegate d in OnHandTrackingDataReceived.GetInvocationList())
                {
                    OnHandTrackingDataReceived -= (Action<HandTrackingData>)d;
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        catch (Exception) { }
    }
}