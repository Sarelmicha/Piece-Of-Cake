using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionLevel[] levels = null;

    Dictionary<int, Dictionary<Stat, float>> lookupTable = null;

    public float GetScore(Stat stat, int level)
    {
        BuildLookup();

       return lookupTable[level][stat];
    }

    private void BuildLookup()
    {
        if (lookupTable != null)
        {
            return;
        }
        // Populate the lookup table dictionary
        lookupTable = new Dictionary<int, Dictionary<Stat, float>>();

        foreach (ProgressionLevel progressionLevel in levels)
        {
            var stateLookupTable = new Dictionary<Stat, float>();

            foreach (ProgressionStat progressionStat in progressionLevel.stats)
            {
                stateLookupTable[progressionStat.stat] = progressionStat.score;
            }

            lookupTable[progressionLevel.level] = stateLookupTable;
        }
    }

    [System.Serializable]
    class ProgressionLevel
    {
        public int level;
        public ProgressionStat[] stats;
    }

    [System.Serializable]
    class ProgressionStat
    {
        public Stat stat;
        public float score;
    }
}
