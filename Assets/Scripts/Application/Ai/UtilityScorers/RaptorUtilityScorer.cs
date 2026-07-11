using System.Collections.Generic;
using Core.Ai.Behavior;
using Core.Ai.WorldState;
using UnityEngine;

namespace Application.Ai.UtilityScorers
{
    public static class RaptorUtilityScorer
    {
        private static Dictionary<int, AttackDecision> _attackDict = new();
        public static AttackDecision Evaluate(RaptorWorldState state)
        {
            float lungeScore = ScoreLunge(state);
            float stalkScore = ScoreStalk(state);
            float waitScore = ScoreWait(state);
            float flushScore = ScoreFlush(state);

            // Add small noise to each score
            lungeScore += Random.Range(0f, 0.1f);
            stalkScore += Random.Range(0f, 0.1f);
            waitScore += Random.Range(0f, 0.1f);
            flushScore += Random.Range(0f, 0.1f);

            // Pick highest
            return PickHighest(lungeScore, stalkScore, waitScore, flushScore);
        }
        private static AttackDecision PickHighest(float lungeScore, float stalkScore, float waitScore, float flushScore)
        {

            float maxVal = 0f;
            int maxNdx = 0;
            List<float> scores = new() { lungeScore, stalkScore, waitScore, flushScore };

            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i] > maxVal)
                {
                    maxVal = scores[i];
                    maxNdx = i;
                }
                else if (scores[i] == maxVal)
                {
                    // uh re-roll I guess
                    int randVal = Random.Range(0, 1);
                    if (randVal == 0)
                    {
                        maxVal = scores[i];
                        maxNdx = i;
                    }
                }
            }
            return _attackDict[maxNdx];
        }

        private static float ScoreLunge(RaptorWorldState state)
        {
            return 0;
        }

        private static float ScoreStalk(RaptorWorldState state)
        {
            return 0;
        }

        private static float ScoreWait(RaptorWorldState state)
        {
            return 0;
        }

        private static float ScoreFlush(RaptorWorldState state)
        {
            return 0;
        }
    }
}