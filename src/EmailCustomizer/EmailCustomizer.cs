using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;
using Microsoft.BizTalk.Component;
using System.ComponentModel;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.XLANGs.BaseTypes;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Microsoft.BizTalk.Streaming;
namespace BizTalkComponents.PipelineComponents.EmailCustomizer
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Encoder)]
    [System.Runtime.InteropServices.Guid("9d0e4103-4cce-4536-83fa-4a5040674ad6")]
    public partial class EmailCustomizer : IBaseComponent, IComponent, IComponentUI, IPersistPropertyBag
    {
        private const string FilenamesPropertyName = "FileNames",
            XSLTFilePathPropertyName = "XSLTFilePath",
            ApplyXsltOnBodyPartPropertyName = "ApplyXsltOnBodyPart",
            EmailBodyPropertyName = "EmailBody",
            EnabledPropertyName = "Enabled";

        [DisplayName("Filenames")]
        [Description("Filenames are separated by veritical pipe (|), parts with empty name will not be attached to your e-mail")]
        public string FileNames { get; set; }

        [DisplayName("XSLT File Path")]
        [Description("Xslt file path you want to apply on the body part of the message.")]
        public string XSLTFilePath { get; set; }

        [DisplayName("Apply XSLT On Body Part")]
        [Description("True to perform the transformation, false to ignore the transformation.")]
        public bool ApplyXsltOnBodyPart { get; set; }

        [DisplayName("Email Body")]
        [Description("Your fixed e-mail plain text or html-formatted.")]
        public string EmailBody { get; set; }

        [Description("True to activate the component, false to skip over it.")]
        public bool Enabled { get; set; }

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            if (!Enabled)
            {
                return pInMsg;
            }
            string errorMessage;
            if (!Validate(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
            IBaseMessageFactory messageFactory = pContext.GetMessageFactory();
            IBaseMessage newMsg = messageFactory.CreateMessage();
            newMsg.Context = pInMsg.Context;
            MemoryStream ms = new MemoryStream();

            //Create Html body
            IBaseMessagePart bodyPart = messageFactory.CreateMessagePart();
            if (ApplyXsltOnBodyPart)
            {
                if (string.IsNullOrEmpty(XSLTFilePath))
                    throw new ArgumentNullException("XsltFilePath is null");
                if (!File.Exists(XSLTFilePath))
                    throw new FileNotFoundException(string.Format("Cannot find the xslt file '{0}'.", XSLTFilePath));
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(XSLTFilePath);
                Stream originalStream = pInMsg.BodyPart.GetOriginalDataStream();
                if (!originalStream.CanSeek || !originalStream.CanRead)
                {
                    originalStream = new ReadOnlySeekableStream(originalStream);                
                }
                XmlReader reader = XmlReader.Create(originalStream);
                transform.Transform(reader, null, ms);
                originalStream.Seek(0, SeekOrigin.Begin);
                pInMsg.BodyPart.Data = originalStream;
            }
            else
            {
                byte[] buff = Encoding.UTF8.GetBytes(EmailBody);
                ms.Write(buff, 0, buff.Length);
            }
            ms.Seek(0, SeekOrigin.Begin);
            bodyPart.Data = ms;
            bodyPart.Charset = "UTF-8";
            bodyPart.ContentType = "text/html";
            newMsg.AddPart("body", bodyPart, true);

            //Add all message parts as attachments
            int i = 0;
            string[] filenames = FileNames.Split('|');
            while (i < pInMsg.PartCount & i < filenames.Length)
            {
                if (!string.IsNullOrEmpty(filenames[i]))
                {
                    string partName = "";
                    IBaseMessagePart part = pInMsg.GetPartByIndex(i, out partName),
                    newPart = messageFactory.CreateMessagePart();
                    Stream originalStream = part.GetOriginalDataStream();
                    newPart.Data = originalStream;
                    newPart.Charset = part.Charset;
                    newPart.ContentType = "text/xml";
                    newPart.PartProperties.Write("FileName", "http://schemas.microsoft.com/BizTalk/2003/mime-properties", filenames[i]);
                    newMsg.AddPart(filenames[i], newPart, false);
                }
                i++;
            }
            return newMsg;

        }
    }
}
