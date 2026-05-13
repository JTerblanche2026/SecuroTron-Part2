using System;
using System.Collections.Generic;

namespace SecuroTronGUI.ChatBot
{
    public class ChatEngine
    {
        // ── User memory ──────────────────────────────────────────────
        private string _userName = "Friend";
        private string _lastTopic = "";
        private string _userInterest = "";
        private readonly List<string> _conversationHistory = new List<string>();

        // ── Property for username ────────────────────────────────────
        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }

        // ── Main response handler ────────────────────────────────────
        public string GetResponse(string input)
        {
            // Handle empty or whitespace input
            if (string.IsNullOrWhiteSpace(input))
                return "It looks like you did not type anything. Please try asking me a question!";

            string lowered = input.ToLower().Trim();

            // ── Log conversation ─────────────────────────────────────
            _conversationHistory.Add(input);

            // ── Check for exit ───────────────────────────────────────
            if (lowered == "bye" || lowered == "goodbye" ||
                lowered == "exit" || lowered == "quit")
                return ResponseBank.GeneralResponses["bye"];

            // ── Check sentiment first ────────────────────────────────
            string sentimentResponse = CheckSentiment(lowered);
            if (!string.IsNullOrEmpty(sentimentResponse))
                return sentimentResponse;

            // ── Check memory and interest ────────────────────────────
            if (lowered.Contains("interested in") || lowered.Contains("i like") ||
                lowered.Contains("i care about"))
            {
                return HandleInterest(lowered);
            }

            // ── Check for random tip requests first ──────────────────
            if (lowered.Contains("password tip") || lowered.Contains("password advice"))
            {
                _lastTopic = "password";
                return $"Here is a password tip for you, {_userName}: " +
                        ResponseBank.GetRandomPasswordTip();
            }

            if (lowered.Contains("phishing tip") || lowered.Contains("phishing advice"))
            {
                _lastTopic = "phishing";
                return $"Here is a phishing tip for you, {_userName}: " +
                        ResponseBank.GetRandomPhishingTip();
            }

            if (lowered.Contains("safe browsing") || lowered.Contains("browsing tip") ||
                lowered.Contains("browsing advice") || lowered.Contains("browsing"))
            {
                _lastTopic = "browsing";
                return $"Here is a browsing tip for you, {_userName}: " +
                        ResponseBank.GetRandomBrowsingTip();
            }

            if (lowered.Contains("privacy tip") || lowered.Contains("privacy advice"))
            {
                _lastTopic = "privacy";
                return $"Here is a privacy tip for you, {_userName}: " +
                        ResponseBank.GetRandomPrivacyTip();
            }

            if (lowered.Contains("scam tip") || lowered.Contains("scam advice"))
            {
                _lastTopic = "scam";
                return $"Here is a scam awareness tip for you, {_userName}: " +
                        ResponseBank.GetRandomScamTip();
            }

            // ── Handle follow-up requests ────────────────────────────
            if (lowered.Contains("another") || lowered.Contains("tell me more") ||
                lowered.Contains("explain more") || lowered.Contains("give me another") ||
                lowered.Contains("more info") || lowered.Contains("elaborate"))
            {
                return HandleFollowUp();
            }

            // ── Check keyword responses ──────────────────────────────
            if (lowered.Contains("password"))
            {
                _lastTopic = "password";
                return ResponseBank.KeywordResponses["password"];
            }
            if (lowered.Contains("phishing"))
            {
                _lastTopic = "phishing";
                return ResponseBank.KeywordResponses["phishing"];
            }
            if (lowered.Contains("scam"))
            {
                _lastTopic = "scam";
                return ResponseBank.KeywordResponses["scam"];
            }
            if (lowered.Contains("privacy"))
            {
                _lastTopic = "privacy";
                return ResponseBank.KeywordResponses["privacy"];
            }
            if (lowered.Contains("malware"))
            {
                _lastTopic = "malware";
                return ResponseBank.KeywordResponses["malware"];
            }
            if (lowered.Contains("virus"))
            {
                _lastTopic = "virus";
                return ResponseBank.KeywordResponses["virus"];
            }
            if (lowered.Contains("vpn"))
            {
                _lastTopic = "vpn";
                return ResponseBank.KeywordResponses["vpn"];
            }
            if (lowered.Contains("2fa"))
            {
                _lastTopic = "2fa";
                return ResponseBank.KeywordResponses["2fa"];
            }
            if (lowered.Contains("firewall"))
            {
                _lastTopic = "firewall";
                return ResponseBank.KeywordResponses["firewall"];
            }
            if (lowered.Contains("encryption"))
            {
                _lastTopic = "encryption";
                return ResponseBank.KeywordResponses["encryption"];
            }
            if (lowered.Contains("ransomware"))
            {
                _lastTopic = "ransomware";
                return ResponseBank.KeywordResponses["ransomware"];
            }
            if (lowered.Contains("social engineering"))
            {
                _lastTopic = "social engineering";
                return ResponseBank.KeywordResponses["social engineering"];
            }

            // ── Check general responses ──────────────────────────────
            if (lowered.Contains("how are you"))
                return ResponseBank.GeneralResponses["how are you"];
            if (lowered.Contains("what is your purpose"))
                return ResponseBank.GeneralResponses["what is your purpose"];
            if (lowered.Contains("what can i ask"))
                return ResponseBank.GeneralResponses["what can i ask"];
            if (lowered.Contains("hello") || lowered == "hi")
                return ResponseBank.GeneralResponses["hello"];
            if (lowered.Contains("help"))
                return ResponseBank.GeneralResponses["help"];
            if (lowered.Contains("bye") || lowered.Contains("goodbye"))
                return ResponseBank.GeneralResponses["bye"];

            // ── Recall user interest if set ──────────────────────────
            if (!string.IsNullOrEmpty(_userInterest))
                return $"As someone interested in {_userInterest}, you might want to " +
                       $"ask me about {_userInterest} tips or threats related to it. " +
                       $"I did not quite understand that though — could you rephrase?";

            // ── Default fallback ─────────────────────────────────────
            return ResponseBank.DefaultResponse;
        }

        // ── Sentiment checker ────────────────────────────────────────
        private string CheckSentiment(string lowered)
        {
            foreach (var entry in ResponseBank.SentimentResponses)
            {
                if (lowered.Contains(entry.Key.ToLower()))
                {
                    string tip = GetFollowUpTip();
                    return $"{entry.Value}\n\n💡 Tip: {tip}";
                }
            }
            return string.Empty;
        }

        // ── Interest handler ─────────────────────────────────────────
        private string HandleInterest(string lowered)
        {
            foreach (var keyword in ResponseBank.KeywordResponses.Keys)
            {
                if (lowered.Contains(keyword.ToLower()))
                {
                    _userInterest = keyword;
                    _lastTopic = keyword;
                    return $"Great! I will remember that you are interested in {keyword}. " +
                           $"It is a crucial part of staying safe online. " +
                           $"Feel free to ask me anything about {keyword}!";
                }
            }
            return "That sounds interesting! Could you be more specific about " +
                   "which cybersecurity topic you are interested in?";
        }

        // ── Follow-up handler ────────────────────────────────────────
        private string HandleFollowUp()
        {
            string tip = GetFollowUpTip();
            if (!string.IsNullOrEmpty(tip))
                return $"Here is more on {_lastTopic} for you, {_userName}: {tip}";

            return $"What topic would you like to explore further, {_userName}? " +
                    "Try asking about passwords, phishing, browsing, scams or privacy!";
        }

        // ── Get tip based on last topic ──────────────────────────────
        private string GetFollowUpTip()
        {
            return _lastTopic switch
            {
                "password"         => ResponseBank.GetRandomPasswordTip(),
                "phishing"         => ResponseBank.GetRandomPhishingTip(),
                "browsing"         => ResponseBank.GetRandomBrowsingTip(),
                "privacy"          => ResponseBank.GetRandomPrivacyTip(),
                "scam"             => ResponseBank.GetRandomScamTip(),
                "malware"          => ResponseBank.GetRandomPasswordTip(),
                "virus"            => ResponseBank.GetRandomPasswordTip(),
                "vpn"              => ResponseBank.GetRandomBrowsingTip(),
                "2fa"              => ResponseBank.GetRandomPasswordTip(),
                "firewall"         => ResponseBank.GetRandomBrowsingTip(),
                "encryption"       => ResponseBank.GetRandomPrivacyTip(),
                "ransomware"       => ResponseBank.GetRandomPasswordTip(),
                "social engineering" => ResponseBank.GetRandomPhishingTip(),
                _                  => ResponseBank.GetRandomPasswordTip()
            };
        }

        // ── Welcome message ──────────────────────────────────────────
        public string GetWelcomeMessage()
        {
            return "Hello! Welcome to Securo-Tron — your Cybersecurity Awareness Assistant. " +
                   "I am here to help you stay safe online. What is your name?";
        }

        // ── Set username ─────────────────────────────────────────────
        public string SetUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "I did not catch that. Could you please enter your name?";

            _userName = name.Trim();
            return $"Great to meet you, {_userName}! 🎉 You can ask me about passwords, " +
                   $"phishing, scams, safe browsing, privacy and more. How can I help you today?";
        }
    }
}