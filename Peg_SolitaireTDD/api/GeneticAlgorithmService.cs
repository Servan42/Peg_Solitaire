using Peg_SolitaireTDD.Model;
using Peg_SolitaireTDD.spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Peg_SolitaireTDD.api
{
    public class GeneticAlgorithmService
    {
        public List<ReplayStep> Run()
        {
            int populationSize = 1000;
            int numberOfLocusToKeep = 20;
            int maxGenerationNumber = 1000;
            int genCount = 0;

            var population = GenerateBasePopulation(populationSize);
            while (genCount < maxGenerationNumber)
            {
                population = SelectBestLocus(numberOfLocusToKeep, population);
                Console.WriteLine(population[0].Score);
                population = BreedAndMutate(population, populationSize, 0.01);
                genCount++;
            }
            return null;
        }

        public List<ReplayGame> GenerateBasePopulation(int populationSize)
        {
            List<ReplayGame> returnPopulation = new();
            for (int a = 0; a < populationSize; a++)
            {
                ReplayGame gameReplay = new();
                gameReplay.replaySteps.Add(new ReplayStep((3, 1), (3, 3, GameServiceHelper.MOVE_TOWARDS_J)));
                for (int b = 0; b < GameServiceHelper.MAX_MOVES_IN_A_GAME - 1; b++)
                {
                    var initalPosition = GameServiceHelper.GenerateRandomValidInitialPosition();
                    var destination = GameServiceHelper.GenerateRandomValidDestinationPositionFromInitialPosition(initalPosition);
                    gameReplay.replaySteps.Add(new ReplayStep(initalPosition, destination));
                }
                returnPopulation.Add(gameReplay);
            }
            return returnPopulation;
        }

        public List<ReplayGame> SelectBestLocus(int numberOfLocusToKeep, List<ReplayGame> population)
        {
            ReplayService replayService = new((3, 3));

            population.ForEach(l => l.Score = replayService.ComputeReplayScore(l.replaySteps));

            return population.OrderBy(l => l.Score)
                .Take(numberOfLocusToKeep)
                .ToList();
        }

        public ReplayGame Breed(ReplayGame parent1, ReplayGame parent2)
        {
            ReplayGame child = new();
            for (int i = 0; i < GameServiceHelper.MAX_MOVES_IN_A_GAME; i = i + 2)
            {
                child.replaySteps.Add(parent1.replaySteps[i]);
                child.replaySteps.Add(parent2.replaySteps[i + 1]);
            }
            return child;
        }

        public ReplayGame Mutate(ReplayGame child)
        {
            child.replaySteps.ForEach(x =>
            {
                var initalPosition = GameServiceHelper.GenerateRandomValidInitialPosition();
                var destination = GameServiceHelper.GenerateRandomValidDestinationPositionFromInitialPosition(initalPosition);
                x.BallInitialPosition = initalPosition;
                x.BallDestination = destination;
            });
            return child;
        }

        public List<ReplayGame> BreedAndMutate(List<ReplayGame> population, int populationSize, double mutationProbability)
        {
            List<ReplayGame> replayGames = new();

            for (int i = 0; i < populationSize; i++)
            {
                var child = Breed(population[StaticRandom.Rand(0, population.Count)], population[StaticRandom.Rand(0, population.Count)]);
                if ((mutationProbability * 100) > StaticRandom.Rand(0, 101)) child = Mutate(child);
                replayGames.Add(child);
            }

            return replayGames;
        }
    }
}
