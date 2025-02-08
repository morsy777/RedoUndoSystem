namespace Undo_and_Redo_using_stack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var commandStack = new Stack<AppendTextCommand>();
            var redoStack = new Stack<AppendTextCommand>();
            string originalText = "";
            while (true)
            {
                Console.Write("Type text to append (Exit to exit, Undo to undo): ");
                string input = Console.ReadLine();

                if (input.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (input.Equals("Undo", StringComparison.OrdinalIgnoreCase))
                {
                    // precondition to check if the commandStack is not null.
                    if (commandStack.Count > 0)
                    {
                        // command = _textToAppend in the AppendTextCommand class.
                        var command = commandStack.Pop();
                        redoStack.Push(command);
                        originalText = command.Undo();
                    }
                }
                else if (input.Equals("Redo", StringComparison.OrdinalIgnoreCase))
                {
                    if (redoStack.Count > 0)
                    {
                        var command = redoStack.Pop();
                        originalText = command.Redo();
                    }
                }
                else
                {
                    var command = new AppendTextCommand(originalText, input);
                    originalText = command.Execute();
                    // command = _textToAppend in the AppendTextCommand class.
                    commandStack.Push(command);
                    redoStack.Clear();  // Clear redo stack after new command -> because u can't make more than one redo,
                                        // but u can make more than one undo command.
                }
            }

        }
        class AppendTextCommand
        {
            private string _originalText;
            private string _textToAppend;
            public AppendTextCommand(string originalText, string textToAppend)
            {
                _originalText = originalText;
                _textToAppend = textToAppend;
            }
            public string Execute()
            {
                _originalText += _textToAppend;
                Console.WriteLine(_originalText);
                return _originalText;
            }
            public string Undo()
            {
                _originalText = _originalText.Substring(0, _originalText.Length - _textToAppend.Length);
                Console.WriteLine(_originalText);
                return _originalText;
            }
            public string Redo()
            {
                _originalText += _textToAppend;
                Console.WriteLine(_originalText);
                return _originalText;
            }
        }
    }
  
}