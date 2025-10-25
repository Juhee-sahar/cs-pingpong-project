using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    internal class GameSession
    {
        public string PlayerLeft {  get; set; }
        public string PlayerRight { get; set; }
        public ClientGameState GameState { get; set; }  

        public GameSession(string left, string right) 
        { 
            PlayerLeft = left;  
            PlayerRight = right;
            GameState = new ClientGameState(left, right);   
        }
    }

    internal class GameManager
    {
        // 대기자
        private readonly List<string> waitingList = new();
        // 플레이어 매칭
        private readonly Dictionary<string, GameSession> activeGames = new();
        private readonly object lockObj = new();

        // 대전 상대 매치
        public (string?, string?, GameSession?) HandleActiveMatches(string ip)
        {
            lock (lockObj)
            {
                if (!waitingList.Contains(ip)) waitingList.Add(ip);

                // 웨이팅 리스트는 0 ~ 2 명까지 있다가 >> 채워지면 바로 지워지는 시스템
                if (waitingList.Count >= 2)
                {
                    string player1 = waitingList[0];
                    string player2 = waitingList[1];
                    waitingList.RemoveRange(0, 2);


                    // 여기서 left, right 지정해줘야 함
                    // 세션
                    GameSession session = new(player1, player2);

                    activeGames[player1] = session;
                    activeGames[player2] = session;

                    return (player1, player2, session);
                }

                return (null, null, null); 
            }
        }

        // 현재 ip의 세션 반환
        public GameSession? GetGameSession(string ip)
        {
            lock (lockObj)
            {
                if (!activeGames.ContainsKey(ip)) return null;
                return activeGames[ip];
            }
        }


        // 대전 정보 삭제(대전 끝났을 시)
        public void DeleteActiveMatches(string ip)
        {
            lock (lockObj)
            {
                if (!activeGames.ContainsKey(ip)) return;

                var session = activeGames[ip];
                activeGames.Remove(session.PlayerLeft);
                activeGames.Remove(session.PlayerLeft);
            }
        }

        // 대전자 검색
        public string HandleSearchOpponent(string ip)
        {
            lock (lockObj)
            {
                if (!activeGames.ContainsKey(ip)) return "";
                var session = activeGames[ip];
                if (session.PlayerLeft == ip) return session.PlayerRight;
                else return session.PlayerLeft;
            }
        }
    }

}
