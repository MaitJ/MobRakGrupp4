using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.AudioRecorder;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MicrophonePage : ContentPage
    {
        private readonly AudioRecorderService audioRecorderService =
            new AudioRecorderService();

        private readonly AudioPlayer audioPlayer = 
            new AudioPlayer();

        public MicrophonePage()
        {
            InitializeComponent();
        }

        public async void OnMicrophoneButtonClicked(System.Object sender, System.EventArgs e) 
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            status = await Permissions.RequestAsync<Permissions.Microphone>();
            if (status == PermissionStatus.Granted)
            {
                if (audioRecorderService.IsRecording)
                {
                    audioRecorderService.StopRecording();
                    recordLabel.Text = "Not recording";

                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());
                }
                else
                {
                    audioRecorderService.StartRecording();
                    recordLabel.Text = "Recording";

                }
            }
            else if (status == PermissionStatus.Denied || status == PermissionStatus.Disabled) 
            {
                recordLabel.Text = "Microphone usage not permitted/disabled";
            }
        }
    }
}