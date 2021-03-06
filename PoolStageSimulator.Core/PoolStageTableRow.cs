using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolStageSimulator.Core
{
    public class PoolStageTableRow : IEquatable<PoolStageTableRow> , IComparable<PoolStageTableRow>
    {
        public Team Team { get; set; }
        // 1. points
        public int TotalPoints { get; set; }
        // 2. goal difference
        public int GoalDifference { get; set; }
        // 3. scored points
        public int TotalGoalsAgainstOpponents { get; set; }
        // 4. lost points
        public int TotalGoalsMadeByOpponents { get; set; }
        // 5. required results within individual competitions
        // This is required when above 4 points remain equal between one or more teams
        public string DefeatedTeamsByName => string.Join(", ", DefeatedTeams.Select(dt => dt.Name));
        public List<Team> DefeatedTeams { get; set; } = new List<Team>();

        public int CompareTo(PoolStageTableRow other)
        {
            // Based on https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=netframework-4.8
            // A return value of 0 means this record is equal to the other record
            // A return value of 1 means this record is higher than the other record
            // A return value of -1 means this record is lower than the other record
            if(Team == other.Team) 
                return 0;

            if (TotalPoints > other.TotalPoints) return 1;
            if (TotalPoints < other.TotalPoints) return -1;
            
            if (GoalDifference > other.GoalDifference) return 1;
            if (GoalDifference < other.GoalDifference) return -1;

            if (TotalGoalsAgainstOpponents > other.TotalGoalsAgainstOpponents) return 1;
            if (TotalGoalsAgainstOpponents < other.TotalGoalsAgainstOpponents) return -1;

            if (TotalGoalsMadeByOpponents < other.TotalGoalsMadeByOpponents) return 1;
            if (TotalGoalsMadeByOpponents > other.TotalGoalsMadeByOpponents) return -1;

            if(DefeatedTeams?.Contains(other.Team) == true) return 1;
            if(other.DefeatedTeams?.Contains(Team) == true) return -1;

            return 0;
        }

        public bool Equals(PoolStageTableRow other)
        {
            return CompareTo(other) == 0;
        }
    }
}