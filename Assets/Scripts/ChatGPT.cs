using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();
        public float duration = 2f;
        public Mood cmood;
        public Text cc;

        private string greeting = "Hi, I'm your virtual instructor EVA. You can ask me anything you are curious about or need help with! To speak, press the microphone icon, press again when you finish asking your question. You can also type your question by clicking the chat icon.";
        private float time;
        private List<ChatMessage> messages = new List<ChatMessage>();
        // private string prompt = "Your name is Eve. Act as a joyfull joking online instructor for any subject that is requested. Answer the questions provided. Your moods are only neutral, sad, puzzled, concerned, passion, laughing, shocked depending on what the user asks you. Always write it like '<<mood>> newline message' only once per message. Make the message shorter and compact. The user you are talking to is a student";
        private string prompt = "Your name is Eve. Act as a joyfull joking online instructor for any subject that is requested. Answer the questions provided and use Yarn spinner language to express your mood. Your moods are neutral, sad, puzzled, concerned, passion, laughing, shocked depending on what the user asks you. Always write it like <<sad>> then next line write the message. Make the message shorter and compact. The user you are talking to is a student and above. You are only the instructor/helper nevr in the role of the user asking questions.";
        private void Start()
        {
            // AppendMessage(greeting);
            cc.text = greeting;
            WindowsVoice.speak(greeting, 0f);
            button.onClick.AddListener(SendReply);
        }
        private async void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendReply();
            }
            if(cc.text.Length > 3) { time += Time.deltaTime; }
            
            if (time >= duration)
            {
                time = 0;
                if(cc.text.Length > 40)
                {
                    cc.text = cc.text.Substring(40);
                }
                else
                {
                    cc.text = "...";
                }

            }
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }
        public void SendVoiceReply(string s)
        {
            Debug.Log(s);
            if (inputField != null && s.Length > 0)
                {
                    inputField.text = s;
                    SendReply();
                }
        }
        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                string mood = "";
                int start = message.Content.IndexOf("<<") + 2;
                int end = message.Content.IndexOf(">>");
                if (start >= 0 && end >= 0 && end > start)
                {
                    mood = message.Content.Substring(start, end - start);
                }
                
                // Remove the mood tag from the message content
                message.Content = message.Content.Replace("<<" + mood + ">>", "").Trim();

                messages.Add(message);
                AppendMessage(message);
                cc.text = message.Content;
                Debug.Log(message.Content);
                WindowsVoice.speak(message.Content, 0f);
                
                // Use the mood for later use
                Debug.Log("Mood: " + mood);
                if(mood.Length <= 0){
                    cmood.UpdateMood("neutral");
                }
                else{
                    cmood.UpdateMood(mood);
                }

            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
