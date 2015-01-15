using System;
using System.Timers;
using ShadowTester.Domain.Captures;

namespace ShadowTester.Domain.Recorder
{
    public class ProcessRecorder
    {
        private Timer timer;
        private RecordConfiguration configuration;
        private ISystemCapturer systemCapturer;

        public ProcessRecorder(ISystemCapturer systemCapturer)
        {
            this.systemCapturer = systemCapturer;
        }

        public void Configure(RecordConfiguration configuration)
        {
            this.configuration = configuration;
            timer = new Timer();
            timer.Interval = configuration.Period;
            timer.Elapsed += OnElapsedTime;
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            systemCapturer.CaptureForegroundProcess(GetCaptureName(), configuration.ExpectedProcesses);
        }

        public double Period 
        { 
            get 
            { 
                return timer.Interval; 
            }
        }

        public void Start()
        {
            systemCapturer.CaptureSystemInformation(GetCaptureName());
            timer.Enabled = true;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private string GetCaptureName()
        {
            return configuration.CapturesPath + "/" + DateTime.Now.Ticks.ToString() + ".jpg";
        }
    }
}