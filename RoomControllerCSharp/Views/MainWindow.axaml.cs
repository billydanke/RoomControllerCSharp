using Avalonia.Controls;

namespace RoomControllerCSharp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

#if !DEBUG
        // Set the cursor to None in a release build
        this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.None);
#endif
    }
}
