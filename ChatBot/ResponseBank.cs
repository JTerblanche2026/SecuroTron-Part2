using System;
using System.Collections.Generic;

namespace SecuroTronGUI.ChatBot
{
    public static class ResponseBank
    {
        // ── Random generator ─────────────────────────────────────────
        private static readonly Random _random = new Random();

        // ── Random tip lists ─────────────────────────────────────────
        private static readonly List<string> _passwordTips = new List<string>
        {
            "Use at least 12 characters mixing uppercase, lowercase, numbers and symbols.",
            "Never reuse the same password across multiple accounts.",
            "Consider using a password manager like Bitwarden or LastPass.",
            "Avoid using personal info like birthdays or names in your passwords.",
            "Enable two-factor authentication wherever possible for extra security."
        };

        private static readonly List<string> _phishingTips = new List<string>
        {
            "Be cautious of emails asking for personal information — legitimate companies never do this.",
            "Always check the sender's email address carefully for slight misspellings.",
            "Hover over links before clicking to see where they actually lead.",
            "If an email creates urgency or panic, it is likely a phishing attempt.",
            "When in doubt, go directly to the website instead of clicking email links."
        };

        private static readonly List<string> _browsingTips = new List<string>
        {
            "Always look for HTTPS and a padlock icon before entering any personal info.",
            "Avoid using public Wi-Fi for banking or sensitive activities.",
            "Keep your browser and extensions updated to patch security vulnerabilities.",
            "Use a reputable ad blocker to reduce exposure to malicious ads.",
            "Clear your cookies and cache regularly to protect your browsing history."
        };

        private static readonly List<string> _privacyTips = new List<string>
        {
            "Review your social media privacy settings regularly.",
            "Be careful about what personal information you share online.",
            "Use a VPN when browsing on public networks.",
            "Regularly check which apps have access to your camera and microphone.",
            "Delete accounts and apps you no longer use to reduce your digital footprint."
        };

        private static readonly List<string> _scamTips = new List<string>
        {
            "If an offer seems too good to be true, it probably is.",
            "Never send money or personal details to someone you have not verified.",
            "Be wary of unsolicited phone calls claiming to be from your bank or SARS.",
            "Scammers often create urgency — take your time and verify before acting.",
            "Report suspected scams to the South African Fraud Prevention Service."
        };

        // ── Keyword response dictionary ───────────────────────────────
        public static readonly Dictionary<string, string> KeywordResponses =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "password",    "Passwords are your first line of defence! Use strong, unique passwords for every account and never share them." },
            { "phishing",    "Phishing is when attackers disguise themselves as trusted sources to steal your info. Always verify before you click!" },
            { "scam",        "Scams are becoming more sophisticated in South Africa. If something feels too good to be true, it probably is." },
            { "privacy",     "Protecting your privacy online is crucial. Limit what you share and regularly review your account settings." },
            { "malware",     "Malware is malicious software designed to damage or gain unauthorised access to your device. Keep your antivirus updated!" },
            { "virus",       "Computer viruses can spread quickly. Never download software from untrusted sources and scan regularly." },
            { "vpn",         "A VPN encrypts your internet connection, keeping your data safe especially on public Wi-Fi networks." },
            { "2fa",         "Two-factor authentication adds an extra layer of security. Enable it on all your important accounts!" },
            { "firewall",    "A firewall monitors incoming and outgoing network traffic and blocks suspicious activity." },
            { "encryption",  "Encryption converts your data into unreadable code so only authorised parties can access it." },
            { "ransomware",  "Ransomware locks your files and demands payment. Always back up your data and never pay the ransom!" },
            { "social engineering", "Social engineering manipulates people into revealing confidential information. Always verify identities before sharing anything." },
            { "safe browsing", "Always look for HTTPS before entering personal info, avoid public Wi-Fi for sensitive tasks, and keep your browser updated!" },
        };

        // ── General conversation dictionary ───────────────────────────
        public static readonly Dictionary<string, string> GeneralResponses =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "how are you",          "I am running securely and feeling great! Ready to help you stay safe online. 😊" },
            { "what is your purpose", "I am your Cybersecurity Awareness Assistant! I help South African citizens understand and avoid cyber threats." },
            { "what can i ask",       "You can ask me about passwords, phishing, scams, privacy, malware, VPNs, 2FA, firewalls, ransomware and more!" },
            { "hello",                "Hello there! Great to see you taking cybersecurity seriously. How can I help you today?" },
            { "help",                 "I can help you with: passwords, phishing, scams, safe browsing, privacy, malware, VPNs and more. Just ask!" },
            { "bye",                  "Stay safe online! Remember to keep your passwords strong and never click suspicious links. Goodbye! 👋" },
            { "goodbye",              "Goodbye! Keep practising good cybersecurity habits every day. Stay safe! 👋" }
        };

        // ── Sentiment responses ───────────────────────────────────────
        public static readonly Dictionary<string, string> SentimentResponses =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "worried",     "It is completely understandable to feel that way. Cyber threats can be overwhelming, but I am here to help. Let me share some tips to keep you safe." },
            { "scared",      "Do not worry — knowledge is your best defence! Let me help you understand how to protect yourself online." },
            { "frustrated",  "I understand your frustration. Cybersecurity can feel complex, but let us break it down together step by step." },
            { "confused",    "No worries at all! Cybersecurity can be tricky. Let me explain things as clearly as possible for you." },
            { "curious",     "I love your curiosity! That is the best attitude when it comes to staying safe online. What would you like to explore?" },
            { "excited",     "That enthusiasm is wonderful! Let us channel that energy into learning how to stay cyber safe!" },
            { "angry",       "I understand your frustration. Let us work through this together and find the best solution for you." },
            { "unsure",      "That is perfectly fine! Ask me anything and I will guide you through it at your own pace." }
        };

        // ── Random tip getters ────────────────────────────────────────
        public static string GetRandomPasswordTip()  => _passwordTips[_random.Next(_passwordTips.Count)];
        public static string GetRandomPhishingTip()  => _phishingTips[_random.Next(_phishingTips.Count)];
        public static string GetRandomBrowsingTip()  => _browsingTips[_random.Next(_browsingTips.Count)];
        public static string GetRandomPrivacyTip()   => _privacyTips[_random.Next(_privacyTips.Count)];
        public static string GetRandomScamTip()      => _scamTips[_random.Next(_scamTips.Count)];

        // ── Default fallback ──────────────────────────────────────────
        public static string DefaultResponse =>
            "I am not sure I understand. Could you try rephrasing? You can ask me about passwords, phishing, scams, or safe browsing.";
    }
}