using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;

namespace BizTalkComponents.PipelineComponents.EmailCustomizer
{
    public partial class EmailCustomizer
    {
        public string Name { get { return "Email Customizer"; } }
        public string Version { get { return "1.0"; } }
        public string Description { get { return "Customize your e-mail message using xsl to create html-formatted e-mail and attach message parts to the e-mail"; } }

        public IntPtr Icon
        {
            get { return IntPtr.Zero; }
        }

        public IEnumerator Validate(object projectSystem)
        {
            return ValidationHelper.Validate(this, false).ToArray().GetEnumerator();
        }

        public bool Validate(out string errorMessage)
        {
            var errors = ValidationHelper.Validate(this, true).ToArray();

            if (errors.Any())
            {
                errorMessage = string.Join(",", errors);

                return false;
            }

            errorMessage = string.Empty;

            return true;
        }

        public void GetClassID(out Guid classID)
        {
            classID = new Guid("7A638E15-52B8-4F77-80B4-A248887FF44B");
        }

        public void InitNew()
        {
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.CanRead & prop.CanWrite)
                {
                    prop.SetValue(this, PropertyBagHelper.ReadPropertyBag(propertyBag, prop.Name, prop.GetValue(this)));
                }
            }
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.CanRead & prop.CanWrite)
                {
                    PropertyBagHelper.WritePropertyBag(propertyBag, prop.Name, prop.GetValue(this));
                }
            }
        }
    }
}
