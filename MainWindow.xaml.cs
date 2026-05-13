using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SecuroTronGUI.ChatBot;

namespace SecuroTronGUI
{
    public partial class MainWindow : Window
    {
        // ── Fields ───────────────────────────────────────────────────
        private readonly ChatEngine _chatEngine = new ChatEngine();
        private bool _nameEntered = false;

        // ── Constructor ──────────────────────────────────────────────
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        // ── On window load ───────────────────────────────────────────
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Play voice greeting
            VoiceGreeting.Play();

            // Show welcome message
            AddBotMessage(_chatEngine.GetWelcomeMessage());

            // Focus input box
            UserInputBox.Focus();
        }

        // ── Send button click ────────────────────────────────────────
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        // ── Enter key press ──────────────────────────────────────────
        private void UserInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ProcessInput();
        }

        // ── Process user input ───────────────────────────────────────
        private void ProcessInput()
        {
            string input = UserInputBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
                return;

            // Show user message
            AddUserMessage(input);

            // Clear input box
            UserInputBox.Clear();

            // Handle name entry first
            if (!_nameEntered)
            {
                string response = _chatEngine.SetUserName(input);
                AddBotMessage(response);
                _nameEntered = true;
                return;
            }

            // Get and show bot response
            string botResponse = _chatEngine.GetResponse(input);
            AddBotMessage(botResponse);

            // Scroll to bottom
            ChatScrollViewer.ScrollToBottom();
        }

        // ── Add bot message bubble ───────────────────────────────────
        private void AddBotMessage(string message)
        {
            // Outer container
            var container = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 6, 0, 6)
            };

            // Avatar
            var avatar = new Border
            {
                Width = 36,
                Height = 36,
                CornerRadius = new CornerRadius(18),
                Background = new SolidColorBrush(Color.FromRgb(33, 38, 45)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(88, 166, 255)),
                BorderThickness = new Thickness(1.5),
                Margin = new Thickness(0, 0, 10, 0),
                VerticalAlignment = VerticalAlignment.Top
            };

            var avatarText = new TextBlock
            {
                Text = "🤖",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            avatar.Child = avatarText;

            // Message bubble
            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(22, 27, 34)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(48, 54, 61)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(0, 12, 12, 12),
                Padding = new Thickness(14, 10, 14, 10),
                MaxWidth = 600
            };

            var messagePanel = new StackPanel();

            // Bot name label
            var nameLabel = new TextBlock
            {
                Text = "🔒 Securo-Tron",
                Foreground = new SolidColorBrush(Color.FromRgb(88, 166, 255)),
                FontSize = 11,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(0, 0, 0, 4)
            };

            // Message text
            var messageText = new TextBlock
            {
                Text = message,
                Foreground = new SolidColorBrush(Color.FromRgb(230, 237, 243)),
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                LineHeight = 22
            };

            messagePanel.Children.Add(nameLabel);
            messagePanel.Children.Add(messageText);
            bubble.Child = messagePanel;

            container.Children.Add(avatar);
            container.Children.Add(bubble);

            ChatPanel.Children.Add(container);
            ChatScrollViewer.ScrollToBottom();
        }

        // ── Add user message bubble ──────────────────────────────────
        private void AddUserMessage(string message)
        {
            // Outer container aligned right
            var container = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 6, 0, 6)
            };

            // Message bubble
            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(31, 111, 235)),
                CornerRadius = new CornerRadius(12, 0, 12, 12),
                Padding = new Thickness(14, 10, 14, 10),
                MaxWidth = 500
            };

            var messageText = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                LineHeight = 22
            };

            bubble.Child = messageText;
            container.Children.Add(bubble);
            ChatPanel.Children.Add(container);
            ChatScrollViewer.ScrollToBottom();
        }
    }
}