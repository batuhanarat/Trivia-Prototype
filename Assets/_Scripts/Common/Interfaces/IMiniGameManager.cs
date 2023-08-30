public interface IMiniGameManager
{
    void Initialize(ScoringManager scoringManager, TurnManager turnManager);
    void StartGame();
    void EndGame();
}
