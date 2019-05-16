# Overview
Email Customizer is a BizTalk pipeline component allows you to format your email you want to send through the SMTP adapter.
It Allows you to send HTML/plain text emails with attachments.
you can apply different scenarios using this component, based on the properties you set.
# Properties
Here is a full description of the properties:

| property | Type | Description |
| ------ | ------ |------ |
| Enabled | boolean | self explained |
| ApplyXsltOnBodyPart| boolean | True to apply the specified xslt file on the body part, this can be used to generate an HTML formatted email including values from the original BizTalk message body |
| XSLTFilePath | string | The XSLT file path that you want to use to generate an HTML email, if this field is empty and ApplyXsltOnBodyPart is set to true, the component will throw  ArgumentNullException, if the specified file does not exist, it will throw FileNotFoundException |
| XSLTFilePath | string | The path of the XSLT file you want to apply on the body part, this property is ignored if ApplyXsltOnBodyPart is set to false |
| Filenames | string | Filenames for each part of the messaage if it is a multipart message separated by vertical pipe \|, the body part is always the first one e.g. body.xml\|part1.xml\|part2.txt, to omit a part, leave its name empty for e.g. body.xml\|\|part2.txt.
| EmailBody | string | the body of the Email,  it can be plain text or html formatted, this property is ignored if ApplyXsltOnBodyPart is set to True |

# How To Use
Email Customizer is planned to used with SMTP adapter, and it is desgined to be used in a BizTalk Send Pipeline with the assistance of MIME Encoder, this means that an MIME encoder should be used after the Email Customizer.
Make sure that the MIME encoder's property "Send Body Part As Attachment" is set to false, the email body was set in Email Customizer will be sent as attachment as well, and there will not be any text shown in the email.
When MIME Encoder is used, the message
