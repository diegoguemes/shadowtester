using System;
using System.IO;
using ShadowTester.Domain.Recorder;
using ShadowTester.Domain.Video;

namespace ShadowTester.Presentation.CommandLine.Commands
{
    public class StopRecordingCommand : ConsoleCommand
    {
        private VideoMaker videoMaker;
        private ProcessRecorder processRecorder;
        private RecordConfiguration configuration;

        public StopRecordingCommand(ProcessRecorder processRecorder, VideoMaker videoMaker, RecordConfiguration configuration)
        {
            this.processRecorder = processRecorder;
            this.videoMaker = videoMaker;
            this.configuration = configuration;
        }

        public override void Execute()
        {
            processRecorder.Stop();
            Console.WriteLine("Recording the video...");
            try
            {
                if (!File.Exists(configuration.CapturesPath + "/" + configuration.Name + ".avi") || ConsoleHelper.GetBooleanInputFromConsole())
                {
                    if(videoMaker.FromImages(configuration.CapturesPath + "/" + configuration.Name + ".avi", configuration.CapturesPath, configuration.Fps))
                    {
                        Console.WriteLine("The video was successfully created");
                    }
                    
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}