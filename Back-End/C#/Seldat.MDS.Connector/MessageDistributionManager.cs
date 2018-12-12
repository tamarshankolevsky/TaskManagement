using Newtonsoft.Json;
using Seldat.MDS.Connector.Models.Messages.MessagesDistribution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Seldat.MDS.Connector
{
    public class MessageDistributionManager
    {
        public static string SendMessage(MessageDistribution message)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Post("distribute", content);

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string SendEmail(int templateId, string email, Dictionary<string, object> information = null)
        {

            EmailMessageDistribution messageDistribution = new EmailMessageDistribution()
            {
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { Email = email, Info = information } }
            };
            return SendMessage(messageDistribution);
        }

        public static string SendEmailByMailingListId(int templateId, int mailingListId, Dictionary<string, object> information = null)
        {
            Template template = TemplateManager.Get(templateId);
            template.Translations.ForEach(t => t.Parameters = information);

            EmailMessageDistribution messageDistribution = new EmailMessageDistribution()
            {
                Template = template,
                To = new List<IContact>() { new MailingList() { Id = mailingListId } }
            };
            return SendMessage(messageDistribution);
        }

        public static string SendEmail(int templateId, string email, string language, Dictionary<string, Object> information = null)
        {
            EmailMessageDistribution messageDistribution = new EmailMessageDistribution()
            {
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { Email = email, Info = information, Language = language != null ? new Language { Culture = language } : null } }
            };
            return SendMessage(messageDistribution);
        }

        public static string SendEmail(int templateId, string email, Dictionary<string, Object> information = null, BNRequest bnRequest = null)
        {
            EmailMessageDistribution messageDistribution = new EmailMessageDistribution()
            {
                bnRequst = bnRequest,
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { Email = email, Info = information } }
            };
            return SendMessage(messageDistribution);
        }


        public static string SendEmail(int templateId, List<string> emails, Dictionary<string, Object> information = null)
        {
            EmailMessageDistribution messageDistribution = new EmailMessageDistribution()
            {
                Template = new Template { Id = templateId },
                To = new List<IContact>()
            };

            emails.ForEach(e => messageDistribution.To.Add(new Contact { Email = e, Info = information }));

            return SendMessage(messageDistribution);
        }

        public static string SendEmail(int templateId, List<string> emails, List<string> cc, List<string> bcc, List<Attachment> attachments, Dictionary<string, Object> information = null)
        {
            EmailMessageDistribution messageDistribution = new EmailMessageDistribution()
            {
                Template = new Template { Id = templateId },
                To = new List<IContact>(),
                CC = new List<IContact>(),
                Bcc = new List<IContact>(),
                Attachments = attachments
            };

            emails.ForEach(e => messageDistribution.To.Add(new Contact { Email = e, Info = information }));
            cc.ForEach(c => messageDistribution.CC.Add(new Contact { Email = c }));
            bcc.ForEach(b => messageDistribution.Bcc.Add(new Contact { Email = b }));

            return SendMessage(messageDistribution);
        }

        public static string SendSms(int templateId, string phoneNumber, Dictionary<string, Object> information = null)
        {
            SmsMessageDistribution messageDistribution = new SmsMessageDistribution()
            {
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { PhoneNumber = phoneNumber, Info = information } }
            };
            return SendMessage(messageDistribution);
        }

        public static string SendSms(int templateId, string phoneNumber, Dictionary<string, Object> information = null, BNRequest bnRequest = null)
        {
            SmsMessageDistribution messageDistribution = new SmsMessageDistribution()
            {
                bnRequst = bnRequest,
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { PhoneNumber = phoneNumber, Info = information } }
            };
            return SendMessage(messageDistribution);
        }
        public static string SendPushNotification(int templateId, string token, OperationSystem os, Dictionary<string, Object> information = null)
        {
            PushNotificationDistribution messageDistribution = new PushNotificationDistribution()
            {
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { Token = token, OS = os, Info = information } }
            };
            return SendMessage(messageDistribution);
        }

        public static string SendPushNotification(int templateId, string token, OperationSystem os, Dictionary<string, Object> information = null, BNRequest bnRequest = null)
        {
            PushNotificationDistribution messageDistribution = new PushNotificationDistribution()
            {
                bnRequst = bnRequest,
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { Token = token, OS = os, Info = information } }
            };
            return SendMessage(messageDistribution);
        }
        public static string SendPushNotification(int templateId, int contactId, Dictionary<string, Object> information = null)
        {
            PushNotificationDistribution messageDistribution = new PushNotificationDistribution()
            {
                Template = new Template { Id = templateId },
                To = new List<IContact>() { new Contact() { Id = contactId } }
            };
            return SendMessage(messageDistribution);
        }

        public static string UpdateMessage(int id, MessageDistribution message)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Put("distribute/update/{0}", content, id);

            return response.Content.ReadAsStringAsync().Result;
        }

        public static List<MessageDistribution> GetAllByType(MessageType type)
        {
            HttpResponseMessage response = Base.Get("distribute/type/{0}", type);

            return JsonConvert.DeserializeObject<List<MessageDistribution>>(response.Content.ReadAsStringAsync().Result);
        }

        public static List<MessageDistribution> GetFutureTasks()
        {
            HttpResponseMessage response = Base.Get("distribute/futureTasks");

            return JsonConvert.DeserializeObject<List<MessageDistribution>>(response.Content.ReadAsStringAsync().Result);
        }

        public static MessageDistribution GetById(int id, MessageType type)
        {
            HttpResponseMessage response = Base.Get("distribute/{0}/{1}", id, type);

            return JsonConvert.DeserializeObject<MessageDistribution>(response.Content.ReadAsStringAsync().Result);
        }

        public static List<Message> GetMessagesByDistributionId(int id, MessageType type)
        {
            HttpResponseMessage response = Base.Get("distribute/distribute/{0}/{1}", id, type);

            return JsonConvert.DeserializeObject<List<Message>>(response.Content.ReadAsStringAsync().Result);
        }

        public static string GetTemplateParser(int templateId, Contact contact)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

            HttpResponseMessage response = Base.Post("template/" + templateId, content);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}