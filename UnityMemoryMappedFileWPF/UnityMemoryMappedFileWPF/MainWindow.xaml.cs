﻿using System.Windows;
using UnityMemoryMappedFile;

namespace UnityMemoryMappedFileWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MemoryMappedFileServer server;
        private MemoryMappedFileClient client;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await client.SendCommandAsync(new PipeCommands.SendMessage { Message = "TestFromWPF" });
        }

        private async void MoveButton_Click(object sender, RoutedEventArgs e)
        {
            await client.SendCommandAsync(new PipeCommands.MoveObject { X = +1.0f });
        }

        private async void GetCurrentXButton_Click(object sender, RoutedEventArgs e)
        {
            await client.SendCommandWaitAsync(new PipeCommands.GetCurrentPosition(), d =>
            {
                var ret = (PipeCommands.ReturnCurrentPosition)d;
                Dispatcher.Invoke(() => ReceiveTextBlock.Text = $"{ret.CurrentX}");
            });
        }
        
        
        private void Client_Received(object sender, DataReceivedEventArgs e)
        {
            if (e.CommandType == typeof(PipeCommands.SendMessage))
            {
                var d = (PipeCommands.SendMessage)e.Data;
                MessageBox.Show($"[Client]ReceiveFromServer:{d.Message}");
            }
        }

        private void StartServer_Click(object sender, RoutedEventArgs e)
        {
            server = new MemoryMappedFileServer();
            server.Start("SamplePipeName");
        }

        private void StopServer_Click(object sender, RoutedEventArgs e)
        {
            server.Stop();
        }

        private void StartClient_Click(object sender, RoutedEventArgs e)
        {
            client = new MemoryMappedFileClient();
            client.ReceivedEvent += Client_Received;
            client.Start("SamplePipeName");
        }

        private void StopClient_Click(object sender, RoutedEventArgs e)
        {
            client.Stop();
        }
    }
}
