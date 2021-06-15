using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class EmailTemplate
    {
        public EmailTemplate()
        {

        }
        public List<EmailTemplates> emailTemplates()
        {
            return new List<EmailTemplates>
            {
                new EmailTemplates{TemplateSubject="Reset password", TemplateBody=""}
            };
        }

        public EmailTemplates FindEmailTemplate(string subject)
        {
            return this.emailTemplates().Find(x => x.TemplateSubject == subject);
        }
    }
    public class EmailTemplates
    {
        public string TemplateBody { get; set; }
        public string TemplateSubject { get; set; }
    }
}
