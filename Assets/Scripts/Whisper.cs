using System;
using UnityEngine;
using UnityEngine.UI;

namespace OpenAI
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private Button recordButton;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;

        public Sprite micOn;
        public Sprite micOff;
        public Sprite micMuted;
        public ChatGPT cgpt;

        private readonly string fileName = "output.wav";
        private readonly int duration = 5;

        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
            recordButton.onClick.AddListener(StartRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }

        private async void StartRecording()
        {
            if (!isRecording)
            {
                Debug.Log("Start...");
                isRecording = true;
                recordButton.image.sprite = micOn;

                var index = PlayerPrefs.GetInt("user-mic-device-index");
                clip = Microphone.Start(dropdown.options[index].text, true, duration, 44100);
            }
            else
            {
                Debug.Log("Ended...");
                message.text = "Transcripting...";

                Microphone.End(null);
                byte[] data = SaveWav.Save(fileName, clip);

                var req = new CreateAudioTranscriptionsRequest
                {
                    FileData = new FileData() { Data = data, Name = "audio.wav" },
                    // File = Application.persistentDataPath + "/" + fileName,
                    Model = "whisper-1",
                    Language = "en"
                };
                var res = await openai.CreateAudioTranscription(req);

                message.text = res.Text;
                recordButton.image.sprite = micMuted;
                cgpt.SendVoiceReply(res.Text);
            }
            
        }

        private void Update()
        {
            if (isRecording)
            {
                time += Time.deltaTime;

                if (time >= duration)
                {
                    time = 0;
                    isRecording = false;
                }
            }
        }
    }
}
