using System;
using System.Collections.Generic;
using System.Linq;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Grzybobranie {
    public class GameStateGenerator {
        public List<GameState> allStates;
        private Dice dice;

        public GameStateGenerator(GameState startingState, Dice dice) {
            this.dice = dice;
            allStates = new List<GameState>();
            startingState.index = 0;
            allStates.Add(startingState);
            generateStates();
        }

        private void generateStates() {
            int lowerBound = 0;
            int higherBound;
            while (!listReady()) {
                higherBound = allStates.Count;
                for (int i = lowerBound; i < higherBound; i++) {
                    GameState current = allStates[i];
                    if (current.mushroomsOneCount <= (current.mushroomsLeftCount + current.mushroomsTwoCount) && current.mushroomsTwoCount <= (current.mushroomsLeftCount + current.mushroomsOneCount)) {
                        if (current.playerOnePos != 0 && current.playerTwoPos != 0) {
                            for (int j = 0; j < dice.values.Length; j++) {
                                int first = current.playerOnePos;
                                int second = current.playerTwoPos;
                                if (current.turn)
                                    first += dice.values[j];
                                else if (!current.turn)
                                    second += dice.values[j];
                                GameState newState = new GameState(current.nextTurn(), first, second, current.mushroomsOneCount, current.mushroomsTwoCount, current.mushroomMap);
                                addToEquationAndList(current, newState);
                            }
                        } else if (current.playerOnePos == 0 && current.mushroomsOneCount >= current.mushroomsTwoCount) {
                            current.win = true;
                        } else if (current.playerTwoPos == 0 && current.mushroomsOneCount > current.mushroomsTwoCount) {
                            current.win = true;
                        }
                    } else if (current.mushroomsOneCount > (current.mushroomsLeftCount + current.mushroomsTwoCount)) {
                        current.win = true;
                    }
                    current.ready = true;
                }
                lowerBound = higherBound;
            }
        }

        public MyMatrix generateMatrix() {
            int size = allStates.Count();
            MyMatrix matrix = new MyMatrix(size, size);
            foreach (var state in allStates) {
                if (state.win)
                    matrix[state.index, state.index] = 1.0;
                else
                    matrix[state.index, state.index] = -1.0;
                foreach (var subState in state.equation) {
                    matrix[state.index, subState.index] = (double)state.countSameStates(subState, dice) / dice.probabilitySum;
                }
            }
            return matrix;
        }

        public MyMatrix generateVector() {
            int size = allStates.Count();
            MyMatrix vector = new MyMatrix(size, 1);
            foreach (var state in allStates) {
                if (state.win)
                    vector[state.index, 0] = 1.0;
            }
            return vector;
        }

        private bool listReady() {
            foreach (var state in allStates)
                if (!state.ready)
                    return false;
            return true;
        }

        private void addToEquationAndList(GameState state, GameState addedState) {
            if (allStates.Contains(addedState)) {
                int index = allStates.IndexOf(addedState);
                state.equation.Add(allStates[index]);
            } else {
                addedState.index = allStates.Count;
                allStates.Add(addedState);
                state.equation.Add(addedState);
            }
        }

        public override string ToString() {
            string tmp = "";
            foreach (var state in allStates) {
                tmp += state.equationToString();
            }
            return tmp;
        }
    }
}
