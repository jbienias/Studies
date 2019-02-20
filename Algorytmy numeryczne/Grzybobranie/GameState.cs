using System;
using System.Collections.Generic;
using System.Linq;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Grzybobranie {
    public class GameState {
        public static int mapSize; //potrzebne przy liczeniu modulo(+) podczas ruchu / tworzeniu nowego stanu
        public int index; //uzyte glownie do array listy globalnej podczas generacji, aby rozroznic stany
        public int playerOnePos; //poz gracza 1
        public int playerTwoPos; //poz gracza 2
        public int mushroomsOneCount; //liczba grzybow gracza 1
        public int mushroomsTwoCount; //liczba grzybow gracza 2
        public int mushroomsLeftCount; //pozostala liczba grzybow na mapie w stanie
        public bool[] mushroomMap; //aktualne rozmieszczenie grzybów w stanie
        public bool turn; // true -> tura gracza 1, false -> tura gracza 2
        public bool win; //czy dany stan wygrywa gracz 1 ?
        public bool ready; //czy dany stan gry jest juz wyliczony
        public bool end; //czy dany stan gry jest stanem konczącym grę (wykorzystany w monte carlo)
        public List<GameState> equation; //kolejne stany na ktore mozemy wejsc z tego stanu, zwane z reguly subStanami (subState)

        public GameState(Data data) { //uzywany tylko do stworzenia PIERWSZEGO stanu gry
            turn = true; // wiemy, ze pierwszy ruch ma ZAWSZE gracz 1
            index = 0;
            mapSize = data.mapSize;
            playerOnePos = data.playerOnePos;
            playerTwoPos = data.playerTwoPos;
            mushroomsOneCount = 0;
            mushroomsTwoCount = 0;
            mushroomMap = (bool[])data.mushroomMap.Clone();
            mushroomsLeftCount = data.mushroomCount;
            equation = new List<GameState>();
        }

        public GameState(bool turn, int playerOnePos, int playerTwoPos, int mushroomsOneCount, int mushroomsTwoCount, bool[] mushroomMap) {
            this.turn = turn;
            this.playerOnePos = modulo(playerOnePos, mapSize);
            this.playerTwoPos = modulo(playerTwoPos, mapSize);
            this.mushroomsOneCount = mushroomsOneCount;
            this.mushroomsTwoCount = mushroomsTwoCount;
            this.mushroomMap = (bool[])mushroomMap.Clone();
            if (this.turn == false && this.mushroomMap[this.playerOnePos]) {
                this.mushroomsOneCount++;
                this.mushroomMap[this.playerOnePos] = false;
            } else if (this.turn && this.mushroomMap[this.playerTwoPos]) {
                this.mushroomsTwoCount++;
                this.mushroomMap[this.playerTwoPos] = false;
            }
            mushroomsLeftCount = countMushrooms();
            equation = new List<GameState>();
        }

        public bool nextTurn() {
            if (turn)
                return false;
            else
                return true;
        }

        public int countMushrooms() {
            int count = 0;
            for (int i = 0; i < mushroomMap.Length; i++)
                if (mushroomMap[i])
                    count++;
            return count;
        }

        public int countSameStates(GameState state, Dice dice) {
            int count = 0;
            int i = 0;
            foreach (var s in equation) {
                if (s.Equals(state))
                    count += dice.probability[i];
                i++;
            }
            return count;
        }

        public static int modulo(int x, int m) {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            if (obj == null || obj.GetType() != GetType()) return false;
            GameState gameState = (GameState)obj;
            if (turn != gameState.turn) return false;
            if (playerOnePos != gameState.playerOnePos) return false;
            if (playerTwoPos != gameState.playerTwoPos) return false;
            if (mushroomsOneCount != gameState.mushroomsOneCount) return false;
            if (mushroomsTwoCount != gameState.mushroomsTwoCount) return false;
            return mushroomMap.SequenceEqual(gameState.mushroomMap);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            string tmp = "GameState : " + index + "\n";
            tmp += "Map size : " + mapSize + "\n";
            tmp += "Player 1 position : " + playerOnePos + "\n";
            tmp += "Player 2 position : " + playerTwoPos + "\n";
            tmp += "Player 1 mushrooms : " + mushroomsOneCount + "\n";
            tmp += "Player 2 mushrooms : " + mushroomsTwoCount + "\n";
            tmp += "Mushroom map : ";
            for (int i = 0; i < mushroomMap.Length; i++) {
                tmp += mushroomMap[i] + " ";
            }
            tmp += "\n";
            tmp += "Winning state? : " + win + "\n";
            tmp += "Is ready? : " + ready + "\n";
            return tmp;
        }

        public string equationToString() {
            string tmp = "X" + index;
            if (equation.Count > 0) { tmp += " = "; }
            for (int i = 0; i < equation.Count; i++) {
                tmp += "X" + equation[i].index;
                if (i != equation.Count - 1)
                    tmp += " + ";
            }
            tmp += "\n";
            return tmp;
        }
    }
}

