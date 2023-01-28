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
        /// La boule est bien déplacée
        /// </summary>
        [Test]
        public void Ball_moved_should_be_moved_correctly()
        {
            // GIVEN
            Case ball = _gameService.PickBallToPlay();
            (int i, int j) ballInitialPosistion = ball.Posistion;
            (int i, int j, string orientation) ballDestination = ball.BallValidDestinations[0];

            // WHEN
            _ = _gameService.MoveBall(ball, ballDestination);

            // THEN
            Assert.True(_gameService.Gameboard.CaseList[ballInitialPosistion.i][ballInitialPosistion.j].IsEmpty);
            Assert.False(_gameService.Gameboard.CaseList[ballDestination.i][ballDestination.j].IsEmpty);
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
            Case deletedBallCase;
            (int i, int j) ballInitialPosistion = ball.Posistion;
            (int i, int j, string orientation) ballDestination = ball.BallValidDestinations[0];

            // WHEN
            deletedBallCase = _gameService.MoveBall(ball, ballDestination);

            // THEN
            Assert.True(deletedBallCase.IsEmpty);
        }

        /// <summary>
        /// Suivant une boule choisie
        /// Quand je déplace la boule dans une destinations non valide
        /// Je reçois une exception
        /// </summary>
        [Test]
        public void Invalid_move_should_throw_exception()
        {
            // GIVEN
            Case ball = _gameService.PickBallToPlay();

            // WHEN
            Action wrongMoveAction = () => _gameService.MoveBall(ball, (0,0,"i"));

            // THEN
            Assert.Throws<Peg_SolitaireTDD.api.InvalidMoveException>(wrongMoveAction.Invoke);
        }
    }
}