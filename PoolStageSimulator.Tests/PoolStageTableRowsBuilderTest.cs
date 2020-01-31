using System.Linq;
using NUnit.Framework;
using PoolStageSimulator.Core;

namespace PoolStageSimulator.Tests
{
    [TestFixture]
    public class PoolStageTableRowsBuilderTest
    {
        public IPoolStageTableRowsBuilder PoolStageTableRowsBuilder { get; private set; }

        [SetUp]
        public void Setup()
        {
            PoolStageTableRowsBuilder = new PoolStageTableRowsBuilder();
        }

        [Test]
        public void Build_UsingMainTeamsLostCompetition_IncludesOpponentsTotalsInResults()
        {
            // 1. Arrange
            var mainTeam = new Team("Loser");
            var opponentsTeam = new Team("Winner");

            // 2. Act
            var mainTeamsCompetitionResult = new CompetitionResult
            {
                ParticipatingTeam = mainTeam,
                OpponentsTeam = opponentsTeam,
                TotalGoalsAgainstOpponent = 0,
                TotalGoalsMadeByOpponent = 1,
            };
            PoolStageTableRowsBuilder.Add(mainTeamsCompetitionResult);
            var someOtherCompetitionResult = new CompetitionResult
            {
                ParticipatingTeam = new Team(""),
                OpponentsTeam = new Team(""),
                TotalGoalsAgainstOpponent = 0,
                TotalGoalsMadeByOpponent = 0,
            };
            PoolStageTableRowsBuilder.Add(someOtherCompetitionResult);
            var poolStageTableRows = PoolStageTableRowsBuilder.Build();

            // 3. Assert
            var opponentsRow = poolStageTableRows.Single(record => record.Team == opponentsTeam);
            Assert.AreEqual(3, opponentsRow.TotalPoints);
            Assert.AreEqual(0, opponentsRow.TotalGoalsMadeByOpponents);
            Assert.AreEqual(1, opponentsRow.TotalGoalsAgainstOpponents);
            Assert.AreEqual(1, opponentsRow.GoalDifference);
        }
    }
}