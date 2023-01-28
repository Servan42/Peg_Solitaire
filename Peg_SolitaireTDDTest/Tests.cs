using Peg_SolitaireTDD.api;
using Peg_SolitaireTDD.Model;

namespace Peg_SolitaireTDDTest
{
    public class Tests
    {
        GameService _gameService;

        [SetUp]
        public void Setup()
        {
            _gameService = new GameService();
        }
        /// <summary>
        /// Avec un plateau de jeu vide
        /// J'initalise le plateau
        /// Le plateau contient 36 boules et un trou au centre
        /// </summary>
        [Test] public void Board_is_correctly_initialized()
        {
            // GIVEN
            // Setup();

            // WHEN
            // Init function in GameService construcor

            // THEN
            Assert.That(_gameService.NumberOfRemainingBalls(), Is.EqualTo(36));
            Assert.That(_gameService.Gameboard.CaseList[3][3].IsEmpty);
        }

        /// <summary>
        /// Suivant le plateau de jeu
        /// Quand je choisis une boule
        /// Cette boule est déplacable
        /// </summary>
        [Test]
        public void Chosen_ball_should_be_movable()
        {
            // GIVEN
            Case ball;

            /// WHEN
            ball = _gameService.PickBallToPlay();

            /// THEN
            Assert.That(ball.BallIsMovable);
        }

        /// <summary>
        /// Suivant une boule choisie
        /// Quand je déplace la boule dans une de ses destinations valides
        /// la boule sautée est supprimée
        /// </summary>
        [Test]
        public void Ball_crossed_over_by_another_moved_ball_should_be_removed_from_board()
        {
            // GIVEN
            Case ball = _gameService.PickBallToPlay();

            // WHEN
            _gameService.MoveBall(ball, ball.BallValidDestinations[0]);

            // THEN
            //Assert.That(crossed over ball is deleted from the board)
        }
    }
}