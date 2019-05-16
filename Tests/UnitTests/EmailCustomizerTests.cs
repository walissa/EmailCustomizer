using System;
using BizTalkComponents.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;
using Microsoft.BizTalk.Component;
using System.Net.Mail;


namespace BizTalkComponents.PipelineComponents.EmailCustomizer.Tests.UnitTests
{
    [TestClass]
    public class EmailCustomizerTests
    {
        [TestMethod]
        public void SendPlainTextWithAttachment()
        {
            var pipeline = PipelineFactory.CreateEmptySendPipeline();
            var component = new EmailCustomizer
            {
                Enabled = true,
                FileNames = "BodyPart.xml",
                EmailBody = "This is a plain text e-mail with body part as attachment.\nRegards"
            };
            pipeline.AddComponent(component, PipelineStage.Encode);
            //adding MIME encoder component to the sendpipeline so we can view the structure of the output message.
            var mime = new MIME_SMIME_Encoder();
            pipeline.AddComponent(mime, PipelineStage.Encode);
            //create a message with body part only.
            var message = MessageHelper.Create(System.IO.File.ReadAllText(TestFiles.BodyPart_FilePath));
            var output = pipeline.Execute(message);
            System.IO.StreamReader reader = new System.IO.StreamReader(output.BodyPart.GetOriginalDataStream());
            var ret = reader.ReadToEnd();
        }

        [TestMethod]
        public void SendPlainTextAndAttachParts()
        {
            var pipeline = PipelineFactory.CreateEmptySendPipeline();
            var component = new EmailCustomizer
            {
                Enabled = true,
                FileNames = "BodyPart.xml||Part2.xml",// string array for parts' filenames separated by |, adding empty text results in skipping the corespondant part from being attached.
                EmailBody = "This is a plain text e-mail with several parts attached.\nRegards"
            };
            pipeline.AddComponent(component, PipelineStage.Encode);
            //adding MIME encoder component to the sendpipeline so we can view the structure of the output message.
            var mime = new MIME_SMIME_Encoder();
            pipeline.AddComponent(mime, PipelineStage.Encode);
            //create a message with body part only.
            var message = MessageHelper.Create(System.IO.File.ReadAllText(TestFiles.BodyPart_FilePath),
                System.IO.File.ReadAllText(TestFiles.Part1_FilePath),
                System.IO.File.ReadAllText(TestFiles.Part2_FilePath));
            var output = pipeline.Execute(message);
            System.IO.StreamReader reader = new System.IO.StreamReader(output.BodyPart.GetOriginalDataStream());
            var ret = reader.ReadToEnd();
        }


        [TestMethod]
        public void SendHTMLFormattedEmailWithAttachment()
        {
            var pipeline = PipelineFactory.CreateEmptySendPipeline();
            var component = new EmailCustomizer
            {
                Enabled = true,
                FileNames = "BodyPartASFileName.xml",
                XSLTFilePath=TestFiles.EmailFormatterFilePath,
                ApplyXsltOnBodyPart=true// if it is true the XSLT file be aplied on the 
            };
            pipeline.AddComponent(component, PipelineStage.Encode);
            //adding MIME encoder component to the sendpipeline so we can view the structure of the output message.
            var mime = new MIME_SMIME_Encoder();
            pipeline.AddComponent(mime, PipelineStage.Encode);
            //create a message with body part only.
            var message = MessageHelper.Create(System.IO.File.ReadAllText(TestFiles.BodyPart_FilePath),
                System.IO.File.ReadAllText(TestFiles.Part1_FilePath),
                System.IO.File.ReadAllText(TestFiles.Part2_FilePath));
            var output = pipeline.Execute(message);
            System.IO.StreamReader reader = new System.IO.StreamReader(output.BodyPart.GetOriginalDataStream());
            var ret = reader.ReadToEnd();
        }
    }
}
