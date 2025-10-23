using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameSocket
{
    public class TCPSocketClient
    {

        // 이벤트: 수신 메시지, 연결 끊김 통지
        public event Action<string>? MessageReceived;
        public event Action? Disconnected;

        // 서버 접속에 필요한 데이터 준비
        private IPAddress? _serverIPAddress = IPAddress.Parse("127.0.0.1");
        private int _serverPort;
        private TcpClient? _client;

        // getter 구현 (외부에서 변경 불가, 내부 메서드를 통해서만 변경)
        public IPAddress? ServerIPAddress => _serverIPAddress;
        public int ServerPort => _serverPort;
        public TcpClient? Client => _client;

        // setter 구현 (IP 검증 및 설정)
        public bool SetServerIPAddress(string addressStr)
        {
            if (!IPAddress.TryParse(addressStr, out var parsed))
            {
                // UI/상위 로직에서 사용자 알림을 하도록 이벤트나 반환값만 제공
                return false;
            }

            _serverIPAddress = parsed;
            return true;
        }

        // setter 구현 (포트 번호 검증 및 설정 메서드)
        public bool SetServerPort(string portStr)
        {
            if (!int.TryParse(portStr, out var portNum) || portNum <= 0 || portNum > 65535)
            {
                return false;
            }

            _serverPort = portNum;
            return true;
        }

        // 서버 접속 메서드
        public async Task ConnectToServerAsync()
        {
            if (_serverIPAddress == null || _serverPort == 0)
            {
                throw new InvalidOperationException("Server IP 또는 Port가 설정되지 않았습니다.");
            }

            if (_client == null)
            {
                _client = new TcpClient();
            }

            try
            {
                await _client.ConnectAsync(_serverIPAddress, _serverPort);
                // 연결되면 수신 루프 시작
                _ = Task.Run(() => ReadDataAsync(_client));
            }
            catch
            {
                // 연결 실패는 상위(호출자)에서 처리하도록 예외를 다시 던짐
                throw;
            }
        }

        // 서버로부터 데이터 수신 대기 메서드 (메시지를 받으면 이벤트 발생)
        private async Task ReadDataAsync(TcpClient client)
        {
            try
            {
                using (var netStream = client.GetStream())
                using (var reader = new StreamReader(netStream))
                {
                    char[] buffer = new char[1024];

                    while (true)
                    {
                        int readCount = await reader.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

                        if (readCount == 0)
                        {
                            // 0이면 연결 종료
                            Disconnected?.Invoke();
                            client.Close();
                            break;
                        }

                        // 실제 받은 길이만큼 문자열 생성
                        string message = new string(buffer, 0, readCount);
                        MessageReceived?.Invoke(message);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // 스트림이 닫힌 경우, 연결 끊김 알림
                Disconnected?.Invoke();
            }
            catch (Exception)
            {
                // 기타 예외 시에도 연결 끊김 이벤트 발생시키는 것이 안전
                Disconnected?.Invoke();
            }
        }

        // 서버로 데이터 전송
        public async Task SendData(string userInput)
        {
            if (string.IsNullOrEmpty(userInput) || _client == null || !_client.Connected)
            {
                throw new InvalidOperationException("서버에 연결되어 있지 않거나 전송할 데이터가 없습니다.");
            }

            try
            {
                using (var writer = new StreamWriter(_client.GetStream(), leaveOpen: true))
                {
                    writer.AutoFlush = true;
                    await writer.WriteAsync(userInput).ConfigureAwait(false);
                }
            }
            catch
            {
                // 전송 실패는 상위에서 처리하도록 예외 발생
                throw;
            }
        }

        // 명시적 연결 해제 메서드 (UI에서 연결 해제 버튼에 연결)
        public void Disconnect()
        {
            try
            {
                if (_client != null)
                {
                    _client.Close();
                    _client = null;
                }
            }
            finally
            {
                Disconnected?.Invoke();
            }
        }


    }
}
