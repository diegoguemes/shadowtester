using ShadowTester.Domain.Captures;
using ShadowTester.Domain.Recorder;
using ShadowTester.Domain.System;
using ShadowTester.Domain.Video;

namespace ShadowTester.Domain
{
    public class Factory
    {
        private static ProcessRecorder processRecorder;
        private static VideoMaker videoMaker;
        private static SystemCapturer systemCapturer;

        public static ProcessRecorder ProcessRecorder
        {
            get
            {
                if(processRecorder == null)
                {
                    processRecorder = new ProcessRecorder(SystemCapturer);
                }
                return processRecorder;
            }
        }

        public static SystemCapturer SystemCapturer
        {
            get
            {
                if (systemCapturer == null)
                {
                    systemCapturer = new SystemCapturer(new SystemMonitor(), new ImagesCapturer());
                }
                return systemCapturer;
            }
        }


        public static VideoMaker VideoMaker
        {
            get
            {
                if (videoMaker == null)
                {
                    videoMaker = new VideoMaker(new VideoMakerValidator(), new ImagesSequenceRenamer());
                }
                return videoMaker;
            }
        }
    }
}