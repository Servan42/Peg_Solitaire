using Peg_SolitaireTDD;
using Peg_SolitaireTDD.api;
using Peg_SolitaireTDD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg_SolitaireTDDTest
{
    public class GeneticAlgorithmServiceTests
    {
        // Establish a population
        // Select the n best locus
        // Breed them to full pop
        // Mutate some of them
        // Loop until max generation number or satisfying result

        GeneticAlgorithmService _geneticAlgorithmService;

        [SetUp]
        public void SetUp()
        {
            _geneticAlgorithmService = new GeneticAlgorithmService();
        }

        [Test]
        public void Should_init_a_start_population()
        {
            // Given
            List<ReplayGame> population;
            int populationSize = 100;

            // When
            population = _geneticAlgorithmService.GenerateBasePopulation(populationSize);

            // Then
            Assert.That(population.Count, Is.EqualTo(populationSize), "Population size is invalid");
            Assert.True(population.All(l => l.replaySteps.Count == 36), "ReplayStep list size is invalid");
        }

        [Test]
        public void Should_select_the_best_locus()
        {
            // Given
            List<ReplayGame> bestLocusList;
            int populationSize = 100;
            int numberOfLocusToKeep = 20;
            List<ReplayGame> population = _geneticAlgorithmService.GenerateBasePopulation(populationSize);

            // When
            bestLocusList = _geneticAlgorithmService.SelectBestLocus(numberOfLocusToKeep, population);

            // Then
            Assert.That(bestLocusList.Count, Is.EqualTo(numberOfLocusToKeep), "Number of kept locus is invalid");
        }
    }
}
