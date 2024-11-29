using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private CommandInvoker invoker = new CommandInvoker();

    public void PlayAgain()
    {
        invoker.SetCommand(new PlayAgainCommand());
        invoker.Invoke();
    }

    public void MainMenu()
    {
        invoker.SetCommand(new MainMenuCommand());
        invoker.Invoke();
    }

    public void QuitGame()
    {
        invoker.SetCommand(new QuitGameCommand());
        invoker.Invoke();
    }
}

// Command Pattern Implementation
public interface ICommand
{
    void Execute();
}

public class PlayAgainCommand : ICommand
{
    public void Execute()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

public class QuitGameCommand : ICommand
{
    public void Execute()
    {
        Application.Quit();
    }
}

public class MainMenuCommand : ICommand
{
    public void Execute()
    {
        SceneManager.LoadScene(0);
    }
}

public class CommandInvoker
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void Invoke()
    {
        _command?.Execute();
    }
}
