using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    internal class GameManager
    {
        // 대기자
        List<string> waitingList = new();

        // 플레이어 매칭
        Dictionary<string, string> activeMatches = new();


        private readonly object lockObj = new();

        // 대전 상대 매치
        public (string?, string?) HandleActiveMatches(string ip)
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

                    activeMatches[player1] = player2;
                    activeMatches[player2] = player1;

                    return (player1, player2);
                }

                return (null, null); 
            }
        }

        // 대전자 찾기
        public string HandleSearchOpponent(string ip)
        {

            lock (lockObj)
            {
                if (!activeMatches.ContainsKey(ip)) return "";

                // 대전자 찾기
                string opponent = activeMatches[ip];

                return opponent; 
            }

        }

        // 대전 정보 삭제(대전 끝났을 시)
        public void DeleteActiveMatches(string ip)
        {
            lock (lockObj)
            {
                if (!activeMatches.ContainsKey(ip)) return;

                string opponent = activeMatches[ip];

                activeMatches.Remove(ip);
                activeMatches.Remove(opponent); 
            }
        }
                
    }

}
