
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class CommandLineView : FrameView
{
    TextField commandLiner = null!;
    public static string commandContent = "";

    public event Action<string> OnNewCommand = delegate { };

    public CommandLineView() : base($"Command")
    {
        commandLiner = new TextField()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            CanFocus = true,
            ColorScheme = Colors.TopLevel,
            Text = commandContent,
        };


        Add(commandLiner);


        commandLiner.KeyDown += (e) =>
        {
            if (e.KeyEvent.Key == Key.Enter)
            {
                if (commandLiner.Text.ToString() == "") return;
                string inputText = this.commandLiner.Text.ToString()!;
                ExecuteCommand(inputText);
                this.commandLiner.Text = "";

            }

        };
    }

    public void ExecuteCommand(string cmd)
    {
        OnNewCommand?.Invoke(cmd);
    }
}