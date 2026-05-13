using System;
using System.IO;
using System.Media;

namespace SecuroTronGUI.ChatBot
{
    public static class VoiceGreeting
    {
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public static void Play()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string wavPath = Path.Combine(basePath, "Assets", "greeting.wav");

                if (File.Exists(wavPath))
                {
                    SoundPlayer player = new SoundPlayer(wavPath);
                    player.PlaySync();
                }
            }
            catch
            {
                // Silently handle — GUI will show without interruption
            }
        }
    }
}